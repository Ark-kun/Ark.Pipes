using System;

namespace Ark.Pipes {
    public static class TupleProvider {
        public static Provider<Tuple<T1, T2>> Create<T1, T2>(Provider<T1> provider1, Provider<T2> provider2) {
            return Provider<Tuple<T1, T2>>.Create(Tuple.Create, provider1, provider2);
        }

        public static Provider<Tuple<T1, T2, T3>> Create<T1, T2, T3>(Provider<T1> provider1, Provider<T2> provider2, Provider<T3> provider3) {
            return Provider<Tuple<T1, T2, T3>>.Create(Tuple.Create, provider1, provider2, provider3);
        }

        public static Provider<Tuple<T1, T2, T3, T4>> Create<T1, T2, T3, T4>(Provider<T1> provider1, Provider<T2> provider2, Provider<T3> provider3, Provider<T4> provider4) {
            return Provider<Tuple<T1, T2, T3, T4>>.Create(Tuple.Create, provider1, provider2, provider3, provider4);
        }
    }
}