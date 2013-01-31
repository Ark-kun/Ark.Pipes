using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ark {
    public static class WeakExtensions {
        abstract class WeakDelegateWithDeregistration<TDelegate> : WeakDelegate<TDelegate> where TDelegate : class {
            Action<TDelegate> _unregister;

            protected WeakDelegateWithDeregistration(TDelegate handler, Action<TDelegate> unregister)
                : base(handler) {
                _unregister = unregister;
            }

            public abstract TDelegate Handler { get; }

            public static implicit operator TDelegate(WeakDelegateWithDeregistration<TDelegate> wh) {
                return wh.Handler;
            }

            public void Unregister() {
                if (_unregister != null) {
                    _unregister(Handler);
                    _unregister = null;
                }
            }
        }

        sealed class WeakEventHandler<TEventArgs> : WeakDelegateWithDeregistration<EventHandler<TEventArgs>>
            where TEventArgs : EventArgs {

            public WeakEventHandler(EventHandler<TEventArgs> eventHandler, Action<EventHandler<TEventArgs>> unregister)
                : base(eventHandler, unregister) {
            }

            public void Invoke(object sender, TEventArgs e) {
                if(!TryInvoke(sender, e)) {
                    Unregister();
                }
            }

            public override EventHandler<TEventArgs> Handler {
                get { return Invoke; }
            }
        }

        sealed class WeakAction : WeakDelegateWithDeregistration<Action> {
            public WeakAction(Action eventHandler, Action<Action> unregister)
                : base(eventHandler, unregister) {
            }

            public void Invoke() {
                if (!TryInvoke()) {
                    Unregister();
                }
            }

            public override Action Handler {
                get { return Invoke; }
            }
        }

        sealed class WeakAction<T> : WeakDelegateWithDeregistration<Action<T>> {

            public WeakAction(Action<T> eventHandler, Action<Action<T>> unregister)
                : base(eventHandler, unregister) {
            }

            public void Invoke(T arg) {
                if (!TryInvoke(arg)) {
                    Unregister();
                }
            }

            public override Action<T> Handler {
                get { return Invoke; }
            }
        }

        public static TDelegate Remove<TDelegate>(TDelegate eventHandlers, TDelegate handlerToRemove) where TDelegate : class {
            if (eventHandlers == null) {
                return null;
            }
            if (handlerToRemove == null) {
                return eventHandlers;
            }
            var delegateEventHandlers = eventHandlers as Delegate;
            if (delegateEventHandlers == null) {
                throw new ArgumentException("eventHandlers must have a delegate type.");
            }
            var delegateRemoveHandler = handlerToRemove as Delegate;
            if (delegateRemoveHandler == null) {
                throw new ArgumentException("handlerToRemove must have a delegate type.");
            }

            var handlersToRemove = new List<Delegate>();
            Delegate[] eventInvocationList = null;
            var removeInvocationList = delegateRemoveHandler.GetInvocationList();

            foreach (Action handler in removeInvocationList) {
                bool found = false;
                if (handler.IsSensibleToMakeWeak()) {
                    if (eventInvocationList == null) {
                        eventInvocationList = delegateEventHandlers.GetInvocationList();
                    }
                    foreach (var eventHandler in eventInvocationList) {
                        var weakEventHandler = eventHandler.Target as WeakDelegate<TDelegate>;
                        if (weakEventHandler != null && weakEventHandler.Equals(handler)) {
                            found = true;
                            handlersToRemove.Add(eventHandler);
                        }
                    }
                }
                if (!found) {
                    handlersToRemove.Add(handler);
                }
            }

            foreach (var handler in handlersToRemove) {
                delegateEventHandlers = System.Delegate.Remove(delegateEventHandlers, handler);
            }
            return delegateEventHandlers as TDelegate;
        }

        public static void RemoveFrom<TEventArgs>(this EventHandler<TEventArgs> handlerToRemove, ref EventHandler<TEventArgs> eventHandlers) where TEventArgs : EventArgs {
            eventHandlers = Remove(eventHandlers, handlerToRemove);
        }

        public static void RemoveFrom(this Action handlerToRemove, ref Action eventHandlers) {
            eventHandlers = Remove(eventHandlers, handlerToRemove);
        }

        public static void RemoveFrom<T>(this Action<T> handlerToRemove, ref Action<T> eventHandlers) {
            eventHandlers = Remove(eventHandlers, handlerToRemove);
        }

        public static bool IsSensibleToMakeWeak(this Delegate handler) {
            //Don't wrap static methods and lambdas with closures
            return !handler.Method.IsStatic && handler.Target != null && handler.Method.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length == 0;
        }

        public static bool IsStatic(this Delegate handler) {
            return handler.Method.IsStatic || handler.Target != null;
        }

        public static bool IsClosureMethod(this Delegate handler) {
            return handler.Method.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length == 0;
        }

        public static EventHandler<TEventArgs> Weaken<TEventArgs>(this EventHandler<TEventArgs> handlers, Action<EventHandler<TEventArgs>> unregister) where TEventArgs : EventArgs {
            EventHandler<TEventArgs> weakHandlers = null;
            var invocationList = handlers.GetInvocationList();

            foreach (EventHandler<TEventArgs> handler in invocationList) {
                if (handler.IsSensibleToMakeWeak()) {
                    weakHandlers += new WeakEventHandler<TEventArgs>(handler, unregister);
                } else {
                    weakHandlers += handler;
                }
            }

            return weakHandlers;
        }

        public static Action<T> Weaken<T>(this Action<T> handlers, Action<Action<T>> unregister) {
            Action<T> weakHandlers = null;
            var invocationList = handlers.GetInvocationList();

            foreach (Action<T> handler in invocationList) {
                if (handler.IsSensibleToMakeWeak()) {
                    weakHandlers += new WeakAction<T>(handler, unregister);
                } else {
                    weakHandlers += handler;
                }
            }

            return weakHandlers;
        }

        public static Action Weaken(this Action handlers, Action<Action> unregister) {
            Action weakHandlers = null;
            var invocationList = handlers.GetInvocationList();

            foreach (Action handler in invocationList) {
                if (handler.IsSensibleToMakeWeak()) {
                    weakHandlers += new WeakAction(handler, unregister);
                } else {
                    weakHandlers += handler;
                }
            }

            return weakHandlers;
        }
    }
}
