using System;

namespace Ark.Pipes {
    public interface IIn<T> {
        void SetValue(T value);
    }

    public interface IOut<T> {
        T GetValue();
    }

    public abstract class Provider<T> : IOut<T> {
        static public implicit operator Provider<T>(T value) {
            return new Constant<T>(value);
        }

        static public implicit operator Provider<T>(Func<T> value) {
            return new Function<T>(value);
        }

        [System.Runtime.CompilerServices.SpecialName]
        static public Provider<T> op_Implicit<T1>(Func<T1, T> value) {
            return new Function<T1, T>(value);
        }

        static public implicit operator Provider<T>(Func<T, T> value) {
            return new Function<T, T>(value);
        }

        static public implicit operator T(Provider<T> provider) {
            return provider.Value;
        }

        public abstract T GetValue();

        public T Value {
            get { return GetValue(); }
        }
    }

    public sealed class Function<TResult> : Provider<TResult> {
        private Func<TResult> _f;

        public Function(Func<TResult> f) {
            _f = f;
        }

        public override TResult GetValue() {
            return _f();
        }
    }

    public sealed class Constant<T> : Provider<T> {
        static Constant<T> _default = new Constant<T>(default(T));
        private T _value;

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

    public sealed class Variable<T> : Provider<T>, IIn<T> {
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
        }

        public new T Value {
            get { return GetValue(); }
            set { SetValue(value); }
        }
    }

    public class Function<T, TResult> : DynamicConverter<T, TResult> {
        private Func<T, TResult> _function;

        public Function(Func<T, TResult> f) {
            _function = f;
        }

        public Function(Func<T, TResult> f, Provider<T> argument)
            : base(argument) {
            _function = f;
        }

        public override TResult GetValue() {
            return _function(Input);
        }
    }
}