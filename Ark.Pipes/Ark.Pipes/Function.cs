using System;

namespace Ark.Pipes {
    public sealed class Function<TResult> : Provider<TResult> {
        private Func<TResult> _function;

        public Function(Func<TResult> function) {
            _function = function;
        }

        public override TResult GetValue() {
            return _function();
        }
    }

    public class Function<T, TResult> : Provider<TResult> {
        private Func<T, TResult> _function;
        private Property<T> _arg;

        public Function(Func<T, TResult> function)
            : this(function, Constant<T>.Default) {
        }

        public Function(Func<T, TResult> function, Provider<T> arg) {
            _function = function;
            _arg = arg;
        }

        public override TResult GetValue() {
            return _function(_arg.Value);
        }

        public Property<T> Argument {
            get { return _arg; }
            set { _arg.Provider = value.Provider; }
        }
    }

    public class Function<T1, T2, TResult> : Provider<TResult> {
        private Func<T1, T2, TResult> _function;
        private Property<T1> _arg1;
        private Property<T2> _arg2;

        public Function(Func<T1, T2, TResult> function)
            : this(function, Constant<T1>.Default, Constant<T2>.Default) {
        }

        public Function(Func<T1, T2, TResult> function, Provider<T1> arg1, Provider<T2> arg2) {
            _function = function;
            _arg1 = arg1;
            _arg2 = arg2;
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value);
        }

        public Property<T1> Argument1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public Property<T2> Argument2 {
            get { return _arg2; }
            set { _arg2.Provider = value.Provider; }
        }
    }
}