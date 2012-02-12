using System;

namespace Ark.Pipes {
    public abstract class Provider<T> : IOut<T> {
        public abstract T GetValue();

        public T Value {
            get { return GetValue(); }
        }

        static public implicit operator Provider<T>(T value) {
            return new Constant<T>(value);
        }

        //static public implicit operator Provider<T>(object value) {
        //    Type thisType = typeof(T);
        //    Type otherType = typeof(object);
        //    if (thisType == otherType) {
        //        throw new NotSupportedException(string.Format("Cannot convert {0} to {1}", otherType, thisType));
        //    }
        //    return new Constant<T>(value);
        //}

        //[System.Runtime.CompilerServices.SpecialName]
        //static public Provider<T> op_Implicit<T1>(Provider<T1> value) where T1 : T {
        //    Type thisType = typeof(T);
        //    Type otherType = typeof(T1);
        //    if (!thisType.IsAssignableFrom(otherType)) {
        //        throw new NotSupportedException(string.Format("Cannot convert {0} to {1}", otherType, thisType));
        //    }
        //    return new Function<T1, T>((v) => (T)Convert.ChangeType(v, thisType), value);
        //}

        static public implicit operator Provider<T>(Func<T> value) {
            return new Function<T>(value);
        }

        [System.Runtime.CompilerServices.SpecialName]
        static public Provider<T> op_Implicit(IOut<T> value) {
            return new Function<T>(value);
        }

        [System.Runtime.CompilerServices.SpecialName]
        static public Provider<T> op_Implicit<T1>(Func<T1, T> value) {
            return new Function<T1, T>(value);
        }

        static public implicit operator Provider<T>(Func<T, T> value) {
            return new Function<T, T>(value);
        }

        static public implicit operator T(Provider<T> provider) {
            return provider.Value;
        }

        static public Constant<T> Create(T value) {
            return new Constant<T>(value);
        }

        static public Provider<T> Create<T1>(Func<T1, T> value) {
            return new Function<T1, T>(value);
        }

        static public NotifyingFunction<T> Create(Func<T> value, ref Action changedTrigger) {
            return new NotifyingFunction<T>(value, ref changedTrigger);
        }

        static public NotifyingFunction<T1, T> Create<T1>(Func<T1, T> value, NotifyingProvider<T1> arg) {
            return new NotifyingFunction<T1, T>(value, arg);
        }

        public NotifyingProvider<T> AddInvalidator(ref Action changedTrigger) {
            return new NotifyingFunction<T>(this.GetValue, ref changedTrigger);
        }
    }
}