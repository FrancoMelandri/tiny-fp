using TinyFp.Extensions;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
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
    }
}
