using Ark.Pipes;

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

namespace Ark.Geometry { //.Pipes {
    public class Vector3Components {
        Provider<TFloat> _x;
        Provider<TFloat> _y;
        Provider<TFloat> _z;

        public Vector3Components()
            : this(Constant<TFloat>.Default, Constant<TFloat>.Default, Constant<TFloat>.Default) {
        }

        public Vector3Components(Provider<TFloat> x, Provider<TFloat> y, Provider<TFloat> z) {
            _x = x;
            _y = y;
            _z = z;
        }

        public Vector3Components(Provider<Vector3> vectors) {
            _x = Provider.Create((v) => v.X, vectors);
            _y = Provider.Create((v) => v.Y, vectors);
            _z = Provider.Create((v) => v.Z, vectors);
        }

#if !PORTABLE
        public static Vector3Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider.Create(p => (TFloat)(((dynamic)p).X), point);
            var y = Provider.Create(p => (TFloat)(((dynamic)p).Y), point);
            var z = Provider.Create(p => (TFloat)(((dynamic)p).Z), point);
            return new Vector3Components(x, y, z);
        }
#endif

        public Provider<Vector3> ToVectors3() {
            return Provider.Create((x, y, z) => new Vector3(x, y, z), _x, _y, _z);
        }

        public Provider<TFloat> X {
            get { return _x; }
        }

        public Provider<TFloat> Y {
            get { return _y; }
        }

        public Provider<TFloat> Z {
            get { return _z; }
        }
    }
}
