using Ark.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ark {
    abstract class FastMulticastDelegate<THandler> : IEnumerable<THandler> where THandler : class {
        SafeIndexedLinkedList<THandler> _handlers = new SafeIndexedLinkedList<THandler>(0);

        public void AddHandler(THandler handlers) {
            _handlers.AddRange(handlers.GetTypedInvocationList());
        }

        public void AddHandlers(IEnumerable<THandler> handlers) {
            _handlers.AddRange(handlers.SelectMany(h => h.GetTypedInvocationList()));
        }

        public void RemoveHandler(THandler handlers) {
            _handlers.RemoveRange(handlers.GetTypedInvocationList());
        }

        public IEnumerator<THandler> GetEnumerator() {
            return _handlers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    class FastMulticastAction : FastMulticastDelegate<Action> {
        public void Invoke() {
            foreach (var handler in this) {
                handler();
            }
        }
    }

    class FastMulticastAction<T> : FastMulticastDelegate<Action<T>> {
        public void Invoke(T arg) {
            foreach (var handler in this) {
                handler(arg);
            }
        }
    }

    class FastMulticastEventHandler<TEventArgs> : FastMulticastDelegate<EventHandler<TEventArgs>> where TEventArgs : EventArgs {
        public void Invoke(object sender, TEventArgs e) {
            foreach (var handler in this) {
                handler(sender, e);
            }
        }
    }
}
