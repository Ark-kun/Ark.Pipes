using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ark {
    public abstract class WeakHandler<T> where T : class {
        protected WeakReference _targetReference;
        protected MethodInfo _method;
        Action<T> _unregister;

        protected WeakHandler(T eventHandler, Action<T> unregister) {
            var delegateHandler = eventHandler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _targetReference = new WeakReference(delegateHandler.Target);
            _method = delegateHandler.Method;
            _unregister = unregister;
        }

        public void Unregister() {
            if (_unregister != null) {
                _unregister(Handle);
                _unregister = null;
            }
        }

        public bool IsWrapperOf(Delegate handler) {
            return handler != null && handler.Method == _method && handler.Target == _targetReference.Target;
        }

        public abstract T Handle { get; }

        public static implicit operator T(WeakHandler<T> wh) {
            return wh.Handle;
        }

        public static void RemoveHandler(ref T eventHandlers, T handlerToRemove) {
            var delegateEventHandlers = eventHandlers as Delegate;
            if (delegateEventHandlers == null)
                throw new ArgumentException("Agrument 1 must have a delegate type.");
            var delegateRemoveHandler = handlerToRemove as Delegate;
            if (delegateRemoveHandler == null)
                throw new ArgumentException("Agrument 2 must have a delegate type.");

            var handlersToRemove = new List<Delegate>();
            Delegate[] eventInvocationList = null;
            var removeInvocationList = delegateRemoveHandler.GetInvocationList();

            foreach (Action handler in removeInvocationList) {
                bool found = false;
                if (handler.CanBeWeak()) {
                    if (eventInvocationList == null) {
                        eventInvocationList = delegateEventHandlers.GetInvocationList();
                    }
                    foreach (var eventHandler in eventInvocationList) {
                        var weakEventHandler = eventHandler.Target as WeakHandler<T>;
                        if (weakEventHandler != null && weakEventHandler.IsWrapperOf(handler)) {
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
                delegateEventHandlers = Delegate.Remove(delegateEventHandlers, handler);
            }
            eventHandlers = delegateEventHandlers as T;
        }
    }


    public sealed class WeakEventHandler<TEventArgs> : WeakHandler<EventHandler<TEventArgs>>
        where TEventArgs : EventArgs {

        public WeakEventHandler(EventHandler<TEventArgs> eventHandler, Action<EventHandler<TEventArgs>> unregister)
            : base(eventHandler, unregister) {
        }

        public void Invoke(object sender, TEventArgs e) {
            object target = _targetReference.Target;
            if (target != null) {
                _method.Invoke(target, new object[] { sender, e });
            } else {
                Unregister();
            }
        }

        public override EventHandler<TEventArgs> Handle {
            get { return Invoke; }
        }
    }

    public sealed class WeakActionEventHandler : WeakHandler<Action> {

        public WeakActionEventHandler(Action eventHandler, Action<Action> unregister)
            : base(eventHandler, unregister) {
        }

        public void Invoke() {
            object target = _targetReference.Target;
            if (target != null) {
                _method.Invoke(target, null);
            } else {
                Unregister();
            }
        }

        public override Action Handle {
            get { return Invoke; }
        }
    }

    public sealed class WeakActionEventHandler<T> : WeakHandler<Action<T>> {

        public WeakActionEventHandler(Action<T> eventHandler, Action<Action<T>> unregister)
            : base(eventHandler, unregister) {
        }

        public void Invoke(T arg) {
            object target = _targetReference.Target;
            if (target != null) {
                _method.Invoke(target, new object[] { arg });
            } else {
                Unregister();
            }
        }

        public override Action<T> Handle {
            get { return Invoke; }
        }
    }

    public static class WeakEventHelpers {
        public static void RemoveFrom<TEventArgs>(this EventHandler<TEventArgs> handlerToRemove, ref EventHandler<TEventArgs> eventHandlers) where TEventArgs : EventArgs {
            WeakHandler<EventHandler<TEventArgs>>.RemoveHandler(ref eventHandlers, handlerToRemove);
        }

        public static void RemoveFrom(this Action handlerToRemove, ref Action eventHandlers) {
            WeakHandler<Action>.RemoveHandler(ref eventHandlers, handlerToRemove);
        }

        public static void RemoveFrom<T>(this Action<T> handlerToRemove, ref Action<T> eventHandlers) {
            WeakHandler<Action<T>>.RemoveHandler(ref eventHandlers, handlerToRemove);
        }

        public static bool CanBeWeak(this Delegate handler) {
            //Don't wrap static methods and lambdas with closures
            return !handler.Method.IsStatic && handler.Target != null && handler.Method.DeclaringType.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Length == 0;
        }

        public static EventHandler<TEventArgs> Weaken<TEventArgs>(this EventHandler<TEventArgs> handlers, Action<EventHandler<TEventArgs>> unregister) where TEventArgs : EventArgs {
            EventHandler<TEventArgs> weakHandlers = null;
            var invocationList = handlers.GetInvocationList();

            foreach (EventHandler<TEventArgs> handler in invocationList) {
                if (handler.CanBeWeak()) {
                    weakHandlers += handler;
                } else {
                    weakHandlers += new WeakEventHandler<TEventArgs>(handler, unregister);
                }
            }

            return weakHandlers;
        }

        public static Action<T> Weaken<T>(this Action<T> handlers, Action<Action<T>> unregister) {
            Action<T> weakHandlers = null;
            var invocationList = handlers.GetInvocationList();

            foreach (Action<T> handler in invocationList) {
                if (handler.CanBeWeak()) {
                    weakHandlers += handler;
                } else {
                    weakHandlers += new WeakActionEventHandler<T>(handler, unregister);
                }
            }

            return weakHandlers;
        }

        public static Action Weaken(this Action handlers, Action<Action> unregister) {
            Action weakHandlers = null;
            var invocationList = handlers.GetInvocationList();

            foreach (Action handler in invocationList) {
                if (handler.CanBeWeak()) {
                    weakHandlers += handler;
                } else {
                    weakHandlers += new WeakActionEventHandler(handler, unregister);
                }
            }

            return weakHandlers;
        }
    }
}
