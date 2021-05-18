using System;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace TinyFp.Common
{
    public partial struct Result<A>
    {
        [Pure]
        public Result<B> Map<B>(Func<A, B> f)
            => IsFaulted
                ? new Result<B>(Exception)
                : new Result<B>(f(Value));

        [Pure]
        public async Task<Result<B>> MapAsync<B>(Func<A, Task<B>> f)
            => IsFaulted
                ? new Result<B>(Exception)
                : new Result<B>(await f(Value));
    }
}
