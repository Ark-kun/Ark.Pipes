using System;

namespace Ark.Pipes {
    public class Vector3Components {
        Property<double> _x;
        Property<double> _y;
        Property<double> _z;
        Provider<double> _length;

        public Vector3Components()
            : this(0, 0, 0) {
        }

        public Vector3Components(Provider<dynamic> point)
            : this(new Function<double>(() => point.Value.X), new Function<double>(() => point.Value.Y), new Function<double>(() => point.Value.Z)) {
        }

        public Vector3Components(Provider<double> x, Provider<double> y, Provider<double> z) {
            _x = x;
            _y = y;
            _z = z;

            _length = new Function<double, double, double, double>((X, Y, Z) => Math.Sqrt(X * X + Y * Y + z * Z), _x, _y, _z);
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

        public Property<double> Z {
            get { return _z; }
            set { _z.Provider = value.Provider; }
        }
    }
}
