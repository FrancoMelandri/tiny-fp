using System.Diagnostics.Contracts;

namespace TinyFp;

public static partial class TaskExtensions
{
    [Pure]
    public static async Task<Unit> ToTaskUnit<T>(this Task @this)
    {
        await @this;
        return Unit.Default;
    }

    [Pure]
    public static Task<T> AsTask<T>(this T @this)
        => Task.FromResult(@this);

    [Pure]
    public static async Task<M> MapAsync<A, M>(this Task<A> @this,
        Func<A, Task<M>> fn)
        => await fn(await @this);
    
    public static async Task<T> TeeAsync<T>(this Task<T> @this,
        Func<T, Task<T>> tee)
        => await tee(await @this);

    public static Task<T> TeeAsync<T>(this Task<T> @this, Action<T> tee)
        => @this.TeeAsync(_ =>
        {
            tee(_);
            return Task.FromResult(_);
        });

    public static async Task<T> TeeAsync<T>(this Task<T> @this, Func<T, Task> tee)
    {
        var result = await @this;
        await tee(result);
        return result;
    }

    public static async Task<T> TeeWhenAsync<T>(this Task<T> @this, Func<T, Task> tee, Func<bool> when)
    {
        var result = await @this;
        if (when())
            await tee(result);
        return result;
    }

    public static async Task<T> TeeWhenAsync<T>(this Task<T> @this, Func<T, Task> tee, Func<T, bool> when)
    {
        var result = await @this;
        if (when(result))
            await tee(result);
        return result;
    }
}