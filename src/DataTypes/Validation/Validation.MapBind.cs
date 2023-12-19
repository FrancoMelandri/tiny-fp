using System.Diagnostics.Contracts;

namespace TinyFp;

public readonly partial struct Validation<FAIL, SUCCESS>
{
    [Pure]
    public Validation<FAIL, U> Bind<U>(Func<SUCCESS, Validation<FAIL, U>> f)
        => IsSuccess
            ? f(_success)
            : Validation<FAIL, U>.Fail(_fail);

    [Pure]
    public Validation<U, SUCCESS> BindFail<U>(Func<FAIL, Validation<U, SUCCESS>> f)
        => IsFail
            ? f(_fail)
            : Validation<U, SUCCESS>.Success(_success);

    [Pure]
    public Validation<FAIL, M> Map<M>(Func<SUCCESS, M> map)
        => IsSuccess
            ? Validation<FAIL, M>.Success(map(_success))
            : Validation<FAIL, M>.Fail(_fail);

    [Pure]
    public Validation<M, SUCCESS> MapFail<M>(Func<FAIL, M> map)
        => IsFail
            ? Validation<M, SUCCESS>.Fail(map(_fail))
            : Validation<M, SUCCESS>.Success(_success);
}