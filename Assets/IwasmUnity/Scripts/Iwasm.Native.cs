#nullable enable
using System;
using System.Runtime.InteropServices;

using uint8_t = System.Byte;
using uint32_t = System.UInt32;
using System.Runtime.CompilerServices;

namespace IwasmUnity
{
    internal unsafe static partial class Iwasm
    {
        private const CallingConvention Cdecl = CallingConvention.Cdecl;

        private const string DllName =
#if UNITY_IOS && ENABLE_IL2CPP && !UNITY_EDITOR
            "__Internal";
#else
            "iwasm";
#endif

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern bool wasm_runtime_init();

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_runtime_destroy();

        /// <summary>wasm_runtime_load</summary>
        /// <remarks>
        /// <paramref name="buf"/> は書き込み可能、かつモジュールがアンロードされるまで参照可能でなくてはならない。
        /// </remarks>
        /// <param name="buf"></param>
        /// <param name="size"></param>
        /// <param name="error_buf"></param>
        /// <param name="error_buf_size"></param>
        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_module_t wasm_runtime_load(
            uint8_t* buf,
            uint32_t size,
            char_ptr error_buf,
            uint32_t error_buf_size);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_runtime_unload(wasm_module_t module);

        [DllImport(DllName, CallingConvention = Cdecl)]
        private static extern void wasm_runtime_set_wasi_args_ex(
            wasm_module_t module,
            byte** dir_list,
            uint32_t dir_count,
            byte** map_dir_list,
            uint32_t map_dir_count,
            byte** env,
            uint32_t env_count,
            byte** argv,
            int argc,
            int stdinfd,
            int stdoutfd,
            int stderrfd);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_module_inst_t wasm_runtime_instantiate(
            wasm_module_t module,
            uint32_t stack_size,
            uint32_t heap_size,
            char_ptr error_buf,
            uint32_t error_buf_size);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_runtime_deinstantiate(wasm_module_inst_t module_inst);

        /// <summary>wasm_runtime_get_exec_env_singleton</summary>
        /// <remarks>
        /// ランタイム内部で module instance に対して暗黙的に作られる environment を取得します。
        /// そのため、この environment を wasm_runtime_destroy_exec_env で破棄してはならない。
        /// </remarks>
        /// <param name="module_inst"></param>
        /// <returns></returns>
        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_exec_env_t wasm_runtime_get_exec_env_singleton(wasm_module_inst_t module_inst);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_function_inst_t wasm_runtime_lookup_wasi_start_function(wasm_module_inst_t module_inst);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern bool wasm_runtime_register_natives(
            const_char_ptr module_name,
            NativeSymbol* native_symbols,
            uint32_t n_native_symbols);


        // wasm_runtime_register_natives_raw の 引数 native_symbols について、NativeSymbol.func_ptr には
        // void foo(wasm_exec_env_t exec_env, uint64 *args);
        // の形の関数ポインタを必ず入れなければならない。ここが wasm_runtime_register_natives とは違う。
        // コールバックからの実行時には args[i] (i=0, ..) には引数の値が 64 bits で入っているので、適切に取り出す必要がある。
        // 戻り値がある場合は、args[0] に書き込む
        // 
        // (例) int32 somefunc(int32 arg0, float32 arg1)
        // 
        // int32 arg0 = *(int32*)(args + 0);
        // float32 arg1 = *(float32*)(args + 1);
        // int32 ret = funcPtr(arg0, arg1);         // somefunc の呼び出し
        // args[0] = (uint64)ret;
        //
        // 特に、float32, float64 は数値のキャストではなくポインタ型の解釈変更 (==メモリ表現はそのまま) をする必要があることに注意。
        //
        // この説明がわからなければ、インポートのコールバックを呼び出しているネイティブ側の実装を読んで。
        // wasm_runtime_common.c の wasm_runtime_invoke_native_raw 関数。

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern bool wasm_runtime_register_natives_raw(
            const_char_ptr module_name,
            NativeSymbol* native_symbols,
            uint32_t n_native_symbols);

