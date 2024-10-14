using System.Diagnostics.Contracts;
using static TinyFp.Prelude;

namespace TinyFp.Extensions;

public static partial class Functional
{
    [Pure]
    public static Either<L, R[]> Traverse<L, T, R>(
        this Either<L, T>[] values,
        Func<T, R> onRight)
        => values
        .Fold(Right<L, R[]>([]),
             (acc, either) => acc.Bind(_ => either.Map(__ => _.Append(onRight(__)).ToArray())));

    [Pure]
    public static Either<L, T[]> Traverse<L, T>(
        this Either<L, T>[] values)
        => values.Traverse(_ => _);

    [Pure]
    public static Task<Either<L, R>[]> Traverse<L, T, R>
        (this Func<T, Task<Either<L, R>>>[] funcs,
        Task<Either<L, T>> @value)
        => funcs.Fold(new List<Task<Either<L, R>>>(),
                     (state, current) => state.Concat([@value.BindAsync(_ => current(_))]).ToList())
        .Map(Task.WhenAll)
        .MapAsync(_ => _.ToArray().AsTask());
}