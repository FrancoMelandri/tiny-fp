using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TinyFp.Exceptions;

namespace TinyFp.DataTypes
{
    public struct Validation<FAIL, SUCCESS>        
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
        public static implicit operator Validation<FAIL, SUCCESS>(SUCCESS value)
            => value == null
                ? throw new ValueIsNullException()
                : Success(value);

        [Pure]
        public static implicit operator Validation<FAIL, SUCCESS>(FAIL value)
            => value == null
                ? throw new ValueIsNullException()
                : Fail(value);

        [Pure]
        public R Match<R>(Func<SUCCESS, R> onSucc, Func<FAIL, R> onFail) 
            => IsSuccess ? onSucc(_success) : onFail(_fail);

        [Pure]
        public R Match<R>(R Succ, Func<FAIL, R> onFail) 
            => IsSuccess ? Succ : onFail(_fail);

        [Pure]
        public R Match<R>(Func<SUCCESS, R> onSucc, R Fail)
            => IsSuccess ? onSucc(_success) : Fail;

        [Pure]
        public R Match<R>(R Succ, R Fail) 
            => IsSuccess ? Succ : Fail;

        [Pure]
        public Task<R> MatchAsync<R>(Func<SUCCESS, Task<R>> onSucc, Func<FAIL, Task<R>> onFail)
            => IsSuccess ? onSucc(_success) : onFail(_fail);

        [Pure]
        public Task<R> MatchAsync<R>(Func<SUCCESS, Task<R>> onSucc, Task<R> Fail)
            => IsSuccess ? onSucc(_success) : Fail;

        [Pure]
        public Task<R> MatchAsync<R>(Task<R> Succ, Func<FAIL, Task<R>> onFail)
            => IsSuccess ? Succ : onFail(_fail);

        [Pure]
        public Task<R> MatchAsync<R>(Task<R> Succ, Task<R> Fail)
            => IsSuccess ? Succ : Fail;
    }
}
