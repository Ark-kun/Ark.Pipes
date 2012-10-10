using System;
using Ark.Pipes;

namespace Ark.Pipes {
    public abstract class TriggerBase : ITrigger {
        public event Action Triggered;

        protected void SignalTriggered() {
            var handler = Triggered;
            if (handler != null)
                handler();
        }
    }

    public class ManualTrigger : TriggerBase {
        public void Trigger() {
            SignalTriggered();
        }
    }
}
