namespace Ark.Pipes {
    public sealed class Constant<T> : NotifyingProvider<T> {
        static Constant<T> _default = new Constant<T>(default(T));
        private readonly T _value;

        public Constant(T value) {
            _value = value;
        }

        public override T GetValue() {
            return _value;
        }

        public static Constant<T> Default {
            get {
                return _default;
            }
        }
    }
}