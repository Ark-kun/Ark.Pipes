using System;

namespace System {
    class WeakReference<T> : WeakReference {
        public WeakReference(T target)
            : base(target) {
        }

        public bool TryGetTarget(out T target) {
            T local = Target;
            target = local;
            return (local != null);
        }

        public void SetTarget(T target) {
            Target = target;
        }

        private new T Target {
            get { return (T)base.Target; }
            set { base.Target = value; }
        }
    }
}