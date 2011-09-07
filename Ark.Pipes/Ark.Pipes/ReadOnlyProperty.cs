namespace Ark.Pipes {
    public sealed class ReadOnlyProperty<T> : IOut<T>, IOut<Provider<T>> {
        private Provider<T> _provider;

        public ReadOnlyProperty(Provider<T> provider) {
            _provider = provider;
        }

        public T Value {
            get { return _provider.GetValue(); }
        }

        public Provider<T> Provider {
            get { return _provider; }
        }

        T IOut<T>.GetValue() {
            return _provider.GetValue();
        }

        Provider<T> IOut<Provider<T>>.GetValue() {
            return _provider;
        }

        public static implicit operator Provider<T>(ReadOnlyProperty<T> property) {
            return property._provider;
        }

        public static implicit operator ReadOnlyProperty<T>(Provider<T> provider) {
            return new ReadOnlyProperty<T>(provider);
        }

        public static implicit operator T(ReadOnlyProperty<T> property) {
            return property.Value;
        }

        public static implicit operator ReadOnlyProperty<T>(T provider) {
            return new ReadOnlyProperty<T>(provider);
        }
    }
}