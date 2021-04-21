using System;
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

        public static Either<L, R> Right(R right) => new Either<L, R>(right);

        public static Either<L, R> Left(L left) => new Either<L, R>(left);

        public Either<L, R> IfRightDo(Action<R> action)
        {
            if (_isRight) action(_right);
            return this;
        }

        public Either<L, R> IfLeftDo(Action<L> action)
        {
            if (!_isRight) action(_left);
            return this;
        }

        public Either<L, M> MapRight<M>(Func<R, M> map)
            => _isRight ? Either<L, M>.Right(map(_right)) : Either<L, M>.Left(_left);

        public async Task<Either<L, M>> MapRightAsync<M>(Func<R, Task<M>> mapRightAsync)
            => _isRight ? Either<L, M>.Right(await mapRightAsync(_right)) : Either<L, M>.Left(_left);

        public Either<M, R> MapLeft<M>(Func<L, M> map)
            => !_isRight ? Either<M, R>.Left(map(_left)) : Either<M, R>.Right(_right);

        public Either<L, M> Bind<M>(Func<R, Either<L, M>> bind)
            => _isRight ? bind(_right) : Either<L, M>.Left(_left);

        public Task<Either<L, M>> BindAsync<M>(Func<R, Task<Either<L, M>>> bindAsync)
            => _isRight ? bindAsync(_right) : Task.FromResult(Either<L, M>.Left(_left));

        public M Match<M>(Func<R, M> onRight, Func<L, M> onLeft)
            => IsRight ? onRight(_right) : onLeft(_left);

        public static implicit operator Either<L, R> (R right) => Right(right);
        public static implicit operator Either<L, R> (L left) => Left(left);
    }
}