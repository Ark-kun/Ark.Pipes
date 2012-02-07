using System;
using System.Collections.Generic;


namespace Ark.Pipes {
    //public class PropertyArray<T> : Provider<T[]>, IIn<T[]>, IIn<Provider<T>[]>, IOut<Provider<T>[]>, INotifyProviderChanged {
    public class NotifyingProviderArrayBase<T> : NotifyingProvider<T[]>, IOut<NotifyingProvider<T>[]>, INotifyElementChanged {
        protected NotifyingProperty<T>[] _properties;

        public event Action<int> ElementChanged;

        public NotifyingProviderArrayBase(int size) {
            _properties = new NotifyingProperty<T>[size];
            for (int i = 0; i < size; i++) {
                _properties[i] = new NotifyingProperty<T>();
                _properties[i].ValueChanged += () => OnElementChanged(i);
            }
        }

        public NotifyingProviderArrayBase(IList<T> values) {
            int size = values.Count;
            _properties = new NotifyingProperty<T>[size];
            for (int i = 0; i < size; i++) {
                _properties[i] = new NotifyingProperty<T>(values[i]);
                _properties[i].ValueChanged += () => OnElementChanged(i);
            }
        }

        public NotifyingProviderArrayBase(IList<NotifyingProvider<T>> providers) {
            int size = providers.Count;
            _properties = new NotifyingProperty<T>[size];
            for (int i = 0; i < size; i++) {
                _properties[i] = new NotifyingProperty<T>(providers[i]);
                _properties[i].ValueChanged += () => OnElementChanged(i);
            }
        }

        public NotifyingProvider<T>[] Providers {
            get {
                int size = _properties.Length;
                NotifyingProvider<T>[] providers = new NotifyingProvider<T>[size];
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

        void OnElementChanged(int idx) {
            if (ElementChanged != null) {
                ElementChanged(idx);
            }
        }

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

        NotifyingProvider<T>[] IOut<NotifyingProvider<T>[]>.GetValue() {
            return Providers;
        }
    }

    public class NotifyingProviderArray<T> : NotifyingProviderArrayBase<T> {
        public NotifyingProviderArray(int size)
            : base(size) {
        }

        public NotifyingProviderArray(IList<T> values)
            : base(values) {
        }

        public NotifyingProviderArray(IList<NotifyingProvider<T>> providers)
            : base(providers) {
        }


        public NotifyingProvider<T> this[int idx] {
            get {
                return _properties[idx].AsReadOnly();
            }
            set {
                _properties[idx].Provider = value;
            }
        }

        public static implicit operator NotifyingProviderArray<T>(NotifyingProvider<T>[] provider) {
            return new NotifyingProviderArray<T>(provider);
        }


        public static implicit operator T[](NotifyingProviderArray<T> property) {
            return property.Value;
        }

        public static implicit operator NotifyingProviderArray<T>(T[] value) {
            return new NotifyingProviderArray<T>(value);
        }
    }

    public class NotifyingReadOnlyProviderArray<T> : NotifyingProviderArrayBase<T> {
        public NotifyingReadOnlyProviderArray(int size)
            : base(size) {
        }

        public NotifyingReadOnlyProviderArray(IList<T> values)
            : base(values) {
        }

        public NotifyingReadOnlyProviderArray(IList<NotifyingProvider<T>> providers)
            : base(providers) {
        }


        public NotifyingProvider<T> this[int idx] {
            get {
                return _properties[idx].AsReadOnly();
            }
        }

        public static implicit operator NotifyingReadOnlyProviderArray<T>(NotifyingProvider<T>[] provider) {
            return new NotifyingReadOnlyProviderArray<T>(provider);
        }

        public static implicit operator T[](NotifyingReadOnlyProviderArray<T> property) {
            return property.Value;
        }

        public static implicit operator NotifyingReadOnlyProviderArray<T>(T[] value) {
            return new NotifyingReadOnlyProviderArray<T>(value);
        }
    }

}
