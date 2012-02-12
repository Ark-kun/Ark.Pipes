using System;
namespace Ark.Pipes {
    public class ReadableVariable<T> : NotifyingProvider<T> {
        protected T _value;

        protected ReadableVariable()
            : this(default(T)) {
        }

        protected ReadableVariable(T value) {
            _value = value;
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
            OnValueChanged();
        }
    }
}