using System.Diagnostics.Contracts;

namespace TinyFp;

public static partial class TaskExtensions
{
    [Pure]
    public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
        Func<R, M> onRight,
        Func<L, M> onLeft)
        => (await @this).Match(onRight, onLeft);

    [Pure]
    public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
        Func<R, Task<M>> onRight,
        Func<L, M> onLeft)
        => await (await @this).MatchAsync(onRight, onLeft);

    [Pure]
    public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
        Func<R, M> onRight,
        Func<L, Task<M>> onLeft)
        => await (await @this).MatchAsync(onRight, onLeft);

    [Pure]
    public static async Task<M> MatchAsync<L, R, M>(this Task<Either<L, R>> @this,
        Func<R, Task<M>> onRight,
        Func<L, Task<M>> onLeft)
        => await (await @this).MatchAsync(onRight, onLeft);

    [Pure]
    public static async Task<Either<L, M>> BindAsync<L, R, M>(this Task<Either<L, R>> @this, 
        Func<R, Either<L, M>> onRight)
        => (await @this).Bind(onRight);

    [Pure]
    public static async Task<Either<L, M>> BindAsync<L, R, M>(this Task<Either<L, R>> @this, 
        Func<R, Task<Either<L, M>>> onRight)
        => await (await @this).BindAsync(onRight);

    [Pure]
    public static async Task<Either<M, R>> BindLeftAsync<L, R, M>(this Task<Either<L, R>> @this, 
        Func<L, Either<M, R>> onLeft)
        => (await @this).BindLeft(onLeft);

    [Pure]
    public static async Task<Either<L, M>> MapAsync<L, R, M>(this Task<Either<L, R>> @this, 
        Func<R, M> onRight)
        => (await @this).Map(onRight);

    [Pure]
    public static async Task<Either<L, M>> MapAsync<L, R, M>(this Task<Either<L, R>> @this, 
        Func<R, Task<M>> onRight)
        => await (await @this).MapAsync(onRight);

    [Pure]
    public static async Task<Either<M, R>> MapLeftAsync<L, R, M>(this Task<Either<L, R>> @this, 
        Func<L, M> onLeft)
        => (await @this).MapLeft(onLeft);

    [Pure]
    public static async Task<Either<L, M>> GuardMapAsync<L, R, M>(this Task<Either<L, R>> @this, Func<R, M> delegateIfDefault, params (Func<R, bool> evaluateExpression, Func<R, M> delegateIfEvalTrue)[] guards)
        => (await @this).GuardMap(delegateIfDefault, guards);

    [Pure]
    public static async Task<Either<L, M>> GuardMapAsync<L, R, M>(this Task<Either<L, R>> @this, Func<R, Task<M>> delegateIfDefault, params (Func<R, bool> evaluateExpression, Func<R, Task<M>> delegateIfEvalTrue)[] guards)
        => await (await @this).GuardMapAsync(delegateIfDefault, guards);

    [Pure]
    public static async Task<Either<L, M>> GuardBindAsync<L, R, M>(this Task<Either<L, R>> @this, Func<R, Either<L, M>> delegateIfDefault, params (Func<R, bool> evaluateExpression, Func<R, Either<L, M>> delegateIfEvalTrue)[] guards)
        => (await @this).GuardBind(delegateIfDefault, guards);

    [Pure]
    public static async Task<Either<A, B>> GuardBindAsync<L, R, A, B>(this Task<Either<L, R>> @this, Func<L, Either<A, B>> left, Func<R, Either<A, B>> delegateIfDefault, params (Func<R, bool> evaluateExpression, Func<R, Either<A, B>> delegateIfEvalTrue)[] guards)
        => (await @this).GuardBind(left, delegateIfDefault, guards);
    [Pure]
    public static async Task<Either<L, M>> GuardBindAsync<L, R, M>(this Task<Either<L, R>> @this, Func<R, Task<Either<L, M>>> delegateIfDefault, params (Func<R, bool> evaluateExpression, Func<R, Task<Either<L, M>>> delegateIfEvalTrue)[] guards)
        => await (await @this).GuardBindAsync(delegateIfDefault, guards);

    [Pure]
    public static async Task<Either<A, B>> GuardBindAsync<L, R, A, B>(this Task<Either<L, R>> @this, Func<L, Task<Either<A, B>>> left, Func<R, Task<Either<A, B>>> delegateIfDefault, params (Func<R, bool> evaluateExpression, Func<R, Task<Either<A, B>>> delegateIfEvalTrue)[] guards)
        => await (await @this).GuardBindAsync(left, delegateIfDefault, guards);
}