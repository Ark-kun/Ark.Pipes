namespace Ark.Pipes {
    public sealed class Constant<T> : Provider<T> {
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

#if !NOTIFICATIONS_DISABLE
        public override INotifier Notifier {
            get { return Ark.Pipes.Notifier.Constant; }
        }
#endif
    }
}