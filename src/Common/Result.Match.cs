using System.Diagnostics.Contracts;

namespace TinyFp.Common
{
    public partial struct Result<A>
    {
        [Pure]
        public R Match<R>(Func<A, R> succ, Func<Exception, R> fail)
            => IsFaulted
                ? fail(Exception)
                : succ(Value);

        [Pure]
        public Task<R> MatchAsync<R>(Func<A, Task<R>> succAsync, Func<Exception, Task<R>> failAsync)
            => IsFaulted
                ? failAsync(Exception)
                : succAsync(Value);
    }
}
