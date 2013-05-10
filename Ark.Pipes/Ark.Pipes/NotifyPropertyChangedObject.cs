using System;
using System.ComponentModel;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    class NotifyPropertyChangedObject : INotifyPropertyChanged, IDisposable {
        IHasNotifier _provider;
        PropertyChangedEventArgs _eventArgs;

        public event PropertyChangedEventHandler PropertyChanged;

        public NotifyPropertyChangedObject(IHasNotifier provider, string propertyName) {
            _provider = provider;
            _eventArgs = new PropertyChangedEventArgs(propertyName);
            _provider.Notifier.ValueChanged += OnValueChanged;
        }

        void SignalPropertyChanged() {
            var handlers = PropertyChanged;
            if (handlers != null) {
                handlers.Invoke(_provider, _eventArgs);
            }
        }

        void OnValueChanged() {
            SignalPropertyChanged();
        }

        public void Dispose() {
            _provider.Notifier.ValueChanged -= OnValueChanged;
        }
    }
}

namespace Ark.Pipes {
    public static partial class Extensions {
        public static INotifyPropertyChanged AsINotifyPropertyChanged<T>(this IHasNotifier provider, string propertyName = "value") {
            return new NotifyPropertyChangedObject(provider, propertyName);
        }
    }
}
#endif