        /// <summary>wasm_runtime_lookup_function</summary>
        /// <remarks>
        /// <paramref name="signature"/> は現在は無視されます。(iwasm v1.1.2)
        /// </remarks>
        /// <param name="module_inst"></param>
        /// <param name="name"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_function_inst_t wasm_runtime_lookup_function(
            wasm_module_inst_t module_inst,
            const_char_ptr name,
            const_char_ptr signature);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern uint32_t wasm_func_get_param_count(
            wasm_function_inst_t func_inst,
            wasm_module_inst_t module_inst);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern uint32_t wasm_func_get_result_count(
            wasm_function_inst_t func_inst,
            wasm_module_inst_t module_inst);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_func_get_param_types(
            wasm_function_inst_t func_inst,
            wasm_module_inst_t module_inst,
            wasm_valkind_t* param_types);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_func_get_result_types(
            wasm_function_inst_t func_inst,
            wasm_module_inst_t module_inst,
            wasm_valkind_t* result_types);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern bool wasm_runtime_call_wasm_a(
            wasm_exec_env_t exec_env,
            wasm_function_inst_t function,
            uint32_t num_results,
            wasm_val_t* results,
            uint32_t num_args,
            wasm_val_t* args);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void* wasm_runtime_addr_app_to_native(
            wasm_module_inst_t module_inst,
            uint32_t app_offset);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern bool wasm_runtime_get_app_addr_range(
            wasm_module_inst_t module_inst,
            uint32_t app_offset,
            uint32_t* p_app_start_offset,
            uint32_t* p_app_end_offset);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_module_inst_t wasm_runtime_get_module_inst(wasm_exec_env_t exec_env);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void* wasm_runtime_get_function_attachment(wasm_exec_env_t exec_env);


