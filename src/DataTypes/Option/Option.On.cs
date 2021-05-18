using System;
using System.Diagnostics.Contracts;
using TinyFp.Extensions;

namespace TinyFp
{
    public partial struct Option<A>
    {
        [Pure]
        public Option<A> OnSome(Action<A> action)
            => this
                .Tee(@this => { if (@this._isSome) action(@this._value); });

        [Pure]
        public Option<A> OnNone(Action action)
            => this
                .Tee(@this => { if (!@this._isSome) action(); });

        [Pure]
        public A OnNone(Func<A> func)
            => _isSome ? _value : func();

        [Pure]
        public A OnNone(A val)
            => _isSome ? _value : val;
    }
}
