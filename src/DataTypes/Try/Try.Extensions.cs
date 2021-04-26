using System;
using System.Diagnostics.Contracts;
using TinyFp.Common;

namespace TinyFp
{
    public static class TryExtensions
    {
        public static Try<A> Memo<A>(this Try<A> ma)
        {
            var run = false;
            var result = new Result<A>();
            return () =>
            {
                if (run) return result;
                var ra = ma.Try();
                if (result.IsSuccess)
                {
                    run = true;
                    result = ra;
                }
                return ra;
            };
        }

        [Pure]
        public static Result<T> Try<T>(this Try<T> self)
        {
            try
            {
                if (self == null)
                {
                    throw new ArgumentNullException(nameof(self));
                }
                return self();
            }
            catch (Exception e)
            {
                return new Result<T>(e);
            }
        }

        [Pure]
        public static R Match<A, R>(this Try<A> self, Func<A, R> Succ, Func<Exception, R> Fail)
        {
            var res = self.Try();
            return res.IsSuccess ?
                Succ(res.Value) :
                Fail(res.Exception);
        }

        [Pure]
        public static R Match<A, R>(this Try<A> self, Func<A, R> Succ, R Fail)
        {
            var res = self.Try();
            return res.IsFaulted ?
                Fail :
                Succ(res.Value);
        }

        [Pure]
        public static A OnFail<A>(this Try<A> self, A failValue)
            => OnFail(self, () => failValue);

        [Pure]
        public static A OnFail<A>(this Try<A> self, Func<A> Fail)
        {
            var res = self.Try();
            return res.IsSuccess ?
                res.Value :
                Fail();
        }

        [Pure]
        public static Either<Exception, A> ToEither<A>(this Try<A> self)
        {
            var res = self.Try();
            return res.IsFaulted ?
                Either<Exception, A>.Left(res.Exception) :
                Either<Exception, A>.Right(res.Value);
        }

        [Pure]
        public static Try<B> Bind<A, B>(this Try<A> ma, Func<A, Try<B>> f)
            => Memo(() =>
                {
                    try
                    {
                        var ra = ma.Try();
                        return ra.IsSuccess ?
                            f(ra.Value)() :
                            new Result<B>(ra.Exception);
                    }
                    catch (Exception e)
                    {
                        return new Result<B>(e);
                    }
                });

        public static Try<A> Do<A>(this Try<A> ma, Action<A> f) => () =>
        {
            var r = ma.Try();
            if (!r.IsFaulted)
            {
                f(r.Value);
            }
            return r;
        };
    }
}