#nullable enable
using System;
using System.Text;

namespace IwasmUnity
{
    unsafe partial class Iwasm
    {
        public static uint GetMemorySize(wasm_module_inst_t instanceNative)
        {
            uint start = 0;
            uint end = 0;
            wasm_runtime_get_app_addr_range(instanceNative, 0, &start, &end);
            return end;
        }

        public static void WasmRuntimeSetWasiArgs(Module module, string[] envs, string[] args)
        {
            // TODO: Dispose them when the module is disposed. Or they are leaked.
            // (But it's not matter much)
            var envsNative = new RawStringArray(envs);
            var argsNative = new RawStringArray(args);

            wasm_runtime_set_wasi_args_ex(
                module: module.ModuleNative,
                dir_list: null,
                dir_count: 0,
                map_dir_list: null,
                map_dir_count: 0,
                env: (byte**)envsNative.Ptr,
                env_count: (uint)envsNative.Length,
                argv: (byte**)argsNative.Ptr,
                argc: argsNative.Length,
                stdinfd: -1,
                stdoutfd: -1,
                stderrfd: -1);
        }

        public static void WasmRuntimeCallWasm(
            Instance instance,
            wasm_function_inst_t function,
            uint num_results,
            wasm_val_t* results,
            uint num_args,
            wasm_val_t* args)
        {
            var success = wasm_runtime_call_wasm_a(instance.EnvironmentNative, function, num_results, results, num_args, args);
            if (success == false)
            {
                byte* messagePtr = wasm_runtime_get_exception(instance.InstanceNative);
                int i = 0;
                while (true)
                {
                    if (messagePtr[i] == (byte)'\0')
                    {
                        var message = Encoding.UTF8.GetString(messagePtr, i);
                        throw new IwasmException(message);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }
    }
}
