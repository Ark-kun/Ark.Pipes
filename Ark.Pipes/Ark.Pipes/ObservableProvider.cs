using System;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    public class ObservableProvider<T> : Provider<T>, IObservable<T>, IDisposable {
        INotifyingOut<T> _provider;
        Action _onDispose;

        public ObservableProvider(INotifyingOut<T> provider) {
            _provider = provider;
        }

        public IDisposable Subscribe(IObserver<T> observer) {
            Action onNext = () => observer.OnNext(_provider.GetValue());
            Action onDispose = () => observer.OnCompleted();
            Action onUnsubscribe = () => { _provider.Notifier.ValueChanged -= onNext; _onDispose -= onDispose; };

            _provider.Notifier.ValueChanged += onNext;
            _onDispose += onDispose;
            return new DisposableCallback(onUnsubscribe);
        }

        public override T GetValue() {
            return _provider.GetValue();
        }

        public override INotifier Notifier {
            get { return _provider.Notifier; }
        }

        public void Dispose() {
            var handler = _onDispose;
            if (handler != null) {
                handler();
            }
            _onDispose = null;
            _provider = null;
        }

        class DisposableCallback : IDisposable {
            Action _onDisposing;

            public DisposableCallback(Action onDisposing) {
                _onDisposing = onDisposing;
            }

            public void Dispose() {
                var handler = _onDisposing;
                if (handler != null) {
                    handler();
                }
            }
        }
    }
}

namespace Ark.Pipes {
    public static partial class Extensions {
        public static IObservable<T> AsObservable<T>(this INotifyingOut<T> provider) {
            return new ObservableProvider<T>(provider);
        }
    }
}
#endif
