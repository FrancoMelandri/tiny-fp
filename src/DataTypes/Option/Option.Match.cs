using System.Diagnostics.Contracts;

namespace TinyFp
{
    public partial struct Option<A>
    {
        [Pure]
        public B Match<B>(Func<A, B> onSome, Func<B> onNone)
            => _isSome ? onSome(_value) : onNone();

        [Pure]
        public Unit Match(Action<A> onSome, Action onNone)
        {
            if (_isSome) onSome(_value); else onNone();
            return Unit.Default;
        }

        [Pure]
        public Task<B> MatchAsync<B>(Func<A, Task<B>> onSome, Func<Task<B>> onNone)
            => _isSome ? onSome(_value) : onNone();

        [Pure]
        public Task<B> MatchAsync<B>(Func<A, B> onSome, Func<Task<B>> onNone)
            => _isSome ? onSome(_value).AsTask() : onNone();

        [Pure]
        public Task<B> MatchAsync<B>(Func<A, Task<B>> onSome, Func<B> onNone)
            => _isSome ? onSome(_value) : onNone().AsTask();
    }
}
