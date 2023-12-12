using System.Diagnostics.Contracts;

namespace TinyFp
{
    public partial struct Option<A>
    {

        [Pure]
        public Option<A> Filter(Predicate<A> predicate)
            => predicate == null ? this :
                                    IsNone ? this : 
                                            predicate(_value) ? this : None();
                                                
    }
}
