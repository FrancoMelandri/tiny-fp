using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TinyFp.Exceptions;

namespace TinyFp
{
    public partial struct Validation<FAIL, SUCCESS>
    {
        private readonly FAIL _fail;
        private readonly SUCCESS _success;
        private readonly ValidationStateType _state;

        private Validation(SUCCESS success)
        {
            _success = success;
            _fail = default;
            _state = ValidationStateType.Success;
        }

        private Validation(FAIL fail)
        {
            _success = default;
            _fail = fail;
            _state = ValidationStateType.Fail;
        }

        [Pure]
        public static Validation<FAIL, SUCCESS> Success(SUCCESS success) 
            => new(success);

        [Pure]
        public static Validation<FAIL, SUCCESS> Fail(FAIL fail) 
            => new(fail);

        [Pure]
        public bool IsFail 
            => _state == ValidationStateType.Fail;

        [Pure]
        public bool IsSuccess 
            => _state == ValidationStateType.Success;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Validation<FAIL, SUCCESS>(SUCCESS value)
            => value == null
                ? throw new ValueIsNullException()
                : Success(value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Validation<FAIL, SUCCESS>(FAIL value)
            => value == null
                ? throw new ValueIsNullException()
                : Fail(value);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator true(Validation<FAIL, SUCCESS> value)
            => value.IsSuccess;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [ExcludeFromCodeCoverage]
        public static bool operator false(Validation<FAIL, SUCCESS> value)
            => value.IsFail;

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !(Validation<FAIL, SUCCESS> value)
            => value.IsFail;
    }
}
