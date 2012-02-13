﻿using System;

namespace Ark.Pipes {
    public interface IIn<in T> {
        void SetValue(T value);
    }

    public interface IOut<out T> {
        T GetValue();
    }

    public interface INotifyingOut<out T> : IOut<T>, INotifyValueChanged {
    }

    public interface INotifyValueChanged {
        event Action ValueChanged;
    }

    public interface INotifyElementChanged {
        event Action<int> ElementChanged;
    }
}
