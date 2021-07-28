using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace TinyFp
{
    public class Union<T1, T2, T3>
    {
        private readonly short _type;
        private readonly T1 _t1;
        private readonly T2 _t2;
        private readonly T3 _t3;

        public bool IsT1 => _type == 1;
        public bool IsT2 => _type == 2;
        public bool IsT3 => _type == 3;

        private Union(T1 t1)
        {
            _type = 1;
            _t1 = t1;
        }

        private Union(T2 t2)
        {
            _type = 2;
            _t2 = t2;
        }

        private Union(T3 t3)
        {
            _type = 3;
            _t3 = t3;
        }

        public static Union<T1, T2, T3> FromT1(T1 t1) => new Union<T1, T2, T3>(t1);
        public static Union<T1, T2, T3> FromT2(T2 t2) => new Union<T1, T2, T3>(t2);
        public static Union<T1, T2, T3> FromT3(T3 t3) => new Union<T1, T2, T3>(t3);

        [Pure]
        public M Match<M>(Func<T1, M> onT1, Func<T2, M> OnT2, Func<T3, M> OnT3)
            => _type switch
            {
                1 => onT1(_t1),
                2 => OnT2(_t2),
                _ => OnT3(_t3)
            };

        [Pure]
        public Task<M> MatchAsync<M>(Func<T1, Task<M>> onT1Async, 
                                     Func<T2, Task<M>> onT2Async, 
                                     Func<T3, Task<M>> onT3Async)
            => _type switch
            {
                1 => onT1Async(_t1),
                2 => onT2Async(_t2),
                _ => onT3Async(_t3)
            };

        public static implicit operator Union<T1, T2, T3>(T1 t1) => FromT1(t1);
        public static implicit operator Union<T1, T2, T3>(T2 t2) => FromT2(t2);
        public static implicit operator Union<T1, T2, T3>(T3 t3) => FromT3(t3);
    }
}
