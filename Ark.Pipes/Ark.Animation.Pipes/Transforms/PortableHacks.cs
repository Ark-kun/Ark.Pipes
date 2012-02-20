namespace System.Collections.Generic {
    public class HashSet<T> : ICollection<T> {
        private Dictionary<T, byte> _dict;

        public HashSet() {
            _dict = new Dictionary<T, byte>();
        }

        public void Add(T item) {
            _dict.Add(item, 0);
        }

        public void Clear() {
            _dict.Clear();
        }

        public bool Contains(T item) {
            return _dict.ContainsKey(item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(T item) {
            return _dict.Remove(item);
        }

        public IEnumerator<T> GetEnumerator() {
            return _dict.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _dict.Keys.GetEnumerator();
        }

        public int Count {
            get { return _dict.Keys.Count; }
        }

        public bool IsReadOnly {
            get { return false; }
        }
    }
}