namespace Ark.Pipes {
    public static class Variable {
        public static Variable<T> Create<T>() {
            return new Variable<T>();
        }

        public static Variable<T> Create<T>(T value) {
            return new Variable<T>(value);
        }
    }

    public sealed class Variable<T> : ReadableVariable<T>, IIn<T> {
        public Variable() { }

        public Variable(T value)
            : base(value) {
        }

        public new T Value {
            get { return GetValue(); }
            set { base.SetValue(value); }
        }

        public new void SetValue(T value) {
            base.SetValue(value);
        }
    }
}