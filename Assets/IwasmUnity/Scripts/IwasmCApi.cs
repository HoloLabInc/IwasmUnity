#nullable enable
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using uint8_t = System.Byte;
using uint32_t = System.UInt32;
using size_t = System.UIntPtr;
using System.Text;
using System.Collections.Generic;

namespace IwasmUnity
{
    public static class Hello
    {
        public static void RunSample(byte[] wasm, Action action)
        {
            SampleAction = action;
            IwasmCApi.Sample(wasm);
        }

        public static Action? SampleAction { get; private set; }
    }

    public static class MemorySample
    {
        public static void RunSample(byte[] wasm)
        {
            IwasmCApi.MemorySample(wasm);
        }
    }

    public static class AddSample
    {
        public static void RunSample(byte[] wasm)
        {
            IwasmCApi.AddSample(wasm);
        }
    }

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
        private static extern wasm_externkind_t wasm_extern_kind(wasm_extern_t_ptr ext);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_func_t_ptr wasm_extern_as_func(wasm_extern_t_ptr ext);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_memory_t_ptr wasm_extern_as_memory(wasm_extern_t_ptr ext);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern size_t wasm_memory_data_size(wasm_memory_t_ptr memory);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern byte* wasm_memory_data(wasm_memory_t_ptr memory);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_byte_vec_new_uninitialized(wasm_byte_vec_t* out_bytes, size_t size);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_byte_vec_delete(wasm_byte_vec_t* bytes);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_valtype_vec_new(wasm_valtype_vec_t* vec, size_t size, wasm_valtype_t** data);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_valtype_vec_new_uninitialized(
            wasm_valtype_vec_t* out_vec,
            size_t size);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_extern_vec_delete(wasm_extern_vec_t* externs);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_trap_t_ptr wasm_func_call(
            wasm_func_t_ptr func,
            wasm_val_vec_t* args,
            wasm_val_vec_t* results);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_functype_t_ptr wasm_functype_new(
            wasm_valtype_vec_t* parameters,
            wasm_valtype_vec_t* results);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_func_t_ptr wasm_func_new(
            wasm_store_t_ptr store,
            wasm_functype_t_ptr functype,
            IntPtr callback     // delegate* unmanaged[Cdecl]<wasm_val_vec_t*, wasm_val_vec_t*, wasm_trap_t_ptr>
            );

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern wasm_func_t_ptr wasm_func_new_with_env(
            wasm_store_t_ptr store,
            wasm_functype_t_ptr type,
            IntPtr callback,    // delegate* unmanaged[Cdecl]<void*, wasm_val_vec_t*, wasm_val_vec_t*, wasm_trap_t_ptr>
            void* env,
            void* finalizer);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_functype_delete(wasm_functype_t_ptr func_type);


        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_trap_delete(wasm_trap_t_ptr trap);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern void wasm_trap_message(wasm_trap_t_ptr trap, wasm_byte_vec_t* out_message);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern uint8_t wasm_index_of_export(wasm_instance_t_ptr inst, byte* name, wasm_externkind_t kind, uint32_t* out_index);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern uint8_t wasm_index_of_func_import(wasm_module_t_ptr module, byte* module_name, byte* name, uint32_t* out_index);

        [DllImport(Iwasm.DllName, CallingConvention = Cdecl)]
        private static extern uint8_t wasm_count_of_import(wasm_module_t_ptr module, uint32_t* out_count);

        static wasm_functype_t_ptr wasm_functype_new_0_0()
        {
            wasm_valtype_vec_t parameters;
            wasm_valtype_vec_t results;
            wasm_valtype_vec_new_uninitialized(&parameters, new size_t(0));
            wasm_valtype_vec_new_uninitialized(&results, new size_t(0));
            return wasm_functype_new(&parameters, &results);
        }

        static wasm_functype_t_ptr wasm_functype_new_N_0(wasm_valtype_t** p, int paramCount)
        {
            wasm_valtype_vec_t parameters;
            wasm_valtype_vec_t results;
            wasm_valtype_vec_new(&parameters, new size_t((uint)paramCount), p);
            wasm_valtype_vec_new_uninitialized(&results, new size_t(0));
            return wasm_functype_new(&parameters, &results);
        }

