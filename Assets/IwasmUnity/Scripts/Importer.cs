#nullable enable
using AOT;
using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace IwasmUnity
{
    internal unsafe static partial class Importer
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void NativeCallbackAction(wasm_exec_env_t execEnv, ulong* args);

        private static int _importIdSource;
        private static readonly ConcurrentDictionary<int, ImportData> _importedStore = new ConcurrentDictionary<int, ImportData>();
        private static readonly NativeCallbackAction _onNativeCallback;
        private static readonly IntPtr _onNativeCallbackFuncPtr;

        static Importer()
        {
            _onNativeCallback = OnNativeCallback;
            _onNativeCallbackFuncPtr = Marshal.GetFunctionPointerForDelegate(_onNativeCallback);
        }

        [MonoPInvokeCallback(typeof(NativeCallbackAction))]
        private static void OnNativeCallback(wasm_exec_env_t execEnv, ulong* args)
        {
            // ネイティブコードはマネージド例外をハンドリングできないため例外を漏らしてはいけない。全て握りつぶす
            try
            {
                var importId = (int)Iwasm.wasm_runtime_get_function_attachment(execEnv);
                var importData = _importedStore[importId];
                importData.InvokeImport(execEnv, args);
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogException(ex);
            }
        }

        private static int GenerateNewImportId() => Interlocked.Increment(ref _importIdSource) - 1;

        private static string Sig<T>()
        {
            return
                typeof(T) == typeof(int) ? "i" :
                typeof(T) == typeof(long) ? "I" :
                typeof(T) == typeof(float) ? "f" :
                typeof(T) == typeof(double) ? "F" :
                throw new NotSupportedException($"{typeof(T)} is not supported");
        }
        private static string SigVoid() => "";

        private static bool Register(
            string moduleName, string funcName, string signature,
            int argCount, int retCount,
            Delegate import,
            Action<ImportedFuncState> onInvoke)
        {
            var nativeSymbolsBytes = UnmanagedBytes.Empty;
            var funcNameBytes = UnmanagedBytes.Empty;
            var moduleNameBytes = UnmanagedBytes.Empty;
            var sig = UnmanagedBytes.Empty;
            try
            {
                nativeSymbolsBytes = new UnmanagedBytes(sizeof(NativeSymbol));
                funcNameBytes = UnmanagedBytes.CreateAsciiNullTerminated(funcName);
                moduleNameBytes = UnmanagedBytes.CreateAsciiNullTerminated(moduleName);
                sig = UnmanagedBytes.CreateAsciiNullTerminated(signature);
                int importId = GenerateNewImportId();
                var nativeSymbol = (NativeSymbol*)nativeSymbolsBytes.Ptr;
                *nativeSymbol = new NativeSymbol
                {
                    symbol = funcNameBytes.AsPointer(),
                    func_ptr = (void*)_onNativeCallbackFuncPtr,
                    signature = sig.AsPointer(),
                    attachment = (void*)importId,
                };
                var resource = new[] { nativeSymbolsBytes, funcNameBytes, moduleNameBytes, sig };
                var importData = new ImportData(argCount, retCount, import, onInvoke, resource);
                _importedStore[importId] = importData;

                return Iwasm.wasm_runtime_register_natives_raw(
                    moduleNameBytes.AsPointer(),
                    nativeSymbol,
                    1);
            }
            catch
            {
                nativeSymbolsBytes.Dispose();
                funcNameBytes.Dispose();
                moduleNameBytes.Dispose();
                sig.Dispose();
                throw;
            }
        }
    }

    public unsafe readonly struct ImportedContext
    {
        private readonly wasm_exec_env_t _execEnv;
        private readonly wasm_module_inst_t _instance;

        internal ImportedContext(wasm_exec_env_t execEnv)
        {
            _execEnv = execEnv;
            _instance = Iwasm.wasm_runtime_get_module_inst(execEnv);
        }

        public void* GetMemory(int appAddress) => GetMemory((uint)appAddress);

        public void* GetMemory(uint appAddress)
        {
            return Iwasm.wasm_runtime_addr_app_to_native(_instance, appAddress);
        }

        public uint GetMemorySize() => Iwasm.GetMemorySize(_instance);

        public string ReadUtf8(int appAddress, int byteLength)
        {
            var ptr = (byte*)GetMemory(appAddress);
            return Encoding.UTF8.GetString(ptr, byteLength);
        }
    }

    internal sealed class ImportData : IDisposable
    {
        private readonly int _argCount;
        private readonly int _retCount;
        private readonly Delegate _import;
        private readonly Action<ImportedFuncState> _onInvoke;
        private readonly UnmanagedBytes[] _nativeMems;

        public ImportData(int argCoung, int retCount, Delegate import, Action<ImportedFuncState> onInvoke, UnmanagedBytes[] nativeMems)
        {
            _argCount = argCoung;
            _retCount = retCount;
            _import = import;
            _onInvoke = onInvoke;
            _nativeMems = nativeMems;
        }

        ~ImportData() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            for (int i = 0; i < _nativeMems.Length; i++)
            {
                _nativeMems[i].Dispose();
            }
        }

        public unsafe void InvokeImport(wasm_exec_env_t execEnv, ulong* args)
        {
            var state = new ImportedFuncState(execEnv, args, _argCount, _retCount, _import);
            _onInvoke.Invoke(state);
        }
    }

    internal unsafe readonly struct ImportedFuncState
    {
        public wasm_exec_env_t ExecEnv { get; }
        public ulong* Args { get; }
        public int ArgCount { get; }
        public int RetCount { get; }
        public Delegate Import { get; }
        public ImportedFuncState(wasm_exec_env_t execEnv, ulong* args, int argCount, int retCount, Delegate import)
        {
            ExecEnv = execEnv;
            Args = args;
            ArgCount = argCount;
            RetCount = retCount;
            Import = import;
        }

        public ImportedContext GetContext() => new ImportedContext(ExecEnv);
    }
}
