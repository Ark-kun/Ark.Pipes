using System;
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

namespace Ark.Geometry.Pipes {
    public static class Extensions {
        public static Provider<Vector2> Add(this Provider<Vector2> v1s, Provider<Vector2> v2s) {
            return Provider<Vector2>.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<Vector2> Add(this Provider<Vector2> v1s, Vector2 v2) {
            return Provider<Vector2>.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<Vector2> Multiply(this Provider<Vector2> vs, TFloat multiplier) {
            return Provider<Vector2>.Create((v) => v * multiplier, vs);
        }

        public static Provider<Vector2> Divide(this Provider<Vector2> vs, TFloat divider) {
            return vs.Multiply(1 / divider);
        }

        public static Provider<Vector2> Negate(this Provider<Vector2> vs) {
            return Provider<Vector2>.Create((v) => -v, vs);
        }

        public static Provider<Vector2> Subtract(this Provider<Vector2> v1s, Provider<Vector2> v2s) {
            return Provider<Vector2>.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<Vector2> Subtract(this Provider<Vector2> v1s, Vector2 v2) {
            return Provider<Vector2>.Create((v1) => v1 - v2, v1s);
        }

        public static Vector2Components ToComponents(this Provider<Vector2> vectors) {
            return new Vector2Components(vectors);
        }

        public static Provider<Vector3> ToVectors3XY(this Provider<Vector2> vectors) {
            return Provider<Vector3>.Create((v) => new Vector3(v.X, v.Y, 0), vectors);
        }

        public static Provider<Vector3> ToVectors3XZ(this Provider<Vector2> vectors) {
            return Provider<Vector3>.Create((v) => new Vector3(v.X, 0, v.Y), vectors);
        }

        public static Provider<Vector3> Add(this Provider<Vector3> v1s, Provider<Vector3> v2s) {
            return Provider<Vector3>.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<Vector3> Add(this Provider<Vector3> v1s, Vector3 v2) {
            return Provider<Vector3>.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<Vector3> Multiply(this Provider<Vector3> vs, TFloat multiplier) {
            return Provider<Vector3>.Create((v) => v * multiplier, vs);
        }

        public static Provider<Vector3> Divide(this Provider<Vector3> vs, TFloat divider) {
            return vs.Multiply(1 / divider);
        }

        public static Provider<Vector3> Negate(this Provider<Vector3> vs) {
            return Provider<Vector3>.Create((v) => -v, vs);
        }

        public static Provider<Vector3> Subtract(this Provider<Vector3> v1s, Provider<Vector3> v2s) {
            return Provider<Vector3>.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<Vector3> Subtract(this Provider<Vector3> v1s, Vector3 v2) {
            return Provider<Vector3>.Create((v1) => v1 - v2, v1s);
        }

        public static Vector3Components ToComponents(this Provider<Vector3> vectors) {
            return new Vector3Components(vectors);
        }

        public static Provider<Vector2> ToVectors2XY(this Provider<Vector3> vectors) {
            return Provider<Vector2>.Create((v) => new Vector2(v.X, v.Y), vectors);
        }
        public static Provider<Vector2> ToVectors2XZ(this Provider<Vector3> vectors) {
            return Provider<Vector2>.Create((v) => new Vector2(v.X, v.Z), vectors);
        }
    }
}

namespace Ark.Geometry {
    public static class Extensions {
        public static bool IsZero(this Vector2 vector) {
            return vector.X == 0 && vector.Y == 0;
        }

        public static bool IsZero(this Vector3 vector) {
            return vector.X == 0 && vector.Y == 0 && vector.Z == 0;
        }

#if FRAMEWORK_WPF
        public static TFloat Length(this Vector2 vector) {
            return vector.Length;
        }

        public static TFloat Length(this Vector3 vector) {
            return vector.Length;
        }

        public static TFloat LengthSquared(this Vector2 vector) {
            return vector.LengthSquared;
        }

        public static TFloat LengthSquared(this Vector3 vector) {
            return vector.LengthSquared;
        }
#endif
        public static Vector2 ToVector2(this Vector3 v) {
            return new Vector2(v.X, v.Y);
        }

        public static Vector2 ToVector2XY(this Vector3 v) {
            return new Vector2(v.X, v.Y);
        }

        public static Vector2 ToVector2XZ(this Vector3 v) {
            return new Vector2(v.X, v.Z);
        }

        public static Vector3 ToVector3(this Vector2 v) {
            return new Vector3(v.X, v.Y, 0);
        }

        public static Vector3 ToVector3XY(this Vector2 v) {
            return new Vector3(v.X, v.Y, 0);
        }

        public static Vector3 ToVector3XZ(this Vector2 v) {
            return new Vector3(v.X, 0, v.Y);
        }

        public static TFloat Angle(this Vector2 v) {
            return (TFloat)Math.Atan2(v.X, v.Y);
        }

        public static Vector2 Rotate(this Vector2 v, TFloat angle) {
            TFloat sin = (TFloat)Math.Sin(angle);
            TFloat cos = (TFloat)Math.Cos(angle);
            return new Vector2(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos);
        }
    }
}
