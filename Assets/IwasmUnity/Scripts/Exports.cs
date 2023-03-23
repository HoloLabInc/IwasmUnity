#nullable enable
using System;
using uint32_t = System.UInt32;

namespace IwasmUnity
{
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
}
