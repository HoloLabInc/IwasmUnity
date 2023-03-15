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
        public readonly wasm_val_t* Args;
        public readonly uint ArgCount;
        public readonly wasm_val_t* Results;
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
            return Args[i].GetValueAs<T>();
        }

        public void SetResult<T>(uint i, T value) where T : unmanaged
        {
            Results[i] = wasm_val_t.From<T>(value);
        }

        public ImportedContext AsContext()
        {
            return new ImportedContext(Instance.Memory);
        }
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
