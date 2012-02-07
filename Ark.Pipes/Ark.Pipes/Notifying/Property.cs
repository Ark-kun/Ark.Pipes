namespace Ark.Pipes {
    public sealed class NotifyingProperty<T> : NotifyingProvider<T>, IIn<T>, IIn<NotifyingProvider<T>>, IOut<NotifyingProvider<T>>, INotifyProviderChanged {
        private NotifyingProvider<T> _provider;
        public event System.Action ProviderChanged;

        public NotifyingProperty() {
            _provider = Constant<T>.Default;
        }

        public NotifyingProperty(T value) {
            _provider = new Constant<T>(value);
        }

        public NotifyingProperty(NotifyingProvider<T> provider) {
            _provider = provider;
            _provider.ValueChanged += OnValueChanged;
        }

        public new T Value {
            get { return _provider.GetValue(); }
            set { Provider = new Constant<T>(value); }
        }

        public NotifyingProvider<T> Provider {
            get { return _provider; }
            set {
                if (value != _provider) {
                    _provider.ValueChanged -= OnValueChanged;
                    _provider = value;
                    _provider.ValueChanged += OnValueChanged;
                    OnProviderChanged();
                    OnValueChanged();                    
                }
            }
        }

        public NotifyingProvider<T> AsReadOnly() {
            return new NotifyingFunction<T>(this);
        }

        void OnProviderChanged() {
            if (ProviderChanged != null) {
                ProviderChanged();
            }
        }

        void IIn<T>.SetValue(T value) {
            Provider = new Constant<T>(value);
        }

        public override T GetValue() {
            return _provider.GetValue();
        }

        void IIn<NotifyingProvider<T>>.SetValue(NotifyingProvider<T> value) {
            Provider = value;
        }

        NotifyingProvider<T> IOut<NotifyingProvider<T>>.GetValue() {
            return _provider;
        }

        [System.Runtime.CompilerServices.SpecialName]
        public static NotifyingProperty<T> op_Implicit(NotifyingProvider<T> provider) {
            return new NotifyingProperty<T>(provider);
        }

        public static implicit operator T(NotifyingProperty<T> property) {
            return property.Value;
        }

        public static implicit operator NotifyingProperty<T>(T value) {
            return new NotifyingProperty<T>(value);
        }        
    }
}