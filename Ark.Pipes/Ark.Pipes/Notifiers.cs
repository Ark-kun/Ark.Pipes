using System;
using System.Collections.Generic;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    public class Notifier : INotifier {
        bool _isReliable = false;
        public event Action ValueChanged;
        public event Action<bool> ReliabilityChanged;

        public Notifier()
            : this(false) {
        }

        public Notifier(bool isReliable) {
            _isReliable = isReliable;
        }

        public static INotifier AlwaysUnreliable { get { return UnreliableNotifier.Instance; } }

        protected void OnValueChanged() {
            var handler = ValueChanged;
            if (handler != null) {
                handler();
            }
        }

        public bool IsReliable {
            get {
                return _isReliable;
            }
            protected set {
                if (value != _isReliable) {
                    _isReliable = value;
                    var handler = ReliabilityChanged;
                    if (handler != null) {
                        handler(_isReliable);
                    }
                }
            }
        }
    }

    class UnreliableNotifier : INotifier {
        private UnreliableNotifier() { }

        static UnreliableNotifier _instance = new UnreliableNotifier();

        public static UnreliableNotifier Instance {
            get { return _instance; }
        }

        event Action INotifier.ValueChanged {
            add { }
            remove { }
        }

        event Action<bool> INotifier.ReliabilityChanged {
            add { }
            remove { }
        }

        public bool IsReliable {
            get { return false; }
        }
    }


    public class PrivateNotifier : Notifier {
        public PrivateNotifier() { }

        public PrivateNotifier(bool isReliable) : base(isReliable) { }

        public void SetReliability(bool value) {
            IsReliable = value;
        }

        public new void OnValueChanged() {
            base.OnValueChanged();
        }

        //TODO:Unsubscribe?
        public void SubscribeTo(INotifier notifier) {
            notifier.ValueChanged += OnValueChanged;
            notifier.ReliabilityChanged += SetReliability;
            IsReliable = notifier.IsReliable;
        }

        public void UnsubscribeTo(INotifier notifier) {
            notifier.ValueChanged -= OnValueChanged;
            notifier.ReliabilityChanged -= SetReliability;
        }
    }

    public class ArrayNotifier : Notifier {
        bool[] _isReliable;
        int _unreliableCount;

        public ArrayNotifier(int size) {
            _isReliable = new bool[size];
            _unreliableCount = size;
        }

        public void SetReliability(int index, bool value) {
            if (index >= _isReliable.Length)
                throw new ArgumentOutOfRangeException();

            if (value != _isReliable[index]) {
                if (value) {
                    _unreliableCount--;
                    if (_unreliableCount == 0) {
                        IsReliable = true;
                    }
                } else {
                    _unreliableCount++;
                    IsReliable = false;
                }
            }
        }

        //TODO:Unsubscribe?
        public void SubscribeTo(int index, INotifier notifier) {
            notifier.ValueChanged += OnValueChanged;
            notifier.ReliabilityChanged += (value) => SetReliability(index, value);
            SetReliability(index, notifier.IsReliable);
        }
    }
}
#endif
