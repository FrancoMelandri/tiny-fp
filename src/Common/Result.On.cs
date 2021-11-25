using System.Diagnostics.Contracts;

namespace TinyFp.Common
{
    public partial struct Result<A>
    {
        [Pure]
        public A OnFail(A defaultValue)
            => IsFaulted
                ? defaultValue
                : Value;

        [Pure]
        public A OnFail(Func<Exception, A> f)
            => IsFaulted
                ? f(Exception)
                : Value;
    }
}
