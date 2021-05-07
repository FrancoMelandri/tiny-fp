using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System;

namespace TinyFp
{
    public static class TaskExtensions
    {
        [Pure]
        public static Task<T> AsTask<T>(this T @this) 
            => Task.FromResult(@this);

        [Pure]
        public static async Task<M> MapAsync<A, M>(this Task<A> @this,
                                                   Func<A, Task<M>> fn)
            => await fn(await @this);

        [Pure]
        public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
                                                        Func<R, M> onRight,
                                                        Func<L, M> onLeft)
            => (await @this).Match(onRight, onLeft);

        [Pure]
        public static async Task<Either<L, M>> BindAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                  Func<R, Either<L, M>> onRight)
            => (await @this).Bind(onRight);

        [Pure]
        public static async Task<Either<M, R>> BindLeftAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                      Func<L, Either<M, R>> onLeft)
            => (await @this).BindLeft(onLeft);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this,
                                                     Func<A, B> onSome,
                                                     Func<B> onNone)
            => (await @this).Match(onSome, onNone);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this, 
                                                     Func<A, B> onSome, 
                                                     Func<Task<B>> onNone)
            => await (await @this).MatchAsync(onSome, onNone);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this, 
                                                     Func<A, Task<B>> onSome, 
                                                     Func<Task<B>> onNone)
            => await (await @this).MatchAsync(onSome, onNone);

        [Pure]
        public static async Task<B> MatchAsync<A, B>(this Task<Option<A>> @this, 
                                                     Func<A, Task<B>> onSome, 
                                                     Func<B> onNone)
            => await (await @this).MatchAsync(onSome, onNone);

        [Pure]
        public static async Task<Option<B>> BindAsync<A, B>(this Task<Option<A>> @this,
                                                            Func<A, Option<B>> onBind)
            => (await @this).Bind(onBind);
    }
}
