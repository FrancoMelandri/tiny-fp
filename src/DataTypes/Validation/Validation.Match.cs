using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TinyFp
{
    public partial struct Validation<FAIL, SUCCESS>
    {
        [Pure]
        public R Match<R>(Func<SUCCESS, R> onSucc, Func<FAIL, R> onFail)
            => IsSuccess ? onSucc(_success) : onFail(_fail);

        [Pure]
        public R Match<R>(R Succ, Func<FAIL, R> onFail)
            => IsSuccess ? Succ : onFail(_fail);

        [Pure]
        public R Match<R>(Func<SUCCESS, R> onSucc, R Fail)
            => IsSuccess ? onSucc(_success) : Fail;

        [Pure]
        public R Match<R>(R Succ, R Fail)
            => IsSuccess ? Succ : Fail;

        [Pure]
        public Task<R> MatchAsync<R>(Func<SUCCESS, Task<R>> onSucc, Func<FAIL, Task<R>> onFail)
            => IsSuccess ? onSucc(_success) : onFail(_fail);

        [Pure]
        public Task<R> MatchAsync<R>(Func<SUCCESS, Task<R>> onSucc, Task<R> Fail)
            => IsSuccess ? onSucc(_success) : Fail;

        [Pure]
        public Task<R> MatchAsync<R>(Task<R> Succ, Func<FAIL, Task<R>> onFail)
            => IsSuccess ? Succ : onFail(_fail);

        [Pure]
        public Task<R> MatchAsync<R>(Task<R> Succ, Task<R> Fail)
            => IsSuccess ? Succ : Fail;
    }
}
