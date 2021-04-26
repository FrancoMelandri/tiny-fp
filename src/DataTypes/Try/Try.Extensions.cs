using System;
using System.Diagnostics.Contracts;
using TinyFp;
using TinyFp.Common;

public static class TryExtensions
{
    public static Try<A> Memo<A>(this Try<A> ma)
    {
        bool run = false;
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
}
