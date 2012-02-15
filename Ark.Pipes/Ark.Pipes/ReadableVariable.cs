using System;
namespace Ark.Pipes {
    public class ReadableVariable<T> : ProviderWithNotifier<T> {
        protected T _value;

        protected ReadableVariable()
            : this(default(T)) {
        }

        protected ReadableVariable(T value) {
            _value = value;
            _notifier.SetReliability(true);
        }

        public ReadableVariable(T value, out Action<T> changer) {
            _value = value;
            changer = SetValue;
        }

        public override T GetValue() {
            return _value;
        }

        protected void SetValue(T value) {
            _value = value;
            _notifier.OnValueChanged();
        }
    }
}