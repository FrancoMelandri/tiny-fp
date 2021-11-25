using System.Diagnostics.Contracts;

namespace TinyFp
{
    public class Union<T1, T2>
    {
        private readonly short _type;
        private readonly T1 _t1;
        private readonly T2 _t2;

        public bool IsT1 => _type == 1;
        public bool IsT2 => _type == 2;

        private Union(T1 t1)
        {
            if (t1 is null) throw new ArgumentNullException(nameof(t1));
            _type = 1;
            _t1 = t1;
        }

        private Union(T2 t2)
        {
            if (t2 is null) throw new ArgumentNullException(nameof(t2));
            _type = 2;
            _t2 = t2;
        }

        public static Union<T1, T2> FromT1(T1 t1) => new Union<T1, T2>(t1);

        public static Union<T1, T2> FromT2(T2 t2) => new Union<T1, T2>(t2);

        [Pure]
        public M Match<M>(Func<T1, M> onT1, Func<T2, M> OnT2)
            => IsT1 ? onT1(_t1) : OnT2(_t2);

        [Pure]
        public Task<M> MatchAsync<M>(Func<T1, Task<M>> onT1Async, Func<T2, Task<M>> onT2Async)
            => IsT1 ? onT1Async(_t1) : onT2Async(_t2);

        public static implicit operator Union<T1, T2> (T1 t1) => FromT1(t1);
        public static implicit operator Union<T1, T2> (T2 t2) => FromT2(t2);
    }
}