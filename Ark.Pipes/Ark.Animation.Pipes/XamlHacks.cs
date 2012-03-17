using System;
using Ark.Animation;
using Ark.Pipes;
using Ark.Geometry;

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
using Matrix2 = System.Windows.Media.Matrix;
using Matrix3 = System.Windows.Media.Media3D.Matrix3D;
#else
#error Bad geometry framework
#endif

namespace Ark.Geometry {
#if FRAMEWORK_WPF
    public static class XamlVector2 {
        public static TFloat Length(this Vector2 vector) {
            return vector.Length;
        }

        public static TFloat LengthSquared(this Vector2 vector) {
            return vector.LengthSquared;
        }

        public static Vector2 Normalize(Vector2 value) {
            value.Normalize();
            return value;
        }

        public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result) {
            result = new Vector2();
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static void Multiply(ref Vector2 value1, TFloat scaleFactor, out Vector2 result) {
            result = new Vector2();
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
        }

        public static Vector2 Multiply(Vector2 value1, Vector2 value2) {
            return new Vector2(value1.X * value2.X, value1.Y * value2.Y);
        }

        public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result) {
            result = new Vector2();
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        public static TFloat Distance(Vector2 vector1, Vector2 vector2) {
            return (vector2 - vector1).Length;
        }

        public static TFloat DistanceSquared(Vector2 vector1, Vector2 vector2) {
            return (vector2 - vector1).LengthSquared;            
        }

        public static TFloat Cross(Vector2 vector1, Vector2 vector2) {
            return Vector2.CrossProduct(vector1, vector2);
        }
    }

    public static class XamlVector3 {
        public static TFloat Length(this Vector3 vector) {
            return vector.Length;
        }

        public static TFloat LengthSquared(this Vector3 vector) {
            return vector.LengthSquared;
        }

        public static Vector3 Normalize(Vector3 value) {
            value.Normalize();
            return value;
        }

        public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
            result = value1 + value2;
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        public static void Multiply(ref Vector3 value1, TFloat scaleFactor, out Vector3 result) {
            result = new Vector3();
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        public static Vector3 Multiply(Vector3 value1, Vector3 value2) {
            return new Vector3(value1.X * value2.X, value1.Y * value2.Y, value1.Z * value2.Z);
        }

        public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result) {
            result = new Vector3();
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        public static TFloat Distance(Vector3 vector1, Vector3 vector2) {
            return (vector2 - vector1).Length;
        }

        public static TFloat DistanceSquared(Vector3 vector1, Vector3 vector2) {
            return (vector2 - vector1).LengthSquared;
        }

        public static Vector3 Cross(Vector3 vector1, Vector3 vector2) {
            return Vector3.CrossProduct(vector1, vector2);
        }
    }

    public static class XamlQuaternion {
        public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2) {
            return Quaternion.Multiply(quaternion1, quaternion2);
        }

        public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result) {
            result = Quaternion.Multiply(quaternion1, quaternion2);
        }
    }
#endif
}
