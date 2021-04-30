using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TinyFp
{
    public struct Either<L, R>
    {
        private readonly bool _isRight;
        private readonly R _right;
        private readonly L _left;

        public bool IsRight => _isRight;
        public bool IsLeft => !_isRight;

        private Either(R right)
        {
            if (right is null) throw new ArgumentNullException(nameof(right));
            _isRight = true;
            _right = right;
            _left = default;
        }

        private Either(L left)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            _isRight = false;
            _left = left;
            _right = default;
        }

        public static Either<L, R> Right(R right) => new(right);

        public static Either<L, R> Left(L left) => new(left);

        [Pure]
        public Either<L, R> OnRight(Action<R> action)
        {
            if (_isRight) action(_right);
            return this;
        }

        [Pure]
        public Either<L, R> OnLeft(Action<L> action)
        {
            if (!_isRight) action(_left);
            return this;
        }

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
            => _isRight ? bindAsync(_right) : Task.FromResult(Either<L, M>.Left(_left));

        [Pure]
        public Either<B, R> BindLeft<B>(Func<L, Either<B, R>> bind)
            => !_isRight ? bind(_left) : Either<B, R>.Right(_right);

        [Pure]
        public Task<Either<B, R>> BindLeftAsync<B>(Func<L, Task<Either<B, R>>> bindAsync)
            => !_isRight ? bindAsync(_left) : Task.FromResult(Either<B, R>.Right(_right));

        [Pure]
        public M Match<M>(Func<R, M> onRight, Func<L, M> onLeft)
            => IsRight ? onRight(_right) : onLeft(_left);

        [Pure]
        public Task<M> MatchAsync<M>(Func<R, Task<M>> onRightAsync, Func<L, Task<M>> onLeftAsync)
            => IsRight ? onRightAsync(_right) : onLeftAsync(_left);

        public static implicit operator Either<L, R> (R right) => Right(right);
        public static implicit operator Either<L, R> (L left) => Left(left);
    }
}