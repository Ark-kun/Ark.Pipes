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
    public class Orbit {
        public static Vector2 Position(Vector2 center, TFloat radius, TFloat angularVelocity, TFloat phase, TFloat t) {
            TFloat angle = phase + angularVelocity * t;
            return center + new Vector2((TFloat)Math.Cos(angle), (TFloat)Math.Sin(angle)) * radius;
        }

        public static OrientedPosition2 OrientedPosition(Vector2 center, TFloat radius, TFloat angularVelocity, TFloat t) {
            return OrientedPosition(center, radius, angularVelocity, 0, t);
        }

        public static OrientedPosition2 OrientedPosition(Vector2 center, TFloat radius, TFloat angularVelocity, TFloat phase, TFloat t) {
            Vector2 position = Position(center, radius, angularVelocity, phase, t);
            TFloat orientation = phase + angularVelocity * t + Math.Sign(angularVelocity) * (TFloat)Math.PI / 2;
            return new OrientedPosition2(position, orientation);
        }

        public static Vector2 Position(Tuple<Vector2, TFloat, TFloat, TFloat> parameters, TFloat t) {
            return Position(parameters.Item1, parameters.Item2, parameters.Item3, parameters.Item4, t);
        }

        public static OrientedPosition2 OrientedPosition(Tuple<Vector2, TFloat, TFloat, TFloat> parameters, TFloat t) {
            return OrientedPosition(parameters.Item1, parameters.Item2, parameters.Item3, parameters.Item4, t);
        }

        public static OrientedPosition2 OrientedPosition(Tuple<Vector2, TFloat, TFloat> parameters, TFloat t) {
            return OrientedPosition(parameters.Item1, parameters.Item2, parameters.Item3, t);
        }
    }
}