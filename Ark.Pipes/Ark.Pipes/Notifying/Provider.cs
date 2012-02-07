using System;

namespace Ark.Pipes {
   public abstract class NotifyingProvider<T> : Provider<T>, INotifyingOut<T> {
        public event Action ValueChanged;

        protected void OnValueChanged() {
            if (ValueChanged != null) {
                ValueChanged();
            }
        }
    }
}