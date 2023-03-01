#nullable enable
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using uint8_t = System.Byte;
using uint32_t = System.UInt32;
using size_t = System.UIntPtr;

namespace IwasmUnity
{
    internal unsafe static class IwasmCApi
    {
        private const CallingConvention Cdecl = CallingConvention.Cdecl;

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_engine_t_ptr wasm_engine_new();

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_engine_delete(wasm_engine_t_ptr engine);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_store_t_ptr wasm_store_new(wasm_engine_t_ptr engine);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_store_delete(wasm_store_t_ptr store);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_module_t_ptr wasm_module_new(wasm_store_t_ptr store, wasm_byte_vec_t* binary);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_module_delete(wasm_module_t_ptr module);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_instance_delete(wasm_instance_t_ptr instance);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_instance_t_ptr wasm_instance_new(
            wasm_store_t_ptr store,
            wasm_module_t_ptr module,
            wasm_extern_vec_t* imports,
            wasm_trap_t_ptr* trap);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_instance_t_ptr wasm_instance_new_with_args(
            wasm_store_t_ptr store,
            wasm_module_t_ptr module,
            wasm_extern_vec_t* imports,
            wasm_trap_t_ptr* trap,
            uint32_t stack_size,
            uint32_t heap_size);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_extern_t_ptr wasm_func_as_extern(wasm_func_t_ptr func);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_instance_exports(wasm_instance_t_ptr instance, wasm_extern_vec_t* out_exports);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_func_t_ptr wasm_extern_as_func(wasm_extern_t_ptr ext);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_byte_vec_new_uninitialized(wasm_byte_vec_t* out_bytes, size_t size);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_byte_vec_delete(wasm_byte_vec_t* bytes);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_extern_vec_delete(wasm_extern_vec_t* externs);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_trap_t_ptr wasm_func_call(
            wasm_func_t_ptr func,
            wasm_val_vec_t* args,
            wasm_val_vec_t* results);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_trap_delete(wasm_trap_t_ptr trap);

        private static void Sample(byte[] wasm)
        {
            // see https://github.com/bytecodealliance/wasm-micro-runtime/blob/9f0c4b63ac21c975aee7aae30236e306c8e49d13/samples/wasm-c-api/src/hello.c

            // hello.wat
            /*
(module
  (func $hello (import "" "hello"))
  (func (export "run") (call $hello))
)
             */


            var engine = wasm_engine_new();
            try
            {
                var store = wasm_store_new(engine);
                try
                {
                    wasm_module_t_ptr module;
                    {
                        wasm_byte_vec_t binary;
                        try
                        {
                            fixed (byte* source = wasm)
                            {
                                uint len = (uint)wasm.Length;
                                wasm_byte_vec_new_uninitialized(&binary, new size_t(len));
                                Buffer.MemoryCopy(source, binary.data, len, len);
                            }
                            module = wasm_module_new(store, &binary);
                        }
                        finally
                        {
                            wasm_byte_vec_delete(&binary);
                        }
                    }
                    try
                    {
                        // TODO: not implemented yet
                        //own wasm_functype_t*hello_type = wasm_functype_new_0_0();
                        //own wasm_func_t*hello_func =
                        //  wasm_func_new(store, hello_type, hello_callback);

                        //wasm_functype_delete(hello_type);

                        ////wasm_extern_t* externs[] = { wasm_func_as_extern(hello_func) };
                        //wasm_extern_t_ptr externs = { wasm_func_as_extern(hello_func) };


                        //wasm_extern_vec_t imports = WASM_ARRAY_VEC(externs);
                        //own wasm_instance_t*instance =
                        //  wasm_instance_new(store, module, &imports, NULL);

                        //var instance = wasm_instance_new(store, module, &imports, null);


                        var instance = wasm_instance_new(store, module, null, null);
                        try
                        {
                            wasm_extern_vec_t exports;
                            wasm_func_t_ptr func;
                            try
                            {
                                wasm_instance_exports(instance, &exports);
                                func = wasm_extern_as_func(exports.data[0]);

                                wasm_val_vec_t args = wasm_val_vec_t.Empty;
                                wasm_val_vec_t results = wasm_val_vec_t.Empty;
                                var trap = wasm_func_call(func, &args, &results);
                                if (trap.IsNull == false)
                                {
                                    // error
                                    wasm_trap_delete(trap);
                                }

                            }
                            finally
                            {
                                wasm_extern_vec_delete(&exports);
                            }
                        }
                        finally
                        {
                            wasm_instance_delete(instance);
                        }
                    }
                    finally
                    {
                        wasm_module_delete(module);
                    }

                }
                finally
                {
                    wasm_store_delete(store);
                }
            }
            finally
            {
                wasm_engine_delete(engine);
            }
        }
    }

    internal readonly struct wasm_engine_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_store_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_module_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_instance_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_extern_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_func_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_trap_t_ptr
    {
        private readonly IntPtr _p;
        public bool IsNull => _p == IntPtr.Zero;
    }

    internal unsafe readonly struct wasm_byte_vec_t
    {
        public readonly size_t size;
        public readonly byte* data;
        public readonly size_t num_elems;
        public readonly size_t size_of_elem;
        public readonly void* _lock;
    }

    internal unsafe readonly struct wasm_extern_vec_t
    {
        public readonly size_t size;
        public readonly wasm_extern_t_ptr* data;
        public readonly size_t num_elems;
        public readonly size_t size_of_elem;
        public readonly void* _lock;
    }

    internal unsafe readonly struct wasm_val_vec_t
    {
        public readonly size_t size;
        public readonly wasm_val_t* data;
        public readonly size_t num_elems;
        public readonly size_t size_of_elem;
        public readonly void* _lock;

        private wasm_val_vec_t(
            size_t size,
            wasm_val_t* data,
            size_t num_elems,
            size_t size_of_elem,
            void* @lock)
        {
            this.size = size;
            this.data = data;
            this.num_elems = num_elems;
            this.size_of_elem = size_of_elem;
            this._lock = @lock;
        }

        public static wasm_val_vec_t Empty => new wasm_val_vec_t(
            new size_t(0),
            null,
            new size_t(0),
            new size_t(0),
            null);
    }
}
