using System;
using System.Diagnostics.Contracts;
using TinyFp.Common;
using System.Threading.Tasks;

namespace TinyFp
{
    public static class TryAsyncExtensions
    {
        [Pure]
        public static async Task<Result<T>> Try<T>(this TryAsync<T> self)
        {
            try
            {
                if (self == null)
                {
                    throw new ArgumentNullException(nameof(self));
                }
                return await self();
            }
            catch (Exception e)
            {
                return new Result<T>(e);
            }
        }

        [Pure]
        public static async Task<R> Match<A, R>(this TryAsync<A> self, Func<A, R> Succ, Func<Exception, R> Fail)
        {
            var res = await self.Try();
            return res.IsFaulted
                ? Fail(res.Exception)
                : Succ(res.Value);
        }

        [Pure]
        public static async Task<R> Match<A, R>(this TryAsync<A> self, Func<A, Task<R>> Succ, Func<Exception, R> Fail)
        {
            var res = await self.Try();
            return res.IsFaulted
                ? Fail(res.Exception)
                : await Succ(res.Value);
        }

        [Pure]
        public static async Task<R> Match<A, R>(this TryAsync<A> self, Func<A, R> Succ, Func<Exception, Task<R>> Fail)
        {
            var res = await self.Try();
            return res.IsFaulted
                ? await Fail(res.Exception)
                : Succ(res.Value);
        }

        [Pure]
        public static async Task<R> Match<A, R>(this TryAsync<A> self, Func<A, Task<R>> Succ, Func<Exception, Task<R>> Fail)
        {
            var res = await self.Try();
            return res.IsFaulted
                ? await Fail(res.Exception)
                : await Succ(res.Value);
        }

        [Pure]
        public static async Task<R> Match<A, R>(this TryAsync<A> self, Func<A, R> Succ, R Fail)
        {
            var res = await self.Try();
            return res.IsFaulted
                ? Fail
                : Succ(res.Value);
        }

        [Pure]
        public static async Task<R> Match<A, R>(this TryAsync<A> self, Func<A, Task<R>> Succ, R Fail)
        {
            var res = await self.Try();
            return res.IsFaulted
                ? Fail
                : await Succ(res.Value);
        }
    }
}
