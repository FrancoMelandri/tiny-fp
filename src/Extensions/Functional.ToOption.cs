namespace TinyFp.Extensions
{
    public static partial class Functional
    {
        public static Option<M> ToOption<A, M>(this A @this,
                                               Func<A, M> map,
                                               Predicate<A> noneWhen)
            => @this == null || noneWhen(@this) ?
                Option<M>.None :
                Option<M>.Some(map(@this));

        public static Option<A> ToOption<A>(this A @this, Predicate<A> noneWhen)
            => ToOption(@this, _ => _, noneWhen);

        public static Option<A> ToOption<A>(this A @this)
            => ToOption(@this, _ => _, _ => false);

        public static async Task<Option<M>> ToOptionAsync<A, M>(this Task<A> @this,
                                                                Func<A, M> map,
                                                                Predicate<A> noneWhen)
        {
            var value = await @this;
            return value == null || noneWhen(value) ?
                    Option<M>.None :
                    Option<M>.Some(map(value));
        }

        public static Task<Option<A>> ToOptionAsync<A>(this Task<A> @this, Predicate<A> noneWhen)
            => ToOptionAsync(@this, _ => _, noneWhen);

        public static Task<Option<A>> ToOptionAsync<A>(this Task<A> @this)
            => ToOptionAsync(@this, _ => _, _ => false);

    }
}
