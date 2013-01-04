using System;
using System.Reflection;

namespace Ark {
    public class WeakDelegate<TDelegate> : SingleDelegate<TDelegate> where TDelegate : class {
        WeakReference _targetReference;
        MethodInfo _method;

        public WeakDelegate(TDelegate handler)
            : base(handler) { //Only the first handler is used if there are multiple handlers.
            if ((object)handler == null) {
                throw new ArgumentNullException("handler must not be null");
            }
            var delegateHandler = handler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _targetReference = new WeakReference(delegateHandler.Target);
            _method = delegateHandler.Method;
        }


        public override object Target {
            get { return _targetReference.Target; }
        }

        public override MethodInfo Method {
            get { return _method; }
        }

        public override TDelegate Invoke {
            get { return new DynamicInvokeAdapter<TDelegate>(DynamicInvoke).Invoke; }
        }

        bool TryDynamicInvoke(params object[] args) {
            object result;
            return TryDynamicInvoke(args, out result);
        }

        public override bool TryDynamicInvoke(object[] args, out object result) {
            object target = Target;
            if (target == null) {
                result = null;
                return false;
            }
            result = _method.Invoke(target, args);
            return true;
        }

        public override object DynamicInvoke(params object[] args) {
            object target = Target;
            if (target == null) {
                throw new InvalidOperationException("The delegate target is not alive.");
            }
            return _method.Invoke(target, args);
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

        protected bool TryInvoke<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) {
            return TryDynamicInvoke(new object[] { arg1, arg2, arg3 });
        }

        protected bool TryInvoke<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            return TryDynamicInvoke(new object[] { arg1, arg2, arg3, arg4 });
        }
    }
}
