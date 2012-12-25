using Ark.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Ark {
    public static class WeakMulticastDelegate {
        public static WeakMulticastDelegate<TDelegate> Create<TDelegate>() where TDelegate : class {
            return new WeakMulticastDelegate<TDelegate>();
        }
    }

    public class WeakMulticastDelegate<TDelegate> : IEnumerable<WeakDelegate<TDelegate>> where TDelegate : class {
        SafeIndexedLinkedList<WeakDelegate<TDelegate>> _handlers = new SafeIndexedLinkedList<WeakDelegate<TDelegate>>(0);
        TDelegate _invokeHandler;

        static WeakMulticastDelegate() {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate))) {
                throw new InvalidOperationException("The TDelegate generic parameter of WeakMulticastDelegate must be a delegate type.");
            }
        }

        public WeakMulticastDelegate() {
            _invokeHandler = CreateInvokeHandler();
        }

        protected virtual TDelegate CreateInvokeHandler() {
            var invokeHandler = Delegate.CreateDelegate(typeof(TDelegate), this, "InvokeInternal") as TDelegate;
            if (invokeHandler == null) {
                throw new NotImplementedException(string.Format("Delegates of type {0} are not supported. Only delegates with 0-4 [generic] by-value parameters and no return value are supported.", typeof(TDelegate)));
            }
            return invokeHandler;
        }

        public void AddHandler(TDelegate handler) {
            if (handler != null) {
                _handlers.AddRange(handler.GetTypedInvocationList().Select(h => new WeakDelegate<TDelegate>(h)));
            }
        }

        public void RemoveHandler(TDelegate handler) {
            if (handler != null) {
                _handlers.RemoveRange(handler.GetTypedInvocationList().Select(h => new WeakDelegate<TDelegate>(h)));
            }
        }

        public void RemoveHandler(WeakDelegate<TDelegate> weakHandler) {
            if (weakHandler != null) {
                _handlers.Remove(weakHandler);
            }
        }

        public void RemoveAll(WeakDelegate<TDelegate> weakHandler) {
            if (weakHandler != null) {
                _handlers.RemoveAll(weakHandler);
            }
        }

        public void DynamicInvoke(object[] args) {
            foreach (var handler in this) {
                if (!handler.TryDynamicInvoke(args)) {
                    RemoveAll(handler); //TODO: Check that the right handlers are removed. FIX: Need to fix equality comparison/removal for dead delegates.
                }
            }
        }

        void InvokeInternal() {
            DynamicInvoke(null);
        }

        void InvokeInternal<T>(T arg) {
            DynamicInvoke(new object[] { arg });
        }

        void InvokeInternal<T1, T2>(T1 arg1, T2 arg2) {
            DynamicInvoke(new object[] { arg1, arg2 });
        }

        void InvokeInternal<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3) {
            DynamicInvoke(new object[] { arg1, arg2, arg3 });
        }

        void InvokeInternal<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            DynamicInvoke(new object[] { arg1, arg2, arg3, arg4 });
        }

        public TDelegate Invoke {
            get { return _invokeHandler; }
        }

        public IEnumerator<WeakDelegate<TDelegate>> GetEnumerator() {
            return _handlers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    //public sealed class WeakActions : WeakMulticastDelegate<Action> {
    //    public void Invoke() {
    //        DynamicInvoke(null);
    //    }
    //}

    //public sealed class WeakActions<T> : WeakMulticastDelegate<Action<T>> {
    //    public void Invoke(T arg) {
    //        DynamicInvoke(new object[] { arg });
    //    }
    //}

    //public sealed class WeakEventHandlers<TEventArgs> : WeakMulticastDelegate<EventHandler<TEventArgs>> where TEventArgs : EventArgs {
    //    public void Invoke(object sender, TEventArgs e) {
    //        DynamicInvoke(new object[] { sender, e });
    //    }
    //}
}
