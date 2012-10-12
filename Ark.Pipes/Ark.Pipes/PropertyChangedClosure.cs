using System;
using System.ComponentModel;

#if !NOTIFICATIONS_DISABLE
namespace Ark.Pipes {
    class PropertyChangedClosure : IEquatable<PropertyChangedEventHandler>, IEquatable<PropertyChangedClosure>, IEquatable<Action> {
        Object _sender;
        PropertyChangedEventHandler _handler;
        static PropertyChangedEventArgs _eventArgs = new PropertyChangedEventArgs("value");

        public PropertyChangedClosure(PropertyChangedEventHandler handler, object sender) {
            _handler = handler;
            _sender = sender;
        }

        public void Invoke() {
            _handler(_sender, _eventArgs);
        }

        public override bool Equals(object obj) {
            var action = obj as Action;
            if (action != null) {
                return Equals(action);
            }
            var closure = obj as PropertyChangedClosure;
            if (closure != null) {
                return Equals(closure);
            }
            var handler = obj as PropertyChangedEventHandler;
            if (handler != null) {
                return Equals(handler);
            }
            return false;
        }

        public override int GetHashCode() {
            return _handler.GetHashCode();
        }

        public bool Equals(PropertyChangedEventHandler other) {
            return _handler == other;
        }

        public bool Equals(PropertyChangedClosure other) {
            return _handler == other._handler;
        }

        public bool Equals(Action other) {
            var closure = other.Target as PropertyChangedClosure;
            return closure != null && closure._handler == _handler;
        }
    }
}
#endif