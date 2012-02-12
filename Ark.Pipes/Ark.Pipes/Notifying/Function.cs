using System;

namespace Ark.Pipes {
    public sealed class NotifyingFunction<TResult> : NotifyingProvider<TResult> {
        private Func<TResult> _function;

        public NotifyingFunction(Func<TResult> function, ref Action changedTrigger) {
            _function = function;
            changedTrigger += OnValueChanged;
        }

        public NotifyingFunction(INotifyingOut<TResult> source) {
            _function = source.GetValue;
            source.ValueChanged += OnValueChanged;
        }

        public override TResult GetValue() {
            return _function();
        }
    }

    public class NotifyingFunction<T, TResult> : NotifyingProvider<TResult> {
        private Func<T, TResult> _function;
        private NotifyingProperty<T> _arg;

        public NotifyingFunction(Func<T, TResult> function)
            : this(function, Constant<T>.Default) {
        }

        public NotifyingFunction(Func<T, TResult> function, NotifyingProvider<T> arg) {
            _function = function;
            _arg = new NotifyingProperty<T>(arg);
            _arg.ValueChanged += OnValueChanged;
        }

        public NotifyingProperty<T> Argument {
            get { return _arg; }
            set {
                _arg.ValueChanged -= OnValueChanged;
                _arg.Value = value.Provider;
                _arg.ValueChanged += OnValueChanged;
            }
        }

        public override TResult GetValue() {
            return _function(_arg.Value);
        }
    }

    public class NotifyingFunction<T1, T2, TResult> : NotifyingProvider<TResult> {
        private Func<T1, T2, TResult> _function;
        private NotifyingProperty<T1> _arg1;
        private NotifyingProperty<T2> _arg2;

        public NotifyingFunction(Func<T1, T2, TResult> function)
            : this(function, Constant<T1>.Default, Constant<T2>.Default) {
        }

        public NotifyingFunction(Func<T1, T2, TResult> function, NotifyingProvider<T1> arg1, NotifyingProvider<T2> arg2) {
            _function = function;
            _arg1 = new NotifyingProperty<T1>(arg1);
            _arg2 = new NotifyingProperty<T2>(arg2);
            _arg1.ValueChanged += OnValueChanged;
            _arg2.ValueChanged += OnValueChanged;
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value);
        }

        public NotifyingProperty<T1> Argument1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public NotifyingProperty<T2> Argument2 {
            get { return _arg2; }
            set {
                _arg2.Provider = value.Provider;
                OnValueChanged();
            }
        }
    }

    public class NotifyingFunction<T1, T2, T3, TResult> : NotifyingProvider<TResult> {
        private Func<T1, T2, T3, TResult> _function;
        private NotifyingProperty<T1> _arg1;
        private NotifyingProperty<T2> _arg2;
        private NotifyingProperty<T3> _arg3;

        public NotifyingFunction(Func<T1, T2, T3, TResult> function)
            : this(function, Constant<T1>.Default, Constant<T2>.Default, Constant<T3>.Default) {
        }

        public NotifyingFunction(Func<T1, T2, T3, TResult> function, NotifyingProvider<T1> arg1, NotifyingProvider<T2> arg2, NotifyingProvider<T3> arg3) {
            _function = function;
            _arg1 = new NotifyingProperty<T1>(arg1);
            _arg2 = new NotifyingProperty<T2>(arg2);
            _arg3 = new NotifyingProperty<T3>(arg3);
            _arg1.ValueChanged += OnValueChanged;
            _arg2.ValueChanged += OnValueChanged;
            _arg3.ValueChanged += OnValueChanged;
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value, _arg3.Value);
        }

        public NotifyingProperty<T1> Argument1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public NotifyingProperty<T2> Argument2 {
            get { return _arg2; }
            set {
                _arg2.Provider = value.Provider;
                OnValueChanged();
            }
        }

        public NotifyingProperty<T3> Argument3 {
            get { return _arg3; }
            set {
                _arg3.Provider = value.Provider;
                OnValueChanged();
            }
        }
    }
}