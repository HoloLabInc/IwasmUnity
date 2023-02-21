#nullable enable
using System;

namespace IwasmUnity
{
    public unsafe sealed partial class Function
    {
        private const string MismatchedArgCountError = "Number of arguments is mismatched";
        private const string MismatchedRetCountError = "Number of returns is mismatched";
        private const string MismatchedRetTypeError = "Type of returns is mismatched";
        private const string MismatchedArgTypeError = "Type of args is mismatched";

        private readonly wasm_function_inst_t _func;
        private readonly wasm_valkind_t[] _argTypes;
        private readonly wasm_valkind_t[] _resultTypes;
        private readonly Instance _instance;

        internal Function(Instance instance, wasm_function_inst_t func)
        {
            var instanceNative = instance.InstanceNative;
            var paramCount = Iwasm.wasm_func_get_param_count(func, instanceNative);
            wasm_valkind_t[] argTypes;
            if (paramCount > 0)
            {
                argTypes = new wasm_valkind_t[paramCount];
                fixed (wasm_valkind_t* p = argTypes)
                {
                    Iwasm.wasm_func_get_param_types(func, instanceNative, p);
                }
            }
            else
            {
                argTypes = Array.Empty<wasm_valkind_t>();
            }
            var resultCount = Iwasm.wasm_func_get_result_count(func, instanceNative);
            wasm_valkind_t[] resultTypes;
            if (resultCount > 0)
            {
                resultTypes = new wasm_valkind_t[resultCount];
                fixed (wasm_valkind_t* p = resultTypes)
                {
                    Iwasm.wasm_func_get_result_types(func, instanceNative, p);
                }
            }
            else
            {
                resultTypes = Array.Empty<wasm_valkind_t>();
            }

            _func = func;
            _argTypes = argTypes;
            _resultTypes = resultTypes;
            _instance = instance;
        }

        public UntypedFunc ToUntypedDelegate()
        {
            return new UntypedFunc(Call);
        }

        public unsafe object? Call(params object[] args)
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
            CallUnchecked(argValues, argCount, results, resultCount);
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

        private unsafe void CallUnchecked(wasm_val_t* args, int argCount, wasm_val_t* results, int resultCount)
        {
            System.Diagnostics.Debug.Assert(argCount == _argTypes.Length);
            System.Diagnostics.Debug.Assert(resultCount == _resultTypes.Length);
            Iwasm.WasmRuntimeCallWasm(_instance, _func, (uint)resultCount, results, (uint)argCount, args);
        }

        private static void Ensure(bool condition, string message)
        {
            if (condition == false) { throw new InvalidOperationException(message); }
        }
    }

    public delegate object? UntypedFunc(params object[] args);
}
