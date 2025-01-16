namespace TinyFp.Extensions;

public static partial class Functional
{
    public static T Which<T>(Func<bool> condition, Func<T> onTrue, Func<T> onFalse)
       => condition() ? onTrue() : onFalse();

    public static T Which<T, S>(this S @value, Func<S, bool> condition, Func<S, T> onTrue, Func<S, T> onFalse)
       => condition(@value) ? onTrue(@value) : onFalse(@value);

    public static async Task<T> Which<T>(Func<bool> condition, Func<Task<T>> onTrue, Func<Task<T>> onFalse)
      => condition() ? await onTrue() : await onFalse();

    public static async Task<T> Which<T, S>(this S @value, Func<S, bool> condition, Func<S, Task<T>> onTrue, Func<S, Task<T>> onFalse)
       => condition(@value) ? await onTrue(@value) : await onFalse(@value);
}