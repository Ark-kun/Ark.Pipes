using Ark.Collections;
using Ark.Pipes;
using System;

namespace Ark.Pipes.Collections {
    class ProviderListenerCollection : HoleyList<WeakReference<IProviderListener>>, IProviderListener {
        public void AddWeak(IProviderListener item) {
            base.Add(new WeakReference<IProviderListener>(item));
        }

        public bool Remove(IProviderListener item) {
            var references = _items;
            for (int i = 0; i < references.Length; i++) {
                var reference = references[i];
                if (!reference.IsNull()) {
                    IProviderListener target;
                    if (reference.TryGetTarget(out target)) {
                        if (item.Equals(target)) {
                            if (base.Remove(reference, i)) {
                                return true;
                            }
                        }
                    } else {
                        base.Remove(reference, i);
                    }
                }
            }
            return false;
        }

        public void OnValueChanged() {
            var references = _items;
            for (int i = 0; i < references.Length; i++) {
                var reference = references[i];
                if (!reference.IsNull()) {
                    IProviderListener target;
                    if (reference.TryGetTarget(out target)) {
                        target.OnValueChanged();
                    } else {
                        base.Remove(reference, i);
                    }
                }
            }
        }

        public void OnStartedNotifying() {
            var references = _items;
            for (int i = 0; i < references.Length; i++) {
                var reference = references[i];
                if (!reference.IsNull()) {
                    IProviderListener target;
                    if (reference.TryGetTarget(out target)) {
                        target.OnStartedNotifying();
                    } else {
                        base.Remove(reference, i);
                    }
                }
            }
        }

        public void OnStoppedNotifying() {
            var references = _items;
            for (int i = 0; i < references.Length; i++) {
                var reference = references[i];
                if (!reference.IsNull()) {
                    IProviderListener target;
                    if (reference.TryGetTarget(out target)) {
                        target.OnStoppedNotifying();
                    } else {
                        base.Remove(reference, i);
                    }
                }
            }
        }
    }
}