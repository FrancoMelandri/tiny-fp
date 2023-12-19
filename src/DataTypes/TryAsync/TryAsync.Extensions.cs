using System.Diagnostics.Contracts;
using TinyFp.Common;

namespace TinyFp;

public static class TryAsyncExtensions
{
    [Pure]
    public static async Task<Result<T>> Try<T>(this TryAsync<T> @this)
    {
        try
        {
            return await @this();
        }
        catch (Exception e)
        {
            return new Result<T>(e);
        }
    }

    public static TryAsync<A> Memo<A>(this TryAsync<A> @this)
    {
        var isMemoized = false;
        var memoized = new Result<A>();
        return new TryAsync<A>(async () =>
            {
                if (isMemoized) return memoized;

                var @try = await  @this.Try();
                if (@try.IsSuccess)
                {
                    isMemoized = true;
                    memoized = @try;
                }

                return @try;
            }
        );
    }

    [Pure]
    public static async Task<R> Match<A, R>(this TryAsync<A> @this, Func<A, R> Succ, Func<Exception, R> Fail)
    {
        var res = await @this.Try();
        return res.IsFaulted
            ? Fail(res.Exception)
            : Succ(res.Value);
    }

    [Pure]
    public static async Task<R> Match<A, R>(this TryAsync<A> @this, Func<A, Task<R>> Succ, Func<Exception, R> Fail)
    {
        var res = await @this.Try();
        return res.IsFaulted
            ? Fail(res.Exception)
            : await Succ(res.Value);
    }

    [Pure]
    public static async Task<R> Match<A, R>(this TryAsync<A> @this, Func<A, R> Succ, Func<Exception, Task<R>> Fail)
    {
        var res = await @this.Try();
        return res.IsFaulted
            ? await Fail(res.Exception)
            : Succ(res.Value);
    }

    [Pure]
    public static async Task<R> Match<A, R>(this TryAsync<A> @this, Func<A, Task<R>> Succ, Func<Exception, Task<R>> Fail)
    {
        var res = await @this.Try();
        return res.IsFaulted
            ? await Fail(res.Exception)
            : await Succ(res.Value);
    }

    [Pure]
    public static async Task<R> Match<A, R>(this TryAsync<A> @this, Func<A, R> Succ, R Fail)
    {
        var res = await @this.Try();
        return res.IsFaulted
            ? Fail
            : Succ(res.Value);
    }

    [Pure]
    public static async Task<R> Match<A, R>(this TryAsync<A> @this, Func<A, Task<R>> Succ, R Fail)
    {
        var res = await @this.Try();
        return res.IsFaulted
            ? Fail
            : await Succ(res.Value);
    }

    [Pure]
    public static async Task<A> OnFail<A>(this TryAsync<A> @this, A failValue)
    {
        var res = await @this.Try();
        return res.IsSuccess ?
            res.Value :
            failValue;
    }

    [Pure]
    public static async Task<A> OnFail<A>(this TryAsync<A> @this, Func<Task<A>> Fail)
        => await @this.OnFail(await Fail());

    [Pure]
    public static async Task<A> OnFail<A>(this TryAsync<A> @this, Func<A> Fail)
        => await @this.OnFail(Fail());

    [Pure]
    public static async Task<A> OnFail<A>(this TryAsync<A> @this, Func<Exception, A> Fail)
    {
        var res = await @this.Try();
        return res.IsSuccess ?
            res.Value :
            Fail(res.Exception);
    }

    [Pure]
    public static async Task<A> OnFail<A>(this TryAsync<A> @this, Func<Exception, Task<A>> Fail)
    {
        var res = await @this.Try();
        return res.IsSuccess ?
            res.Value :
            await Fail(res.Exception);
    }

    [Pure]
    public static async Task<Either<Exception, A>> ToEither<A>(this TryAsync<A> @this)
    {
        var res = await @this.Try();
        return res.IsFaulted ?
            Either<Exception, A>.Left(res.Exception) :
            Either<Exception, A>.Right(res.Value);
    }

    [Pure]
    public static TryAsync<B> Bind<A, B>(this TryAsync<A> @this, Func<A, TryAsync<B>> f) 
        => Memo(async () =>
        {
            try
            {
                var ra = await @this();
                return ra.IsSuccess ?
                    await f(ra.Value)() :
                    new Result<B>(ra.Exception);                        
            }
            catch (Exception e)
            {
                return new Result<B>(e);
            }
        });

    public static TryAsync<A> Do<A>(this TryAsync<A> @this, Action<A> f) => new TryAsync<A>(async () =>
    {
        var r = await @this.Try();
        if (!r.IsFaulted)
        {
            f(r.Value);
        }
        return r;
    });
}