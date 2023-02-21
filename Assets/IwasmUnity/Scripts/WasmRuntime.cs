#nullable enable
using System;

namespace IwasmUnity
{
    public unsafe static class WasmRuntime
    {
        public static void Init()
        {
            if (Iwasm.wasm_runtime_init() == false)
            {
                throw new IwasmException("failed to init runtime");
            }
        }

        public static void ImportAction(string moduleName, string funcName, Action<ImportedContext> import)

        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1>(string moduleName, string funcName, Action<ImportedContext, T1> import)
            where T1 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2>(string moduleName, string funcName, Action<ImportedContext, T1, T2> import)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportAction(moduleName, funcName, import);
        }

        public static void ImportFunc<TResult>(string moduleName, string funcName, Func<ImportedContext, TResult> import)

            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, TResult> import)
            where T1 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where TResult : unmanaged
        {
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        public static void ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> import)
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
            CheckImportArg(moduleName, funcName, import);
            Importer.ImportFunc(moduleName, funcName, import);
        }

        private static void CheckImportArg(string moduleName, string funcName, Delegate import)
        {
            if (string.IsNullOrEmpty(moduleName)) { throw new ArgumentException(moduleName); }
            if (string.IsNullOrEmpty(funcName)) { throw new ArgumentException(funcName); }
            if (import is null) { throw new ArgumentNullException(nameof(import)); }
        }
    }
}
