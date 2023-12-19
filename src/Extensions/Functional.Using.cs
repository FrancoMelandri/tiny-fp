namespace TinyFp.Extensions;

public static partial class Functional
{
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

    public static async Task<Unit> UsingAsync<D>(D disposable, Func<D, Task> action)
        where D : IDisposable
    {
        using (disposable)
            await action(disposable);
        return Unit.Default;
    }

    public static async Task<T> UsingAsync<D, T>(D disposable, Func<D, Task<T>> func)
        where D : IDisposable
    {
        using (disposable)
            return await func(disposable);
    }

    public static async Task<Unit> UsingAsync<D1, D2>(D1 disposable1, Func<D1, D2> createDisposable2, Func<D1, D2, Task> action)
        where D1 : IDisposable
        where D2 : IDisposable
    {
        using (disposable1)
        using (var disposable2 = createDisposable2(disposable1))
            await action(disposable1, disposable2);
        return Unit.Default;
    }

    public static async Task<T> UsingAsync<D1, D2, T>(D1 disposable1, Func<D1, D2> createDisposable2, Func<D1, D2, Task<T>> func)
        where D1 : IDisposable
        where D2 : IDisposable
    {
        using (disposable1)
        using (var disposable2 = createDisposable2(disposable1))
            return await func(disposable1, disposable2);
    }
}