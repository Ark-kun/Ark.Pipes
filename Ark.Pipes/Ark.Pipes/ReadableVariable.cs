using System;
namespace Ark.Pipes {
    public class ReadableVariable<T> :
#if NOTIFICATIONS_DISABLE
        Provider<T>
#else
        ProviderWithNotifier<T>
#endif
    {
        protected T _value;

        protected ReadableVariable()
            : this(default(T)) {
        }

        protected ReadableVariable(T value) {
            _value = value;
#if !NOTIFICATIONS_DISABLE
            _notifier.SetReliability(true);
#endif
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
#if !NOTIFICATIONS_DISABLE
            _notifier.SignalValueChanged();
#endif
        }
    }
}