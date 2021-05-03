using TinyFp.DataTypes;

namespace TinyFp
{
    public static partial class Prelude
    {
        public static Validation<FAIL, SUCCESS> Success<FAIL, SUCCESS>(SUCCESS success)
            => Validation<FAIL, SUCCESS>.Success(success);

        public static Validation<FAIL, SUCCESS> Fail<FAIL, SUCCESS>(FAIL fail)
            => Validation<FAIL, SUCCESS>.Fail(fail);
    }
}
