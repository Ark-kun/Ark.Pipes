using System.Collections.Generic;

namespace Ark.Collections {
    interface ICollectionEx<T> : IEnumerable<T> {
        void Add(T value);
        void AddRange(IEnumerable<T> value);
        void Remove(T value);
        void RemoveRange(IEnumerable<T> value);
    }
}
