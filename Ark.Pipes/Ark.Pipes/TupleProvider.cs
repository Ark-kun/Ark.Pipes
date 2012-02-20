using System;

namespace Ark.Pipes {
    public class TupleProvider {
        public static TupleProvider<T1, T2> Create<T1, T2>(T1 item1, T2 item2) {
            return new TupleProvider<T1, T2>(item1, item2);
        }

        public static TupleProvider<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) {
            return new TupleProvider<T1, T2, T3>(item1, item2, item3);
        }

        public static TupleProvider<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) {
            return new TupleProvider<T1, T2, T3, T4>(item1, item2, item3, item4);
        }
    }

    public class TupleProvider<T1, T2> : Provider<Tuple<T1, T2>> {
        private Property<T1> _arg1;
        private Property<T2> _arg2;
        protected ArrayNotifier _notifier = new ArrayNotifier(2);

        public TupleProvider(Provider<T1> arg1, Provider<T2> arg2) {
            _arg1 = new Property<T1>(arg1);
            _arg2 = new Property<T2>(arg2);
            _notifier.SubscribeTo(0, _arg1.Notifier);
            _notifier.SubscribeTo(1, _arg2.Notifier);
        }

        public override Tuple<T1, T2> GetValue() {
            return Tuple.Create(_arg1.Value, _arg2.Value);
        }

        public Property<T1> Provider1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public Property<T2> Provider2 {
            get { return _arg2; }
            set { _arg2.Provider = value.Provider; }
        }

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }

    public class TupleProvider<T1, T2, T3> : Provider<Tuple<T1, T2, T3>> {
        private Property<T1> _arg1;
        private Property<T2> _arg2;
        private Property<T3> _arg3;
        protected ArrayNotifier _notifier = new ArrayNotifier(3);

        public TupleProvider(Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3) {
            _arg1 = new Property<T1>(arg1);
            _arg2 = new Property<T2>(arg2);
            _arg3 = new Property<T3>(arg3);
            _notifier.SubscribeTo(0, _arg1.Notifier);
            _notifier.SubscribeTo(1, _arg2.Notifier);
            _notifier.SubscribeTo(2, _arg3.Notifier);
        }

        public override Tuple<T1, T2, T3> GetValue() {
            return Tuple.Create(_arg1.Value, _arg2.Value, _arg3.Value);
        }

        public Property<T1> Provider1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public Property<T2> Provider2 {
            get { return _arg2; }
            set { _arg2.Provider = value.Provider; }
        }

        public Property<T3> Provider3 {
            get { return _arg3; }
            set { _arg3.Provider = value.Provider; }
        }

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }

    public class TupleProvider<T1, T2, T3, T4> : Provider<Tuple<T1, T2, T3, T4>> {
        private Property<T1> _arg1;
        private Property<T2> _arg2;
        private Property<T3> _arg3;
        private Property<T4> _arg4;
        protected ArrayNotifier _notifier = new ArrayNotifier(4);

        public TupleProvider(Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3, Provider<T4> arg4) {
            _arg1 = new Property<T1>(arg1);
            _arg2 = new Property<T2>(arg2);
            _arg3 = new Property<T3>(arg3);
            _arg4 = new Property<T4>(arg4);
            _notifier.SubscribeTo(0, _arg1.Notifier);
            _notifier.SubscribeTo(1, _arg2.Notifier);
            _notifier.SubscribeTo(2, _arg3.Notifier);
            _notifier.SubscribeTo(4, _arg4.Notifier);
        }

        public override Tuple<T1, T2, T3, T4> GetValue() {
            return Tuple.Create(_arg1.Value, _arg2.Value, _arg3.Value, _arg4.Value);
        }

        public Property<T1> Provider1 {
            get { return _arg1; }
            set { _arg1.Provider = value.Provider; }
        }

        public Property<T2> Provider2 {
            get { return _arg2; }
            set { _arg2.Provider = value.Provider; }
        }

        public Property<T3> Provider3 {
            get { return _arg3; }
            set { _arg3.Provider = value.Provider; }
        }

        public Property<T4> Provider4 {
            get { return _arg4; }
            set { _arg4.Provider = value.Provider; }
        }

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }
}