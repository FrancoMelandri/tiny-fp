namespace TinyFp.Extensions;

public static partial class Functional
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

    public static M Map<A, M>(this A @this, Func<A, M> fn)
        => fn(@this);

    public static void Do<T>(this T @this, Action<T> action)
        => action(@this);
}