using System;

namespace Ark.Pipes {
    public interface IIn<in T> {
        void SetValue(T value);
    }

    public interface IOut<out T> {
        T GetValue();
    }

    public interface INotifyingOut<out T> : IOut<T>, IHasNotifier {
    }

    public interface INotifyElementChanged {
        event Action<int> ElementChanged;
    }

    public interface IHasNotifier {
        INotifier Notifier { get; }
    }

    public interface INotifier {
        event Action ValueChanged;
        event Action<bool> ReliabilityChanged;
        bool IsReliable { get; }
    }
}
