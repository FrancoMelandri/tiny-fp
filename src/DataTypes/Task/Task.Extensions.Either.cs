using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System;

namespace TinyFp
{
    public static partial class TaskExtensions
    {
        [Pure]
        public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
                                                        Func<R, M> onRight,
                                                        Func<L, M> onLeft)
            => (await @this).Match(onRight, onLeft);

        [Pure]
        public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
                                                        Func<R, Task<M>> onRight,
                                                        Func<L, M> onLeft)
            => await (await @this).MatchAsync(onRight, onLeft);

        [Pure]
        public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
                                                        Func<R, M> onRight,
                                                        Func<L, Task<M>> onLeft)
            => await (await @this).MatchAsync(onRight, onLeft);

        [Pure]
        public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
                                                        Func<R, Task<M>> onRight,
                                                        Func<L, Task<M>> onLeft)
            => await (await @this).MatchAsync(onRight, onLeft);

        [Pure]
        public static async Task<Either<L, M>> BindAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                  Func<R, Either<L, M>> onRight)
            => (await @this).Bind(onRight);

        [Pure]
        public static async Task<Either<L, M>> BindAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                  Func<R, Task<Either<L, M>>> onRight)
            => await (await @this).BindAsync(onRight);

        [Pure]
        public static async Task<Either<M, R>> BindLeftAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                      Func<L, Either<M, R>> onLeft)
            => (await @this).BindLeft(onLeft);

        [Pure]
        public static async Task<Either<L, M>> MapAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                 Func<R, M> onRight)
            => (await @this).Map(onRight);

        [Pure]
        public static async Task<Either<L, M>> MapAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                 Func<R, Task<M>> onRight)
            => await (await @this).MapAsync(onRight);

        [Pure]
        public static async Task<Either<M, R>> MapLeftAsync<L, R, M>(this Task<Either<L, R>> @this, 
                                                                      Func<L, M> onLeft)
            => (await @this).MapLeft(onLeft);
    }
}
