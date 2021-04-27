using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TinyFp.Common;

namespace TinyFp
{
    public static partial class Prelude
    {
        [Pure]
        public static TryAsync<A> TryAsync<A>(Func<Task<A>> f) 
            => new TryAsync<A>(async () => new Result<A>(await f()));

        [Pure]
        public static TryAsync<A> TryAsync<A>(A v) =>
             () => new Result<A>(v).AsTask();
    }
}