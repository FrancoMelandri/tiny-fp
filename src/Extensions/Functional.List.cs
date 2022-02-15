using System.Diagnostics.Contracts;

namespace TinyFp.Extensions
{
    public static partial class Functional
    {
        [Pure]
        public static IEnumerable<R> Filter<L, R>(this IEnumerable<Either<L, R>> @this)
            => @this.Where(_ => _.IsRight).Select(_ => _._right);

        [Pure]
        public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this)
            => @this.Where(_ => _.IsSome).Select(_ => _._value);

        [Pure]
        public static IEnumerable<R> Filter<L, R>(this IEnumerable<Either<L, R>> @this, Func<R, bool> predicate)
            => @this.Where(_ => _.IsRight && predicate(_._right)).Select(_ => _._right);

        [Pure]
        public static IEnumerable<T> Filter<T>(this IEnumerable<Option<T>> @this, Func<T, bool> predicate)
            => @this.Where(_ => _.IsSome && predicate(_._value)).Select(_ => _._value);

        [Pure]
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> @this, Func<T, bool> predicate)
            => @this.Where(predicate);

        [Pure]
        public static IEnumerable<L> FilterLeft<L, R>(this IEnumerable<Either<L, R>> @this)
            => @this.Where(_ => _.IsLeft).Select(_ => _._left);

        [Pure]
        public static IEnumerable<L> FilterLeft<L, R>(this IEnumerable<Either<L, R>> @this, Func<L, bool> predicate)
            => @this.Where(_ => _.IsLeft && predicate(_._left)).Select(_ => _._left);

        [Pure]
        public static Unit ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (var item in @this)
            {
                action(item);
            }
            return Unit.Default;
        }

        [Pure]
        public static S Fold<S, T>(this IEnumerable<T> @this, S state, Func<S, T, S> folder)
        {
            @this.ForEach(_ => state = folder(state, _));
            return state;
        }

        [Pure]
        public static T Reduce<T>(this IEnumerable<T> @this, Func<T, T, T> reducer)
            => @this.Fold(default, reducer);

        [Pure]
        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> @this, Func<T, R> map)
            => @this.Select(map);

        [Pure]
        public static IEnumerable<M> Map<T, M>(this IEnumerable<Option<T>> values, Func<T, M> map)
            => values.Where(_ => _.IsSome).Select(_ => map(_._value));

        [Pure]
        public static IEnumerable<M> Map<L, R, M>(this IEnumerable<Either<L, R>> @this, Func<R, M> map)
            => @this.Where(_ => _.IsRight).Select(_ => map(_._right));

        [Pure]
        public static IEnumerable<M> MapLeft<L, R, M>(this IEnumerable<Either<L, R>> @this, Func<L, M> map)
            => @this.Where(_ => _.IsLeft).Select(_ => map(_._left));
    }
}
