using System;
using System.Diagnostics.Contracts;

namespace TinyFp
{
    public static partial class Prelude
    {
        [Pure]
        public static Try<A> Try<A>(Func<A> f) =>
            () => f();

        [Pure]
        public static Try<A> Try<A>(A v) =>
             () => v;
    }
}
