using System;

namespace Ark.Animation {
    public abstract class Clock {
        public event Action Tick;

        protected void OnTick() {
            var handler = Tick;
            if (handler != null)
                handler();
        }
    }

    public class ManualClock : Clock {
        public void DoTick() {
            OnTick();
        }
    }
}
