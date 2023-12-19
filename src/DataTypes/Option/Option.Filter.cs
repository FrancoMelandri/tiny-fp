using System.Diagnostics.Contracts;

namespace TinyFp;

public readonly partial struct Option<A>
{
    [Pure]
    public Option<A> Filter(Predicate<A> predicate)
        => (predicate, this) switch {
            ( Predicate<A> p, Option<A> { IsSome: true } t ) => p(t._value) ? t : None(),
            _ => this
        };
                                                
}