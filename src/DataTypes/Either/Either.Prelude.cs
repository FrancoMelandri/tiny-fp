namespace TinyFp
{
    public static partial class Prelude
    {
        public static Either<L, R> Right<L, R>(R right)
            => Either<L, R>.Right(right);

        public static Either<L, R> Left<L, R>(L left)
            => Either<L, R>.Left(left);
    }
}
