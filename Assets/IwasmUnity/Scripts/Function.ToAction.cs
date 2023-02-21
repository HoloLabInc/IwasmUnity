#nullable enable
using System;

namespace IwasmUnity
{
    unsafe partial class Function
    {
        public Action ToAction()
        {
            const int argCount = 0;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            return () =>
            {
                CallUnchecked(null, argCount, null, retCount);
            };
        }

        public unsafe Action<T1> ToAction<T1>()
            where T1 : unmanaged
        {
            const int argCount = 1;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            return (a1) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2> ToAction<T1, T2>()
            where T1 : unmanaged
            where T2 : unmanaged
        {
            const int argCount = 2;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            return (a1, a2) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3> ToAction<T1, T2, T3>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            const int argCount = 3;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            return (a1, a2, a3) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4> ToAction<T1, T2, T3, T4>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
        {
            const int argCount = 4;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            return (a1, a2, a3, a4) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5> ToAction<T1, T2, T3, T4, T5>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
        {
            const int argCount = 5;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6> ToAction<T1, T2, T3, T4, T5, T6>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
        {
            const int argCount = 6;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7> ToAction<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
        {
            const int argCount = 7;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8> ToAction<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
        {
            const int argCount = 8;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
        {
            const int argCount = 9;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
        {
            const int argCount = 10;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
            where T11 : unmanaged
        {
            const int argCount = 11;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T11>() == _argTypes[10], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                    wasm_val_t.From(a11),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
            where T11 : unmanaged
            where T12 : unmanaged
        {
            const int argCount = 12;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T11>() == _argTypes[10], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T12>() == _argTypes[11], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                    wasm_val_t.From(a11),
                    wasm_val_t.From(a12),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
            where T11 : unmanaged
            where T12 : unmanaged
            where T13 : unmanaged
        {
            const int argCount = 13;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T11>() == _argTypes[10], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T12>() == _argTypes[11], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T13>() == _argTypes[12], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                    wasm_val_t.From(a11),
                    wasm_val_t.From(a12),
                    wasm_val_t.From(a13),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
            where T11 : unmanaged
            where T12 : unmanaged
            where T13 : unmanaged
            where T14 : unmanaged
        {
            const int argCount = 14;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T11>() == _argTypes[10], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T12>() == _argTypes[11], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T13>() == _argTypes[12], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T14>() == _argTypes[13], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                    wasm_val_t.From(a11),
                    wasm_val_t.From(a12),
                    wasm_val_t.From(a13),
                    wasm_val_t.From(a14),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
            where T11 : unmanaged
            where T12 : unmanaged
            where T13 : unmanaged
            where T14 : unmanaged
            where T15 : unmanaged
        {
            const int argCount = 15;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T11>() == _argTypes[10], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T12>() == _argTypes[11], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T13>() == _argTypes[12], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T14>() == _argTypes[13], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T15>() == _argTypes[14], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                    wasm_val_t.From(a11),
                    wasm_val_t.From(a12),
                    wasm_val_t.From(a13),
                    wasm_val_t.From(a14),
                    wasm_val_t.From(a15),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }

        public unsafe Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> ToAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where T10 : unmanaged
            where T11 : unmanaged
            where T12 : unmanaged
            where T13 : unmanaged
            where T14 : unmanaged
            where T15 : unmanaged
            where T16 : unmanaged
        {
            const int argCount = 16;
            const int retCount = 0;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T3>() == _argTypes[2], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T4>() == _argTypes[3], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T5>() == _argTypes[4], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T6>() == _argTypes[5], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T7>() == _argTypes[6], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T8>() == _argTypes[7], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T9>() == _argTypes[8], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T10>() == _argTypes[9], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T11>() == _argTypes[10], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T12>() == _argTypes[11], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T13>() == _argTypes[12], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T14>() == _argTypes[13], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T15>() == _argTypes[14], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T16>() == _argTypes[15], MismatchedArgTypeError);
            return (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                    wasm_val_t.From(a3),
                    wasm_val_t.From(a4),
                    wasm_val_t.From(a5),
                    wasm_val_t.From(a6),
                    wasm_val_t.From(a7),
                    wasm_val_t.From(a8),
                    wasm_val_t.From(a9),
                    wasm_val_t.From(a10),
                    wasm_val_t.From(a11),
                    wasm_val_t.From(a12),
                    wasm_val_t.From(a13),
                    wasm_val_t.From(a14),
                    wasm_val_t.From(a15),
                    wasm_val_t.From(a16),
                };
                CallUnchecked(args, argCount, null, retCount);
            };
        }
    }
}
