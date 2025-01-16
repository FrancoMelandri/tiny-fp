using System.Diagnostics.Contracts;

namespace TinyFp.Extensions;

public static partial class Functional
{
    [Pure]
    public static Func<T2, R> Apply<T1, T2, R>(this Func<T1, T2, R> func, T1 t1)
         => t2 => func(t1, t2);

    [Pure]
    public static Func<T2, T3, R> Apply<T1, T2, T3, R>(this Func<T1, T2, T3, R> func, T1 t1)
          => (t2, t3) => func(t1, t2, t3);

    [Pure]
    public static Func<T2, T3, T4, R> Apply<T1, T2, T3, T4, R>(this Func<T1, T2, T3, T4, R> func, T1 t1)
        => (t2, t3, t4) => func(t1, t2, t3, t4);
}