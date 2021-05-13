using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System;

namespace TinyFp
{
    public static partial class TaskExtensions
    {
        [Pure]
        public static Task<T> AsTask<T>(this T @this)
            => Task.FromResult(@this);

        [Pure]
        public static async Task<M> MapAsync<A, M>(this Task<A> @this,
                                                   Func<A, Task<M>> fn)
            => await fn(await @this);
    }
}
