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
using StaticVector2 = Ark.Geometry.Primitives.Double.Vector2;
using StaticVector3 = Ark.Geometry.Primitives.Double.Vector3;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
using StaticVector2 = Ark.Geometry.Primitives.Single.Vector2;
using StaticVector3 = Ark.Geometry.Primitives.Single.Vector3;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
using StaticVector2 = Microsoft.Xna.Framework.Vector2;
using StaticVector3 = Microsoft.Xna.Framework.Vector3;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Ark.Geometry.Primitives;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
using StaticVector2 = Ark.Geometry.Primitives.XamlVector2;
using StaticVector3 = Ark.Geometry.Primitives.XamlVector3;
#else
#error Bad geometry framework
#endif

namespace Ark.Geometry {
    public static class Extensions {
        public static bool IsZero(this Vector2 vector) {
            return vector.X == 0 && vector.Y == 0;
        }

        public static bool IsZero(this Vector3 vector) {
            return vector.X == 0 && vector.Y == 0 && vector.Z == 0;
        }

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
            return (TFloat)Math.Atan2(v.Y, v.X);
        }

        public static Vector2 Rotate(this Vector2 v, TFloat angle) {
            TFloat sin = (TFloat)Math.Sin(angle);
            TFloat cos = (TFloat)Math.Cos(angle);
            return new Vector2(v.X * cos - v.Y * sin, v.X * sin + v.Y * cos);
        }

        public static Quaternion MultiplyAngle(this Quaternion q, TFloat multiplier) {
            TFloat oldCos = q.W;
            TFloat oldSin = (TFloat)Math.Sqrt(q.X * q.X + q.Y * q.Y + q.Z * q.Z);
            if (oldSin == 0) {
                return Quaternion.Identity;
            }
            TFloat oldAngle = (TFloat)Math.Atan2(oldSin, oldCos);
            TFloat newAngle = oldAngle * multiplier;
            TFloat newCos = (TFloat)Math.Cos(newAngle);
            TFloat newSin = (TFloat)Math.Sin(newAngle);
            TFloat k = newSin / oldSin;
            return new Quaternion(k * q.X, k * q.Y, k * q.Z, newCos);
        }
    }
}