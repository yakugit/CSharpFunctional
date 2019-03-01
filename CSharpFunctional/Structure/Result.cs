
namespace CSharpFunctional.Structure
{
    /// <summary>
    /// Immutable structure for function return value.
    /// </summary>
    public struct Result<T, ET>
    {
        /// <summary>
        /// have a value
        /// </summary>
        public bool IsOK { get; }

        /// <summary>
        /// not have a value
        /// </summary>
        public bool IsError => !IsOK;

        /// <summary>
        /// in Success Value
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// in Error Value 
        /// </summary>
        public ET ErrorValue { get; }

        /// <summary>
        /// private constructor
        /// </summary>
        /// <param name="value">in Success Value</param>
        /// <param name="errorValue">in Error Value</param>
        /// <param name="isOK">isOK Flag</param>
        private Result(T value, ET errorValue, bool isOK)
        {
            IsOK = isOK;
            Value = value;
            ErrorValue = errorValue;
        } 
        
        /// <summary>
        /// in success value create static function
        /// </summary>
        /// <param name="value">in success value</param>
        /// <returns>new in sucess structure</returns>
        public static Result<T, ET> Success(T value)
        {
            return new Result<T, ET>(value, default(ET), true);
        }

        /// <summary>
        /// in error value create static function
        /// </summary>
        /// <param name="value">in error value</param>
        /// <returns>new in error value structure</returns>
        public static Result<T, ET> Error(ET value)
        {
            return new Result<T, ET>(default(T), value, false);
        }
    }
}
