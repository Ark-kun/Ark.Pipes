namespace Ark.Pipes {
    public sealed class Property<T> : IIn<T>, IOut<T>, IIn<Provider<T>>, IOut<Provider<T>> {
        private Provider<T> _provider;

        public Property() {
            _provider = Constant<T>.Default;
        }

        public Property(Provider<T> provider) {
            _provider = provider;
        }

        public T Value {
            get { return _provider.GetValue(); }
            set { _provider = value; }
        }

        public Provider<T> Provider {
            get { return _provider; }
            set { _provider = value; }
        }

        public ReadOnlyProperty<T> AsReadOnly() {
            return new ReadOnlyProperty<T>(this);
        }

        void IIn<T>.SetValue(T value) {
            _provider = value;
        }

        T IOut<T>.GetValue() {
            return _provider.GetValue();
        }

        void IIn<Provider<T>>.SetValue(Provider<T> value) {
            _provider = value;
        }

        Provider<T> IOut<Provider<T>>.GetValue() {
            return _provider;
        }

        public static implicit operator Provider<T>(Property<T> property) {
            return property._provider;
        }

        public static implicit operator Property<T>(Provider<T> provider) {
            return new Property<T>(provider);
        }

        public static implicit operator T(Property<T> property) {
            return property.Value;
        }

        public static implicit operator Property<T>(T provider) {
            return new Property<T>(provider);
        }
    }
}

//TODO:
//Provider "stack" (Property Value Precedence slots  http://msdn.microsoft.com/en-us/library/ms743230.aspx )
//Property binder that doesn't require  instance
//Data context
//Attached properties
//INotifyPropertyChanged    

