using CSharpFunctional.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpFunctional.Extention
{
    /// <summary>
    /// Extention Run Return Result Function with Stream  
    /// </summary>
    public static class DisposeResultExtention
    {
        /// <summary>
        /// Combine acquisition, execution, and conversion
        /// </summary>
        /// <typeparam name="D">type of Disposable</typeparam>
        /// <typeparam name="T">in Success Type</typeparam>
        /// <typeparam name="ET">in Error Type</typeparam>
        /// <param name="returnDisposableFunc">Return Disposable Function</param>
        /// <param name="exceptionConverter">Exception to in Error Type Converter</param>
        /// <param name="useDisposableFunc">Function with Stream</param>
        /// <returns>Result<T,ET></returns>
        public static Result<T,ET> Run<D, T, ET>(this Func<D> returnDisposableFunc, Func<Exception, ET> exceptionConverter, 
            Func<D, Result<T, ET>> useDisposableFunc) where D : IDisposable
        {
            try
            {
                using (var disposable = returnDisposableFunc())
                {
                    return useDisposableFunc(disposable);
                }
            }
            catch(Exception ex)
            {
                return Result<T, ET>.Error(exceptionConverter(ex));
            }
        }

        /// <summary>
        /// Combine acquisition, execution, and conversion
        /// </summary>
        /// <typeparam name="D">type of Disposable</typeparam>
        /// <typeparam name="T">in Success Type</typeparam>
        /// <typeparam name="ET">in Error Type</typeparam>
        /// <param name="returnDisposableFunc">Return Stream Function</param>
        /// <param name="exceptionConverter">Exception to in Error Type Converter</param>
        /// <param name="useDisposableAction">Action with Disposable</param>
        /// <returns>Result<T,ET></returns>
        public static Result<T, ET> Run<D, T, ET>(this Func<D> returnDisposableFunc, Func<Exception, ET> exceptionConverter,
            Action<D> useDisposableAction) where D : IDisposable
        {
            try
            {
                using (var stream = returnDisposableFunc())
                {
                    useDisposableAction(stream);
                    return Result<T, ET>.Success(default(T));
                }
            }
            catch (Exception ex)
            {
                return Result<T, ET>.Error(exceptionConverter(ex));
            }
        }
    }
}
