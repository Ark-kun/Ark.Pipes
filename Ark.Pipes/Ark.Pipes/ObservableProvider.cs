using System;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    class ObservableProvider<T> : IObservable<T> {
        INotifyingOut<T> _provider;

        public ObservableProvider(INotifyingOut<T> provider) {
            _provider = provider;
        }

        public IDisposable Subscribe(IObserver<T> observer) {
            return new Retranslator(_provider, observer);
        }

        class Retranslator : IValueChangeListener, IDisposable {
            INotifyingOut<T> _input;
            IObserver<T> _output;

            public Retranslator(INotifyingOut<T> input, IObserver<T> output) {
                _input = input;
                _output = output;
                _input.Notifier.AddListener(this);
            }

            public void OnValueChanged() {
                _output.OnNext(_input.GetValue());
            }

            public void Dispose() {
                _input.Notifier.RemoveListener(this);
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
