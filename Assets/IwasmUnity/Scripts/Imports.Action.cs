#nullable enable
using System;

namespace IwasmUnity
{
    unsafe partial class Imports
    {
        public bool ImportAction(string moduleName, string funcName, Action<ImportedContext> import)
        {
            var argTypeVec = wasm_valtype_vec_t.Empty;
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext>)s.Import;
                    import.Invoke(
                        s.AsContext());
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1>(string moduleName, string funcName, Action<ImportedContext, T1> import)
            where T1 : unmanaged
        {
            const int ArgCount = 1;
            var t1 = TypeHelper.GetValuetype<T1>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2>(string moduleName, string funcName, Action<ImportedContext, T1, T2> import)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            const int ArgCount = 2;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
        {
            const int ArgCount = 3;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
        {
            const int ArgCount = 4;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
        {
            const int ArgCount = 5;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
        {
            const int ArgCount = 6;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
        {
            const int ArgCount = 7;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where T8 : unmanaged
        {
            const int ArgCount = 8;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9> import)
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
            const int ArgCount = 9;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> import)
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
            const int ArgCount = 10;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var t10 = TypeHelper.GetValuetype<T10>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8),
                        s.Arg<T10>(9));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> import)
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
            const int ArgCount = 11;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var t10 = TypeHelper.GetValuetype<T10>();
            var t11 = TypeHelper.GetValuetype<T11>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8),
                        s.Arg<T10>(9),
                        s.Arg<T11>(10));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> import)
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
            const int ArgCount = 12;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var t10 = TypeHelper.GetValuetype<T10>();
            var t11 = TypeHelper.GetValuetype<T11>();
            var t12 = TypeHelper.GetValuetype<T12>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8),
                        s.Arg<T10>(9),
                        s.Arg<T11>(10),
                        s.Arg<T12>(11));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> import)
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
            const int ArgCount = 13;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var t10 = TypeHelper.GetValuetype<T10>();
            var t11 = TypeHelper.GetValuetype<T11>();
            var t12 = TypeHelper.GetValuetype<T12>();
            var t13 = TypeHelper.GetValuetype<T13>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12, &t13 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8),
                        s.Arg<T10>(9),
                        s.Arg<T11>(10),
                        s.Arg<T12>(11),
                        s.Arg<T13>(12));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> import)
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
            const int ArgCount = 14;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var t10 = TypeHelper.GetValuetype<T10>();
            var t11 = TypeHelper.GetValuetype<T11>();
            var t12 = TypeHelper.GetValuetype<T12>();
            var t13 = TypeHelper.GetValuetype<T13>();
            var t14 = TypeHelper.GetValuetype<T14>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12, &t13, &t14 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8),
                        s.Arg<T10>(9),
                        s.Arg<T11>(10),
                        s.Arg<T12>(11),
                        s.Arg<T13>(12),
                        s.Arg<T14>(13));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportAction<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string moduleName, string funcName, Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> import)
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
            const int ArgCount = 15;
            var t1 = TypeHelper.GetValuetype<T1>();
            var t2 = TypeHelper.GetValuetype<T2>();
            var t3 = TypeHelper.GetValuetype<T3>();
            var t4 = TypeHelper.GetValuetype<T4>();
            var t5 = TypeHelper.GetValuetype<T5>();
            var t6 = TypeHelper.GetValuetype<T6>();
            var t7 = TypeHelper.GetValuetype<T7>();
            var t8 = TypeHelper.GetValuetype<T8>();
            var t9 = TypeHelper.GetValuetype<T9>();
            var t10 = TypeHelper.GetValuetype<T10>();
            var t11 = TypeHelper.GetValuetype<T11>();
            var t12 = TypeHelper.GetValuetype<T12>();
            var t13 = TypeHelper.GetValuetype<T13>();
            var t14 = TypeHelper.GetValuetype<T14>();
            var t15 = TypeHelper.GetValuetype<T15>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12, &t13, &t14, &t15 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypeVec = wasm_valtype_vec_t.Empty;
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Action<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>)s.Import;
                    import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7),
                        s.Arg<T9>(8),
                        s.Arg<T10>(9),
                        s.Arg<T11>(10),
                        s.Arg<T12>(11),
                        s.Arg<T13>(12),
                        s.Arg<T14>(13),
                        s.Arg<T15>(14));
                });
            }
            finally
            {
                // IwasmCApi.wasm_functype_delete(functype);
            }
        }
    }
}
