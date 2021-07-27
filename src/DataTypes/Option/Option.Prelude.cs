namespace TinyFp
{
    public static partial class Prelude
    {
        public static Option<A> Some<A>(A some)
            => Option<A>.Some(some);

        public static Option<A> None<A>()
            => Option<A>.None();
    }
}
