using System;

namespace TinyFp
{
    public class DiscriminatedUnion<T1, T2>
    {
        private readonly short _type;
        private readonly T1 _t1;
        private readonly T2 _t2;

        public bool IsT1 => _type == 1;
        public bool IsT2 => _type == 2;

        private DiscriminatedUnion(T1 t1)
        {
            if (t1 is null) throw new ArgumentNullException(nameof(t1));
            _type = 1;
            _t1 = t1;
        }

        private DiscriminatedUnion(T2 t2)
        {
            if (t2 is null) throw new ArgumentNullException(nameof(t2));
            _type = 2;
            _t2 = t2;
        }

        public static DiscriminatedUnion<T1, T2> FromT1(T1 t1) => new DiscriminatedUnion<T1, T2>(t1);
        public static DiscriminatedUnion<T1, T2> FromT2(T2 t2) => new DiscriminatedUnion<T1, T2>(t2);

        public Y Match<Y>(Func<T1, Y> mapT1, Func<T2, Y> mapT2)
            => IsT1 ? mapT1(_t1) :
               IsT2 ? mapT2(_t2) :
               throw new Exception();

        public static implicit operator DiscriminatedUnion<T1, T2> (T1 t1) => FromT1(t1);
        public static implicit operator DiscriminatedUnion<T1, T2> (T2 t2) => FromT2(t2);
    }
}