using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TinyFp.Extensions;

namespace TinyFp
{
    public partial struct Either<L, R>
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
            => this
                .Tee(@this => { if (@this._isRight) action(@this._right); });

        [Pure]
        public Either<L, R> OnLeft(Action<L> action)
            => this
                .Tee(@this => { if (!@this._isRight) action(@this._left); });

        public static implicit operator Either<L, R>(R right) => Right(right);
        public static implicit operator Either<L, R>(L left) => Left(left);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Either<L, R> value)
            => value.IsRight;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public static bool operator false(Either<L, R> value)
            => value.IsLeft;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !(Either<L, R> value)
            => value.IsLeft;
    }
}