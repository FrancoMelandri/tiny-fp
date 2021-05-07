using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TinyFp
{
    public struct Option<A>
    {
        private readonly bool _isSome;
        private readonly A _value;

        public bool IsSome 
            => _isSome;

        public bool IsNone 
            => !_isSome;

        private Option(bool isSome, A value)
        {
            _isSome = isSome;
            _value = value;
        }

        public static Option<A> None()
            => new(false, default);

        public static Option<A> Some(A value) 
            => new(true, value);

        [Pure]
        public Option<A> OnSome(Action<A> action)
        {
            if (_isSome) action(_value);
            return this;
        }

        [Pure]
        public Option<A> OnNone(Action action)
        {
            if (!_isSome) action();
            return this;
        }

        [Pure]
        public A OnNone(Func<A> func)
            => _isSome ? _value : func();

        [Pure]
        public A OnNone(A val)
            => _isSome ? _value : val;

        [Pure]
        public Option<B> Map<B>(Func<A, B> map)
            => _isSome ? Option<B>.Some(map(_value)) : 
                         Option<B>.None();

        [Pure]
        public async Task<Option<B>> MapAsync<B>(Func<A, Task<B>> mapAsync)
            => _isSome ? Option<B>.Some(await mapAsync(_value)) : 
                         Option<B>.None();

        [Pure]
        public Option<B> Bind<B>(Func<A, Option<B>> bind)
            => _isSome ? bind(_value) : Option<B>.None();

        [Pure]
        public async Task<Option<B>> BindAsync<B>(Func<A, Task<Option<B>>> bindAsync)
            => _isSome ? await bindAsync(_value) : Option<B>.None();

        [Pure]
        public B Match<B>(Func<A, B> onSome, Func<B> onNone)
            => _isSome ? onSome(_value) : onNone();

        [Pure]
        public Task<B> MatchAsync<B>(Func<A, Task<B>> onSome, Func<Task<B>> onNone)
            => _isSome ? onSome(_value) : onNone();

        [Pure]
        public Task<B> MatchAsync<B>(Func<A, B> onSome, Func<Task<B>> onNone)
            => _isSome ? Task.FromResult(onSome(_value)) : onNone();

        [Pure]
        public Task<B> MatchAsync<B>(Func<A, Task<B>> onSome, Func<B> onNone)
            => _isSome ? onSome(_value) : Task.FromResult(onNone());

        [Pure]
        public Either<L, A> ToEither<L>(Func<L> onLeft)
            =>  _isSome ?
                    Either<L, A>.Right(_value) :
                    Either<L, A>.Left(onLeft());

        [Pure]
        public Either<L, A> ToEither<L>(L left)
            =>  ToEither(() => left);
    }
}