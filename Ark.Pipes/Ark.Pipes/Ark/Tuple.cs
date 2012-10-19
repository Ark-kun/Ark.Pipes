using System;

namespace Ark {
    static class Tuple {
        public static Tuple<T1> Create<T1>(T1 item1) {
            return new Tuple<T1>(item1);
        }

        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) {
            return new Tuple<T1, T2>(item1, item2);
        }

        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }

        // From System.Web.Util.HashCodeCombiner 
        internal static int CombineHashCodes(int h1, int h2) {
            return (((h1 << 5) + h1) ^ h2);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3) {
            return CombineHashCodes(CombineHashCodes(h1, h2), h3);
        }

        internal static int CombineHashCodes(int h1, int h2, int h3, int h4) {
            return CombineHashCodes(CombineHashCodes(h1, h2), CombineHashCodes(h3, h4));
        }
    }

    class Tuple<T1> : IEquatable<Tuple<T1>> {
        private readonly T1 _Item1;

        public T1 Item1 { get { return _Item1; } }

        public Tuple(T1 item1) {
            _Item1 = item1;
        }

        public override int GetHashCode() {
            return _Item1.GetHashCode();
        }

        public bool Equals(Tuple<T1> other) {
            return other._Item1.Equals(_Item1);
        }
    }

    class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>> {
        private readonly T1 _Item1;
        private readonly T2 _Item2;

        public T1 Item1 { get { return _Item1; } }
        public T2 Item2 { get { return _Item2; } }

        public Tuple(T1 item1, T2 item2) {
            _Item1 = item1;
            _Item2 = item2;
        }

        public override int GetHashCode() {
            return Tuple.CombineHashCodes(_Item1.GetHashCode(), _Item2.GetHashCode());
        }

        public bool Equals(Tuple<T1, T2> other) {
            return other._Item1.Equals(_Item1) && other._Item2.Equals(_Item2);
        }
    }

    class Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>> {
        private readonly T1 _Item1;
        private readonly T2 _Item2;
        private readonly T3 _Item3;

        public T1 Item1 { get { return _Item1; } }
        public T2 Item2 { get { return _Item2; } }
        public T3 Item3 { get { return _Item3; } }

        public Tuple(T1 item1, T2 item2, T3 item3) {
            _Item1 = item1;
            _Item2 = item2;
            _Item3 = item3;
        }

        public override int GetHashCode() {
            return Tuple.CombineHashCodes(_Item1.GetHashCode(), _Item2.GetHashCode(), _Item3.GetHashCode());
        }

        public bool Equals(Tuple<T1, T2, T3> other) {
            return other._Item1.Equals(_Item1) && other._Item2.Equals(_Item2) && other._Item3.Equals(_Item3);
        }
    }

    class Tuple<T1, T2, T3, T4> : IEquatable<Tuple<T1, T2, T3, T4>> {
        private readonly T1 _Item1;
        private readonly T2 _Item2;
        private readonly T3 _Item3;
        private readonly T4 _Item4;

        public T1 Item1 { get { return _Item1; } }
        public T2 Item2 { get { return _Item2; } }
        public T3 Item3 { get { return _Item3; } }
        public T4 Item4 { get { return _Item4; } }

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) {
            _Item1 = item1;
            _Item2 = item2;
            _Item3 = item3;
            _Item4 = item4;
        }

        public override int GetHashCode() {
            return Tuple.CombineHashCodes(_Item1.GetHashCode(), _Item2.GetHashCode(), _Item3.GetHashCode(), _Item4.GetHashCode());
        }

        public bool Equals(Tuple<T1, T2, T3, T4> other) {
            return other._Item1.Equals(_Item1) && other._Item2.Equals(_Item2) && other._Item3.Equals(_Item3) && other._Item4.Equals(_Item4);
        }
    }
}
