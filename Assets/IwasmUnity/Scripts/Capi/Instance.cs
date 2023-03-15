#nullable enable
using System;
using uint32_t = System.UInt32;

namespace IwasmUnity.Capi
{
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

        internal unsafe Instance(Module module, Imports? imports, uint32_t stackSize, uint32_t heapSize)
        {
            if (module.Store.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(module.Store));
            }
            if (module.IsDisposed)
            {
                throw new ObjectDisposedException(nameof(module));
            }

            wasm_instance_t_ptr instance;
            if (imports != null)
            {
                var externs = imports.GetExterns();

                fixed (wasm_extern_t_ptr* data = externs)
                {
                    var importExternVec = new wasm_extern_vec_t(data, (uint)externs.Length);
                    instance = IwasmCApi.wasm_instance_new_with_args(module.Store.StoreNative, module.ModuleNative, &importExternVec, null, stackSize, heapSize);
                }
            }
            else
            {
                var importExternVec = wasm_extern_vec_t.Empty;
                instance = IwasmCApi.wasm_instance_new_with_args(module.Store.StoreNative, module.ModuleNative, &importExternVec, null, stackSize, heapSize);
            }

            if (instance.IsNull)
            {
                throw new ArgumentException("Failed to create an instance.");
            }
            _instance = instance;

            _exports = new Exports(this);
            imports?.SetInstance(this);
        }

        public void Dispose()
        {
            _exports.DisposeInternal();

            IwasmCApi.wasm_instance_delete(_instance);
            _instance = wasm_instance_t_ptr.Null;
        }
    }
}
