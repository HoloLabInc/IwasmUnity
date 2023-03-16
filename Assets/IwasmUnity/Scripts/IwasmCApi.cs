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
    internal unsafe static class IwasmCApi
    {
        private const CallingConvention Cdecl = CallingConvention.Cdecl;
        private const string DllName =
#if UNITY_IOS && ENABLE_IL2CPP && !UNITY_EDITOR
            "__Internal";
#else
            "iwasm";
#endif

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_engine_t_ptr wasm_engine_new();

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_engine_delete(wasm_engine_t_ptr engine);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_store_t_ptr wasm_store_new(wasm_engine_t_ptr engine);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_store_delete(wasm_store_t_ptr store);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_module_t_ptr wasm_module_new(wasm_store_t_ptr store, wasm_byte_vec_t* binary);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_module_delete(wasm_module_t_ptr module);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_instance_delete(wasm_instance_t_ptr instance);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_instance_t_ptr wasm_instance_new(
            wasm_store_t_ptr store,
            wasm_module_t_ptr module,
            wasm_extern_vec_t* imports,
            wasm_trap_t_ptr* trap);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_instance_t_ptr wasm_instance_new_with_args(
            wasm_store_t_ptr store,
            wasm_module_t_ptr module,
            wasm_extern_vec_t* imports,
            wasm_trap_t_ptr* trap,
            uint32_t stack_size,
            uint32_t heap_size);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_extern_t_ptr wasm_func_as_extern(wasm_func_t_ptr func);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_instance_exports(wasm_instance_t_ptr instance, wasm_extern_vec_t* out_exports);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_externkind_t wasm_extern_kind(wasm_extern_t_ptr ext);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_func_t_ptr wasm_extern_as_func(wasm_extern_t_ptr ext);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_memory_t_ptr wasm_extern_as_memory(wasm_extern_t_ptr ext);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern size_t wasm_memory_data_size(wasm_memory_t_ptr memory);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern byte* wasm_memory_data(wasm_memory_t_ptr memory);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_byte_vec_new_uninitialized(wasm_byte_vec_t* out_bytes, size_t size);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_byte_vec_delete(wasm_byte_vec_t* bytes);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_valtype_vec_new(wasm_valtype_vec_t* vec, size_t size, wasm_valtype_t** data);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_valtype_vec_new_uninitialized(
            wasm_valtype_vec_t* out_vec,
            size_t size);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_extern_vec_delete(wasm_extern_vec_t* externs);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_functype_t_ptr wasm_func_type(wasm_func_t_ptr func);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern size_t wasm_func_param_arity(wasm_func_t_ptr func);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern size_t wasm_func_result_arity(wasm_func_t_ptr func);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_trap_t_ptr wasm_func_call(
            wasm_func_t_ptr func,
            wasm_val_vec_t* args,
            wasm_val_vec_t* results);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_functype_t_ptr wasm_functype_new(
            wasm_valtype_vec_t* parameters,
            wasm_valtype_vec_t* results);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_func_t_ptr wasm_func_new(
            wasm_store_t_ptr store,
            wasm_functype_t_ptr functype,
            IntPtr callback     // delegate* unmanaged[Cdecl]<wasm_val_vec_t*, wasm_val_vec_t*, wasm_trap_t_ptr>
            );

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern wasm_func_t_ptr wasm_func_new_with_env(
            wasm_store_t_ptr store,
            wasm_functype_t_ptr type,
            IntPtr callback,    // delegate* unmanaged[Cdecl]<void*, wasm_val_vec_t*, wasm_val_vec_t*, wasm_trap_t_ptr>
            void* env,
            void* finalizer);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_functype_delete(wasm_functype_t_ptr func_type);


        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_trap_delete(wasm_trap_t_ptr trap);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern void wasm_trap_message(wasm_trap_t_ptr trap, wasm_byte_vec_t* out_message);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern uint8_t wasm_index_of_export(wasm_instance_t_ptr inst, byte* name, wasm_externkind_t kind, uint32_t* out_index);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern uint8_t wasm_index_of_func_import(wasm_module_t_ptr module, byte* module_name, byte* name, uint32_t* out_index);

        [DllImport(DllName, CallingConvention = Cdecl)]
        public static extern uint8_t wasm_count_of_import(wasm_module_t_ptr module, uint32_t* out_count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate wasm_trap_t_ptr ImportDelegate(wasm_val_vec_t* args, wasm_val_vec_t* results);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate wasm_trap_t_ptr ImportWithEnvDelegate(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results);
    }

    internal readonly struct wasm_engine_t_ptr
    {
        private readonly IntPtr _p;

        public static wasm_engine_t_ptr Null => default;

        public bool IsNull => _p == IntPtr.Zero;
    }

    internal readonly struct wasm_store_t_ptr
    {
        private readonly IntPtr _p;

        public static wasm_store_t_ptr Null => default;

        public bool IsNull => _p == IntPtr.Zero;
    }

    internal readonly struct wasm_module_t_ptr
    {
        private readonly IntPtr _p;

        public static wasm_module_t_ptr Null => default;
        public bool IsNull => _p == IntPtr.Zero;
    }

    internal readonly struct wasm_instance_t_ptr
    {
        private readonly IntPtr _p;
        public static wasm_instance_t_ptr Null => default;
        public bool IsNull => _p == IntPtr.Zero;
    }

    internal readonly struct wasm_extern_t_ptr
    {
        private readonly IntPtr _p;

        public static wasm_extern_t_ptr Null => default;
        public bool IsNull => _p == IntPtr.Zero;
    }

    internal readonly struct wasm_func_t_ptr
    {
        private readonly IntPtr _p;
        public static wasm_func_t_ptr Null => default;
        public bool IsNull => _p == IntPtr.Zero;
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
        public static wasm_trap_t_ptr ErrorInImportedFunc => default;   // TODO:
        public bool IsNull => _p == IntPtr.Zero;
    }

    internal unsafe readonly struct wasm_functype_t_ptr
    {
        private readonly wasm_functype_t* _p;

        public uint32_t extern_kind => _p->extern_kind;
        public wasm_valtype_vec_t* parameters => _p->parameters;
        public wasm_valtype_vec_t* results => _p->results;
    }

    internal unsafe struct wasm_functype_t
    {
        public uint32_t extern_kind;
        public wasm_valtype_vec_t* parameters;
        public wasm_valtype_vec_t* results;
    }

    internal readonly struct wasm_valtype_t
    {
        public readonly wasm_valkind_t kind;

        public wasm_valtype_t(wasm_valkind_t kind)
        {
            this.kind = kind;
        }
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

        public wasm_extern_vec_t(wasm_extern_t_ptr* data, uint dataLen)
        {
            this.size = new size_t(dataLen);
            this.data = data;
            this.num_elems = new size_t(dataLen);
            this.size_of_elem = new size_t((uint)sizeof(wasm_val_t));
            this._lock = null;
        }
    }

    internal unsafe readonly struct wasm_valtype_vec_t
    {
        public readonly size_t size;
        public readonly wasm_valtype_t** data;
        public readonly size_t num_elems;
        public readonly size_t size_of_elem;
        public readonly void* _lock;

        public static wasm_valtype_vec_t Empty => new wasm_valtype_vec_t(null, 0);

        public wasm_valtype_vec_t(wasm_valtype_t** data, uint dataLen)
        {
            this.size = new size_t(dataLen);
            this.data = data;
            this.num_elems = new size_t(dataLen);
            this.size_of_elem = new size_t((uint)sizeof(wasm_valtype_t*));
            this._lock = null;
        }
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
        public static wasm_valkind_t GetValueKind<T>() where T : unmanaged
        {
            return
                typeof(T) == typeof(int) ? wasm_valkind_t.WASM_I32 :
                typeof(T) == typeof(long) ? wasm_valkind_t.WASM_I64 :
                typeof(T) == typeof(float) ? wasm_valkind_t.WASM_F32 :
                typeof(T) == typeof(double) ? wasm_valkind_t.WASM_F64 :
                throw new ArgumentException($"Invalid type: {typeof(T).FullName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static wasm_valtype_t GetValuetype<T>() where T : unmanaged
        {
            return new wasm_valtype_t(GetValueKind<T>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static wasm_valkind_t GetValueKind(Type type)
        {
            return
                type == typeof(int) ? wasm_valkind_t.WASM_I32 :
                type == typeof(long) ? wasm_valkind_t.WASM_I64 :
                type == typeof(float) ? wasm_valkind_t.WASM_F32 :
                type == typeof(double) ? wasm_valkind_t.WASM_F64 :
                throw new ArgumentException($"Invalid type: {type.FullName}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static wasm_valtype_t GetValuetype(Type type)
        {
            return new wasm_valtype_t(GetValueKind(type));
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

        public static wasm_val_t FromObject(object x)
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
        /// This wrap type is for aligning <see cref="wasm_val_t"/>.
        /// </remarks>
        private readonly struct KindWrap
        {
            private readonly UIntPtr _value;

            private KindWrap(wasm_valkind_t value) => _value = (UIntPtr)value;

            public static implicit operator wasm_valkind_t(KindWrap x) => (wasm_valkind_t)x._value;
            public static implicit operator KindWrap(wasm_valkind_t value) => new KindWrap(value);
        }
    }

    public sealed class WasmTrapException : Exception
    {
        public WasmTrapException(string message) : base(message)
        {
        }
    }
}
