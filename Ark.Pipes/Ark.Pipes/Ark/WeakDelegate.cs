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
            get { return CreateInvokeHandler(); }
        }

        protected virtual TDelegate CreateInvokeHandler() {
            string methodName = (_method.ReturnType == typeof(void)) ? "InvokeInternal" : "InvokeFunctionInternal";
            var invokeHandler = Delegate.CreateDelegate(typeof(TDelegate), this, methodName) as TDelegate;
            if (invokeHandler == null) {
                throw new NotImplementedException(string.Format("Delegates of type {0} are not supported. Only delegates with 0-4 [generic] by-value parameters are supported.", typeof(TDelegate)));
            }
            return invokeHandler;
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

        protected void InvokeInternal() {
            DynamicInvoke(null);
        }

        protected void InvokeInternal<T>(T arg) {
            DynamicInvoke(new object[] { arg });
        }

        protected void InvokeInternal<T1, T2>(T1 arg1, T2 arg2) {
            DynamicInvoke(new object[] { arg1, arg2 });
        }

        protected void InvokeInternal<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) {
            DynamicInvoke(new object[] { arg1, arg2, arg3 });
        }

        protected void InvokeInternal<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            DynamicInvoke(new object[] { arg1, arg2, arg3, arg4 });
        }

        protected TResult InvokeFunctionInternal<TResult>() {
            return (TResult)DynamicInvoke(new object[] { });
        }

        protected TResult InvokeFunctionInternal<T, TResult>(T arg) {
            return (TResult)DynamicInvoke(new object[] { arg });
        }

        protected TResult InvokeFunctionInternal<T1, T2, TResult>(T1 arg1, T2 arg2) {
            return (TResult)DynamicInvoke(new object[] { arg1, arg2 });
        }

        protected TResult InvokeFunctionInternal<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3) {
            return (TResult)DynamicInvoke(new object[] { arg1, arg2, arg3 });
        }

        protected TResult InvokeFunctionInternal<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            return (TResult)DynamicInvoke(new object[] { arg1, arg2, arg3, arg4 });
        }
    }
}
