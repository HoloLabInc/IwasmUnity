#nullable enable
using System;
using uint32_t = System.UInt32;
using System.Runtime.InteropServices;
using System.Collections.Concurrent;
using AOT;
using System.Threading;

namespace IwasmUnity
{
    internal unsafe static class ImportBuilder
    {
        // [NOTE]
        // Don't remove this line.
        // C# delegate instances must be held while their function pointers are used in native code.
        // Otherwise, they will be retrieved by the GC and the application will crash.
        private static ImportedCallback _onImportedCallback = OnImportedCallback;

        private static int _importKeySource;
        private static uint GenerateNewImportKey() => (uint)Interlocked.Increment(ref _importKeySource) - 1;
        private static readonly ConcurrentDictionary<uint, ImportedData> _importedStore = new ConcurrentDictionary<uint, ImportedData>();

        public static bool TryBuildImportFunction(
            string moduleName, string funcName, Module module,
            Delegate import,
            wasm_functype_t_ptr functype,
            Action<ImportInvocationState> onInvoke,
            out uint index,
            out wasm_extern_t_ptr ext,
            out Action<Instance> instanceSetter)
        {
            if (import == null) { throw new ArgumentNullException(nameof(import)); }
            if (!TryGetImportIndex(module, moduleName, funcName, out index))
            {
                ext = wasm_extern_t_ptr.Null;
                instanceSetter = null!;
                return false;
            }
            var key = GenerateNewImportKey();
            var f = IwasmCApi.wasm_func_new_with_env(
                module.Store.StoreNative,
                functype,
                Marshal.GetFunctionPointerForDelegate(_onImportedCallback),
                (void*)key,
                null);
            var data = new ImportedData(import, onInvoke);
            _importedStore.TryAdd(key, data);
            ext = IwasmCApi.wasm_func_as_extern(f);
            instanceSetter = data.SetInstance;
            return true;
        }

        public static bool TryGetImportIndex(Module module, string moduleName, string funcName, out uint32_t index)
        {
            using var mn = UnmanagedBytes.CreateAsciiNullTerminated(moduleName);
            using var fn = UnmanagedBytes.CreateAsciiNullTerminated(funcName);
            uint32_t i = 0;
            if (IwasmCApi.wasm_index_of_func_import(module.ModuleNative, mn.AsPointer(), fn.AsPointer(), &i) == 0)
            {
                index = 0;
                return false;
            }
            index = i;
            return true;
        }

        internal sealed class ImportedData
        {
            private readonly Delegate _import;
            private readonly Action<ImportInvocationState> _onInvoke;
            private Instance? _instance;

            public ImportedData(Delegate import, Action<ImportInvocationState> onInvoke)
            {
                _import = import;
                _onInvoke = onInvoke;
            }

            public void SetInstance(Instance instance)
            {
                _instance = instance;
            }

            public unsafe void InvokeImport(wasm_val_vec_t* args, wasm_val_vec_t* results)
            {
                var instance = _instance ?? throw new InvalidOperationException("instance is not set.");

                var state = new ImportInvocationState(
                    args->data, args->num_elems.ToUInt32(),
                    results->data, results->size.ToUInt32(),
                    instance,
                    _import);
                _onInvoke.Invoke(state);
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate wasm_trap_t_ptr ImportedCallback(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results);

        [MonoPInvokeCallback(typeof(ImportedCallback))]
        private static wasm_trap_t_ptr OnImportedCallback(void* env, wasm_val_vec_t* args, wasm_val_vec_t* results)
        {
            // Don't throw any exceptions because native codes cannot manage them.
            // All of them are catched here.
            try
            {
                uint key = (uint)env;
                if (_importedStore.TryGetValue(key, out var imported) == false)
                {
                    UnityEngine.Debug.LogError($"imported function is not found. key: {key}");
                    return wasm_trap_t_ptr.ErrorInImportedFunc;
                }
                imported.InvokeImport(args, results);
                return wasm_trap_t_ptr.Null;
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
                return wasm_trap_t_ptr.ErrorInImportedFunc;
            }
        }
    }
}
