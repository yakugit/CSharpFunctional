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
    public static class StreamResultExtention
    {
        /// <summary>
        /// Combine acquisition, execution, and conversion
        /// </summary>
        /// <typeparam name="S">type of Stream</typeparam>
        /// <typeparam name="T">in Success Type</typeparam>
        /// <typeparam name="ET">in Error Type</typeparam>
        /// <param name="returnStreamFunc">Return Stream Function</param>
        /// <param name="exceptionConverter">Exception to in Error Type Converter</param>
        /// <param name="useStreamFunc">Function with Stream</param>
        /// <returns></returns>
        public static Result<T,ET> Run<S, T, ET>(this Func<S> returnStreamFunc, Func<Exception, ET> exceptionConverter, 
            Func<S, Result<T, ET>> useStreamFunc) where S : Stream
        {
            try
            {
                using (var stream = returnStreamFunc())
                {
                    return useStreamFunc(stream);
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
        /// <typeparam name="S">type of Stream</typeparam>
        /// <typeparam name="T">in Success Type</typeparam>
        /// <typeparam name="ET">in Error Type</typeparam>
        /// <param name="returnStreamFunc">Return Stream Function</param>
        /// <param name="exceptionConverter">Exception to in Error Type Converter</param>
        /// <param name="useStreamAction">Action with Stream</param>
        /// <returns></returns>
        public static Result<T, ET> Run<S, T, ET>(this Func<S> returnStreamFunc, Func<Exception, ET> exceptionConverter,
            Action<S> useStreamAction) where S : Stream
        {
            try
            {
                using (var stream = returnStreamFunc())
                {
                    useStreamAction(stream);
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
