#nullable enable
using System;

namespace IwasmUnity
{
    public unsafe sealed class Instance : IDisposable
    {
        private const uint DefaultStackSize = 2u * 1024 * 1024;   // 2MB
        private const uint DefaultHeapSize = 512u * 1024 * 1024;   // 512MB

        private Module _module;
        private wasm_module_inst_t _instance;

        // Don't destroy it. Reference only
        private wasm_exec_env_t _environmentRef;

        internal wasm_module_inst_t InstanceNative => _instance;
        internal wasm_exec_env_t EnvironmentNative => _environmentRef;

        private Instance(Module module, wasm_module_inst_t instance, wasm_exec_env_t environmentRef)
        {
            _module = module;
            _instance = instance;
            _environmentRef = environmentRef;
        }

        public uint GetMemorySize()
        {
            return Iwasm.GetMemorySize(_instance);
        }

        public static Instance Create(Module module, uint stackSize = DefaultStackSize, uint heapSize = DefaultHeapSize)
        {
            if (module == null) { throw new ArgumentException(nameof(module)); }
            module.ThrowIfDisposed();

            var errBuf = NativeErrorHelper.GetErrorBuf();
            try
            {
                wasm_module_inst_t instance;
                fixed (byte* errBufPtr = errBuf)
                {
                    instance = Iwasm.wasm_runtime_instantiate(module.ModuleNative, stackSize, heapSize, errBufPtr, (uint)errBuf.Length);
                }
                if (instance.IsNull)
                {
                    var message = NativeErrorHelper.GetErrorString(errBuf);
                    throw new IwasmException(message);
                }
                var environment = Iwasm.wasm_runtime_get_exec_env_singleton(instance);
                return new Instance(module, instance, environment);
            }
            finally
            {
                NativeErrorHelper.ReturnErrorBuf(errBuf);
            }
        }

#if IWASM_WITH_WASI
        public void RunWasiStartFunction()
        {
            ThrowIfDisposed();
            var func = Iwasm.wasm_runtime_lookup_wasi_start_function(_instance);
            Iwasm.WasmRuntimeCallWasm(this, func, 0, null, 0, null);
        }
#endif

        public Function FindFunction(string name)
        {
            if (TryFindFunction(name, out var function) == false)
            {
                throw new ArgumentException($"function '{name}' is not found.");
            }
            return function;
        }

        public bool TryFindFunction(string name, out Function function)
        {
            var instance = _instance;
            function = null!;
            if (instance.IsNull)
            {
                return false;
            }
            using (var funcName = UnmanagedBytes.CreateAsciiNullTerminated(name))
            {
                var func = Iwasm.wasm_runtime_lookup_function(instance, funcName.AsPointer(), null);
                if (func.IsNull)
                {
                    function = null!;
                    return false;
                }
                function = new Function(this, func);
                return true;
            }
        }

        public void Dispose()
        {
            if (_instance.IsNull)
            {
                return;
            }
            Iwasm.wasm_runtime_deinstantiate(_instance);
            _instance = wasm_module_inst_t.Null;
        }
        internal void ThrowIfDisposed()
        {
            if (_instance.IsNull)
            {
                throw new ObjectDisposedException(nameof(Module));
            }
        }
    }
}
