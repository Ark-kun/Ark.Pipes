using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ark.Collections;

namespace Ark {
    class WeakEvent<TDelegate> {
        WeakCollection<TDelegate> _handlers;

        public WeakEvent() {
            if (!typeof(Delegate).IsAssignableFrom(typeof(TDelegate)))
                throw new ArgumentException("TDelegate must be a delegate type.");
            //_handlers = new WeakCollection<TDelegate>((h) => WeakDelegate.Weaken(h), null);
        }

        public void Add(TDelegate handler) {
        }

        public void AddStrong(TDelegate handler) {
        }

        public void AddWeak(TDelegate handler) {
        }

        public void Remove(TDelegate handler) {
        }
    }
}
