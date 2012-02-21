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
        event Action<bool> ReliabilityChanged;
        bool IsReliable { get; }
    }

    public interface ITrigger {
        event Action Triggered;
    }
}
