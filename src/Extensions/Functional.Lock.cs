using System.Diagnostics.CodeAnalysis;

namespace TinyFp.Extensions;

[ExcludeFromCodeCoverage]
public static partial class Functional
{
    public static T Lock<T>(object toLock, Func<T> func)
    {
        lock(toLock)
        {
            return func();
        }
    }

    public static Task<T> Lock<T>(object toLock, Func<Task<T>> func)
    {
        lock(toLock)
        {
            return func();
        }
    }

    public static Unit Lock(object toLock, Action func)
    {
        lock(toLock)
        {
            return Unit.Default.Tee(_ => func());
        }
    }
}