using System.Diagnostics.CodeAnalysis;

namespace TinyFp.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class BottomException : Exception
    {
        public static readonly BottomException Default = new BottomException();
        public BottomException() 
            : base()
        { }

        public BottomException(string type)
            : base(type)
        { }

        public BottomException(string message, Exception innerException) 
            : base(message, innerException)
        { }
    }
}
