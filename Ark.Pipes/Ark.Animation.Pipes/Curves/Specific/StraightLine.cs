using System;
using Ark.Animation;
using Ark.Geometry;
using Ark.Geometry.Curves;
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

namespace Ark.Geometry.Curves {
    public class StraightLine {
        public static Vector2 Position(Vector2 origin, Vector2 velocity, TFloat t) {
            return origin + velocity * t;
        }

        public static OrientedPosition2 OrientedPosition(Vector2 origin, Vector2 velocity, TFloat t) {
            Vector2 position = Position(origin, velocity, t);
            TFloat orientation = (TFloat)velocity.Angle();
            return new OrientedPosition2(position, orientation);
        }

        public static Vector2 Position(Tuple<Vector2, Vector2> parameters, TFloat t) {
            return Position(parameters.Item1, parameters.Item2, t);
        }

        public static OrientedPosition2 OrientedPosition(Tuple<Vector2, Vector2> parameters, TFloat t) {
            return OrientedPosition(parameters.Item1, parameters.Item2, t);
        }
    }
}