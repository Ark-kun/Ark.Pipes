using System;
using Ark.Pipes;

namespace Ark.Xna {
    public class Timer : Provider<float> {
        DateTime _startTime;

        public Timer() {
            _startTime = DateTime.UtcNow;
        }

        public Timer(DateTime startTime) {
            _startTime = startTime;
        }

        public override float GetValue() {
            return (float)((DateTime.UtcNow - _startTime).TotalMilliseconds);
        }
    }
}