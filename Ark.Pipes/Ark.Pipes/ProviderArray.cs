using System;
using System.Collections.Generic;
using System.Linq;


namespace Ark.Pipes {
    //TODO:Fix event unsubscribing
    //public class PropertyArray<T> : Provider<T[]>, IIn<T[]>, IIn<Provider<T>[]>, IOut<Provider<T>[]>, INotifyProviderChanged {
    public class ProviderArrayBase<T> : Provider<T[]>,
#if NOTIFICATIONS_DISABLE
        IOut<Provider<T>[]>
#else
        INotifyingOut<Provider<T>[]>, INotifyElementChanged
#endif
    {
        protected Property<T>[] _properties;
#if !NOTIFICATIONS_DISABLE
        protected ArrayNotifier _notifier;
#endif
        public event Action<int> ElementChanged;

        public ProviderArrayBase(int size)
            : this(new Constant<T>[size]) {
        }

        public ProviderArrayBase(IList<T> values)
            : this(values.Select((value) => Provider<T>.Create(value)).ToArray()) {
        }

        public ProviderArrayBase(IList<Provider<T>> providers) {
            int size = providers.Count;
#if !NOTIFICATIONS_DISABLE
           _notifier = new ArrayNotifier(size);
#endif
            _properties = new Property<T>[size];
            for (int i = 0; i < size; i++) {
                _properties[i] = new Property<T>(providers[i]);
#if !NOTIFICATIONS_DISABLE
                _properties[i].Notifier.ValueChanged += () => OnElementChanged(i);
                _notifier.SubscribeTo(i, _properties[i].Notifier);
#endif
           }
        }

        public Provider<T>[] Providers {
            get {
                int size = _properties.Length;
                Provider<T>[] providers = new Provider<T>[size];
                for (int i = 0; i < size; i++) {
                    providers[i] = _properties[i].Provider;
                }
                return providers;
            }
        }

        public T[] Values {
            get {
                return GetValue();
            }
        }

#if !NOTIFICATIONS_DISABLE
        void OnElementChanged(int idx) {
            var handler = ElementChanged;
            if (handler != null) {
                handler(idx);
            }
        }
#endif

        public override T[] GetValue() {
            int size = _properties.Length;
            T[] values = new T[size];
            for (int i = 0; i < size; i++) {
                values[i] = _properties[i].Value;
            }
            return values;
        }

        public int Length {
            get {
                return _properties.Length;
            }
        }

        Provider<T>[] IOut<Provider<T>[]>.GetValue() {
            return Providers;
        }

#if !NOTIFICATIONS_DISABLE
        public override INotifier Notifier {
            get { return _notifier; }
        }
#endif
    }

    public class ProviderArray<T> : ProviderArrayBase<T> {
        public ProviderArray(int size)
            : base(size) {
        }

        public ProviderArray(IList<T> values)
            : base(values) {
        }

        public ProviderArray(IList<Provider<T>> providers)
            : base(providers) {
        }


        public Provider<T> this[int idx] {
            get {
                return _properties[idx].AsReadOnly();
            }
            set {
                _properties[idx].Provider = value;
            }
        }

        public static implicit operator ProviderArray<T>(Provider<T>[] provider) {
            return new ProviderArray<T>(provider);
        }


        public static implicit operator T[](ProviderArray<T> property) {
            return property.Value;
        }

        public static implicit operator ProviderArray<T>(T[] value) {
            return new ProviderArray<T>(value);
        }
    }

    public class ReadOnlyProviderArray<T> : ProviderArrayBase<T> {
        public ReadOnlyProviderArray(int size)
            : base(size) {
        }

        public ReadOnlyProviderArray(IList<T> values)
            : base(values) {
        }

        public ReadOnlyProviderArray(IList<Provider<T>> providers)
            : base(providers) {
        }


        public Provider<T> this[int idx] {
            get {
                return _properties[idx].AsReadOnly();
            }
        }

        public static implicit operator ReadOnlyProviderArray<T>(Provider<T>[] provider) {
            return new ReadOnlyProviderArray<T>(provider);
        }

        public static implicit operator T[](ReadOnlyProviderArray<T> property) {
            return property.Value;
        }

        public static implicit operator ReadOnlyProviderArray<T>(T[] value) {
            return new ReadOnlyProviderArray<T>(value);
        }
    }

}
