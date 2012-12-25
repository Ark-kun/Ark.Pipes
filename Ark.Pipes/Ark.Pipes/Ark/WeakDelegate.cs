using System;
using System.Reflection;

namespace Ark {
    public class WeakDelegate<TDelegate> : IEquatable<WeakDelegate<TDelegate>>, IEquatable<TDelegate> where TDelegate : class {
        WeakReference _targetReference;
        MethodInfo _method;
        int _hashCode;

        public WeakDelegate(TDelegate handler) { //Only the first handler is used if there are multiple handlers.
            if ((object)handler == null) {
                throw new ArgumentNullException("handler must not be null");
            }
            var delegateHandler = handler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _targetReference = new WeakReference(delegateHandler.Target);
            _method = delegateHandler.Method;
            _hashCode = delegateHandler.GetGoodHashCode();
        }

        public bool TryDynamicInvoke(object[] args) {
            object target = _targetReference.Target;
            if (target == null) {
                return false;
            }
            _method.Invoke(target, args);
            return true;
        }

        protected bool TryInvoke() {
            return TryDynamicInvoke(null);
        }

        protected bool TryInvoke<T>(T arg) {
            return TryDynamicInvoke(new object[] { arg });
        }

        protected bool TryInvoke<T1, T2>(T1 arg1, T2 arg2) {
            return TryDynamicInvoke(new object[] { arg1, arg2 });
        }

        public override int GetHashCode() {
            return _hashCode;
        }

        public override bool Equals(object obj) {
            var weakDelegate = obj as WeakDelegate<TDelegate>;
            if ((object)weakDelegate != null) {
                return Equals(weakDelegate);
            }
            var strongDelegate = obj as TDelegate;
            if ((object)strongDelegate != null) {
                return Equals(strongDelegate);
            }
            return false;
        }

        public bool Equals(WeakDelegate<TDelegate> other) {
            return (object)other != null && other._hashCode == _hashCode && other._method == _method && ReferencesAreEqualAndNotNull(other._targetReference.Target, _targetReference.Target);
        }

        public bool Equals(TDelegate other) {
            var otherDelegate = other as Delegate;
            return (object)otherDelegate != null && otherDelegate.GetGoodHashCode() == _hashCode && otherDelegate.Method == _method && ReferencesAreEqualAndNotNull(otherDelegate.Target, _targetReference.Target);
        }

        public static bool operator ==(WeakDelegate<TDelegate> left, WeakDelegate<TDelegate> right) {
            return ((object)left == null && (object)right == null) || left.Equals(right);
        }

        public static bool operator !=(WeakDelegate<TDelegate> left, WeakDelegate<TDelegate> right) {
            return !(left == right);
        }

        static bool ReferencesAreEqualAndNotNull(object obj1, object obj2) {
            return obj1 != null && obj2 != null && Object.ReferenceEquals(obj1, obj2);
        }
    }
}
