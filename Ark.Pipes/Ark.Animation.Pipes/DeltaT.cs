using System;
using Ark.Abstract;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

namespace Ark.Animation {
    public struct DeltaT : IDelta<TFloat>, IMultiplicative<TFloat, DeltaT> {
        TFloat _value;

        private DeltaT(TFloat dt) {
            //if (dt < 0) {
            //    throw new ArgumentOutOfRangeException();
            //}
            _value = dt;
        }

        public static implicit operator TFloat(DeltaT dt) {
            return dt._value;
        }

        public static implicit operator DeltaT(TFloat dt) {
            return new DeltaT(dt);
        }

        public void ConstructFromDifference(ref TFloat value1, ref TFloat value2) {
            _value = value1 - value2;
        }

        public TFloat Plus(ref TFloat value) {
            return value + _value;
        }

        public DeltaT MultipliedBy(ref TFloat multiplier) {
            return _value * multiplier;
        }
    }
}