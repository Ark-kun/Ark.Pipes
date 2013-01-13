using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ark.Collections {
    class SafeIndexedLinkedList<T> : IEnumerable<T>, ICollectionEx<T>, ICanRemoveAll<T> {
        DoubleLinkedList<T> _list = new DoubleLinkedList<T>();
        Dictionary<T, SingleLinkedList<DoubleLinkedNode<T>>> _lookup;

        public SafeIndexedLinkedList() {
            _lookup = new Dictionary<T, SingleLinkedList<DoubleLinkedNode<T>>>();
        }

        public SafeIndexedLinkedList(int capacity) {
            _lookup = new Dictionary<T, SingleLinkedList<DoubleLinkedNode<T>>>(capacity);
        }

        public SafeIndexedLinkedList(int capacity, IEqualityComparer<T> comparer) {
            _lookup = new Dictionary<T, SingleLinkedList<DoubleLinkedNode<T>>>(capacity, comparer);
        }

        public void Add(T value) {
            Append(value);
        }

        public void AddRange(IEnumerable<T> values) {
            AppendRange(values);
        }

        public void Append(T value) {
            lock (_list) {
                AppendWithoutLocking(value);
            }
        }

        void AppendWithoutLocking(T value) {
            var node = _list.Append(value);
            var nodeList = _lookup.GetOrCreateValue(value);
            nodeList.Add(node);
        }


        public void AppendRange(IEnumerable<T> values) {
            lock (_list) {
                foreach (var value in values) {
                    Append(value);
                }
            }
        }

        public void Remove(T value) {
            lock (_list) {
                RemoveWithoutLocking(value);
            }
        }

        public void RemoveRange(IEnumerable<T> values) {
            lock (_list) {
                foreach (var value in values) {
                    RemoveWithoutLocking(value);
                }
            }
        }

        public void RemoveAll(T value) {
            lock (_list) {
                RemoveAllWithoutLocking(value);
            }
        }

        void RemoveWithoutLocking(T value) {
            SingleLinkedList<DoubleLinkedNode<T>> nodeList;
            if (_lookup.TryGetValue(value, out nodeList)) {
                var node = nodeList.PopFirst();
                if (nodeList.IsEmpty) {
                    _lookup.Remove(value);
                }
                _list.Remove(node);
            }
        }

        void RemoveAllWithoutLocking(T value) {
            SingleLinkedList<DoubleLinkedNode<T>> nodeList;
            if (_lookup.TryGetValue(value, out nodeList)) {
                _lookup.Remove(value);
                foreach (var node in nodeList) {
                    _list.Remove(node);
                }
            }
        }

        public IEnumerator<T> GetEnumerator() {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    class SingleLinkedList<T> : IEnumerable<T> {
        SingleLinkedNode<T> _head;

        public bool IsEmpty {
            get { return _head == null; }
        }

        public SingleLinkedNode<T> Add(T value) {
            return Prepend(value);
        }

        public SingleLinkedNode<T> Prepend(T value) {
            var node = new SingleLinkedNode<T>(value);
            if (_head != null) {
                node.Next = _head;
            }
            _head = node;
            return node;
        }

        internal T PopFirst() {
            if (_head == null) {
                throw new InvalidOperationException("The list is empty.");
            }
            var value = _head.Value;
            _head = _head.Next;
            return value;
        }

        public IEnumerator<T> GetEnumerator() {
            var head = _head;
            if (head == null) {
                return EmptyEnumerator<T>.Instance;
            } else {
                return head.GetEnumerator();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    class SingleLinkedNode<T> : IEnumerable<T> {
        internal T _value;
        internal SingleLinkedNode<T> _next;

        public SingleLinkedNode(T value) {
            _value = value;
        }

        public T Value {
            get { return _value; }
        }

        public SingleLinkedNode<T> Next {
            get { return _next; }
            internal set { _next = value; }
        }

        public IEnumerator<T> GetEnumerator() {
            for (var node = this; node != null; node = node.Next) {
                yield return node.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    class DoubleLinkedList<T> : IEnumerable<T> {
        DoubleLinkedNode<T> _head;
        DoubleLinkedNode<T> _tail;

        public DoubleLinkedList() {
        }

        DoubleLinkedList(DoubleLinkedNode<T> head, DoubleLinkedNode<T> tail) {
            _head = head;
            _tail = tail;
        }

        DoubleLinkedNode<T> Head {
            get { return _head; }
        }

        public bool IsEmpty {
            get { return _head == null; }
        }

        public DoubleLinkedNode<T> Add(T value) {
            return Append(value);
        }

        public DoubleLinkedNode<T> Append(T value) {
            var node = new DoubleLinkedNode<T>(value);
            AppendWithoutCloning(node);
            return node;
        }

        void AppendWithoutCloning(DoubleLinkedNode<T> node) {
            AppendWithoutCloning(node, node);
        }

        void AppendWithoutCloning(DoubleLinkedNode<T> head, DoubleLinkedNode<T> tail) {
            if (_head == null) {
                Debug.Assert(_tail == null);
                _head = head;
            } else {
                _tail.Append(head);
            }
            _tail = tail;
        }

        public void Append(DoubleLinkedList<T> list) {
            var head = list.Head.CloneAsRing();
            var tail = head.Previous;
            AppendWithoutCloning(head, tail);
        }

        internal T PopFirst() {
            if (_head == null) {
                throw new InvalidOperationException("The list is empty.");
            }
            var value = _head.Value;
            _head.Next.Previous = null;
            _head = _head.Next;
            return value;
        }

        public void Remove(DoubleLinkedNode<T> node) {
            if (node.Previous != null) {
                node.Previous.Next = node.Next;
            } else { 
                Debug.Assert(node == _head);
                _head = node.Next;
            }
            if (node.Next != null) {
                node.Next.Previous = node.Previous;
            } else {
                Debug.Assert(node == _tail);
                _tail = node.Previous;
            }
        }

        public IEnumerator<T> GetEnumerator() {
            var head = _head;
            if (head == null) {
                return EmptyEnumerator<T>.Instance;
            } else {
                return head.GetEnumerator();
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }

    class DoubleLinkedNode<T> : IEnumerable<T> {
        T _value;
        DoubleLinkedNode<T> _next;
        DoubleLinkedNode<T> _prev;

        public DoubleLinkedNode(T value) {
            _value = value;
        }

        public T Value { get { return _value; } }

        public DoubleLinkedNode<T> Next {
            get { return _next; }
            internal set { _next = value; }
        }

        public DoubleLinkedNode<T> Previous {
            get { return _prev; }
            internal set { _prev = value; }
        }

        public DoubleLinkedNode<T> CloneAsRing() {
            var newHead = new DoubleLinkedNode<T>(this.Value);
            var newNode = newHead;
            for (DoubleLinkedNode<T> node = this; node != null; node = node.Next) {
                var nextNode = new DoubleLinkedNode<T>(node._value);
                newNode.Append(nextNode);
                newNode = nextNode;
            }
            //newNode._next = newHead;
            newHead._prev = newNode; //ring is only closed backwards
            return newHead;
        }

        public void Append(DoubleLinkedNode<T> node) {
            node._prev = this;
            this._next = node;
        }

        //public void Remove() {
        //    if (_prev != null) {
        //        _prev._next = _next;
        //    }
        //    if (_next != null) {
        //        _next._prev = _prev;
        //    }
        //}

        public IEnumerator<T> GetEnumerator() {
            for (var node = this; node != null; node = node.Next) {
                yield return node.Value;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
