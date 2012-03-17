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
    public class Vector2Components {
        Provider<TFloat> _x;
        Provider<TFloat> _y;

        public Vector2Components(Provider<TFloat> x, Provider<TFloat> y) {
            _x = x;
            _y = y;
        }

        public Vector2Components(Provider<Vector2> vectors) {
            _x = Provider.Create((v) => v.X, vectors);
            _y = Provider.Create((v) => v.Y, vectors);
        }

#if !PORTABLE
        public static Vector2Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider.Create(p => (TFloat)(((dynamic)p).X), point);
            var y = Provider.Create(p => (TFloat)(((dynamic)p).Y), point);
            return new Vector2Components(x, y);
        }
#endif

        public Provider<Vector2> ToVectors2() {
            return Provider.Create((x, y) => new Vector2(x, y), _x, _y);
        }

        public Provider<TFloat> X {
            get { return _x; }
        }

        public Provider<TFloat> Y {
            get { return _y; }
        }
    }
}
