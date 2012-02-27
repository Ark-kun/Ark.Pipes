using System;
using Ark.Animation;

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
    public class Circle {
        public static Vector2 Position(Vector2 center, TFloat radius, TFloat angle) {
            return center + new Vector2((TFloat)Math.Cos(angle), (TFloat)Math.Sin(angle)) * radius;
        }

        public static OrientedPosition2 Tangent(Vector2 center, TFloat radius, bool clockWize, TFloat angle) {
            Vector2 position = Position(center, radius, angle);
            TFloat orientation = angle + (clockWize ? 1 : -1) * (TFloat)Math.PI / 2;
            return new OrientedPosition2(position, orientation);
        }

        public static OrientedPosition2 Normal(Vector2 center, TFloat radius, TFloat angle) {
            Vector2 position = Position(center, radius, angle);
            TFloat orientation = angle;
            return new OrientedPosition2(position, orientation);
        }
    }
}