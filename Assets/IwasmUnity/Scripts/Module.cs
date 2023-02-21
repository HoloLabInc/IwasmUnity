#nullable enable
using System;

namespace IwasmUnity
{
    public unsafe sealed class Module : IDisposable
    {
        private wasm_module_t _module;
        private UnmanagedBytes _wasmBytes;

        internal wasm_module_t ModuleNative => _module;

        private Module(wasm_module_t module, UnmanagedBytes wasmBytes)
        {
            _module = module;
            _wasmBytes = wasmBytes;
        }

        public void SetWasiArgs(string[] envs, string[] args)
        {
            Iwasm.WasmRuntimeSetWasiArgs(this, envs, args);
        }

        public static Module LoadWasm(byte[] wasm)
        {
            if (wasm == null) { throw new ArgumentNullException(nameof(wasm)); }
            var wasmBytes = UnmanagedBytes.CopyFrom(wasm);
            try
            {
                return LoadWasmPrivate(wasmBytes);
            }
            catch
            {
                wasmBytes.Dispose();
                throw;
            }
        }

        private static Module LoadWasmPrivate(UnmanagedBytes wasm)
        {
            var errBuf = NativeErrorHelper.GetErrorBuf();
            try
            {
                wasm_module_t module;
                fixed (byte* errBufPtr = errBuf)
                {
                    module = Iwasm.wasm_runtime_load((byte*)wasm.Ptr, (uint)wasm.Length, errBufPtr, (uint)errBuf.Length);
                }
                if (module.IsNull)
                {
                    var message = NativeErrorHelper.GetErrorString(errBuf);
                    throw new IwasmException(message);
                }

                // Don't free bytes data of wasm while the module is alive.
                return new Module(module, wasm);
            }
            finally
            {
                NativeErrorHelper.ReturnErrorBuf(errBuf);
            }
        }

        public void Dispose()
        {
            if (_module.IsNull)
            {
                return;
            }
            Iwasm.wasm_runtime_unload(_module);
            _module = wasm_module_t.Null;
            _wasmBytes.Dispose();
            _wasmBytes = UnmanagedBytes.Empty;
        }

        internal void ThrowIfDisposed()
        {
            if (_module.IsNull)
            {
                throw new ObjectDisposedException(nameof(Module));
            }
        }
    }
}
