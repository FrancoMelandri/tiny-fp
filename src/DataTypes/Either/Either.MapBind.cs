using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TinyFp
{
    public partial struct Either<L, R>
    {
        [Pure]
        public Either<L, M> Map<M>(Func<R, M> map)
            => _isRight ? Either<L, M>.Right(map(_right)) : Either<L, M>.Left(_left);

        [Pure]
        public async Task<Either<L, M>> MapAsync<M>(Func<R, Task<M>> mapAsync)
            => _isRight ? Either<L, M>.Right(await mapAsync(_right)) : Either<L, M>.Left(_left);

        [Pure]
        public Either<M, R> MapLeft<M>(Func<L, M> mapLeft)
            => !_isRight ? Either<M, R>.Left(mapLeft(_left)) : Either<M, R>.Right(_right);

        [Pure]
        public async Task<Either<M, R>> MapLeftAsync<M>(Func<L, Task<M>> mapLeftAsync)
            => !_isRight ? Either<M, R>.Left(await mapLeftAsync(_left)) : Either<M, R>.Right(_right);

        [Pure]
        public Either<L, M> Bind<M>(Func<R, Either<L, M>> bind)
            => _isRight ? bind(_right) : Either<L, M>.Left(_left);

        [Pure]
        public Task<Either<L, M>> BindAsync<M>(Func<R, Task<Either<L, M>>> bindAsync)
            => _isRight ? bindAsync(_right) : Either<L, M>.Left(_left).AsTask();

        [Pure]
        public Either<B, R> BindLeft<B>(Func<L, Either<B, R>> bind)
            => !_isRight ? bind(_left) : Either<B, R>.Right(_right);

        [Pure]
        public Task<Either<B, R>> BindLeftAsync<B>(Func<L, Task<Either<B, R>>> bindAsync)
            => !_isRight ? bindAsync(_left) : Either<B, R>.Right(_right).AsTask();
    }
}