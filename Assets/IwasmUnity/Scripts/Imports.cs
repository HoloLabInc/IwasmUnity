#nullable enable
using System;
using uint8_t = System.Byte;
using uint32_t = System.UInt32;
using size_t = System.UIntPtr;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using AOT;
using System.Threading;
using System.Collections.Generic;
using System.Text;

namespace IwasmUnity.Capi
{
    public unsafe class Imports
    {
        private wasm_extern_t_ptr[] _externArray;
        private readonly List<Action<Instance>> _instanceSetters = new List<Action<Instance>>();
        private Module _module;
        private bool _linked;

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
            return _externArray;
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

        private void ImportCore(string moduleName, string funcName, Delegate action, Action<ImportInvocationState> onInvoke)
        {
            if (_linked)
            {
                throw new InvalidOperationException("the instance is already created.");
            }

            if (ImportBuilder.TryBuildImportFunction(moduleName, funcName, _module, action, onInvoke, out var index, out var ext, out var instanceSetter))
            {
                _externArray[index] = ext;
                _instanceSetters.Add(instanceSetter);
            }
        }

        public void ImportAction(string moduleName, string funcName, Action<ImportedContext> import)
        {
            ImportCore(moduleName, funcName, import, s =>
            {
                var import = (Action<ImportedContext>)s.Import;
                import.Invoke(
                    s.AsContext());
            });
        }

        public void ImportAction<T1>(string moduleName, string funcName, Action<ImportedContext, T1> import)
            where T1 : unmanaged
        {
            ImportCore(moduleName, funcName, import, s =>
            {
                var import = (Action<ImportedContext, T1>)s.Import;
                import.Invoke(
                    s.AsContext(),
                    s.Arg<T1>(0));
            });
        }

