#nullable enable
using System;

namespace IwasmUnity.Capi
{
    public sealed class Store : IDisposable
    {
        private wasm_store_t_ptr _store;
        private Engine _engine;
        internal wasm_store_t_ptr StoreNative => _store;

        public Engine Engine => _engine;

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
}
