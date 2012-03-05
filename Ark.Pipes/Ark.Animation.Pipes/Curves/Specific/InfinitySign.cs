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
    public class InfinitySign {
        public static Vector2 CanonicalPositionOld(TFloat t) {
            TFloat a = t;
            TFloat sqrt2 = (TFloat)Math.Sqrt(2);
            var sinA = Math.Sin(a);
            var cosA = Math.Cos(a);
            var denominator = 2 * sqrt2 * cosA - 3;
            var sinB = sinA / denominator;
            var cosB = (2 * sqrt2 - 3 * cosA) / denominator;
            var C = new Vector2((TFloat)cosA - 1, (TFloat)sinA);
            var D = new Vector2((TFloat)cosB + 1, (TFloat)sinB);
            var E = (C + D) / 2;

            return E;
        }

        public static Vector2 PositionOld(Vector2 focus1, Vector2 focus2, TFloat t) {
            Vector2 r1 = focus1;
            Vector2 r2 = focus2;
            var direction = r2 - r1;
            var scale = direction.Length() / 2;
            var center = (r1 + r2) / 2;
            var result = CanonicalPositionOld(t);
            result = center + result.Rotate(direction.Angle()) * scale;
            return result;
        }

        public static Vector2 CanonicalPosition(TFloat t) {
            TFloat a = 1;
            var sin = Math.Sin(t);
            var cos = Math.Cos(t);
            TFloat k = (TFloat)(a * Math.Sqrt(2) * cos / (sin * sin + 1));
            return new Vector2(1, (TFloat)sin) * k;
        }

        public static Vector2 Position(Vector2 focus1, Vector2 focus2, TFloat t) {
            Vector2 r1 = focus1;
            Vector2 r2 = focus2;
            var direction = r2 - r1;
            var scale = direction.Length() / 2;
            var center = (r1 + r2) / 2;
            var result = CanonicalPosition(t);
            result = center + result.Rotate(direction.Angle()) * scale;
            return result;
        }
    }
}