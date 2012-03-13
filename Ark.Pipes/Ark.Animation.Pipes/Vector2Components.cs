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
        Property<TFloat> _x;
        Property<TFloat> _y;

        public Vector2Components()
            : this(Constant<TFloat>.Default, Constant<TFloat>.Default) {
        }

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
            var x = Provider<TFloat>.Create(p => (TFloat)(((dynamic)p).X), point);
            var y = Provider<TFloat>.Create(p => (TFloat)(((dynamic)p).Y), point);
            return new Vector2Components(x, y);
        }
#endif

        public Provider<Vector2> ToVectors2() {
            return Provider.Create((x, y) => new Vector2(x, y), _x, _y);
        }

        public Property<TFloat> X {
            get { return _x; }
            set { _x.Provider = value.Provider; }
        }

        public Property<TFloat> Y {
            get { return _y; }
            set { _y.Provider = value.Provider; }
        }
    }
}
