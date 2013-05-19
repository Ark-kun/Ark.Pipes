using System;

namespace Ark.Pipes {
    //ProviderHolder<T> is different from ReadableVariable<Provider<T>> because it inherits from Provider<T>, not Provider<Provider<T>>
    public class ProviderHolder<T> : Provider<T> {
        protected Provider<T> _provider;
#if !NOTIFICATIONS_DISABLE
        protected PrivateNotifier _notifier = new PrivateNotifier();
        public event Action ProviderChanged;
#endif

        public ProviderHolder(Provider<T> provider) {
            _provider = provider;
#if !NOTIFICATIONS_DISABLE
            _notifier.Source = _provider.Notifier;
#endif
        }

        public ProviderHolder(Provider<T> provider, out Action<Provider<T>> changer)
            : this(provider) {
            changer = SetProvider;
        }

        protected void SetProvider(Provider<T> value) {
            if (value != _provider) {
                _provider = value;
#if !NOTIFICATIONS_DISABLE
                _notifier.Source = _provider.Notifier;
                SignalProviderChanged();
                _notifier.SignalValueChanged();
#endif
            }
        }

#if !NOTIFICATIONS_DISABLE
        void SignalProviderChanged() {
            var handler = ProviderChanged;
            if (handler != null) {
                handler();
            }
        }
#endif

        public override T GetValue() {
            return _provider.GetValue();
        }
    }
}
