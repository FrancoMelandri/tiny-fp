using System.Diagnostics.CodeAnalysis;

namespace TinyFp.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ValueIsNullException : Exception
    {
        private const string VALUE_IS_NULL = "Value is null.";

        public ValueIsNullException()
            : base(VALUE_IS_NULL)
        { }

        public ValueIsNullException(string message) : base(message)
        { }

        public ValueIsNullException(string message, Exception innerException) : base(message, innerException)
        { }
    }

}
