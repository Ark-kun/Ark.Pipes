namespace Ark.Pipes {
    public sealed class Variable<T> : NotifyingProvider<T>, IIn<T> {
        T _value;

        public Variable()
            : this(default(T)) {
        }

        public Variable(T value) {
            _value = value;
        }

        public override T GetValue() {
            return _value;
        }

        public void SetValue(T value) {
            _value = value;
            OnValueChanged();
        }

        public new T Value {
            get { return GetValue(); }
            set { SetValue(value); }
        }
    }
}