#nullable enable
using System;

namespace IwasmUnity
{
    unsafe partial class Imports
    {
        public bool ImportFunc<TResult>(string moduleName, string funcName, Func<ImportedContext, TResult> import)
            where TResult : unmanaged
        {
            const int ResultCount = 1;
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypeVec = wasm_valtype_vec_t.Empty;
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext());
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, TResult> import)
            where T1 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 1;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 2;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 3;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 4;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 5;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 6;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, TResult> import)
            where T1 : unmanaged
            where T2 : unmanaged
            where T3 : unmanaged
            where T4 : unmanaged
            where T5 : unmanaged
            where T6 : unmanaged
            where T7 : unmanaged
            where TResult : unmanaged
        {
            const int ArgCount = 7;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, TResult> import)
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
            const int ArgCount = 8;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, TResult>)s.Import;
                    var result = import.Invoke(
                        s.AsContext(),
                        s.Arg<T1>(0),
                        s.Arg<T2>(1),
                        s.Arg<T3>(2),
                        s.Arg<T4>(3),
                        s.Arg<T5>(4),
                        s.Arg<T6>(5),
                        s.Arg<T7>(6),
                        s.Arg<T8>(7));
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> import)
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
            const int ArgCount = 9;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> import)
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
            const int ArgCount = 10;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var t10 = TypeHelper.GetValtype<T10>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> import)
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
            const int ArgCount = 11;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var t10 = TypeHelper.GetValtype<T10>();
            var t11 = TypeHelper.GetValtype<T11>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> import)
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
            const int ArgCount = 12;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var t10 = TypeHelper.GetValtype<T10>();
            var t11 = TypeHelper.GetValtype<T11>();
            var t12 = TypeHelper.GetValtype<T12>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> import)
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
            const int ArgCount = 13;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var t10 = TypeHelper.GetValtype<T10>();
            var t11 = TypeHelper.GetValtype<T11>();
            var t12 = TypeHelper.GetValtype<T12>();
            var t13 = TypeHelper.GetValtype<T13>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12, &t13 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> import)
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
            const int ArgCount = 14;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var t10 = TypeHelper.GetValtype<T10>();
            var t11 = TypeHelper.GetValtype<T11>();
            var t12 = TypeHelper.GetValtype<T12>();
            var t13 = TypeHelper.GetValtype<T13>();
            var t14 = TypeHelper.GetValtype<T14>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12, &t13, &t14 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }

        public bool ImportFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(string moduleName, string funcName, Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> import)
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
            const int ArgCount = 15;
            const int ResultCount = 1;
            var t1 = TypeHelper.GetValtype<T1>();
            var t2 = TypeHelper.GetValtype<T2>();
            var t3 = TypeHelper.GetValtype<T3>();
            var t4 = TypeHelper.GetValtype<T4>();
            var t5 = TypeHelper.GetValtype<T5>();
            var t6 = TypeHelper.GetValtype<T6>();
            var t7 = TypeHelper.GetValtype<T7>();
            var t8 = TypeHelper.GetValtype<T8>();
            var t9 = TypeHelper.GetValtype<T9>();
            var t10 = TypeHelper.GetValtype<T10>();
            var t11 = TypeHelper.GetValtype<T11>();
            var t12 = TypeHelper.GetValtype<T12>();
            var t13 = TypeHelper.GetValtype<T13>();
            var t14 = TypeHelper.GetValtype<T14>();
            var t15 = TypeHelper.GetValtype<T15>();
            var tr = TypeHelper.GetValtype<TResult>();
            var argTypes = stackalloc wasm_valtype_t*[ArgCount] { &t1, &t2, &t3, &t4, &t5, &t6, &t7, &t8, &t9, &t10, &t11, &t12, &t13, &t14, &t15 };
            var argTypeVec = new wasm_valtype_vec_t(argTypes, ArgCount);
            var resultTypes = stackalloc wasm_valtype_t*[ResultCount] { &tr };
            var resultTypeVec = new wasm_valtype_vec_t(resultTypes, ResultCount);
            var functype = IwasmCApi.wasm_functype_new(&argTypeVec, &resultTypeVec);
            try
            {
                return ImportCore(moduleName, funcName, import, functype, s =>
                {
                    var import = (Func<ImportedContext, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>)s.Import;
                    var result = import.Invoke(
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
                    s.SetResult(0, result);
                });
            }
            finally
            {
                IwasmCApi.wasm_functype_delete(functype);
            }
        }
    }
}
