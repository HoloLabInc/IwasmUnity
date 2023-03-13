#nullable enable
using System;
using uint8_t = System.Byte;
using uint32_t = System.UInt32;
using size_t = System.UIntPtr;

namespace IwasmUnity.Capi
{
    public unsafe class Imports
    {
        private wasm_extern_t_ptr[] _externArray;
        private Module _module;

        internal wasm_extern_t_ptr[] Externs => _externArray;   // readonly

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

        public void ImportFunction(string moduleName, string funcName, Action<ImportedContext> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (TryGetImportIndex(_module.ModuleNative, moduleName, funcName, out var index))
            {
                var f = DelegateToWasmFunc(action);
                _externArray[index] = IwasmCApi.wasm_func_as_extern(f);
            }
        }

        private static wasm_func_t_ptr DelegateToWasmFunc(Action<ImportedContext> import)
        {
            // TODO:
            throw new NotImplementedException();
        }

        private static bool TryGetImportIndex(wasm_module_t_ptr module, string moduleName, string funcName, out uint32_t index)
        {
            using var mn = UnmanagedBytes.CreateAsciiNullTerminated(moduleName);
            using var fn = UnmanagedBytes.CreateAsciiNullTerminated(funcName);
            uint32_t i = 0;
            if (IwasmCApi.wasm_index_of_func_import(module, mn.AsPointer(), fn.AsPointer(), &i) == 0)
            {
                index = 0;
                return false;
            }
            index = i;
            return true;
        }

        private static wasm_functype_t_ptr FuncType_0_0()
        {
            wasm_valtype_vec_t parameters;
            wasm_valtype_vec_t results;
            IwasmCApi.wasm_valtype_vec_new_uninitialized(&parameters, new size_t(0));
            IwasmCApi.wasm_valtype_vec_new_uninitialized(&results, new size_t(0));
            return IwasmCApi.wasm_functype_new(&parameters, &results);
        }

        private static wasm_functype_t_ptr FuncType_N_0(wasm_valtype_t** p, int paramCount)
        {
            wasm_valtype_vec_t parameters;
            wasm_valtype_vec_t results;
            IwasmCApi.wasm_valtype_vec_new(&parameters, new size_t((uint)paramCount), p);
            IwasmCApi.wasm_valtype_vec_new_uninitialized(&results, new size_t(0));
            return IwasmCApi.wasm_functype_new(&parameters, &results);
        }

        //static void hoge()
        //{
        //    var parameters = stackalloc wasm_valtype_t*[2]
        //    {
        //        default,
        //        default,
        //    };
        //    var f = wasm_functype_new_N_0(parameters, 2);
        //}
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

            fixed (wasm_extern_t_ptr* data = imports.Externs)
            {
                var importExternVec = new wasm_extern_vec_t(data, (uint)imports.Externs.Length);
                var instance = IwasmCApi.wasm_instance_new_with_args(module.Store.StoreNative, module.ModuleNative, &importExternVec, null, stackSize, heapSize);
                if (instance.IsNull)
                {
                    throw new ArgumentException("Failed to create an instance.");
                }
                _instance = instance;
            }

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

        public bool TryGetMemory(out Memory memory)
        {
            for (uint i = 0; i < _exports.num_elems.ToUInt32(); i++)
            {
                var export = _exports.data[i];
                if (IwasmCApi.wasm_extern_kind(export) == wasm_externkind_t.WASM_EXTERN_MEMORY)
                {
                    var mem = IwasmCApi.wasm_extern_as_memory(export);
                    memory = new Memory(mem);
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
        private wasm_func_t_ptr _f;

        internal Function(wasm_func_t_ptr f)
        {
            _f = f;
        }
    }

    public sealed class Memory
    {
        // [NOTE] no need to delete
        private wasm_memory_t_ptr _memory;

        internal Memory(wasm_memory_t_ptr memory)
        {
            _memory = memory;
        }
    }

    public readonly struct ImportedContext
    {
        private readonly Memory _memory;

        internal ImportedContext(Memory memory)
        {
            _memory = memory;
        }
    }
}