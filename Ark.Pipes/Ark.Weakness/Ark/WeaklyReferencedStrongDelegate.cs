using System;
using System.Reflection;

namespace Ark {
    class WeaklyReferencedStrongDelegate<TDelegate> : SingleDelegate<TDelegate> where TDelegate : class {
        WeakReference _handlerReference;

        public WeaklyReferencedStrongDelegate(TDelegate handler)
            : base(handler) {
            if ((object)handler == null) {
                throw new ArgumentNullException("handler must not be null");
            }
            var delegateHandler = handler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _handlerReference = new WeakReference(handler); //TODO: take only the first handler
        }

        public override object Target {
            get {
                var del = Delegate;
                return del != null ? del.Target : null;
            }
        }

        public override MethodInfo Method {
            get {
                var del = Delegate;
                return del != null ? del.Method : null;
            }
        }

        public override TDelegate Invoke {
            get { return (TDelegate)_handlerReference.Target; }
        }

        Delegate Delegate {
            get { return (Delegate)_handlerReference.Target; }
        }

        public override bool TryDynamicInvoke(object[] args, out object result) {
            result = DynamicInvoke(args);
            return true;
        }

        public override object DynamicInvoke(params object[] args) {
            return Delegate.DynamicInvoke(args);
        }
    }
}
