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
    }
}
