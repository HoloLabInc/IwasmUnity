#nullable enable
using System;

namespace IwasmUnity.Capi
{
    unsafe partial class Function
    {
        public unsafe Func<TResult> ToFunc<TResult>()
            where TResult : unmanaged
        {
            const int argCount = 0;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
            return () =>
            {
                wasm_val_t ret;
                CallUnchecked(null, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, TResult> ToFunc<T1, TResult>()
            where T1 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 1;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            return (a1) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                };
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, TResult> ToFunc<T1, T2, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 2;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
            Ensure(TypeHelper.GetValueType<T1>() == _argTypes[0], MismatchedArgTypeError);
            Ensure(TypeHelper.GetValueType<T2>() == _argTypes[1], MismatchedArgTypeError);
            return (a1, a2) =>
            {
                var args = stackalloc wasm_val_t[argCount]
                {
                    wasm_val_t.From(a1),
                    wasm_val_t.From(a2),
                };
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, TResult> ToFunc<T1, T2, T3, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 3;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, TResult> ToFunc<T1, T2, T3, T4, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 4;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, TResult> ToFunc<T1, T2, T3, T4, T5, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 5;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, TResult> ToFunc<T1, T2, T3, T4, T5, T6, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 6;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 7;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 8;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>()
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
            where T9 : unmanaged
            where TResult : unmanaged
        {
            const int argCount = 9;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 10;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 11;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 12;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 13;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 14;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 15;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }

        public unsafe Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>()
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
            where TResult : unmanaged
        {
            const int argCount = 16;
            const int retCount = 1;
            Ensure(_argTypes.Length == argCount, MismatchedArgCountError);
            Ensure(_resultTypes.Length == retCount, MismatchedRetCountError);
            Ensure(TypeHelper.GetValueType<TResult>() == _resultTypes[0], MismatchedRetTypeError);
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
                wasm_val_t ret;
                CallUnchecked(args, argCount, &ret, retCount);
                return ret.GetValueAs<TResult>();
            };
        }
    }
}
