using System;

#if FLOAT_GEOMETRY
using TType = System.Single;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector3;
#else
using TType = System.Double;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector3;
#endif

namespace Ark.Pipes {
    public class Vector3Components {
        Property<TType> _x;
        Property<TType> _y;
        Property<TType> _z;
        Provider<TType> _length;

        public Vector3Components()
            : this(0, 0, 0) {
        }

        public Vector3Components(Provider<TVector3> point)
            : this(new Function<TType>(() => point.Value.X), new Function<TType>(() => point.Value.Y), new Function<TType>(() => point.Value.Z)) {
        }

        public static Vector3Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider<TType>.Create(p => (TType)(((dynamic)p).X), point);
            var y = Provider<TType>.Create(p => (TType)(((dynamic)p).Y), point);
            var z = Provider<TType>.Create(p => (TType)(((dynamic)p).Z), point);
            return new Vector3Components(x, y, z);
        }

        public Vector3Components(Provider<TType> x, Provider<TType> y, Provider<TType> z) {
            _x = x;
            _y = y;
            _z = z;

            _length = new Function<TType, TType, TType, TType>((X, Y, Z) => (TType)Math.Sqrt(X * X + Y * Y + Z * Z), _x, _y, _z);
        }

        public Provider<TType> Length {
            get { return _length; }
        }

        public Property<TType> X {
            get { return _x; }
            set { _x.Provider = value.Provider; }
        }

        public Property<TType> Y {
            get { return _y; }
            set { _y.Provider = value.Provider; }
        }

        public Property<TType> Z {
            get { return _z; }
            set { _z.Provider = value.Provider; }
        }
    }
}
