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

    public class WeakMulticastDelegate<TDelegate> : IInvokable<TDelegate>, IDynamicInvokable, IEnumerable<SingleDelegate<TDelegate>> where TDelegate : class {
        ICollectionEx<SingleDelegate<TDelegate>> _handlers;
        TDelegate _invokeHandler;

        static WeakMulticastDelegate() {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate))) {
                throw new InvalidOperationException("The TDelegate generic parameter of WeakMulticastDelegate must be a delegate type.");
            }
        }

        public WeakMulticastDelegate() {
            _handlers = new SafeIndexedLinkedList<SingleDelegate<TDelegate>>(0);
            _invokeHandler = new DynamicInvokeAdapter<TDelegate>(DynamicInvoke).Invoke;
        }

        public void AddHandler(TDelegate handler) {
            if (handler != null) {
                if (((Delegate)(object)handler).IsStatic()) {
                    AddHandlerStrongly(handler);
                } else {
                    AddHandlerWeakly(handler);
                }
            }
        }

        public void AddHandlerWeakly(TDelegate handler) {
            if (handler != null) {
                AddHandlers(handler.GetTypedInvocationList().Select(h => (SingleDelegate<TDelegate>)new WeakDelegate<TDelegate>(h)));
            }
        }

        public void AddHandlerStrongly(TDelegate handler) {
            if (handler != null) {
                AddHandlers(handler.GetTypedInvocationList().Select(h => (SingleDelegate<TDelegate>)new StrongDelegate<TDelegate>(h)));
            }
        }

        public void AddStrongHandlerWeakly(TDelegate handler) {
            if (handler != null) {
                AddHandlers(handler.GetTypedInvocationList().Select(h => (SingleDelegate<TDelegate>)new WeaklyReferencedStrongDelegate<TDelegate>(h)));
            }
        }

        public void AddHandler(SingleDelegate<TDelegate> singleHandler) {
            if (singleHandler != null) {
                _handlers.Add(singleHandler);
            }
        }

        public void AddHandler(WeakMulticastDelegate<TDelegate> weakHandlers) {
            if (weakHandlers != null) {
                AddHandlers(weakHandlers);
            }
        }

        public void AddHandlers(IEnumerable<SingleDelegate<TDelegate>> handlers) {
            if (handlers != null) {
                _handlers.AddRange(handlers);
            }
        }

        public void RemoveHandler(TDelegate handler) {
            if (handler != null) {
                RemoveHandlers(handler.GetTypedInvocationList().Select(h => (SingleDelegate<TDelegate>)new StrongDelegate<TDelegate>(h)));
            }
        }

        public void RemoveHandler(SingleDelegate<TDelegate> singleHandler) {
            if (singleHandler != null) {
                _handlers.Remove(singleHandler);
            }
        }

        public void RemoveHandlers(IEnumerable<SingleDelegate<TDelegate>> handlers) {
            if (handlers != null) {
                _handlers.RemoveRange(handlers);
            }
        }

        object DynamicInvokeInternal(object[] args) {
            object result = null;
            foreach (var handler in _handlers) {
                var dynamicInvoker = handler.TryGetDynamicInvoker();
                if (dynamicInvoker != null) {
                    result = dynamicInvoker(args);
                } else {
                    var removeAllCollection = _handlers as ICanRemoveAll<SingleDelegate<TDelegate>>;
                    if (removeAllCollection != null) {
                        removeAllCollection.RemoveAll(handler);
                    } else {
                        RemoveHandler(handler);
                    }
                }
            }
            return result;
        }

        protected virtual TDelegate TryGetInvoker() {
            return _invokeHandler;
        }

        protected virtual Func<object[], object> TryGetDynamicInvoker() {
            return DynamicInvokeInternal;
        }

        public object DynamicInvoke(object[] args) {
            var dynamicInvoker = TryGetDynamicInvoker();
            return dynamicInvoker(args);
        }

        public TDelegate Invoke {
            get { return TryGetInvoker(); }
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
