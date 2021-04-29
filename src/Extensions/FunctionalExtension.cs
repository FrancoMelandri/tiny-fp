using System;
using System.Threading.Tasks;

namespace TinyFp.Extensions
{
    public static class FunctionalExtension
    {
        public static T TeeWhen<T>(this T @this, Func<T, T> tee, Func<bool> when)
            => when() ? tee(@this) : @this;

        public static T TeeWhen<T>(this T @this, Func<T, T> tee, Func<T, bool> when)
            => when(@this) ? tee(@this) : @this;

        public static T Tee<T>(this T @this, Func<T, T> tee)
            => tee(@this);

        public static T Tee<T>(this T @this, Action<T> tee)
            => @this.Tee(_ =>
            {
                tee(_);
                return _;
            });

        public static Unit Using(IDisposable disposable, Action action)
        {
            using (disposable)
                action();
            return Unit.Default;
        }

        public static T Using<T>(IDisposable disposable, Func<T> func)
        {
            using (disposable)
                return func();
        }

        public static T Using<T, V>(V disposable, Func<V, T> action)
            where V : IDisposable
        {
            using (disposable)
                return action(disposable);
        }

        public static Unit Using(IDisposable disposable, Action<IDisposable> action)
        {
            using (disposable)
                action(disposable);
            return Unit.Default;
        }

        public static void Do<T>(this T @this, Action<T> action)
            => action(@this);

        public static Option<M> ToOption<A, M>(this A @this,
                                               Func<A, M> map,
                                               Predicate<A> noneWhen)
            => @this == null || noneWhen(@this) ?
                Option<M>.None() :
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
                    Option<M>.None() :
                    Option<M>.Some(map(value));
        }

        public static Task<Option<A>> ToOptionAsync<A>(this Task<A> @this, Predicate<A> noneWhen)
            => ToOptionAsync(@this, _ => _, noneWhen);

        public static Task<Option<A>> ToOptionAsync<A>(this Task<A> @this)
            => ToOptionAsync(@this, _ => _, _ => false);
    }
}
