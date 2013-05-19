using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ark.Collections {
    class HoleyList<T> : ICollection<T> where T : class {
        static T[] _emptyArray = new T[0];
        protected T[] _items;
        int _count;

        object _modificationLocker = new object();

        public HoleyList() {
            _items = _emptyArray;
        }

        public HoleyList(int capacity) {
            if (capacity < 0) {
                throw new ArgumentOutOfRangeException("capacity");
            }
            if (capacity == 0) {
                _items = _emptyArray;
            } else {
                _items = new T[capacity];
            }
        }

        public HoleyList(IEnumerable<T> items) {
            _items = items.ToArray();
            _count = _items.Length;
        }

        public void Add(T item) {
            lock (_modificationLocker) {
                EnsureCapacity(_count + 1);
                for (int i = 0; i < _items.Length; i++) {
                    if (_items[i].IsNull()) {
                        _items[i] = item;
                        _count++;
                        break;
                    }
                }
            }
        }

        public void AddRange(IEnumerable<T> items) {
            lock (_modificationLocker) {
                var list = items as IList<T>;
                if (list != null) {
                    EnsureCapacity(_count + list.Count);
                }
                var en = items.GetEnumerator();
                if (en.MoveNext()) {
                    EnsureCapacity(_count + 1);
                    for (int i = 0; i < _items.Length; i++) {
                        if (_items[i].IsNull()) {
                            _items[i] = en.Current;
                            _count++;
                            if (!en.MoveNext()) {
                                break;
                            }
                            if (_count == _items.Length) {
                                EnsureCapacity(_count + 1);
                                i = -1;
                            }
                        }
                    }
                }
            }
        }

        public bool Remove(T item) {
            if (item == null) {
                throw new ArgumentNullException("item");
            }
            lock (_modificationLocker) {
                for (int i = 0; i < _items.Length; i++) {
                    if (item.Equals(_items[i])) {
                        _items[i] = null;
                        _count--;
                        return true;
                    }
                }
                return false;
            }
        }

        protected bool Remove(T item, int index) {
            //return object.ReferenceEquals(item, Interlocked.CompareExchange(ref _items[index], default(T), item));
            lock (_modificationLocker) {
                if (object.ReferenceEquals(_items[index], item)) {
                    _items[index] = null;
                    _count--;
                    return true;
                }
                return false;
            }
        }

        public void RemoveWhere(Func<T, bool> predicate, int maxCount = -1) {
            if (predicate == null) {
                throw new ArgumentNullException("predicate");
            }
            if (maxCount == 0) {
                return;
            }
            lock (_modificationLocker) {
                for (int i = 0; i < _items.Length; i++) {
                    if ((object)_items[i] != null && predicate(_items[i])) {
                        _items[i] = null;
                        _count--;
                        maxCount--;
                        if (maxCount == 0) {
                            return;
                        }
                    }
                }
            }
        }

        public int Count {
            get { return _count; }
        }

        public int Capacity {
            get { return _items.Length; }
            set {
                lock (_modificationLocker) {
                    if (value < _count) {
                        throw new ArgumentOutOfRangeException("capacity");
                    }
                    if (value != _items.Length) {
                        if (value > 0) {
                            T[] newItems = new T[value];
                            if (_count > 0) {
                                Array.Copy(_items, newItems, _count);
                            }
                            _items = newItems;
                        } else {
                            _items = _emptyArray;
                        }
                    }
                }
            }
        }

        public void ForEach(Action<T> action) {
            T[] items = _items;
            for (int i = 0; i < items.Length; i++) {
                T item = items[i];
                if ((object)item != null) {
                    action(item);
                }
            }
        }

        void EnsureCapacity(int min) {
            lock (_modificationLocker) {
                if (_items.Length < min) {
                    int capacity = (_items.Length == 0 ? 4 : _items.Length * 2);
                    if (capacity < 0) {
                        capacity = int.MaxValue - 1;
                    }
                    if (capacity < min) {
                        capacity = min;
                    }
                    Capacity = capacity;
                }
            }
        }


        public void Clear() {
            lock (_modificationLocker) {
                if (_count > 0) {
                    Array.Clear(_items, 0, _items.Length);
                    _count = 0;
                }
            }
        }

        public bool Contains(T item) {
            if (item == null) {
                throw new ArgumentNullException("item");
            }
            T[] items = _items;
            for (int i = 0; i < items.Length; i++) {
                if (items[i] == item) {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) {
            T[] items = _items;
            int idx = arrayIndex;
            for (int i = 0; i < items.Length; i++) {
                T item = items[i];
                if ((object)item != null) {
                    array[idx++] = item;
                }
            }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        public IEnumerator<T> GetEnumerator() {
            T[] items = _items;
            for (int i = 0; i < items.Length; i++) {
                T item = items[i];
                if ((object)item != null) {
                    yield return item;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
