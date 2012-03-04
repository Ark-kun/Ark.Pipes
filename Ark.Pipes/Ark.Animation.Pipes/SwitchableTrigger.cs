using System;
using Ark.Pipes;

namespace Ark.Animation {
    public sealed class SwitchableTrigger : TriggerBase {
        Provider<bool> _switcher;

        public SwitchableTrigger(Provider<bool> switcher, ITrigger trigger) {
            _switcher = switcher;
            trigger.Triggered += OnTriggerTriggered;
        }

        void OnTriggerTriggered() {
            if (_switcher.Value) {
                OnTriggered();
            }
        }
    }
}