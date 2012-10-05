using System;

namespace Ark.Pipes {
    abstract class Source<TResult> : Provider<TResult> {
        Func<TResult> _function;

        public Source(Func<TResult> function) {
            _function = function;
        }

        public override TResult GetValue() {
            return _function();
        }

        public Func<TResult> Function {
            get { return _function; }
        }

        public static Source<TResult> Create(Func<TResult> function) {
            return new UnreliableSource<TResult>(function);
        }

        public static Source<TResult> Create(IOut<TResult> source) {
            var function = GetFunction(source);
            return new UnreliableSource<TResult>(function);
        }

#if !NOTIFICATIONS_DISABLE
        public static Source<TResult> Create(Func<TResult> function, ITrigger changedTrigger) {
            return new ReliableSource<TResult>(function, changedTrigger);
        }

        public static Source<TResult> Create(IOut<TResult> source, ITrigger changedTrigger) {
            var function = GetFunction(source);
            return new ReliableSource<TResult>(function, changedTrigger);
        }

        public static Source<TResult> Create(INotifyingOut<TResult> source) {
            var function = GetFunction(source);
            return new ReliableSource<TResult>(function, source.Notifier);
        }
#endif

        static Func<TResult> GetFunction(IOut<TResult> source) {
            var sourceSource = source as Source<TResult>;
            Func<TResult> function;
            if (sourceSource != null) {
                function = sourceSource.Function;
            } else {
                function = source.GetValue;
            }
            return function;
        }
    }

#if !NOTIFICATIONS_DISABLE
    sealed class ReliableSource<TResult> : Source<TResult> {
        PrivateNotifier _notifier = new PrivateNotifier(isReliable: true);

        public ReliableSource(Func<TResult> function, ITrigger changedTrigger)
            : base(function) {
            changedTrigger.Triggered += _notifier.SignalValueChanged;
        }

        internal ReliableSource(Func<TResult> function, INotifier notifier)
            : base(function) {
            _notifier.Source = notifier;
        }

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }
#endif

    sealed class UnreliableSource<TResult> : Source<TResult> {
        public UnreliableSource(Func<TResult> function) : base(function) { }

#if !NOTIFICATIONS_DISABLE
        public override INotifier Notifier {
            get { return Ark.Pipes.Notifier.AlwaysUnreliable; }
        }
#endif
    }
}
