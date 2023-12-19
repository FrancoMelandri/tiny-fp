using System.Diagnostics.Contracts;

namespace TinyFp.Common;

public readonly partial struct Result<A>
{
    [Pure]
    public R Match<R>(Func<A, R> success, Func<Exception, R> fail)
        => IsFaulted
            ? fail(Exception)
            : success(Value);

    [Pure]
    public Task<R> MatchAsync<R>(Func<A, Task<R>> successAsync, Func<Exception, Task<R>> failAsync)
        => IsFaulted
            ? failAsync(Exception)
            : successAsync(Value);
}