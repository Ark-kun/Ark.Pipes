using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ark {
    sealed class HashedWeakReference<T> : WeakReference, IEquatable<HashedWeakReference<T>>, IEquatable<T> where T : class {
        private int _hashCode;

        public HashedWeakReference(T target)
            : base(target) {
            if (target == null) {
                throw new ArgumentNullException();
            }
            //_hashCode = target.GetHashCode();
            _hashCode = RuntimeHelpers.GetHashCode(target);
        }

        public override bool Equals(object other) {
            var hashedWeakReference = other as HashedWeakReference<T>;
            if ((object)hashedWeakReference != null) {
                return Equals(hashedWeakReference);
            }
            var obj = other as T;
            if ((object)obj != null) {
                return Equals(obj);
            }
            return false;
        }

        public bool Equals(HashedWeakReference<T> other) {
            return (object)other != null && other._hashCode == _hashCode && ReferencesAreEqualAndNotNull(other.Target, Target);
        }

        public bool Equals(T other) {
            //return (object)other != null && other.GetHashCode() == _hashCode && ReferencesAreEqualAndNotNull(other, Target);
            return (object)other != null && RuntimeHelpers.GetHashCode(other) == _hashCode && ReferencesAreEqualAndNotNull(other, Target);
        }

        public override int GetHashCode() {
            return this._hashCode;
        }

        public static bool operator ==(HashedWeakReference<T> left, HashedWeakReference<T> right) {
            return ((object)left == null && (object)right == null) || left.Equals(right);
        }

        public static bool operator !=(HashedWeakReference<T> left, HashedWeakReference<T> right) {
            return !(left == right);
        }

        public new T Target {
            get { return (T)base.Target; }
        }

        static bool ReferencesAreEqualAndNotNull(object obj1, object obj2) {
            return obj1 != null && obj2 != null && Object.ReferenceEquals(obj1, obj2);
        }
    }

    sealed class DelegateEqualityComparer : IEqualityComparer<MulticastDelegate> {
        public bool Equals(MulticastDelegate x, MulticastDelegate y) {
            return x.GetGoodHashCode() == y.GetGoodHashCode() && x == y;
        }

        public int GetHashCode(MulticastDelegate obj) {
            return obj.GetGoodHashCode();
        }
    }
}
