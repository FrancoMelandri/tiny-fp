using System;

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
            => new Option<A>(false, default);

        public static Option<A> Some(A value) 
            => new Option<A>(true, value);

        public Option<A> OnSome(Action<A> action)
        {
            if (_isSome) action(_value);
            return this;
        }

        public Option<A> OnNone(Action action)
        {
            if (!_isSome) action();
            return this;
        }

        public A OnNone(Func<A> func)
            => _isSome ? _value : func();

        public A OnNone(A val)
            => _isSome ? _value : val;

        public Option<B> Map<B>(Func<A, B> map)
            => _isSome ? Option<B>.Some(map(_value)) : Option<B>.None();

        public Option<B> Bind<B>(Func<A, Option<B>> bind)
            => _isSome ? bind(_value) : Option<B>.None();

        public B Match<B>(Func<A, B> onSome, Func<B> onNone)
            => _isSome ? onSome(_value) : onNone();
    }
}