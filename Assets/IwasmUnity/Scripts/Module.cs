#nullable enable
using System;
using uint32_t = System.UInt32;
using size_t = System.UIntPtr;

namespace IwasmUnity
{
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

        public static unsafe Module CreateFromWasm(Store store, byte[] wasm)
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

        public Imports CreateImports()
        {
            return new Imports(this);
        }

        public Instance CreateInstance(Imports? imports, uint32_t stackSize = 32 * 1024, uint32_t heapSize = 0)
        {
            return new Instance(this, imports, stackSize, heapSize);
        }

        public void Dispose()
        {
            IwasmCApi.wasm_module_delete(_module);
            _module = wasm_module_t_ptr.Null;
        }
    }
}
