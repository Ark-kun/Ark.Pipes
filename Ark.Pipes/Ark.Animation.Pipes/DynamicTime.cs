using System;
using Ark.Animation;
using Ark.Pipes;
using Ark.Geometry;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

namespace Ark.Animation { //.Pipes
    public static class TimeExtensions {
        public static Provider<TFloat> Accelerate(this Provider<TFloat> ts, TFloat multiplier) {
            TFloat t0 = ts.Value;
            return Provider.Create((t) => t0 + (t - t0) * multiplier, ts);
        }

        public static Provider<TFloat> Reset(this Provider<TFloat> timer) {
            TFloat t0 = timer.Value;
            return Provider.Create((t) => t - t0, timer);
        }

        public static Provider<DeltaT> ToDeltaTs(this Provider<TFloat> timer) {
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                TFloat oldTime = time;
                time = newTime;
                return (DeltaT)(newTime - oldTime);
            }, timer);
        }

        public static Provider<Tuple<TFloat, DeltaT>> AddDeltaTs(this Provider<TFloat> timer) {
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                TFloat oldTime = time;
                time = newTime;
                return new Tuple<TFloat, DeltaT>(newTime, newTime - oldTime);
            }, timer);
        }
    }
}

