using System;
using System.Collections.Generic;

namespace Ark.Pipes {
    public static class SwitchableProvider {
        public static Provider<TValue> Create<TKey, TValue>(Provider<TKey> selector, Dictionary<TKey, Provider<TValue>> providers) {
            return new SwitchableProvider<TKey, TValue>(selector, providers);
        }

        public static Provider<TValue> Create<TValue>(Provider<int> selector, IList<Provider<TValue>> providers) {
            return new SwitchableProvider<TValue>(selector, providers);
        }

        public static Provider<TValue> Switch<TKey, TValue>(this Provider<TKey> selector, Dictionary<TKey, Provider<TValue>> providers) {
            return new SwitchableProvider<TKey, TValue>(selector, providers);
        }

        public static Provider<TValue> Switch<TValue>(this Provider<int> selector, IList<Provider<TValue>> providers) {
            return new SwitchableProvider<TValue>(selector, providers);
        }
    }

    sealed class SwitchableProvider<TKey, TValue> : ProviderHolder<TValue>, IValueChangeListener {
        Provider<TKey> _selector;
        TKey _currentKey;
        IDictionary<TKey, Provider<TValue>> _providers;

        public SwitchableProvider(Provider<TKey> selector)
            : base(Constant<TValue>.Default) {
            _selector = selector;
            _providers = new Dictionary<TKey, Provider<TValue>>();
        }

        public SwitchableProvider(Provider<TKey> selector, Dictionary<TKey, Provider<TValue>> providers)
            : base(providers[selector]) {
            _selector = selector;
            _currentKey = _selector;
            _providers = providers;
            _selector.Notifier.AddListener(this);
        }

        void OnSelectorChanged() {
            var key = _selector.Value;
            Provider<TValue> provider;
            //if (key != _currentKey) {
            if (_providers.TryGetValue(key, out provider)) {
                _currentKey = key;
                SetProvider(provider);
            } else {
                //throw new InvalidOperationException("SwitchableProvider was switched to a non-existent provider.");            
            }
            //}
        }

        void IValueChangeListener.OnValueChanged() {
            OnSelectorChanged();
        }
    }

    sealed class SwitchableProvider<TValue> : ProviderHolder<TValue>, IValueChangeListener {
        Provider<int> _selector;
        int _currentKey;
        IList<Provider<TValue>> _providers;

        public SwitchableProvider(Provider<int> selector, int count)
            : base(Constant<TValue>.Default) {
            _selector = selector;
            _providers = new Provider<TValue>[count];
        }

        public SwitchableProvider(Provider<int> selector, IList<Provider<TValue>> providers)
            : base(providers[selector]) {
            _selector = selector;
            _currentKey = _selector;
            _providers = providers;
            _selector.Notifier.AddListener(this);
        }

        void OnSelectorChanged() {
            var key = _selector.Value;            
            //if (key != _currentKey) {
            if (key >= 0 && key < _providers.Count) {
                _currentKey = key;
                var provider = _providers[_currentKey];
                SetProvider(provider);
            } else {
                //throw new InvalidOperationException("SwitchableProvider was switched to a non-existent provider.");
            }
            //}
        }

        void IValueChangeListener.OnValueChanged() {
            OnSelectorChanged();
        }
    }
}
