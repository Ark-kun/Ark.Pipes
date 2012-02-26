using System;
using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

namespace Ark.Animation {
    public class Timer : Provider<TFloat> {
        DateTime _startTime;

        public Timer() {
            _startTime = DateTime.UtcNow;
        }

        public Timer(DateTime startTime) {
            _startTime = startTime;
        }

        public override TFloat GetValue() {
            return (TFloat)((DateTime.UtcNow - _startTime).TotalMilliseconds);
        }
    }
}