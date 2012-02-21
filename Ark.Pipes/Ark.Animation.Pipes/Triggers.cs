﻿using System;
using Ark.Pipes;

namespace Ark.Animation {
    public abstract class TriggerBase : ITrigger {
        public event Action Triggered;

        protected void OnTriggered() {
            var handler = Triggered;
            if (handler != null)
                handler();
        }
    }

    public class ManualTrigger : TriggerBase {
        public void Trigger() {
            OnTriggered();
        }
    }
}