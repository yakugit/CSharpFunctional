using CSharpFunctional.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpFunctional.Extention
{
    /// <summary>
    /// Extention for Result<T, ET>
    /// </summary>
    public static class ResultExtention
    {
        /// <summary>
        /// in success value to new structure extention
        /// </summary>
        /// <typeparam name="T">in success value type</typeparam>
        /// <typeparam name="ET">in error value type</typeparam>
        /// <param name="value">in sucess value</param>
        /// <returns>new success value structure</returns>
        public static Result<T, ET> ToSuccess<T, ET>(this T value)
        {
            return Result<T, ET>.Success(value);
        }

        /// <summary>
        /// in error value to new structure extention
        /// </summary>
        /// <typeparam name="T">in success value type</typeparam>
        /// <typeparam name="ET">in error value type</typeparam>
        /// <param name="value">in error value</param>
        /// <returns>new error value structure</returns>
        public static Result<T, ET> ToError<T, ET>(this ET value)
        {
            return Result<T, ET>.Error(value);
        }

        /// <summary>
        /// convert Result<T, ET> to Result<U, ET>
        /// </summary>
        /// <typeparam name="T">input in success type</typeparam>
        /// <typeparam name="ET">input and output in error type</typeparam>
        /// <typeparam name="U">output in sucess type</typeparam>
        /// <param name="result">input result</param>
        /// <param name="mapFunc">convert function T to U</param>
        /// <returns>converted result structure</returns>
        public static Result<U, ET> Map<T, ET, U>(this Result<T, ET> result, Func<T, U> mapFunc)
        {
            if(result.IsOK)
            {
                return mapFunc(result.Value).ToSuccess<U, ET>();
            }
            else
            {
                return result.ErrorValue.ToError<U, ET>();
            }
        }

        /// <summary>
        /// convert Result<T, ET> to Result<T, U> 
        /// </summary>
        /// <typeparam name="T">in success type</typeparam>
        /// <typeparam name="ET">input in error type</typeparam>
        /// <typeparam name="U">output in error type</typeparam>
        /// <param name="result">input result structure</param>
        /// <param name="mapErrorFunc">output function Result<T, ET> to Result<T, U></param>
        /// <returns>converted result structure</returns>
        public static Result<T, U> mapError<T, ET, U>(this Result<T, ET> result, Func<ET, U> mapErrorFunc)
        {
            if (result.IsOK)
            {
                return result.Value.ToSuccess<T, U>();
            }
            else
            {
                return mapErrorFunc(result.ErrorValue).ToError<T, U>();
            }
        }
    }
}
