using System;

namespace Ark.Pipes {
    public class Function<TResult> : ProviderWithNotifier<TResult> {
        private Func<TResult> _function;

        public Function(Func<TResult> function) {
            _function = function;
        }

        public Function(IOut<TResult> source)
            : this(source.GetValue) {
        }

        public Function(Func<TResult> function, Action<Action> changedTriggerSetter) {
            _function = function;
            _notifier.SetReliability(true);
            changedTriggerSetter(_notifier.OnValueChanged);
        }

        public Function(INotifyingOut<TResult> source) {
            _function = source.GetValue;
            _notifier.SubscribeTo(source.Notifier);
        }

        public override TResult GetValue() {
            return _function();
        }
    }

    public class Function<T, TResult> : ProviderWithNotifier<TResult> {
        private Func<T, TResult> _function;
        private Property<T> _arg;

        public Function(Func<T, TResult> function)
            : this(function, Constant<T>.Default) {
        }

        public Function(Func<T, TResult> function, Provider<T> arg) {
            _function = function;
            _arg = new Property<T>(arg);
            _notifier.SubscribeTo(_arg.Notifier);
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
        protected ArrayNotifier _notifier = new ArrayNotifier(2);

        public Function(Func<T1, T2, TResult> function)
            : this(function, Constant<T1>.Default, Constant<T2>.Default) {
        }

        public Function(Func<T1, T2, TResult> function, Provider<T1> arg1, Provider<T2> arg2) {
            _function = function;
            _arg1 = new Property<T1>(arg1);
            _arg2 = new Property<T2>(arg2);
            _notifier.SubscribeTo(0, _arg1.Notifier);
            _notifier.SubscribeTo(1, _arg2.Notifier);
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

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }

    public class Function<T1, T2, T3, TResult> : Provider<TResult> {
        private Func<T1, T2, T3, TResult> _function;
        private Property<T1> _arg1;
        private Property<T2> _arg2;
        private Property<T3> _arg3;
        protected ArrayNotifier _notifier = new ArrayNotifier(3);

        public Function(Func<T1, T2, T3, TResult> function)
            : this(function, Constant<T1>.Default, Constant<T2>.Default, Constant<T3>.Default) {
        }

        public Function(Func<T1, T2, T3, TResult> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3) {
            _function = function;
            _arg1 = new Property<T1>(arg1);
            _arg2 = new Property<T2>(arg2);
            _arg3 = new Property<T3>(arg3);
            _notifier.SubscribeTo(0, _arg1.Notifier);
            _notifier.SubscribeTo(1, _arg2.Notifier);
            _notifier.SubscribeTo(2, _arg3.Notifier);
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value, _arg3.Value);
        }

        public Property<T1> Argument1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public Property<T2> Argument2 {
            get { return _arg2; }
            set { _arg2.Provider = value.Provider; }
        }

        public Property<T3> Argument3 {
            get { return _arg3; }
            set { _arg3.Provider = value.Provider; }
        }

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }

    public class Function<T1, T2, T3, T4, TResult> : Provider<TResult> {
        private Func<T1, T2, T3, T4, TResult> _function;
        private Property<T1> _arg1;
        private Property<T2> _arg2;
        private Property<T3> _arg3;
        private Property<T4> _arg4;
        protected ArrayNotifier _notifier = new ArrayNotifier(4);

        public Function(Func<T1, T2, T3, T4, TResult> function)
            : this(function, Constant<T1>.Default, Constant<T2>.Default, Constant<T3>.Default, Constant<T4>.Default) {
        }

        public Function(Func<T1, T2, T3, T4, TResult> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3, Provider<T4> arg4) {
            _function = function;
            _arg1 = new Property<T1>(arg1);
            _arg2 = new Property<T2>(arg2);
            _arg3 = new Property<T3>(arg3);
            _arg4 = new Property<T4>(arg4);
            _notifier.SubscribeTo(0, _arg1.Notifier);
            _notifier.SubscribeTo(1, _arg2.Notifier);
            _notifier.SubscribeTo(2, _arg3.Notifier);
            _notifier.SubscribeTo(3, _arg4.Notifier);
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value, _arg3.Value, _arg4.Value);
        }

        public Property<T1> Argument1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public Property<T2> Argument2 {
            get { return _arg2; }
            set { _arg2.Provider = value.Provider; }
        }

        public Property<T3> Argument3 {
            get { return _arg3; }
            set { _arg3.Provider = value.Provider; }
        }

        public Property<T4> Argument3 {
            get { return _arg4; }
            set { _arg4.Provider = value.Provider; }
        }

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }
}