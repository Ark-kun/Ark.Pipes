#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
#else
#error Bad geometry framework
#endif

namespace Ark.Pipes {
    public class Vector3Components {
        Property<TFloat> _x;
        Property<TFloat> _y;
        Property<TFloat> _z;

        public Vector3Components()
            : this(Constant<TFloat>.Default, Constant<TFloat>.Default, Constant<TFloat>.Default) {
        }

        public Vector3Components(Provider<TFloat> x, Provider<TFloat> y, Provider<TFloat> z) {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vector3Components(Provider<Vector3> vectors) {
            _x = Provider<TFloat>.Create((v) => v.X, vectors);
            _y = Provider<TFloat>.Create((v) => v.Y, vectors);
            _z = Provider<TFloat>.Create((v) => v.Z, vectors);
        }

        public static Vector3Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider<TFloat>.Create(p => (TFloat)(((dynamic)p).X), point);
            var y = Provider<TFloat>.Create(p => (TFloat)(((dynamic)p).Y), point);
            var z = Provider<TFloat>.Create(p => (TFloat)(((dynamic)p).Z), point);
            return new Vector3Components(x, y, z);
        }

        public Provider<Vector3> ToVectors3() {
            return Provider<Vector3>.Create((x, y, z) => new Vector3(x, y, z), _x, _x, _x);
        }

        public Property<TFloat> X {
            get { return _x; }
            set { _x.Provider = value.Provider; }
        }

        public Property<TFloat> Y {
            get { return _y; }
            set { _y.Provider = value.Provider; }
        }

        public Property<TFloat> Z {
            get { return _z; }
            set { _z.Provider = value.Provider; }
        }
    }
}
