using System.Diagnostics.Contracts;
using TinyFp.Common;

namespace TinyFp;

public static partial class Prelude
{
    [Pure]
    public static TryAsync<A> TryAsync<A>(Func<Task<A>> f) 
        => new TryAsync<A>(async () => new Result<A>(await f()));
}