        static void hoge()
        {
            var parameters = stackalloc wasm_valtype_t*[2]
            {
                default,
                default,
            };
            var f = wasm_functype_new_N_0(parameters, 2);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate wasm_trap_t_ptr ImportDelegate(wasm_val_vec_t* args, wasm_val_vec_t* results);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate wasm_trap_t_ptr ImportWithEnvDelegate(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results);

        private static ImportDelegate _helloCallback = HelloCallback;
        private static ImportWithEnvDelegate _helloCallbackWithEnv = HelloCallbackWithEnv;

        [AOT.MonoPInvokeCallback(typeof(ImportDelegate))]
        private static wasm_trap_t_ptr HelloCallback(wasm_val_vec_t* args, wasm_val_vec_t* results)
        {
            UnityEngine.Debug.Log("hello !!");
            try
            {
                Hello.SampleAction?.Invoke();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }
            return wasm_trap_t_ptr.Null;
        }

        [AOT.MonoPInvokeCallback(typeof(ImportWithEnvDelegate))]
        private static wasm_trap_t_ptr HelloCallbackWithEnv(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results)
        {
            uint key = (uint)env;
            UnityEngine.Debug.Log($"hello !! key: {key}");
            try
            {
                Hello.SampleAction?.Invoke();
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }
            return wasm_trap_t_ptr.Null;
        }

        private static uint32_t GetImportIndex(wasm_module_t_ptr module, string moduleName, string funcName)
        {
            var moduleNameUtf8 = Encoding.UTF8.GetBytes(moduleName);
            var funcNameUtf8 = Encoding.UTF8.GetBytes(funcName);

            fixed (byte* m = moduleNameUtf8)
            fixed (byte* fn = funcNameUtf8)
            {
                uint32_t index = 0;
                if (wasm_index_of_func_import(module, m, fn, &index) == 0)
                {
                    throw new Exception("import func not found");
                }
                return index;
            }
        }

        private static wasm_memory_t_ptr GetExportMemory(wasm_extern_vec_t* exports)
        {
            for (uint i = 0; i < exports->num_elems.ToUInt32(); i++)
            {
                var export = exports->data[i];
                if (wasm_extern_kind(export) == wasm_externkind_t.WASM_EXTERN_MEMORY)
                {
                    return wasm_extern_as_memory(export);
                }
            }
            return wasm_memory_t_ptr.Null;
        }

        private static wasm_func_t_ptr GetExportFunction(wasm_instance_t_ptr instance, wasm_extern_vec_t* exports, string funcName)
        {
            fixed (byte* name = Encoding.UTF8.GetBytes(funcName + "\0"))
            {
                uint32_t index;
                if (wasm_index_of_export(instance, name, wasm_externkind_t.WASM_EXTERN_FUNC, &index) == 0)
                {
                    throw new Exception("not found");
                }
                return wasm_extern_as_func(exports->data[index]);
            }
        }

        private static void Check<T>(T actual, T expected, string name)
        {
            if (EqualityComparer<T>.Default.Equals(actual, expected) == false)
            {
                throw new Exception($"[{name}] expected: {expected}, actual: {actual}");
            }
        }

        private static void CheckTrap(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (WasmTrapException ex)
            {
                // ok
                UnityEngine.Debug.Log($"trap message: {ex.Message}");
            }
        }

        private static void ThrowIfTrap(wasm_trap_t_ptr trap)
        {
            if (trap.IsNull == false)
            {
                wasm_byte_vec_t message;
                wasm_trap_message(trap, &message);
                var bytelen = (int)message.num_elems;
                var p = message.data;
                if (p[bytelen - 1] == (byte)'\0')
                {
                    bytelen--;
                }
                var messageStr = Encoding.UTF8.GetString(p, bytelen);
                wasm_byte_vec_delete(&message);
                throw new WasmTrapException(messageStr);
            }
        }

        private static wasm_val_t CallFunc0_1(wasm_func_t_ptr func)
        {
            var args = wasm_val_vec_t.Empty;
            wasm_val_t result;
            wasm_val_vec_t results = new wasm_val_vec_t(&result, 1);
            var trap = wasm_func_call(func, &args, &results);
            try
            {
                ThrowIfTrap(trap);
            }
            finally
            {
                wasm_trap_delete(trap);
            }
            return result;
        }

        private static wasm_val_t CallFunc1_1(wasm_func_t_ptr func, int a1)
        {
            var arg = wasm_val_t.I32(a1);
            var argVec = new wasm_val_vec_t(&arg, 1);
            wasm_val_t result;
            wasm_val_vec_t results = new wasm_val_vec_t(&result, 1);
            var trap = wasm_func_call(func, &argVec, &results);
            try
            {
                ThrowIfTrap(trap);
                return result;
            }
            finally
            {
                wasm_trap_delete(trap);
            }
        }

        private static void CallFunc2_0(wasm_func_t_ptr func, int a1, int a2)
        {
            var args = stackalloc wasm_val_t[2]
            {
                wasm_val_t.I32(a1),
                wasm_val_t.I32(a2),
            };
            var argVec = new wasm_val_vec_t(args, 2);
            wasm_val_vec_t results = wasm_val_vec_t.Empty;
            var trap = wasm_func_call(func, &argVec, &results);
            try
            {
                ThrowIfTrap(trap);
                return;
            }
            finally
            {
                wasm_trap_delete(trap);
            }
        }


        public static void AddSample(byte[] wasm)
        {
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
                        var imports = wasm_extern_vec_t.Empty;
                        var instance = wasm_instance_new_with_args(store, module, &imports, null, 32 * 1024, 0);
                        wasm_extern_vec_t exports;
                        wasm_instance_exports(instance, &exports);
                        try
                        {
                            var add = GetExportFunction(instance, &exports, "add");    // (i32, i32) -> i32

                            wasm_val_t* args = stackalloc wasm_val_t[2]
                            {
                                wasm_val_t.I32(10),
                                wasm_val_t.I32(20),
                            };
                            var argsVec = new wasm_val_vec_t(args, 2);
                            wasm_val_t result;
                            wasm_val_vec_t results = new wasm_val_vec_t(&result, 1);
                            var trap = wasm_func_call(add, &argsVec, &results);
                            if (trap.IsNull == false)
                            {
                                throw new Exception("trap");
                            }
                            UnityEngine.Debug.Log($"result: {result.GetI32()}");

                        }
                        finally
                        {
                            wasm_extern_vec_delete(&exports);
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

        public static void MemorySample(byte[] wasm)
        {
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
                        var imports = wasm_extern_vec_t.Empty;
                        var instance = wasm_instance_new_with_args(store, module, &imports, null, 32 * 1024, 0);
                        wasm_extern_vec_t exports;
                        wasm_instance_exports(instance, &exports);
                        try
                        {
                            var memory = GetExportMemory(&exports);
                            var func_size = GetExportFunction(instance, &exports, "size");      // () -> i32
                            var func_load = GetExportFunction(instance, &exports, "load");      // (i32) -> i32
                            var func_store = GetExportFunction(instance, &exports, "store");    // (i32, i32) -> ()

                            //Check(wasm_memory_size(memory) == 2);
                            Check(wasm_memory_data_size(memory), new size_t(0x20000), "memory_data_size");
                            Check(wasm_memory_data(memory)[0], 0, "data 0");
                            Check(wasm_memory_data(memory)[0x1000], 1, "data 1");
                            Check(wasm_memory_data(memory)[0x1003], 4, "data 2");

                            Check(CallFunc0_1(func_size).GetI32(), 2, "call size");
                            Check(CallFunc1_1(func_load, 0).GetI32(), 0, "call load 0");
                            Check(CallFunc1_1(func_load, 0x1000).GetI32(), 1, "call load 1");
                            Check(CallFunc1_1(func_load, 0x1003).GetI32(), 4, "call load 2");
                            Check(CallFunc1_1(func_load, 0x1ffff).GetI32(), 0, "call load 3");

                            CheckTrap(() =>
                            {
                                CallFunc1_1(func_load, 0x20000);
                            });


                            wasm_memory_data(memory)[0x1003] = 5;
                            CallFunc2_0(func_store, 0x1002, 6);
                            CheckTrap(() =>
                            {
                                CallFunc2_0(func_store, 0x20000, 0);
                            });
                            Check(wasm_memory_data(memory)[0x1002], 6, "memory check 0x1002");
                            Check(wasm_memory_data(memory)[0x1003], 5, "memory check 0x1003");
                            Check(CallFunc1_1(func_load, 0x1002).GetI32(), 6, "load 0x1002");
                            Check(CallFunc1_1(func_load, 0x1003).GetI32(), 5, "load 0x1003");
                        }
                        finally
                        {
                            wasm_extern_vec_delete(&exports);
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

        public static void Sample(byte[] wasm)
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

                        wasm_functype_t_ptr hello_type = wasm_functype_new_0_0();
                        wasm_func_t_ptr hello_func;
                        try
                        {
                            uint key = 1234;
                            //hello_func = wasm_func_new(store, hello_type, Marshal.GetFunctionPointerForDelegate(_helloCallback));
                            hello_func = wasm_func_new_with_env(
                                store,
                                hello_type,
                                Marshal.GetFunctionPointerForDelegate(_helloCallbackWithEnv),
                                (void*)key,
                                null);
                        }
                        finally
                        {
                            wasm_functype_delete(hello_type);
                        }
                        uint32_t importCount = 0;
                        if (wasm_count_of_import(module, &importCount) == 0)
                        {
                            throw new Exception("failed to get count of imported funcs");
                        }
                        var externArray = new wasm_extern_t_ptr[importCount];
                        externArray[GetImportIndex(module, "\0", "hello\0")] = wasm_func_as_extern(hello_func);

                        wasm_instance_t_ptr instance;
                        wasm_extern_vec_t imports;
                        fixed (wasm_extern_t_ptr* externs = externArray)
                        {
                            imports = new wasm_extern_vec_t(
                                new size_t(importCount),
                                externs,
                                new size_t(importCount),
                                new size_t((uint)sizeof(wasm_extern_t_ptr)),
                                null);
                            instance = wasm_instance_new(store, module, &imports, null);
                        }


                        try
                        {
                            wasm_extern_vec_t exports;
                            wasm_func_t_ptr func;
                            try
                            {
                                wasm_instance_exports(instance, &exports);

                                fixed (byte* name = Encoding.ASCII.GetBytes("run\0"))
                                {
                                    uint32_t index;
                                    if (wasm_index_of_export(instance, name, wasm_externkind_t.WASM_EXTERN_FUNC, &index) == 0)
                                    {
                                        throw new Exception("not found");
                                    }
                                    func = wasm_extern_as_func(exports.data[index]);
                                }

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

    internal readonly struct wasm_memory_t_ptr
    {
        private readonly IntPtr _p;

        public static wasm_memory_t_ptr Null => default;
    }

    internal readonly struct wasm_trap_t_ptr
    {
        private readonly IntPtr _p;

        public static wasm_trap_t_ptr Null => default;
        public bool IsNull => _p == IntPtr.Zero;
    }

    internal readonly struct wasm_functype_t_ptr
    {
        private readonly IntPtr _p;
    }

    internal readonly struct wasm_valtype_t
    {
        public readonly wasm_valkind_t kind;
    };

    internal enum wasm_externkind_t : byte
    {
        WASM_EXTERN_FUNC,
        WASM_EXTERN_GLOBAL,
        WASM_EXTERN_TABLE,
        WASM_EXTERN_MEMORY,
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

        public static wasm_extern_vec_t Empty => new wasm_extern_vec_t(
            new size_t(0),
            null,
            new size_t(0),
            new size_t(0),
            null);

        public wasm_extern_vec_t(
            size_t size,
            wasm_extern_t_ptr* data,
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
    }

    internal unsafe readonly struct wasm_valtype_vec_t
    {
        public readonly size_t size;
        public readonly wasm_valtype_t** data;
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

        public wasm_val_vec_t(wasm_val_t* data, uint dataLen)
        {
            this.size = new size_t(dataLen);
            this.data = data;
            this.num_elems = new size_t(dataLen);
            this.size_of_elem = new size_t((uint)sizeof(wasm_val_t));
            this._lock = null;
        }

        public wasm_val_vec_t(
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

    public sealed class WasmTrapException : Exception
    {
        public WasmTrapException(string message) : base(message)
        {
        }
    }
}

/*

#define WASM_ARRAY_VEC(array) 
{
    sizeof(array)/sizeof(*(array)),     // size
    array,                              // data
    sizeof(array)/sizeof(*(array)),     // num_elems
    sizeof(*(array)),                   // size_of_elem
    NULL                                // lock
}

 */