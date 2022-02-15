using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace TinyFp
{
    public partial struct Option<A>
    {
        private readonly bool _isSome;
        internal readonly A _value;

        public bool IsSome 
            => _isSome;

        public bool IsNone 
            => !_isSome;

        private Option(bool isSome, A value)
        {
            _isSome = isSome;
            _value = value;
        }

        public static Option<A> None
            => new(false, default);

        public static Option<A> Some(A value) 
            => new(true, value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Option<A> value)
            => value.IsSome;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public static bool operator false(Option<A> value)
            => value.IsNone;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !(Option<A> value)
            => value.IsNone;

        [Pure]
        public Either<L, A> ToEither<L>(Func<L> onLeft)
            => _isSome ?
                    Either<L, A>.Right(_value) :
                    Either<L, A>.Left(onLeft());

        [Pure]
        public Either<L, A> ToEither<L>(L left)
            => ToEither(() => left);
    }
}