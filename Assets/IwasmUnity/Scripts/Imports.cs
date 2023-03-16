#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using uint8_t = System.Byte;
using uint32_t = System.UInt32;
using size_t = System.UIntPtr;

namespace IwasmUnity
{
    public unsafe partial class Imports
    {
        private wasm_extern_t_ptr[] _externArray;
        private readonly List<Action<Instance>> _instanceSetters = new List<Action<Instance>>();
        private Module _module;
        private bool _linked;

        internal Module Module => _module;

        internal Imports(Module module)
        {
            if (module.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(module));
            }
            _module = module;
            uint32_t importCount = 0;
            if (IwasmCApi.wasm_count_of_import(module.ModuleNative, &importCount) == 0)
            {
                throw new Exception("failed to get count of imported funcs");
            }
            var externArray = new wasm_extern_t_ptr[importCount];
            _externArray = externArray;
        }

        internal wasm_extern_t_ptr[] GetExterns()
        {
            return _externArray.ToArray();
        }

        internal void SetInstance(Instance instance)
        {
            foreach (var setter in _instanceSetters)
            {
                setter.Invoke(instance);
            }
            _instanceSetters.Clear();
            _linked = true;
        }

        public bool ImportUntyped(string moduleName, string funcName, Delegate import)
        {
            var m = import.Method;
            var isVoid = m.ReturnType == typeof(void);
            var paramsInfo = m.GetParameters();
            if (paramsInfo.Length == 0 || paramsInfo[0].ParameterType != typeof(ImportedContext))
            {
                throw new ArgumentException($"Argument 0 of import must be of type {nameof(ImportedContext)}");
            }
            var argCount = paramsInfo.Length - 1;
            var resultCount = isVoid ? 0 : 1;
            var argValueTypesTmp = new wasm_valtype_t[argCount];
            var argTypes = new wasm_valtype_t*[argCount];
            var resultValueTypesTmp = new wasm_valtype_t[resultCount];
            var resultTypes = new wasm_valtype_t*[resultCount];

            fixed (wasm_valtype_t* a = argValueTypesTmp)
            fixed (wasm_valtype_t** aRef = argTypes)
            fixed (wasm_valtype_t* r = resultValueTypesTmp)
            fixed (wasm_valtype_t** rRef = resultTypes)
            {
                for (int i = 0; i < argCount; i++)
                {
                    a[i] = TypeHelper.GetValuetype(paramsInfo[i + 1].ParameterType);
                    aRef[i] = &a[i];
                }
                var argTypeVec = new wasm_valtype_vec_t(aRef, (uint)argCount);

                for (int i = 0; i < resultCount; i++)
                {
                    r[i] = TypeHelper.GetValuetype(m.ReturnType);
                    rRef[i] = &r[i];
                }
                var resultTypeVec = new wasm_valtype_vec_t(rRef, (uint)resultCount);
                var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
                try
                {
                    return ImportCore(moduleName, funcName, import, functype, s =>
                    {
                        var import = s.Import;
                        var retType = import.Method.ReturnType;
                        var hasResult = retType != typeof(void);
                        if (s.ArgCount == 1)
                        {
                            var result = import.DynamicInvoke(s.AsContext());
                            if (hasResult)
                            {
                                s.SetResultObject(0, result);
                            }
                        }
                        else
                        {
                            var paramsInfo = import.Method.GetParameters();
                            var args = new object[paramsInfo.Length];
                            args[0] = s.AsContext();
                            for (uint i = 1; i < args.Length; i++)
                            {
                                args[i] = s.ArgObject(i - 1);
                            }
                            var result = import.DynamicInvoke(s.AsContext());
                            if (hasResult)
                            {
                                s.SetResultObject(0, result);
                            }
                        }
                    });
                }
                finally
                {
                    IwasmCApi.wasm_functype_delete(functype);
                }
            }
        }

        private bool ImportCore(string moduleName, string funcName, Delegate import, wasm_functype_t_ptr functype, Action<ImportInvocationState> onInvoke)
        {
            if (_linked)
            {
                throw new InvalidOperationException("the instance is already created.");
            }
            if (ImportBuilder.TryBuildImportFunction(moduleName, funcName, _module, import, functype, onInvoke, out var index, out var ext, out var instanceSetter))
            {
                _externArray[index] = ext;
                _instanceSetters.Add(instanceSetter);
                return true;
            }
            return false;
        }
    }

    internal unsafe readonly struct ImportInvocationState
    {
        private readonly wasm_val_t* Args;
        public readonly uint ArgCount;
        private readonly wasm_val_t* Results;
        public readonly uint ResultCount;
        public readonly Instance Instance;
        public readonly Delegate Import;

        public ImportInvocationState(
            wasm_val_t* args, uint argCount,
            wasm_val_t* results, uint resultCount,
            Instance instance,
            Delegate import)
        {
            Args = args;
            ArgCount = argCount;
            Results = results;
            ResultCount = resultCount;
            Instance = instance;
            Import = import;
        }

        public T Arg<T>(uint i) where T : unmanaged
        {
            if (i >= ArgCount)
            {
                ThrowArgOutOfRange(nameof(i));
            }
            return Args[i].GetValueAs<T>();
        }

        public object ArgObject(uint i)
        {
            if (i >= ArgCount)
            {
                ThrowArgOutOfRange(nameof(i));
            }
            return Args[i].GetValueAsObject();
        }

        public void SetResult<T>(uint i, T value) where T : unmanaged
        {
            if (i >= ResultCount)
            {
                ThrowArgOutOfRange(nameof(i));
            }
            Results[i] = wasm_val_t.From<T>(value);
        }

        public void SetResultObject(uint i, object value)
        {
            if (i >= ResultCount)
            {
                ThrowArgOutOfRange(nameof(i));
            }
            Results[i] = wasm_val_t.FromObject(value);
        }

        public ImportedContext AsContext()
        {
            return new ImportedContext(Instance.Memory);
        }

        private static void ThrowArgOutOfRange(string message) => throw new ArgumentOutOfRangeException(message);
    }

    public readonly struct ImportedContext
    {
        private readonly Memory? _memory;

        public uint MemorySize => _memory?.Size ?? 0;

        public IntPtr MemoryPtr => _memory?.Ptr ?? IntPtr.Zero;

        internal ImportedContext(Memory? memory)
        {
            _memory = memory;
        }
    }
}
