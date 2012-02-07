using System.Collections.Generic;

namespace Ark.Pipes {

    //Canvas.Left[button1] = 13
    //button1[Canvas.Left] = 13 //?
    public class NotifyingAttachedProperty<T> {
        Dictionary<object, NotifyingProvider<T>> _store = new Dictionary<object, NotifyingProvider<T>>();

        public NotifyingProvider<T> this[object obj] {
            get { return _store[obj]; }
            set { _store[obj] = value; }
        }
    }
}
