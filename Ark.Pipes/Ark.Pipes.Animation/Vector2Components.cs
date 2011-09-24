using System;

namespace Ark.Pipes {
    public class Vector2Components {
        Property<double> _x;
        Property<double> _y;
        Provider<double> _length;

        public Vector2Components()
            : this(Constant<double>.Default, Constant<double>.Default) {
        }

        public static Vector2Components From<T>(Provider<T> point) {
            var x = new Function<T, double>(p => (double)(((dynamic)p).X), point);
            var y = new Function<T, double>(p => (double)(((dynamic)p).Y), point);
            return new Vector2Components(x, y);
        }

        public Vector2Components(Provider<double> x, Provider<double> y) {
            _x = x;
            _y = y;
            _length = new Function<double, double, double>((X, Y) => Math.Sqrt(X * X + Y * Y), _x, _y);
        }

        public Provider<double> Length {
            get { return _length; }
        }

        public Property<double> X {
            get { return _x; }
            set { _x.Provider = value.Provider; }
        }

        public Property<double> Y {
            get { return _y; }
            set { _y.Provider = value.Provider; }
        }
    }
}
