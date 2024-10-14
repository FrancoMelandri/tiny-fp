using System;
using System.Threading.Tasks;

namespace TinyFp.Extensions;

public static partial class Functional
{
    public static T While<T>(Func<T> init, Func<T, bool> when, Func<T, T> execute)
    {
        var acc = init();
        while (when(acc))
            acc = execute(acc);
        return acc;
    }

    public async static Task<T> While<T>(Func<T> init, Func<T, bool> when, Func<T, Task<T>> execute)
    {
        var acc = init();
        while (when(acc))
            acc = await execute(acc);
        return acc;
    }

    public static async Task<T> While<T>(Func<Task<T>> init, Func<T, bool> when, Func<T, Task<T>> execute)
    {
        var acc = await init();
        return await While(() => acc, when, execute);
    }
}