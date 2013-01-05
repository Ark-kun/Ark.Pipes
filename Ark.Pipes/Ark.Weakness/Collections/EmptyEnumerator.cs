using System;
using System.Collections.Generic;

namespace Ark.Collections {
    struct EmptyEnumerator<T> : IEnumerator<T> {
        static EmptyEnumerator<T> _instance = new EmptyEnumerator<T>();

        public T Current {
            get { throw new InvalidOperationException("EmptyEnumerator.Current was called."); }
        }

        public void Dispose() { }

        object System.Collections.IEnumerator.Current {
            get { return Current; }
        }

        public bool MoveNext() {
            return false;
        }

        public void Reset() { }

        public static EmptyEnumerator<T> Instance {
            get { return _instance; }
        }
    }
}
