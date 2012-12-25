using System;
using System.Reflection;

namespace Ark {
    class StrongDelegate<TDelegate> : SingleDelegate<TDelegate> where TDelegate : class {
        TDelegate _handler;

        public StrongDelegate(TDelegate handler)
            : base(handler) {
            if ((object)handler == null) {
                throw new ArgumentNullException("handler must not be null");
            }
            var delegateHandler = handler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _handler = handler; //TODO: take only the first handler
        }

        public override object Target {
            get { return Delegate.Target; }
        }

        public override MethodInfo Method {
            get { return Delegate.Method; }
        }

        public override TDelegate Invoke {
            get { return _handler; }
        }

        Delegate Delegate {
            get { return _handler as Delegate; }
        }

        public override bool TryDynamicInvoke(object[] args) {
            Delegate.DynamicInvoke(args);
            return true;
        }
    }
}
