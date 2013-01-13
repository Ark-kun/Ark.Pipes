using System;
using System.Reflection;

namespace Ark {
    public abstract class SingleDelegate<TDelegate> : IEquatable<SingleDelegate<TDelegate>>, IEquatable<TDelegate> where TDelegate : class {
        static InvalidOperationException _targetDeadException = new InvalidOperationException("The delegate target is no longer available.");
        int _hashCode;

        static SingleDelegate() {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate))) {
                throw new InvalidOperationException("The TDelegate generic parameter of must be a delegate type.");
            }
        }

        public SingleDelegate(TDelegate handler) { //Only the first handler is used if there are multiple handlers.
            if ((object)handler == null) {
                throw new ArgumentNullException("handler must not be null");
            }
            var delegateHandler = handler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _hashCode = delegateHandler.GetGoodHashCode(); //TODO: take only the first handler
        }

        public abstract MethodInfo Method { get; }

        public abstract object Target { get; }

        public abstract TDelegate TryGetInvoker();

        public abstract Func<object[], object> TryGetDynamicInvoker();

        public TDelegate Invoke {
            get {
                var invoker = TryGetInvoker();
                if (invoker == null) {
                    throw _targetDeadException;
                }
                return invoker;
            }
        }

        public object DynamicInvoke(params object[] args) {
            var dynamicInvoker = TryGetDynamicInvoker();
            if (dynamicInvoker == null) {
                throw _targetDeadException;
            }
            return dynamicInvoker(args);
        }

        public bool TryDynamicInvoke(object[] args, out object result) {
            var dynamicInvoker = TryGetDynamicInvoker();
            if (dynamicInvoker == null) {
                result = null;
                return false;
            }
            result = dynamicInvoker(args);
            return true;
        }

        public override int GetHashCode() {
            return _hashCode;
        }

        public override bool Equals(object obj) {
            var singleDelegate = obj as SingleDelegate<TDelegate>;
            if ((object)singleDelegate != null) {
                return Equals(singleDelegate);
            }
            var normalDelegate = obj as TDelegate;
            if ((object)normalDelegate != null) {
                return Equals(normalDelegate);
            }
            return false;
        }

        public bool Equals(SingleDelegate<TDelegate> other) {
            return (object)other != null && other._hashCode == _hashCode && other.Method == Method && ReferencesAreEqualAndNotNull(other.Target, Target);
        }

        public bool Equals(TDelegate other) {
            var otherDelegate = other as Delegate;
            return (object)otherDelegate != null && otherDelegate.GetGoodHashCode() == _hashCode && otherDelegate.Method == Method && ReferencesAreEqualAndNotNull(otherDelegate.Target, Target);
        }

        public static bool operator ==(SingleDelegate<TDelegate> left, SingleDelegate<TDelegate> right) {
            return ((object)left == null && (object)right == null) || left.Equals(right);
        }

        public static bool operator !=(SingleDelegate<TDelegate> left, SingleDelegate<TDelegate> right) {
            return !(left == right);
        }

        static bool ReferencesAreEqualAndNotNull(object obj1, object obj2) {
            return obj1 != null && obj2 != null && Object.ReferenceEquals(obj1, obj2);
        }
    }
}
