using TinyFp.Extensions;
using System.Diagnostics.Contracts;
using static TinyFp.Prelude;

namespace TinyFp
{
    public partial struct Either<L, R>
    {
        [Pure]
        public Either<L, M> GuardMap<M>(Func<R, M> delegateIfDefault, params (Func<R,bool> evalExpression, Func<R,M> delegateIfExpressionTrue)[] guards)
            => _isRight ? 
                _right.Map(_ => Right<L, M>(
                                    guards
                                        .Where(guard => guard.evalExpression(_))
                                        .DefaultIfEmpty((_ => true, delegateIfDefault))
                                        .FirstOrDefault()
                                        .delegateIfExpressionTrue(_))) : 
                Either<L, M>.Left(_left);

        [Pure]
        public Task<Either<L, M>> GuardMapAsync<M>(Func<R, Task<M>> delegateIfDefault, params (Func<R, bool> evalExpression, Func<R, Task<M>> delegateIfExpressionTrue)[] guards)
            => _isRight ?
                _right.Map(async _ => Right<L, M>(await
                                    guards
                                        .Where(guard => guard.evalExpression(_))
                                        .DefaultIfEmpty((_ => true, delegateIfDefault))
                                        .FirstOrDefault()
                                        .delegateIfExpressionTrue(_))) :
                Either<L, M>.Left(_left).AsTask();

        [Pure]
        public Either<L,B> GuardBind<B>(Func<R, Either<L, B>> delegateIfDefault, params (Func<R, bool> evalExpression, Func<R, Either<L, B>> delegateIfExpressionTrue)[] guards)
            => _isRight ?
                _right.Map(_ => guards
                                    .Where(guard => guard.evalExpression(_))
                                    .DefaultIfEmpty((_ => true, delegateIfDefault))
                                    .FirstOrDefault()
                                    .delegateIfExpressionTrue(_)) :
                Left<L, B>(_left);

        [Pure]
        public Either<A, B> GuardBind<A, B>(Func<L, Either<A, B>> left, Func<R, Either<A, B>> delegateIfDefault, params (Func<R, bool> evalExpression, Func<R, Either<A, B>> delegateIfExpressionTrue)[] guards)
            => _isRight ?
                _right.Map(_ => guards
                                    .Where(guard => guard.evalExpression(_))
                                    .DefaultIfEmpty((_ => true, delegateIfDefault))
                                    .FirstOrDefault()
                                    .delegateIfExpressionTrue(_)) :
                left(_left);

        [Pure]
        public Task<Either<L, B>> GuardBindAsync<B>(Func<R, Task<Either<L, B>>> delegateIfDefault, params (Func<R, bool> evalExpression, Func<R, Task<Either<L, B>>> delegateIfExpressionTrue)[] guards)
            => _isRight ?
                _right.Map(_ => guards
                                    .Where(guard => guard.evalExpression(_))
                                    .DefaultIfEmpty((_ => true, delegateIfDefault))
                                    .FirstOrDefault()
                                    .delegateIfExpressionTrue(_)) :
                Left<L, B>(_left).AsTask();

        [Pure]
        public Task<Either<A, B>> GuardBindAsync<A, B>(Func<L, Task<Either<A, B>>> left, Func<R, Task<Either<A, B>>> delegateIfDefault, params (Func<R, bool> evalExpression, Func<R, Task<Either<A, B>>> delegateIfExpressionTrue)[] guards)
            => _isRight ?
                _right.Map(_ => guards
                                    .Where(guard => guard.evalExpression(_))
                                    .DefaultIfEmpty((_ => true, delegateIfDefault))
                                    .FirstOrDefault()
                                    .delegateIfExpressionTrue(_)) :
                left(_left);
    }
}
