using Ark.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ark {
    public static class WeakMulticastDelegate {
        public static WeakMulticastDelegate<TDelegate> Create<TDelegate>() where TDelegate : class {
            return new WeakMulticastDelegate<TDelegate>();
        }
    }

    public class WeakMulticastDelegate<TDelegate> : IEnumerable<SingleDelegate<TDelegate>> where TDelegate : class {
        SafeIndexedLinkedList<SingleDelegate<TDelegate>> _handlers = new SafeIndexedLinkedList<SingleDelegate<TDelegate>>(0);
        TDelegate _invokeHandler;

        static WeakMulticastDelegate() {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate))) {
                throw new InvalidOperationException("The TDelegate generic parameter of WeakMulticastDelegate must be a delegate type.");
            }
        }

        public WeakMulticastDelegate() {
            _invokeHandler = new DynamicInvokeAdapter<TDelegate>(DynamicInvoke).Invoke;
        }

        public void AddHandler(TDelegate handler) {
            if (handler != null) {
                _handlers.AddRange(handler.GetTypedInvocationList().Select(h => new WeakDelegate<TDelegate>(h)));
            }
        }

        public void AddHandlerStrong(TDelegate handler) {
            if (handler != null) {
                _handlers.AddRange(handler.GetTypedInvocationList().Select(h => new StrongDelegate<TDelegate>(h)));
            }
        }

        public void AddHandler(SingleDelegate<TDelegate> singleHandler) {
            if (singleHandler != null) {
                _handlers.Add(singleHandler);
            }
        }

        public void AddHandler(WeakMulticastDelegate<TDelegate> weakHandlers) {
            if (weakHandlers != null) {
                _handlers.AddRange(weakHandlers);
            }
        }

        public void RemoveHandler(TDelegate handler) {
            if (handler != null) {
                _handlers.RemoveRange(handler.GetTypedInvocationList().Select(h => new StrongDelegate<TDelegate>(h)));
            }
        }

        public void RemoveHandler(SingleDelegate<TDelegate> singleHandler) {
            if (singleHandler != null) {
                _handlers.Remove(singleHandler);
            }
        }

        public void RemoveAll(TDelegate handler) {
            if (handler != null) {
                foreach (var del in handler.GetTypedInvocationList()) {
                    _handlers.RemoveAll(new WeakDelegate<TDelegate>(del));
                }
            }
        }

        public void RemoveAll(SingleDelegate<TDelegate> singleHandler) {
            if (singleHandler != null) {
                _handlers.RemoveAll(singleHandler);
            }
        }

        public object DynamicInvoke(object[] args) {
            object result = null;
            foreach (var handler in this) {
                if (!handler.TryDynamicInvoke(args, out result)) {
                    _handlers.RemoveAll(handler); //TODO: Check that the right handlers are removed. FIX: Need to fix equality comparison/removal for dead delegates.
                }
            }
            return result;
        }

        public TDelegate Invoke {
            get { return _invokeHandler; }
        }

        public IEnumerator<SingleDelegate<TDelegate>> GetEnumerator() {
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
