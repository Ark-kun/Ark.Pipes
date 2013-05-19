using System;

namespace Ark.Pipes {
    public interface IIn<T> {
        void SetValue(T value);
    }

    public interface IOut<T> {
        T GetValue();
    }

    public interface INotifyingOut<T> : IOut<T>, IHasNotifier {
    }

    public interface INotifyElementChanged {
        event Action<int> ElementChanged;
    }

    public interface IHasNotifier {
        INotifier Notifier { get; }
    }

    public interface INotifier {
        event Action ValueChanged;
        bool IsNotifying { get; }
        void AddListener(IValueChangeListener listener);
        void AddListener(IProviderListener listener);
        void RemoveListener(IValueChangeListener listener);
        void RemoveListener(IProviderListener listener);
    }

    public interface IValueChangeListener {
        void OnValueChanged();
    }

    public interface IProviderListener : IValueChangeListener {
        void OnStartedNotifying();
        void OnStoppedNotifying();
    }

    public interface ITrigger {
        event Action Triggered;
    }
}
