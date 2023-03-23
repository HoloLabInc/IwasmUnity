#nullable enable
using System;

namespace IwasmUnity
{
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
}
