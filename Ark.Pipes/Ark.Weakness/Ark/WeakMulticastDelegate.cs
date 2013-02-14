using Ark.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ark {
    public static class WeakMulticastDelegate {
        public static WeakMulticastDelegate<TDelegate> Create<TDelegate>() where TDelegate : class {
            Type delType = typeof(TDelegate);
            if (delType == typeof(Action)) {
                return new WeakActions() as WeakMulticastDelegate<TDelegate>;
            }
            if (delType.IsGenericType) { 
                Type genericType = delType.GetGenericTypeDefinition();
                Type[] genericArguments = delType.GetGenericArguments();
                if (genericType == typeof(Action<>)) {
                    return (WeakMulticastDelegate<TDelegate>)Activator.CreateInstance(typeof(WeakActions<>).MakeGenericType(genericArguments));
                }
                if (genericType == typeof(EventHandler<>)) {
                    return (WeakMulticastDelegate<TDelegate>)Activator.CreateInstance(typeof(WeakEventHandlers<>).MakeGenericType(genericArguments));
                }
            }
            return new WeakMulticastDelegate<TDelegate>();
        }

        sealed class WeakActions : WeakMulticastDelegate<Action> {
            public WeakActions() {
                _invokeHandler = Invoke;
            }

            public void Invoke() {
                foreach (var handler in GetHandlers()) {
                    handler.Invoke();
                }
            }
        }

        sealed class WeakActions<T> : WeakMulticastDelegate<Action<T>> {
            public WeakActions() {
                _invokeHandler = Invoke;
            }

            public void Invoke(T arg) {
                foreach (var handler in GetHandlers()) {
                    handler.Invoke(arg);
                }
            }
        }

        sealed class WeakEventHandlers<TEventArgs> : WeakMulticastDelegate<EventHandler<TEventArgs>> where TEventArgs : EventArgs {
            public WeakEventHandlers() {
                _invokeHandler = Invoke;
            }

            public void Invoke(object sender, TEventArgs e) {
                foreach (var handler in GetHandlers()) {
                    handler.Invoke(sender, e);
                }
            }
        }
    }

    public class WeakMulticastDelegate<TDelegate> : IInvokable<TDelegate>, IDynamicInvokable, IEnumerable<SingleDelegate<TDelegate>> where TDelegate : class {
        ICollectionEx<SingleDelegate<TDelegate>> _handlers;
        protected TDelegate _invokeHandler;

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
                    RemoveHandler(handler);
                }
            }
            return result;
        }

        TDelegate IInvokable<TDelegate>.TryGetInvoker() {
            return TryGetInvokerInternal();
        }

        Func<object[], object> IDynamicInvokable.TryGetDynamicInvoker() {
            return TryGetDynamicInvokerInternal();
        }

        protected Func<object[], object> TryGetDynamicInvokerInternal() {
            return DynamicInvokeInternal;
        }

        protected TDelegate TryGetInvokerInternal() {
            return _invokeHandler;
        }

        public object DynamicInvoke(object[] args) {
            var dynamicInvoker = TryGetDynamicInvokerInternal();
            return dynamicInvoker(args);
        }

        public TDelegate Invoker {
            get { return TryGetInvokerInternal(); }
        }

        public IEnumerable<TDelegate> GetHandlers() {
            foreach (var handler in _handlers) {
                var invoker = handler.TryGetInvoker();
                if (invoker != null) {
                    yield return invoker;
                } else {
                    RemoveHandler(handler);
                }
            }
        }

        public IEnumerator<SingleDelegate<TDelegate>> GetEnumerator() {
            return _handlers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