        [DllImport(DllName, CallingConvention = Cdecl)]
        private static extern const_char_ptr wasm_runtime_get_exception(wasm_module_inst_t module_inst);
    }

    internal enum wasm_valkind_t : byte
    {
        WASM_I32,
        WASM_I64,
        WASM_F32,
        WASM_F64,
        WASM_ANYREF = 128,
        WASM_FUNCREF,
    }

    internal static class TypeHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static wasm_valkind_t GetValueType<T>() where T : unmanaged
        {
            return
                typeof(T) == typeof(int) ? wasm_valkind_t.WASM_I32 :
                typeof(T) == typeof(long) ? wasm_valkind_t.WASM_I64 :
                typeof(T) == typeof(float) ? wasm_valkind_t.WASM_F32 :
                typeof(T) == typeof(double) ? wasm_valkind_t.WASM_F64 :
                throw new ArgumentException($"Invalid type: {typeof(T).FullName}");
        }
    }

    internal unsafe readonly struct wasm_val_t
    {
        private readonly KindWrap _kind;        // offset = 0
        private readonly ValueUnion _value;     // offset = 4 (32bits) or 8 (64bits)

        public wasm_valkind_t Type => _kind;

        private wasm_val_t(wasm_valkind_t kind, ValueUnion value)
        {
            _kind = kind;
            _value = value;
        }

        public static wasm_val_t I32(int value) => new wasm_val_t(wasm_valkind_t.WASM_I32, ValueUnion.I32(value));

        public static wasm_val_t I64(long value) => new wasm_val_t(wasm_valkind_t.WASM_I64, ValueUnion.I64(value));

        public static wasm_val_t F32(float value) => new wasm_val_t(wasm_valkind_t.WASM_F32, ValueUnion.F32(value));

        public static wasm_val_t F64(double value) => new wasm_val_t(wasm_valkind_t.WASM_F64, ValueUnion.F64(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static wasm_val_t From<T>(T x) where T : unmanaged
        {
            return
                typeof(T) == typeof(int) ? I32(*(int*)&x) :
                typeof(T) == typeof(long) ? I64(*(long*)&x) :
                typeof(T) == typeof(float) ? F32(*(float*)&x) :
                typeof(T) == typeof(double) ? F64(*(double*)&x) :
                throw new ArgumentException($"Invalid value type: {typeof(T).FullName}");
        }

        public static wasm_val_t From(object x)
        {
            return
                x is int intValue ? I32(intValue) :
                x is long longValue ? I64(longValue) :
                x is float floatValue ? F32(floatValue) :
                x is double doubleValue ? F64(doubleValue) :
                throw new ArgumentException($"Invalid value type: {x.GetType().FullName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetI32()
        {
            ValidateKind(wasm_valkind_t.WASM_I32);
            return _value.i32;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetI64()
        {
            ValidateKind(wasm_valkind_t.WASM_I64);
            return _value.i64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetF32()
        {
            ValidateKind(wasm_valkind_t.WASM_F32);
            return _value.f32;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetF64()
        {
            ValidateKind(wasm_valkind_t.WASM_F64);
            return _value.f64;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe T GetValueAs<T>() where T : unmanaged
        {
            var copy = this;
            return
                typeof(T) == typeof(int) ? *(T*)&copy._value.i32 :
                typeof(T) == typeof(long) ? *(T*)&copy._value.i64 :
                typeof(T) == typeof(float) ? *(T*)&copy._value.f32 :
                typeof(T) == typeof(double) ? *(T*)&copy._value.f64 :
                throw new ArgumentException($"Invalid value type: {typeof(T).FullName}");
        }

        public object GetValueAsObject()
        {
            if (Type == wasm_valkind_t.WASM_I32) { return _value.i32; }
            if (Type == wasm_valkind_t.WASM_I64) { return _value.i64; }
            if (Type == wasm_valkind_t.WASM_F32) { return _value.f32; }
            if (Type == wasm_valkind_t.WASM_F64) { return _value.f64; }
            throw new NotSupportedException($"invalid type: {Type}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ValidateKind(wasm_valkind_t kind)
        {
            if (_kind != kind)
            {
                ThrowInvalidKind();
            }
        }

        private static void ThrowInvalidKind() => throw new InvalidOperationException("invalid value kind");

        [StructLayout(LayoutKind.Explicit)]
        private readonly struct ValueUnion
        {
            [FieldOffset(0)]
            public readonly int i32;
            [FieldOffset(0)]
            public readonly long i64;
            [FieldOffset(0)]
            public readonly float f32;
            [FieldOffset(0)]
            public readonly double f64;
            //[FieldOffset(0)]
            //public readonly void* reference;

            public static ValueUnion I32(int value)
            {
                ValueUnion union = default;
                *&union.i32 = value;
                return union;
            }
            public static ValueUnion I64(long value)
            {
                ValueUnion union = default;
                *&union.i64 = value;
                return union;
            }
            public static ValueUnion F32(float value)
            {
                ValueUnion union = default;
                *&union.f32 = value;
                return union;
            }
            public static ValueUnion F64(double value)
            {
                ValueUnion union = default;
                *&union.f64 = value;
                return union;
            }
        }

        /// <summary>Wrap of <see cref="wasm_valkind_t"/></summary>
        /// <remarks>
        /// このラップ型は <see cref="wasm_val_t"/> のアライメントを調整するためにあります。
        /// </remarks>
        private readonly struct KindWrap
        {
            private readonly UIntPtr _value;

            private KindWrap(wasm_valkind_t value) => _value = (UIntPtr)value;

            public static implicit operator wasm_valkind_t(KindWrap x) => (wasm_valkind_t)x._value;
            public static implicit operator KindWrap(wasm_valkind_t value) => new KindWrap(value);
        }
    }

    internal readonly struct wasm_module_t
    {
        private readonly IntPtr _ptr;

        public bool IsNull => _ptr == IntPtr.Zero;
        public static wasm_module_t Null => default;
    }

    internal readonly struct wasm_module_inst_t
    {
        private readonly IntPtr _ptr;

        public bool IsNull => _ptr == IntPtr.Zero;
        public static wasm_module_inst_t Null => default;
    }

    internal readonly struct wasm_exec_env_t
    {
        private readonly IntPtr _ptr;

        public bool IsNull => _ptr == IntPtr.Zero;
        public static wasm_exec_env_t Null => default;
    }

    internal readonly struct wasm_function_inst_t
    {
        private readonly IntPtr _ptr;

        public bool IsNull => _ptr == IntPtr.Zero;
        public static wasm_function_inst_t Null => default;
    }

    /// <summary>
    /// `char*` in C
    /// </summary>
    internal unsafe readonly struct char_ptr
    {
        private readonly byte* _ptr;
        private char_ptr(byte* p) => _ptr = p;
        public static implicit operator byte*(char_ptr p) => p._ptr;
        public static implicit operator char_ptr(byte* p) => new char_ptr(p);
    }

    /// <summary>
    /// `const char*` in C
    /// </summary>
    internal unsafe readonly struct const_char_ptr
    {
        private readonly byte* _ptr;
        private const_char_ptr(byte* p) => _ptr = p;

        public static implicit operator byte*(const_char_ptr p) => p._ptr;
        public static implicit operator const_char_ptr(byte* p) => new const_char_ptr(p);
    }

    internal unsafe struct NativeSymbol
    {
        public const_char_ptr symbol;
        public void* func_ptr;
        public const_char_ptr signature;
        public void* attachment;
    }
}
