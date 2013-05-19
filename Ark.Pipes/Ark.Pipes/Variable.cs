using System;

namespace Ark.Pipes {
    public sealed class Variable<T> :
#if NOTIFICATIONS_DISABLE
        Provider<T>
#else
        ProviderWithNotifier<T>
#endif
        , IIn<T>
    {
        T _value;

        public Variable() {
            _value = default(T);
        }

        public Variable(T value) {
                            _value = value;
#if !NOTIFICATIONS_DISABLE
                _notifier.SetReliability(true);
#endif
        }

        public new T Value {
            get { return GetValue(); }
            set { SetValue(value);
            }
        }

        public override T GetValue() {
            return _value;
        }

        public void SetValue(T value) {
            _value = value;
#if !NOTIFICATIONS_DISABLE
            _notifier.SignalValueChanged();
#endif
        }

        public Provider<T> AsReadOnly() {
            return new ReadOnlyVariable(this);
        }

        sealed class ReadOnlyVariable : Provider<T> {
            Variable<T> _parent;

            public ReadOnlyVariable(Variable<T> parent) {
                _parent = parent;
            }

            public override T GetValue() {
                return _parent._value;
            }

#if !NOTIFICATIONS_DISABLE
            public override INotifier Notifier {
                get { return _parent.Notifier; }
            }
#endif
        }
    }
}