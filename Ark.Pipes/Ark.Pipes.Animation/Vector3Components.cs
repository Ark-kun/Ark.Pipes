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

        public Vector3Components()
            : this(Constant<TType>.Default, Constant<TType>.Default, Constant<TType>.Default) {
        }

        public Vector3Components(Provider<TType> x, Provider<TType> y, Provider<TType> z) {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vector3Components(Provider<TVector3> vectors) {
            _x = Provider<TType>.Create((v) => v.X, vectors);
            _y = Provider<TType>.Create((v) => v.Y, vectors);
            _z = Provider<TType>.Create((v) => v.Z, vectors);
        }

        public static Vector3Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider<TType>.Create(p => (TType)(((dynamic)p).X), point);
            var y = Provider<TType>.Create(p => (TType)(((dynamic)p).Y), point);
            var z = Provider<TType>.Create(p => (TType)(((dynamic)p).Z), point);
            return new Vector3Components(x, y, z);
        }

        public Provider<TVector3> ToVectors3() {
            return Provider<TVector3>.Create((x, y, z) => new TVector3(x, y, z), _x, _x, _x);
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
