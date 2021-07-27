using System;
using System.Diagnostics.Contracts;
using TinyFp.Common;
using TinyFp.Extensions;

namespace TinyFp
{
    public static class TryExtensions
    {
        [Pure]
        private static Result<T> EncapsulateTry<T>(this Try<T> @this)
        {
            try
            {
                return @this();
            }
            catch (Exception e)
            {
                return new Result<T>(e);
            }
        }

        public static Try<A> Memo<A>(this Try<A> @this)
        {
            var isMemoized = false;
            var memoized = new Result<A>();
            return () =>
            {
                if (isMemoized) return memoized;

                var @try = @this.EncapsulateTry();
                if (@try.IsSuccess)
                {
                    isMemoized = true;
                    memoized = @try;
                }

                return @try;
            };
        }

        [Pure]
        public static R Match<A, R>(this Try<A> @this, Func<A, R> Succ, Func<Exception, R> Fail)
            => @this.EncapsulateTry()
                .Map(res => res.IsSuccess ?
                            Succ(res.Value) :
                            Fail(res.Exception));

        [Pure]
        public static R Match<A, R>(this Try<A> @this, Func<A, R> Succ, R Fail)
            => @this.EncapsulateTry()
                .Map(res => res.IsFaulted ?
                            Fail :
                            Succ(res.Value));

        [Pure]
        public static A OnFail<A>(this Try<A> @this, A failValue)
            => OnFail(@this, () => failValue);

        [Pure]
        public static A OnFail<A>(this Try<A> @this, Func<A> Fail)
            => OnFail(@this, _ => Fail());

        [Pure]
        public static A OnFail<A>(this Try<A> @this, Func<Exception, A> Fail)
            => @this.EncapsulateTry()
                .Map(res => res.IsSuccess ?
                            res.Value :
                            Fail(res.Exception));

        [Pure]
        public static Either<Exception, A> ToEither<A>(this Try<A> @this)
            => @this.EncapsulateTry()
                .Map(res => res.IsFaulted ?
                            Either<Exception, A>.Left(res.Exception) :
                            Either<Exception, A>.Right(res.Value));

        [Pure]
        public static Try<B> Bind<A, B>(this Try<A> @this, Func<A, Try<B>> f)
            => Memo(() =>
                {
                    try
                    {

                        var ra = @this.EncapsulateTry();
                        return ra.IsSuccess ?
                            f(ra.Value)() :
                            new Result<B>(ra.Exception);
                    }
                    catch (Exception e)
                    {
                        return new Result<B>(e);
                    }
                });

        [Pure]
        public static Try<B> Map<A, B>(this Try<A> @this, Func<A, B> f)
            => Memo(() => @this.EncapsulateTry()
                            .Map(_ => _.IsSuccess ?
                                        f(_.Value) :
                                        new Result<B>(_.Exception)));
    }
}