namespace Ark.Pipes {
    public sealed class Property<T> : Provider<T>, IIn<T>, IIn<Provider<T>>, IOut<Provider<T>>, INotifyProviderChanged {
        private Provider<T> _provider;
        public event System.Action ProviderChanged;

        public Property() {
            _provider = Constant<T>.Default;
        }

        public Property(Provider<T> provider) {
            _provider = provider;
            _provider.ValueChanged += OnValueChanged;
        }

        public new T Value {
            get { return _provider.GetValue(); }
            set {
                _provider.ValueChanged -= OnValueChanged;
                _provider = new Constant<T>(value);
                OnValueChanged();
            }
        }

        public Provider<T> Provider {
            get { return _provider; }
            set {
                _provider.ValueChanged -= OnValueChanged;
                _provider = value;
                _provider.ValueChanged += OnValueChanged;
                OnProviderChanged();
                OnValueChanged();
            }
        }

        public Provider<T> AsReadOnly() {
            return new Function<T>(this);
        }

        void OnProviderChanged() {
            if (ProviderChanged != null) {
                ProviderChanged();
            }
        }

        void IIn<T>.SetValue(T value) {
            _provider.ValueChanged -= OnValueChanged;
            _provider = value;
            OnValueChanged();
        }

        public override T GetValue() {
            return _provider.GetValue();
        }

        void IIn<Provider<T>>.SetValue(Provider<T> value) {
            Provider = value;
        }

        Provider<T> IOut<Provider<T>>.GetValue() {
            return _provider;
        }

        [System.Runtime.CompilerServices.SpecialName]
        public static Property<T> op_Implicit(Provider<T> provider) {
            return new Property<T>(provider);
        }

        public static implicit operator T(Property<T> property) {
            return property.Value;
        }

        public static implicit operator Property<T>(T value) {
            return new Property<T>(value);
        }        
    }
}

//TODO:
//Provider "stack" (Property Value Precedence slots  http://msdn.microsoft.com/en-us/library/ms743230.aspx )
//Property binder that doesn't require  instance
//Data context
//Attached properties
//INotifyPropertyChanged    

