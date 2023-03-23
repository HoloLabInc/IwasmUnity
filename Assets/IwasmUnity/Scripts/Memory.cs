#nullable enable
using System;

namespace IwasmUnity
{
    public sealed class Memory
    {
        // [NOTE] no need to delete
        private wasm_memory_t_ptr _memory;
        private Instance _instance;

        public uint Size => IwasmCApi.wasm_memory_data_size(_memory).ToUInt32();

        public IntPtr Ptr
        {
            get
            {
                unsafe
                {
                    var ptr = IwasmCApi.wasm_memory_data(_memory);
                    return (IntPtr)ptr;
                }
            }
        }

        internal Memory(wasm_memory_t_ptr memory, Instance instance)
        {
            _memory = memory;
            _instance = instance;
        }
    }
}
