namespace TinyFp.Extensions;

public static partial class Functional
{
    public static Either<L, M> ToEither<A, M, L>(this A @this,
        Func<A, M> map,
        Predicate<A> leftWhen,
        L leftValue)
        => ToOption(@this, map, leftWhen)
            .ToEither(leftValue);

    public static Either<L, M> ToEither<A, M, L>(this A @this,
        Func<A, M> map,
        Predicate<A> leftWhen,
        Func<L> leftValue)
        => ToEither(@this, map, leftWhen, leftValue());

    public static Either<L, R> ToEither<L, R>(this R @this, L leftValue)
        => ToEither(@this, _ => _, _ => false, leftValue);

    public static Either<L, R> ToEither<L, R>(this R @this, Func<L> onLeft)
        => ToEither(@this, _ => _, _ => false, onLeft());

    public static async Task<Either<L, M>> ToEitherAsync<A, M, L>(this Task<A> @this,
        Func<A, M> map,
        Predicate<A> leftWhen,
        L leftValue)
        => (await ToOptionAsync(@this, map, leftWhen))
            .ToEither(leftValue);

    public static Task<Either<L, M>> ToEitherAsync<A, M, L>(this Task<A> @this,
        Func<A, M> map,
        Predicate<A> leftWhen,
        Func<L> leftValue)
        => ToEitherAsync(@this, map, leftWhen, leftValue());

    public static Task<Either<L, R>> ToEitherAsync<L, R>(this Task<R> @this, L leftValue)
        => ToEitherAsync(@this, _ => _, _ => false, leftValue);

    public static Task<Either<L, R>> ToEitherAsync<L, R>(this Task<R> @this, Func<L> onLeft)
        => ToEitherAsync(@this, _ => _, _ => false, onLeft());
}