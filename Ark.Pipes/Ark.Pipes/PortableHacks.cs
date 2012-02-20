﻿namespace System.Runtime.CompilerServices {
    public class SpecialNameAttribute : Attribute { }
}

namespace System {
    public static class Tuple {
        public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2) {
            return new Tuple<T1, T2>(item1, item2);
        }

        public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3) {
            return new Tuple<T1, T2, T3>(item1, item2, item3);
        }

        public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4) {
            return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
        }
    }

    public class Tuple<T1, T2> {
        private readonly T1 _item1;
        private readonly T2 _item2;

        public Tuple(T1 item1, T2 item2) {
            _item1 = item1;
            _item2 = item2;
        }

        public T1 Item1 { get { return _item1; } }
        public T2 Item2 { get { return _item2; } }
    }

    public class Tuple<T1, T2, T3> {
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;

        public Tuple(T1 item1, T2 item2, T3 item3) {
            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
        }

        public T1 Item1 { get { return _item1; } }
        public T2 Item2 { get { return _item2; } }
        public T3 Item3 { get { return _item3; } }
    }

    public class Tuple<T1, T2, T3, T4> {
        private readonly T1 _item1;
        private readonly T2 _item2;
        private readonly T3 _item3;
        private readonly T4 _item4;

        public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) {
            _item1 = item1;
            _item2 = item2;
            _item3 = item3;
            _item4 = item4;
        }

        public T1 Item1 { get { return _item1; } }
        public T2 Item2 { get { return _item2; } }
        public T3 Item3 { get { return _item3; } }
        public T4 Item4 { get { return _item4; } }
    }
}