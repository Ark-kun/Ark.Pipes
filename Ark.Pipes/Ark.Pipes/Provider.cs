using System;

namespace Ark.Pipes {
    public abstract class Provider<T> : IOut<T> {
        static public implicit operator Provider<T>(T value) {
            return new Constant<T>(value);
        }

        static public implicit operator Provider<T>(Func<T> value) {
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

        public abstract T GetValue();

        public T Value {
            get { return GetValue(); }
        }
    }
}