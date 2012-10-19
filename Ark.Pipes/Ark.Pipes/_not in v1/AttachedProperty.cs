using System.Collections.Generic;

namespace Ark.Pipes {
    //Canvas.Left[button1] = 13
    //button1[Canvas.Left] = 13 //?
    //TODO:Change notification
    //FIX: memory leak
    public sealed class AttachedProperty<T> {
        Dictionary<object, Provider<T>> _store = new Dictionary<object, Provider<T>>();

        public Provider<T> this[object obj] {
            get { return _store[obj]; }
            set { _store[obj] = value; }
        }
    }
}
