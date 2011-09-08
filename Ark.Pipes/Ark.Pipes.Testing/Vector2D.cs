using System;

namespace Ark.Pipes.Testing {
    public class Vector2D {
        Property<double> _x;
        Property<double> _y;
        Provider<double> _length;

        public Vector2D()
            : this(0, 0) {
        }

        public Vector2D(Provider<double> x, Provider<double> y) {
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