        public void ImportAction<T1, T2>(string moduleName, string funcName, Action<ImportedContext, T1, T2> import)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            ImportCore(moduleName, funcName, import, s =>
            {
                var import = (Action<ImportedContext, T1, T2>)s.Import;
                import.Invoke(
                    s.AsContext(),
                    s.Arg<T1>(0),
                    s.Arg<T2>(1));
            });
        }


    }

    internal unsafe static class ImportBuilder
    {
        // [NOTE]
        // Don't remove this line.
        // C# delegate instances must be held while their function pointers are used in native code.
        // Otherwise, they will be retrieved by the GC and the application will crash.
        private static ImportedCallback _onImportedCallback = OnImportedCallback;

        private static int _importKeySource;
        private static uint GenerateNewImportKey() => (uint)Interlocked.Increment(ref _importKeySource) - 1;
        private static readonly ConcurrentDictionary<uint, ImportedData> _importedStore = new ConcurrentDictionary<uint, ImportedData>();

        public static bool TryBuildImportFunction(
            string moduleName, string funcName, Module module,
            Delegate import, Action<ImportInvocationState> onInvoke,
            out uint index,
            out wasm_extern_t_ptr ext,
            out Action<Instance> instanceSetter)
        {
            if (import == null) { throw new ArgumentNullException(nameof(import)); }
            if (!TryGetImportIndex(module, moduleName, funcName, out index))
            {
                ext = wasm_extern_t_ptr.Null;
                instanceSetter = null!;
                return false;
            }
            var key = GenerateNewImportKey();
            var f = NewWasmFunc(key, module.Store.StoreNative);
            var data = new ImportedData(import, onInvoke);
            _importedStore.TryAdd(key, data);
            ext = IwasmCApi.wasm_func_as_extern(f);
            instanceSetter = data.SetInstance;
            return true;
        }

        private static wasm_func_t_ptr NewWasmFunc(uint key, wasm_store_t_ptr store)
        {
            var ft = GetWasmFunctype(null, 0, null, 0);
            try
            {
                var func = IwasmCApi.wasm_func_new_with_env(
                    store,
                    ft,
                    Marshal.GetFunctionPointerForDelegate(_onImportedCallback),
                    (void*)key,
                    null);
                return func;
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(ft);
            }

            static wasm_functype_t_ptr GetWasmFunctype(wasm_valtype_t** p, int paramCount, wasm_valtype_t** r, int resultCount)
            {
                wasm_valtype_vec_t parameters;
                IwasmCApi.wasm_valtype_vec_new(&parameters, new size_t((uint)paramCount), p);

                wasm_valtype_vec_t results;
                IwasmCApi.wasm_valtype_vec_new(&results, new size_t((uint)resultCount), r);
                return IwasmCApi.wasm_functype_new(&parameters, &results);
            }
        }

        public static bool TryGetImportIndex(Module module, string moduleName, string funcName, out uint32_t index)
        {
            using var mn = UnmanagedBytes.CreateAsciiNullTerminated(moduleName);
            using var fn = UnmanagedBytes.CreateAsciiNullTerminated(funcName);
            uint32_t i = 0;
            if (IwasmCApi.wasm_index_of_func_import(module.ModuleNative, mn.AsPointer(), fn.AsPointer(), &i) == 0)
            {
                index = 0;
                return false;
            }
            index = i;
            return true;
        }

        internal sealed class ImportedData
        {
            private readonly Delegate _import;
            private readonly Action<ImportInvocationState> _onInvoke;
            private Instance? _instance;

            public ImportedData(Delegate import, Action<ImportInvocationState> onInvoke)
            {
                _import = import;
                _onInvoke = onInvoke;
            }

            public void SetInstance(Instance instance)
            {
                _instance = instance;
            }

            public unsafe void InvokeImport(wasm_val_vec_t* args, wasm_val_vec_t* results)
            {
                var instance = _instance ?? throw new InvalidOperationException("instance is not set.");

                var state = new ImportInvocationState(
                    args->data, args->num_elems.ToUInt32(),
                    results->data, results->num_elems.ToUInt32(),
                    instance,
                    _import);
                _onInvoke.Invoke(state);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate wasm_trap_t_ptr ImportedCallback(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results);

        [MonoPInvokeCallback(typeof(ImportedCallback))]
        private static wasm_trap_t_ptr OnImportedCallback(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results)
        {
            // Don't throw any exceptions because native codes cannot manage them.
            // All of them are catched here.
            try
            {
                uint key = (uint)env;
                if (_importedStore.TryGetValue(key, out var imported) == false)
                {
                    UnityEngine.Debug.LogError($"imported function is not found. key: {key}");
                    return wasm_trap_t_ptr.ErrorInImportedFunc;
                }
                imported.InvokeImport(args, results);
                return wasm_trap_t_ptr.Null;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                return wasm_trap_t_ptr.ErrorInImportedFunc;
            }
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

        public ImportedContext AsContext()
        {
            return new ImportedContext(Instance.Memory);
        }
    }

    public sealed class Engine : IDisposable
    {
        private wasm_engine_t_ptr _engine;

        internal wasm_engine_t_ptr EngineNative => _engine;
        public bool IsDisposed => _engine.IsNull;

        public Engine()
        {
            _engine = IwasmCApi.wasm_engine_new();
            if (_engine.IsNull)
            {
                throw new ArgumentException("Failed to create an engine.");
            }
        }

        public void Dispose()
        {
            IwasmCApi.wasm_engine_delete(_engine);
            _engine = wasm_engine_t_ptr.Null;
        }
    }

    public sealed class Store : IDisposable
    {
        private wasm_store_t_ptr _store;
        private Engine _engine;
        internal wasm_store_t_ptr StoreNative => _store;

        public bool IsDisposed => _store.IsNull;

        public Store(Engine engine)
        {
            if (engine.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(engine));
            }
            _engine = engine;
            _store = IwasmCApi.wasm_store_new(engine.EngineNative);
            if (_store.IsNull)
            {
                throw new ArgumentException("Failed to create a store.");
            }
        }

        public void Dispose()
        {
            IwasmCApi.wasm_store_delete(_store);
            _store = wasm_store_t_ptr.Null;
        }
    }

    public sealed class Module : IDisposable
    {
        private wasm_module_t_ptr _module;
        private Store _store;

        internal wasm_module_t_ptr ModuleNative => _module;
        internal Store Store => _store;

        public bool IsDisposed => _module.IsNull;

        private Module(Store store, wasm_module_t_ptr module)
        {
            _store = store;
            _module = module;
        }

        public unsafe Module CreateFromWasm(Store store, byte[] wasm)
        {
            if (store.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(store));
            }

            wasm_byte_vec_t binary;
            try
            {
                fixed (byte* source = wasm)
                {
                    uint len = (uint)wasm.Length;
                    IwasmCApi.wasm_byte_vec_new_uninitialized(&binary, new size_t(len));
                    Buffer.MemoryCopy(source, binary.data, len, len);
                }
                var module = IwasmCApi.wasm_module_new(store.StoreNative, &binary);
                if (module.IsNull)
                {
                    throw new ArgumentException("Failed to create a module.");
                }
                return new Module(store, module);
            }
            finally
            {
                IwasmCApi.wasm_byte_vec_delete(&binary);
            }
        }

        public Instance CreateInstance(Module module, Imports imports, uint32_t stackSize = 32 * 1024, uint32_t heapSize = 0)
        {
            return new Instance(module, imports, stackSize, heapSize);
        }

        public void Dispose()
        {
            IwasmCApi.wasm_module_delete(_module);
            _module = wasm_module_t_ptr.Null;
        }
    }

    public sealed class Instance : IDisposable
    {
        private wasm_instance_t_ptr _instance;
        private Exports _exports;
        private Memory? _memory;
        private bool _isMemoryFetched;
        public Memory? Memory
        {
            get
            {
                if (_isMemoryFetched == false)
                {
                    Exports.TryGetMemory(out _memory);
                    _isMemoryFetched = true;
                }
                return _memory;
            }
        }

        internal wasm_instance_t_ptr InstanceNative => _instance;
        public bool IsDisposed => _instance.IsNull;

        public Exports Exports => _exports;

        internal unsafe Instance(Module module, Imports imports, uint32_t stackSize, uint32_t heapSize)
        {
            if (module.Store.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(module.Store));
            }
            if (module.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(module));
            }

            var externs = imports.GetExterns();
            fixed (wasm_extern_t_ptr* data = externs)
            {
                var importExternVec = new wasm_extern_vec_t(data, (uint)externs.Length);
                var instance = IwasmCApi.wasm_instance_new_with_args(module.Store.StoreNative, module.ModuleNative, &importExternVec, null, stackSize, heapSize);
                if (instance.IsNull)
                {
                    throw new ArgumentException("Failed to create an instance.");
                }
                _instance = instance;
            }

            imports.SetInstance(this);
            _exports = new Exports(this);
        }

        public void Dispose()
        {
            _exports.DisposeInternal();

            IwasmCApi.wasm_instance_delete(_instance);
            _instance = wasm_instance_t_ptr.Null;
        }
    }

    public unsafe sealed class Exports
    {
        private Instance _instance;
        private wasm_extern_vec_t _exports;

        internal Exports(Instance instance)
        {
            _instance = instance;
            fixed (wasm_extern_vec_t* exports = &_exports)
            {
                IwasmCApi.wasm_instance_exports(instance.InstanceNative, exports);
            }
        }

        public Function GetFunction(string name)
        {
            if (TryGetFunction(name, out var function) == false)
            {
                throw new ArgumentException($"function '{name}' is not found.");
            }
            return function;
        }

        public bool TryGetFunction(string name, out Function function)
        {
            using var fn = UnmanagedBytes.CreateAsciiNullTerminated(name);
            uint32_t index;
            if (IwasmCApi.wasm_index_of_export(_instance.InstanceNative, fn.AsPointer(), wasm_externkind_t.WASM_EXTERN_FUNC, &index) == 0)
            {
                function = null!;
                return false;
            }
            var f = IwasmCApi.wasm_extern_as_func(_exports.data[index]);
            if (f.IsNull)
            {
                function = null!;
                return false;
            }
            function = new Function(f);
            return true;
        }

        internal bool TryGetMemory(out Memory memory)
        {
            for (uint i = 0; i < _exports.num_elems.ToUInt32(); i++)
            {
                var export = _exports.data[i];
                if (IwasmCApi.wasm_extern_kind(export) == wasm_externkind_t.WASM_EXTERN_MEMORY)
                {
                    var mem = IwasmCApi.wasm_extern_as_memory(export);
                    memory = new Memory(mem, _instance);
                    return true;
                }
            }
            memory = null!;
            return false;
        }

        /// <summary>
        /// Only called from internal
        /// </summary>
        internal void DisposeInternal()
        {
            fixed (wasm_extern_vec_t* exports = &_exports)
            {
                IwasmCApi.wasm_extern_vec_delete(exports);
            }
        }
    }

    public sealed class Function
    {
        // [NOTE] no need to delete
        private readonly wasm_func_t_ptr _f;
        private readonly wasm_valkind_t[] _argTypes;
        private readonly wasm_valkind_t[] _resultTypes;

        internal unsafe Function(wasm_func_t_ptr f)
        {
            var functype = IwasmCApi.wasm_func_type(f);
            var argCount = functype.parameters->num_elems.ToUInt32();
            var argTypes = new wasm_valkind_t[argCount];
            for (int i = 0; i < argTypes.Length; i++)
            {
                argTypes[i] = functype.parameters->data[i]->kind;
            }

            var resultCount = functype.results->num_elems.ToUInt32();
            var resultTypes = new wasm_valkind_t[resultCount];
            for (int i = 0; i < resultTypes.Length; i++)
            {
                resultTypes[i] = functype.results->data[i]->kind;
            }

            _f = f;
            _argTypes = argTypes;
            _resultTypes = resultTypes;
        }

        public UntypedFunc ToUntypedDelegate()
        {
            return new UntypedFunc(Call);
        }

        public unsafe object? Call(params object[] args)
        {
            if (args == null) { throw new ArgumentException(nameof(args)); }

            var resultCount = _resultTypes.Length;
            var results = stackalloc wasm_val_t[resultCount];
            var argCount = args.Length;
            var argValues = stackalloc wasm_val_t[argCount];
            for (int i = 0; i < argCount; i++)
            {
                argValues[i] = wasm_val_t.From(args[i]);
            }
            CheckArgs(argValues, argCount);
            CallUnchecked(argValues, (uint)argCount, results, (uint)resultCount);
            if (resultCount == 0)
            {
                return null;
            }
            else if (resultCount == 1)
            {
                return results[0].GetValueAsObject();
            }
            else
            {
                throw new NotSupportedException("multiple return is not supported yet.");
            }
        }

        private unsafe void CheckArgs(wasm_val_t* args, int argCount)
        {
            var expectedTypes = _argTypes;
            if (argCount != expectedTypes.Length)
            {
                throw new ArgumentException($"Number of arguments is invalid: expected {expectedTypes.Length}, actual {argCount}");
            }
            for (int i = 0; i < argCount; i++)
            {
                if (args[i].Type != expectedTypes[i])
                {
                    throw new ArgumentException($"Invalid argument type of argument {i}: expected {args[i].Type}, actual {expectedTypes[i]}");
                }
            }
        }

        private unsafe void CallUnchecked(wasm_val_t* args, uint argCount, wasm_val_t* results, uint resultCount)
        {
            var argVec = new wasm_val_vec_t(args, argCount);
            var resultVec = new wasm_val_vec_t(results, resultCount);
            var trap = IwasmCApi.wasm_func_call(_f, &argVec, &resultVec);
            try
            {
                if (trap.IsNull == false)
                {
                    wasm_byte_vec_t message;
                    IwasmCApi.wasm_trap_message(trap, &message);
                    var bytelen = (int)message.num_elems;
                    var p = message.data;
                    if (p[bytelen - 1] == (byte)'\0')
                    {
                        bytelen--;
                    }
                    var messageStr = Encoding.UTF8.GetString(p, bytelen);
                    IwasmCApi.wasm_byte_vec_delete(&message);
                    throw new WasmTrapException(messageStr);
                }
            }
            finally
            {
                IwasmCApi.wasm_trap_delete(trap);
            }
        }
    }

    public sealed class Memory
    {
        // [NOTE] no need to delete
        private wasm_memory_t_ptr _memory;
        private Instance _instance;

        internal Memory(wasm_memory_t_ptr memory, Instance instance)
        {
            _memory = memory;
            _instance = instance;
        }
    }

    public readonly struct ImportedContext
    {
        private readonly Memory? _memory;

        internal ImportedContext(Memory? memory)
        {
            _memory = memory;
        }
    }
}