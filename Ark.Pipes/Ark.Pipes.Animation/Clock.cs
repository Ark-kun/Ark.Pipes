using System;

namespace Ark.Pipes.Animation {
    public abstract class Clock {
        public event Action Tick;

        protected void OnTick() {
            if (Tick != null)
                Tick();
        }
    }

    public class ManualClock : Clock {
        public void DoTick() {
            OnTick();
        }
    }
}
