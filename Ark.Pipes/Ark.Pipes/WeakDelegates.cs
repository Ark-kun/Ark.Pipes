﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ark {
    //IEquatable<WeakHandler<T>>
    public abstract class WeakDelegate<TDelegate> where TDelegate : class {
        protected WeakReference _targetReference;
        protected MethodInfo _method;
        Action<TDelegate> _unregister;

        protected WeakDelegate(TDelegate eventHandler, Action<TDelegate> unregister) {
            var delegateHandler = eventHandler as Delegate;
            if (delegateHandler == null)
                throw new ArgumentException("Agrument must have a delegate type.");

            _targetReference = new WeakReference(delegateHandler.Target);
            _method = delegateHandler.Method;
            _unregister = unregister;
        }

        public void Unregister() {
            if (_unregister != null) {
                _unregister(Handler);
                _unregister = null;
            }
        }

        public bool IsWrapperOf(Delegate handler) {
            return handler != null && handler.Method == _method && handler.Target == _targetReference.Target; //ReferenceEquals?
        }

        public abstract TDelegate Handler { get; }

        public static implicit operator TDelegate(WeakDelegate<TDelegate> wh) {
            return wh.Handler;
        }
    }


    public sealed class WeakEventHandler<TEventArgs> : WeakDelegate<EventHandler<TEventArgs>>
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

        public override EventHandler<TEventArgs> Handler {
            get { return Invoke; }
        }
    }

    public sealed class WeakAction : WeakDelegate<Action> {

        public WeakAction(Action eventHandler, Action<Action> unregister)
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

        public override Action Handler {
            get { return Invoke; }
        }
    }

    public sealed class WeakAction<T> : WeakDelegate<Action<T>> {

        public WeakAction(Action<T> eventHandler, Action<Action<T>> unregister)
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

        public override Action<T> Handler {
            get { return Invoke; }
        }
    }

    public static class WeakDelegate {
        public static TDelegate Remove<TDelegate>(TDelegate eventHandlers, TDelegate handlerToRemove) where TDelegate : class {
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
                if (handler.IsSensibleToMakeWeak()) {
                    if (eventInvocationList == null) {
                        eventInvocationList = delegateEventHandlers.GetInvocationList();
                    }
                    foreach (var eventHandler in eventInvocationList) {
                        var weakEventHandler = eventHandler.Target as WeakDelegate<TDelegate>;
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