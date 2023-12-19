using System.Diagnostics.Contracts;

namespace TinyFp;

public readonly partial struct Validation<FAIL, SUCCESS>
{
    [Pure]
    public R Match<R>(Func<SUCCESS, R> onSuccess, Func<FAIL, R> onFail)
        => IsSuccess ? onSuccess(_success) : onFail(_fail);

    [Pure]
    public R Match<R>(R Succ, Func<FAIL, R> onFail)
        => IsSuccess ? Succ : onFail(_fail);

    [Pure]
    public R Match<R>(Func<SUCCESS, R> onSuccess, R fail)
        => IsSuccess ? onSuccess(_success) : fail;

    [Pure]
    public R Match<R>(R success, R fail)
        => IsSuccess ? success : fail;

    [Pure]
    public Task<R> MatchAsync<R>(Func<SUCCESS, Task<R>> onSuccess, Func<FAIL, Task<R>> onFail)
        => IsSuccess ? onSuccess(_success) : onFail(_fail);

    [Pure]
    public Task<R> MatchAsync<R>(Func<SUCCESS, Task<R>> onSuccess, Task<R> onFail)
        => IsSuccess ? onSuccess(_success) : onFail;

    [Pure]
    public Task<R> MatchAsync<R>(Task<R> onSuccess, Func<FAIL, Task<R>> onFail)
        => IsSuccess ? onSuccess : onFail(_fail);

    [Pure]
    public Task<R> MatchAsync<R>(Task<R> onSuccess, Task<R> onFail)
        => IsSuccess ? onSuccess : onFail;
}