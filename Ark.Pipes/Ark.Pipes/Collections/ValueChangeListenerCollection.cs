using Ark.Collections;
using System;

namespace Ark.Pipes.Collections {
    class ValueChangeListenerCollection : HoleyList<WeakReference<IValueChangeListener>>, IValueChangeListener {
        public void AddWeak(IValueChangeListener item) {
            base.Add(new WeakReference<IValueChangeListener>(item));
        }

        public bool Remove(IValueChangeListener item) {
            var references = _items;
            for (int i = 0; i < references.Length; i++) {
                var reference = references[i];
                if (!reference.IsNull()) {
                    IValueChangeListener target;
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
                    IValueChangeListener target;
                    if (reference.TryGetTarget(out target)) {
                        target.OnValueChanged();
                    } else {
                        base.Remove(reference, i);
                    }
                }
            }
        }
    }
}
