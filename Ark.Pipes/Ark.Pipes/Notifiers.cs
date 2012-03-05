using System;
using System.Collections.Generic;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    public abstract class Notifier : INotifier {
        bool _isReliable = false;
        Action _valueChanged;
        Action<bool> _reliabilityChanged;

        public Notifier()
            : this(false) {
        }

        public Notifier(bool isReliable) {
            _isReliable = isReliable;
        }

        public static INotifier AlwaysUnreliable {
            get { return ConstantReliabilityNotifier.UnreliableInstance; }
        }

        public static INotifier Constant {
            get { return ConstantReliabilityNotifier.ConstantInstance; }
        }

        protected void OnValueChanged() {
            var handler = _valueChanged;
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
                    var handler = _reliabilityChanged;
                    if (handler != null) {
                        handler(_isReliable);
                    }
                }
            }
        }

        protected bool IsConnected {
            get {
                return _valueChanged != null || _reliabilityChanged != null;
            }
        }

        protected abstract void ConnectToSources();

        protected abstract void DisconnectFromSources();

        public event Action ValueChanged {
            add {
                if (!IsConnected) {
                    ConnectToSources();
                }
                _valueChanged += value;
            }
            remove {
                _valueChanged -= value;
                if (!IsConnected) {
                    DisconnectFromSources();
                }
            }
        }

        public event Action<bool> ReliabilityChanged {
            add {
                if (!IsConnected) {
                    ConnectToSources();
                }
                _reliabilityChanged += value;
            }
            remove {
                _reliabilityChanged -= value;
                if (!IsConnected) {
                    DisconnectFromSources();
                }
            }
        }
    }

    abstract class ConstantReliabilityNotifier : INotifier {
        private ConstantReliabilityNotifier() { }

        static UnreliableNotifier _unreliableInstance = new UnreliableNotifier();
        static ConstantNotifier _constantInstance = new ConstantNotifier();

        public static ConstantReliabilityNotifier UnreliableInstance {
            get { return _unreliableInstance; }
        }

        public static ConstantReliabilityNotifier ConstantInstance {
            get { return _unreliableInstance; }
        }

        event Action INotifier.ValueChanged {
            add { }
            remove { }
        }

        event Action<bool> INotifier.ReliabilityChanged {
            add { }
            remove { }
        }

        sealed class UnreliableNotifier : ConstantReliabilityNotifier {
            public override bool IsReliable {
                get { return false; }
            }
        }

        sealed class ConstantNotifier : ConstantReliabilityNotifier {
            public override bool IsReliable {
                get { return true; }
            }
        }


        public abstract bool IsReliable { get; }
    }

    public sealed class PrivateNotifier : Notifier {
        INotifier _source;

        public PrivateNotifier() { }

        public PrivateNotifier(bool isReliable) : base(isReliable) { }

        public void SetReliability(bool value) {
            IsReliable = value;
        }

        public new void OnValueChanged() {
            base.OnValueChanged();
        }

        public INotifier Source {
            get { return _source; }
            set {
                if (IsConnected) {
                    DisconnectFromSources();
                }
                _source = value;
                if (IsConnected) {
                    ConnectToSources();
                }
            }
        }

        protected override void ConnectToSources() {
            if (_source != null) {
                _source.ValueChanged += OnValueChanged;
                _source.ReliabilityChanged += SetReliability;
                IsReliable = _source.IsReliable;
            }
        }

        protected override void DisconnectFromSources() {
            if (_source != null) {
                _source.ValueChanged -= OnValueChanged;
                _source.ReliabilityChanged -= SetReliability;
                IsReliable = false;
            }
        }
    }

    public sealed class ArrayNotifier : Notifier {
        bool[] _isReliable;
        int _unreliableCount;
        INotifier[] _sources;
        Action<bool>[] _reliabilityListeners;

        public ArrayNotifier(int size) {
            _isReliable = new bool[size];
            _unreliableCount = size;
            _sources = new INotifier[size];
            _reliabilityListeners = new Action<bool>[size];
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

        public INotifier this[int index] {
            get { return _sources[index]; }
            set {
                if (IsConnected) {
                    DisconnectFromSource(index, true);
                }
                _sources[index] = value;
                if (IsConnected) {
                    ConnectToSource(index);
                }
            }
        }

        void ConnectToSource(int index) {
            var notifier = _sources[index];
            if (notifier != null) {
                notifier.ValueChanged += OnValueChanged;
                if (_reliabilityListeners[index] == null) {
                    lock (_reliabilityListeners) {
                        if (_reliabilityListeners[index] == null) {
                            _reliabilityListeners[index] = (value) => SetReliability(index, value);
                        }
                    }
                }
                notifier.ReliabilityChanged += _reliabilityListeners[index];
                SetReliability(index, notifier.IsReliable);
            }
        }

        void DisconnectFromSource(int index, bool reconnecting = false) {
            var notifier = _sources[index];
            if (notifier != null) {
                notifier.ValueChanged -= OnValueChanged;
                notifier.ReliabilityChanged -= _reliabilityListeners[index];
                if (!reconnecting) {
                    SetReliability(index, false);
                }
            }
        }

        protected override void ConnectToSources() {
            for (int i = 0; i < _sources.Length; i++) {
                ConnectToSource(i);
            }
        }

        protected override void DisconnectFromSources() {
            for (int i = 0; i < _sources.Length; i++) {
                DisconnectFromSource(i);
            }
            IsReliable = false;
        }
    }
}
#endif
