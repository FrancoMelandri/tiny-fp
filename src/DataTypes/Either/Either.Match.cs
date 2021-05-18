using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TinyFp
{
    public partial struct Either<L, R>
    {
        [Pure]
        public M Match<M>(Func<R, M> onRight, Func<L, M> onLeft)
            => IsRight ? onRight(_right) : onLeft(_left);

        [Pure]
        public Task<M> MatchAsync<M>(Func<R, Task<M>> onRightAsync, Func<L, Task<M>> onLeftAsync)
            => IsRight ? onRightAsync(_right) : onLeftAsync(_left);

        [Pure]
        public Task<M> MatchAsync<M>(Func<R, M> onRightAsync, Func<L, Task<M>> onLeftAsync)
            => IsRight ? onRightAsync(_right).AsTask() : onLeftAsync(_left);

        [Pure]
        public Task<M> MatchAsync<M>(Func<R, Task<M>> onRightAsync, Func<L, M> onLeftAsync)
            => IsRight ? onRightAsync(_right) : onLeftAsync(_left).AsTask();

        [Pure]
        public Task<M> MatchAsync<M>(Func<R, M> onRightAsync, Func<L, M> onLeftAsync)
            => IsRight ? onRightAsync(_right).AsTask() : onLeftAsync(_left).AsTask();
    }

}