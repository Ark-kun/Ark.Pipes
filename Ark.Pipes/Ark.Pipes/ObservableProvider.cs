using System;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    class ObservableProvider<T> : IObservable<T> {
        INotifyingOut<T> _provider;

        public ObservableProvider(INotifyingOut<T> provider) {
            _provider = provider;
        }

        public IDisposable Subscribe(IObserver<T> observer) {
            Action onNext = () => observer.OnNext(_provider.GetValue());
            Action onUnsubscribe = () => { _provider.Notifier.ValueChanged -= onNext; };

            _provider.Notifier.ValueChanged += onNext;
            return new DisposableCallback(onUnsubscribe);
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
