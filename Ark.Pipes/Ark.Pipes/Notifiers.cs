using Ark.Pipes.Collections;
using System;
using System.Collections.Generic;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    public abstract class Notifier : INotifier {
        bool _isReliable = false;
        Action _valueChanged;
        ValueChangeListenerCollection _valueChangeListeners = new ValueChangeListenerCollection();
        ProviderListenerCollection _providerListeners = new ProviderListenerCollection();

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

        protected void SignalValueChanged() {
            var handler = _valueChanged;
            if (handler != null) {
                handler.Invoke();
            }
            _valueChangeListeners.OnValueChanged();
            _providerListeners.OnValueChanged();
        }

        private void SignalStartedNotifying() {
            _providerListeners.OnStartedNotifying();
        }

        private void SignalStoppedNotifying() {
            _providerListeners.OnStoppedNotifying();
        }

        public bool IsNotifying {
            get {
                return _isReliable;
            }
            protected set {
                if (value != _isReliable) {
                    _isReliable = value;
                    if (_isReliable) {
                        SignalStartedNotifying();
                    } else {
                        SignalStoppedNotifying();
                    }
                }
            }
        }

        protected bool IsConnected {
            get {
                return _valueChanged != null || _valueChangeListeners.Count > 0 || _providerListeners.Count > 0;
            }
        }

        protected abstract void ConnectToSources();

        protected abstract void DisconnectFromSources();

        public event Action ValueChanged {
            add {
                if (!IsConnected) {
                    ConnectToSources();
                }
                _valueChanged += value.Weaken(h => _valueChanged -= h);
            }
            remove {
                value.RemoveFrom(ref _valueChanged);
                if (!IsConnected) {
                    DisconnectFromSources();
                }
            }
        }

        public void AddListener(IValueChangeListener listener) {
            if (!IsConnected) {
                ConnectToSources();
            }
            _valueChangeListeners.AddWeak(listener);
        }

        public void AddListener(IProviderListener listener) {
            if (!IsConnected) {
                ConnectToSources();
            }
            //We're not adding the listener to _valueChangeListeners. Hard to decide, but this way each listener is in a single list.
            _providerListeners.AddWeak(listener);
        }

        public void RemoveListener(IValueChangeListener listener) {
            _valueChangeListeners.Remove(listener);
            if (!IsConnected) {
                DisconnectFromSources();
            }
        }

        public void RemoveListener(IProviderListener listener) {
            _providerListeners.Remove(listener);
            if (!IsConnected) {
                DisconnectFromSources();
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
            get { return _constantInstance; }
        }

        event Action INotifier.ValueChanged {
            add { }
            remove { }
        }

        public void AddListener(IValueChangeListener listener) { }
        public void AddListener(IProviderListener listener) { }
        public void RemoveListener(IValueChangeListener listener) { }
        public void RemoveListener(IProviderListener listener) { }

        sealed class UnreliableNotifier : ConstantReliabilityNotifier {
            public override bool IsNotifying {
                get { return false; }
            }
        }

        sealed class ConstantNotifier : ConstantReliabilityNotifier {
            public override bool IsNotifying {
                get { return true; }
            }
        }


        public abstract bool IsNotifying { get; }
    }

    public class PrivateNotifier : Notifier, IProviderListener {
        INotifier _source;

        public PrivateNotifier() { }

        public PrivateNotifier(bool isReliable) : base(isReliable) { }

        public void SetReliability(bool value) {
            IsNotifying = value;
        }

        public new virtual void SignalValueChanged() {
            base.SignalValueChanged();
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
                _source.AddListener(this);

                IsNotifying = _source.IsNotifying;
            }
        }

        protected override void DisconnectFromSources() {
            if (_source != null) {
                _source.RemoveListener(this);
                IsNotifying = false;
            }
        }

        void IValueChangeListener.OnValueChanged() {
            SignalValueChanged();
        }

        void IProviderListener.OnStartedNotifying() {
            SetReliability(true);
        }

        void IProviderListener.OnStoppedNotifying() {
            SetReliability(false);
        }
    }

    public sealed class PausablePrivateNotifier : PrivateNotifier {
        bool _paused = false;

        public void Pause() {
            _paused = true;
        }

        public void Unpause() {
            _paused = false;
        }

        public override void SignalValueChanged() {
            if (!_paused) {
                base.SignalValueChanged();
            }
        }
    }

    public sealed class ArrayNotifier : Notifier {
        bool[] _isReliable;
        int _unreliableCount;
        INotifier[] _sources;
        Retranslator[] _retranslators;

        class Retranslator : IProviderListener {
            ArrayNotifier _parent;
            int _index;

            public Retranslator(ArrayNotifier parent, int index) {
                _parent = parent;
                _index = index;
            }

            public void OnValueChanged() {
                _parent.SignalValueChanged();
            }

            public void OnStartedNotifying() {
                _parent.SetReliable(_index);
            }

            public void OnStoppedNotifying() {
                _parent.SetUnreliable(_index);
            }
        }

        public ArrayNotifier(int size) {
            _isReliable = new bool[size];
            _unreliableCount = size;
            _sources = new INotifier[size];
            _retranslators = new Retranslator[size];
            for (int index = 0; index < _retranslators.Length; index++) {
                _retranslators[index] = new Retranslator(this, index);
            }
        }

        public void SetReliable(int index) {
            if (index >= _isReliable.Length)
                throw new ArgumentOutOfRangeException();

            if (!_isReliable[index]) {
                _unreliableCount--;
                if (_unreliableCount == 0) {
                    IsNotifying = true;
                }
            }
        }

        public void SetUnreliable(int index) {
            if (index >= _isReliable.Length)
                throw new ArgumentOutOfRangeException();

            if (_isReliable[index]) {
                _unreliableCount++;
                IsNotifying = false;
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
                notifier.AddListener(_retranslators[index]);
                if (notifier.IsNotifying) {
                    SetReliable(index);
                } else {
                    SetUnreliable(index);
                }
            }
        }

        void DisconnectFromSource(int index, bool reconnecting = false) {
            var notifier = _sources[index];
            if (notifier != null) {
                notifier.RemoveListener(_retranslators[index]);
                if (!reconnecting) { //!
                    if (notifier.IsNotifying) {
                        SetReliable(index);
                    } else {
                        SetUnreliable(index);
                    }
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
            IsNotifying = false;
        }
    }
}
#endif
