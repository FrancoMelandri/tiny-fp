using System;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using TinyFp.Exceptions;

namespace TinyFp.Common
{

    public struct Result<A>
    {
        public readonly static Result<A> Bottom = default(Result<A>);

        private readonly ResultState State;
        private readonly A Value;
        private Exception exception;

        private Exception Exception => exception ?? BottomException.Default;

        [Pure]
        public Result(A value)
        {
            State = ResultState.Success;
            Value = value;
            exception = default;
        }

        [Pure]
        public Result(Exception e)
        {
            State = ResultState.Faulted;
            exception = e;
            Value = default(A);
        }

        [Pure]
        public bool IsFaulted 
            => State == ResultState.Faulted;

        [Pure]
        public bool IsBottom 
            => State == ResultState.Faulted && (exception == null || exception is BottomException);

        [Pure]
        public bool IsSuccess 
            => State == ResultState.Success;

        [Pure]
        public R Match<R>(Func<A, R> succ, Func<Exception, R> fail) 
            => IsFaulted
                ? fail(Exception)
                : succ(Value);

        [Pure]
        public Task<R> MatchAsync<R>(Func<A, Task<R>> succAsync, Func<Exception, Task<R>> failAsync) 
            => IsFaulted
                ? failAsync(Exception)
                : succAsync(Value);

        [Pure]
        public Result<B> Map<B>(Func<A, B> f) 
            => IsFaulted
                ? new Result<B>(Exception)
                : new Result<B>(f(Value));

        [Pure]
        public async Task<Result<B>> MapAsync<B>(Func<A, Task<B>> f) 
            => IsFaulted
                ? new Result<B>(Exception)
                : new Result<B>(await f(Value));

        [Pure]
        public A OnFail(A defaultValue) 
            => IsFaulted
                ? defaultValue
                : Value;

        [Pure]
        public A OnFail(Func<Exception, A> f) 
            => IsFaulted
                ? f(Exception)
                : Value;
    }
}
