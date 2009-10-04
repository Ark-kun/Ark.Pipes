using System;

namespace Ark.Pipes {
    public interface In<T> {
        T Value { set; }
    }
    public interface Out<T> {
        T Value { get; }
    }

    public abstract class Provider<T> : Out<T> {
        static public implicit operator Provider<T>(T value) {
            return new Constant<T>(value);
        }

        static public implicit operator Provider<T>(Func<T> value) {
            return new Function<T>(value);
        }

        //static public implicit operator Provider<T>(Func<T1, T> value) {
        //    return new Function<T1, T>(value);
        //}

        static public implicit operator T(Provider<T> provider) {
            return provider.Value;
        }

        public abstract T Value { get; }
    }

    public class Function<TResult> : Provider<TResult> {
        private Func<TResult> _f;

        public Function(Func<TResult> f) {
            _f = f;
        }

        public override TResult Value {
            get {
                return _f();
            }
        }
    }

    public class Constant<T> : Provider<T> {
        static Constant<T> _default = new Constant<T>(default(T));
        private T _value;

        public Constant(T value) {
            _value = value;
        }

        public override T Value {
            get {
                return _value;
            }
        }

        public static Constant<T> Default {
            get {
                return _default;
            }
        }
    }

    public class Property<T> : In<T>, Out<T> {
        private Provider<T> _provider;

        public Property() {
            _provider = Constant<T>.Default;
        }

        public Property(T value) {
            _provider = new Constant<T>(value);
        }

        public Property(Provider<T> provider) {
            _provider = provider;
        }

        public Provider<T> Provider {
            get {
                return _provider;
            }
            set {
                _provider = value;
            }
        }

        public T Value {
            get {
                return _provider.Value;
            }
            set {
                _provider = new Constant<T>(value);
            }
        }

        static public implicit operator T(Property<T> property) {
            return property.Value;
        }
    }

    //public class Consumer<T> : In<T> {
    //    private Provider<T> _provider = new Constant<T>();
    //}

    public class Function<T1, TResult> : Provider<TResult> {
        private Func<T1, TResult> _f;
        private Property<T1> _arg1Provider;

        public Function(Func<T1, TResult> f) {
            _f = f;
            _arg1Provider = new Property<T1>();
        }

        public Function(Func<T1, TResult> f, Provider<T1> arg1Provider) {
            _f = f;
            _arg1Provider = new Property<T1>(arg1Provider);
        }

        public override TResult Value {
            get {
                return _f(_arg1Provider.Provider.Value);
            }
        }

        public Property<T1> Argument1 {
            get {
                return _arg1Provider;
            }
        }
    }
}