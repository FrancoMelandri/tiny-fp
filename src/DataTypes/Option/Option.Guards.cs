using System.Diagnostics.Contracts;

namespace TinyFp;

public readonly partial struct Option<A>
{
    [Pure]
    public Option<B> GuardMap<B>(Func<A, B> delegateIfDefault, params (Func<A, bool> evaluateExpression, Func<A, B> delegateIfExpressionTrue)[] guards)
        => _isSome ?
            Map(_ => guards
                .Where(guard => guard.evaluateExpression(_))
                .DefaultIfEmpty((_ => true, delegateIfDefault))
                .FirstOrDefault()
                .delegateIfExpressionTrue(_)) :
            Option<B>.None();

    [Pure]
    public async Task<Option<B>> GuardMapAsync<B>(Func<A, Task<B>> delegateIfDefault, params (Func<A, bool> evaluateExpression, Func<A, Task<B>> delegateIfExpressionTrue)[] guards)
        => _isSome ?
            await MapAsync(_ => guards
                .Where(guard => guard.evaluateExpression(_))
                .DefaultIfEmpty((_ => true, delegateIfDefault))
                .FirstOrDefault()
                .delegateIfExpressionTrue(_))
            : Option<B>.None();

    [Pure]
    public Option<B> GuardBind<B>(Func<A, Option<B>> delegateIfDefault, params (Func<A, bool> evaluateExpression, Func<A, Option<B>> delegateIfExpressionTrue)[] guards)
        => _isSome ?
            Bind(_ => guards
                .Where(guard => guard.evaluateExpression(_))
                .DefaultIfEmpty((_ => true, delegateIfDefault))
                .FirstOrDefault()
                .delegateIfExpressionTrue(_)) :
            Option<B>.None();

    [Pure]
    public async Task<Option<B>> GuardBindAsync<B>(Func<A, Task<Option<B>>> delegateIfDefault, params (Func<A, bool> evaluateExpression, Func<A, Task<Option<B>>> delegateIfExpressionTrue)[] guards)
        => _isSome ?
            await BindAsync(_ => guards
                .Where(guard => guard.evaluateExpression(_))
                .DefaultIfEmpty((_ => true, delegateIfDefault))
                .FirstOrDefault()
                .delegateIfExpressionTrue(_)) :
            Option<B>.None();
}