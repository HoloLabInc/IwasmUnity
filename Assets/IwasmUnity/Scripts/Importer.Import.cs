#nullable enable
using System;

namespace IwasmUnity
{
    unsafe partial class Importer
    {
        public static bool ImportAction(string moduleName, string funcName, Action<ImportedContext> import)

        {
            var signature = $"(){SigVoid()}";
            return Register(moduleName, funcName, signature, 0, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext>)state.Import;

                import.Invoke(state.GetContext());
            }
        }

        public static bool ImportAction<T1>(string moduleName, string funcName, Action<ImportedContext, T1> import)
            where T1 : unmanaged
        {
            var signature = $"({Sig<T1>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 1, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                import.Invoke(state.GetContext(), a0);
            }
        }

        public static bool ImportAction<T1, T2>(string moduleName, string funcName, Action<ImportedContext, T1, T2> import)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 2, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                import.Invoke(state.GetContext(), a0, a1);
            }
        }

        public static bool ImportAction<T1, T2, T3>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 3, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                import.Invoke(state.GetContext(), a0, a1, a2);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 4, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                import.Invoke(state.GetContext(), a0, a1, a2, a3);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 5, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 6, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 7, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 8, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 9, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 10, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 11, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 12, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}{Sig<T13>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 13, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var a12 = *(T13*)(state.Args + 12);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}{Sig<T13>()}{Sig<T14>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 14, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var a12 = *(T13*)(state.Args + 12);
                var a13 = *(T14*)(state.Args + 13);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13);
            }
        }

        public static bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}{Sig<T13>()}{Sig<T14>()}{Sig<T15>()}){SigVoid()}";
            return Register(moduleName, funcName, signature, 15, 0, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var a12 = *(T13*)(state.Args + 12);
                var a13 = *(T14*)(state.Args + 13);
                var a14 = *(T15*)(state.Args + 14);
                import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14);
            }
        }

        public static bool ImportFunc<TResult>(string moduleName, string funcName, Func<ImportedContext, TResult> import)

            where TResult : unmanaged
        {
            var signature = $"(){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 0, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, TResult>)state.Import;

                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext());
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, TResult> import)
            where T1 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 1, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 2, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 3, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 4, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 5, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 6, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where TResult : unmanaged
        {
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 7, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 8, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 9, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 10, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 11, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 12, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}{Sig<T13>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 13, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var a12 = *(T13*)(state.Args + 12);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}{Sig<T13>()}{Sig<T14>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 14, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var a12 = *(T13*)(state.Args + 12);
                var a13 = *(T14*)(state.Args + 13);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }

        public static bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> import)
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
            var signature = $"({Sig<T1>()}{Sig<T2>()}{Sig<T3>()}{Sig<T4>()}{Sig<T5>()}{Sig<T6>()}{Sig<T7>()}{Sig<T8>()}{Sig<T9>()}{Sig<T10>()}{Sig<T11>()}{Sig<T12>()}{Sig<T13>()}{Sig<T14>()}{Sig<T15>()}){Sig<TResult>()}";
            return Register(moduleName, funcName, signature, 15, 1, import, OnInvoke);

            static void OnInvoke(ImportedFuncState state)
            {
                var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>)state.Import;
                var a0 = *(T1*)(state.Args + 0);
                var a1 = *(T2*)(state.Args + 1);
                var a2 = *(T3*)(state.Args + 2);
                var a3 = *(T4*)(state.Args + 3);
                var a4 = *(T5*)(state.Args + 4);
                var a5 = *(T6*)(state.Args + 5);
                var a6 = *(T7*)(state.Args + 6);
                var a7 = *(T8*)(state.Args + 7);
                var a8 = *(T9*)(state.Args + 8);
                var a9 = *(T10*)(state.Args + 9);
                var a10 = *(T11*)(state.Args + 10);
                var a11 = *(T12*)(state.Args + 11);
                var a12 = *(T13*)(state.Args + 12);
                var a13 = *(T14*)(state.Args + 13);
                var a14 = *(T15*)(state.Args + 14);
                var retPtr = (TResult*)(state.Args + 0);
                try
                {
                    *retPtr = import.Invoke(state.GetContext(), a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14);
                }
                catch
                {
                    *retPtr = default;
                    throw;
                }
            }
        }
    }
}
