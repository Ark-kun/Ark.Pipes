using Ark.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ark {
    class NativeDelegateList<TDelegate> : ICollectionEx<SingleDelegate<TDelegate>> where TDelegate : class {
        Delegate _delegateList;

        public void Add(SingleDelegate<TDelegate> singleDelegate) {
            //lock (_delegateList)
            _delegateList = Delegate.Combine(_delegateList, (Delegate)(object)singleDelegate.Invoke);
        }

        public void AddRange(IEnumerable<SingleDelegate<TDelegate>> delegates) {
            foreach (var singleDelegate in delegates) {
                Add(singleDelegate);
            }
        }

        public void Remove(SingleDelegate<TDelegate> singleDelegate) {
            _delegateList = Delegate.Remove(_delegateList, (Delegate)(object)singleDelegate.Invoke);
        }

        public void RemoveRange(IEnumerable<SingleDelegate<TDelegate>> delegates) {
            foreach (var singleDelegate in delegates) {
                Remove(singleDelegate);
            }
        }

        public IEnumerator<SingleDelegate<TDelegate>> GetEnumerator() {
            return _delegateList.GetInvocationList().Select(d => (SingleDelegate<TDelegate>)d.Target).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
