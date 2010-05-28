﻿using System;

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

    public sealed class Function<TResult> : Provider<TResult> {
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

    public sealed class Constant<T> : Provider<T> {
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

    public sealed class Variable<T> : Provider<T> {
        public T value;

        public Variable()
            : this(default(T)) {
        }

        public Variable(T value) {
            this.value = value;
        }

        public override T Value {
            get {
                return this.value;
            }
        }
    }

    //public class Property<T> : In<T>, Out<T> {
    //    private Provider<T> _provider;

    //    public Property() {
    //        _provider = Constant<T>.Default;
    //    }

    //    public Property(T value) {
    //        _provider = new Constant<T>(value);
    //    }

    //    public Property(Provider<T> provider) {
    //        _provider = provider;
    //    }

    //    public Provider<T> Provider {
    //        get {
    //            return _provider;
    //        }
    //        set {
    //            _provider = value;
    //        }
    //    }

    //    public T Value {
    //        get {
    //            return _provider.Value;
    //        }
    //        set {
    //            _provider = new Constant<T>(value);
    //        }
    //    }

    //    static public implicit operator T(Property<T> property) {
    //        return property.Value;
    //    }
    //}

    //public class Consumer<T> : In<T> {
    //    private Provider<T> _provider = new Constant<T>();
    //}

    public class Function<T, TResult> : DynamicConverter<T, TResult> {
        private Func<T, TResult> _function;

        public Function(Func<T, TResult> f) {
            _function = f;
        }

        public Function(Func<T, TResult> f, Provider<T> argument)
            : base(argument) {
            _function = f;
        }

        public override TResult Value {
            get {
                return _function(Input);
            }
        }
    }
}