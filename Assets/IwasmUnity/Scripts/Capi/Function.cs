#nullable enable
using System;
using System.Text;

namespace IwasmUnity.Capi
{
    public unsafe sealed class Function
    {
        // [NOTE] no need to delete
        private readonly wasm_func_t_ptr _f;
        private readonly wasm_valkind_t[] _argTypes;
        private readonly wasm_valkind_t[] _resultTypes;

        internal Function(wasm_func_t_ptr f)
        {
            var functype = IwasmCApi.wasm_func_type(f);
            var argCount = functype.parameters->num_elems.ToUInt32();
            var argTypes = new wasm_valkind_t[argCount];
            for (int i = 0; i < argTypes.Length; i++)
            {
                argTypes[i] = functype.parameters->data[i]->kind;
            }

            var resultCount = functype.results->num_elems.ToUInt32();
            var resultTypes = new wasm_valkind_t[resultCount];
            for (int i = 0; i < resultTypes.Length; i++)
            {
                resultTypes[i] = functype.results->data[i]->kind;
            }

            _f = f;
            _argTypes = argTypes;
            _resultTypes = resultTypes;
        }

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

        private const string MismatchedArgCountError = "Number of arguments is mismatched";
        private const string MismatchedRetCountError = "Number of returns is mismatched";
        private const string MismatchedRetTypeError = "Type of returns is mismatched";
        private const string MismatchedArgTypeError = "Type of args is mismatched";

        private static void Ensure(bool condition, string message)
        {
            if (condition == false) { throw new InvalidOperationException(message); }
        }

        public UntypedFunc ToUntypedDelegate()
        {
            return new UntypedFunc(Call);
        }

        public object? Call(params object[] args)
        {
            if (args == null) { throw new ArgumentException(nameof(args)); }

            var resultCount = _resultTypes.Length;
            var results = stackalloc wasm_val_t[resultCount];
            var argCount = args.Length;
            var argValues = stackalloc wasm_val_t[argCount];
            for (int i = 0; i < argCount; i++)
            {
                argValues[i] = wasm_val_t.From(args[i]);
            }
            CheckArgs(argValues, argCount);
            CallUnchecked(argValues, (uint)argCount, results, (uint)resultCount);
            if (resultCount == 0)
            {
                return null;
            }
            else if (resultCount == 1)
            {
                return results[0].GetValueAsObject();
            }
            else
            {
                throw new NotSupportedException("multiple return is not supported yet.");
            }
        }

        private void CheckArgs(wasm_val_t* args, int argCount)
        {
            var expectedTypes = _argTypes;
            if (argCount != expectedTypes.Length)
            {
                throw new ArgumentException($"Number of arguments is invalid: expected {expectedTypes.Length}, actual {argCount}");
            }
            for (int i = 0; i < argCount; i++)
            {
                if (args[i].Type != expectedTypes[i])
                {
                    throw new ArgumentException($"Invalid argument type of argument {i}: expected {args[i].Type}, actual {expectedTypes[i]}");
                }
            }
        }

        private void CallUnchecked(wasm_val_t* args, uint argCount, wasm_val_t* results, uint resultCount)
        {
            var argVec = new wasm_val_vec_t(args, argCount);
            var resultVec = new wasm_val_vec_t(results, resultCount);
            var trap = IwasmCApi.wasm_func_call(_f, &argVec, &resultVec);
            try
            {
                if (trap.IsNull == false)
                {
                    wasm_byte_vec_t message;
                    IwasmCApi.wasm_trap_message(trap, &message);
                    var bytelen = (int)message.num_elems;
                    var p = message.data;
                    if (p[bytelen - 1] == (byte)'\0')
                    {
                        bytelen--;
                    }
                    var messageStr = Encoding.UTF8.GetString(p, bytelen);
                    IwasmCApi.wasm_byte_vec_delete(&message);
                    throw new WasmTrapException(messageStr);
                }
            }
            finally
            {
                IwasmCApi.wasm_trap_delete(trap);
            }
        }
    }
}
