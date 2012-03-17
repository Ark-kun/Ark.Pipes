﻿using System;
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
    public static class Extensions_Pipes {
        #region 2D
        public static Provider<TFloat> Length(this Provider<Vector2> vectors) {
            return Provider.Create((v) => v.Length(), vectors);
        }

        public static Provider<TFloat> LengthSquared(this Provider<Vector2> vectors) {
            return Provider.Create((v) => v.LengthSquared(), vectors);
        }

        public static Provider<TFloat> DistanceTo(this Provider<Vector2> vectors1, Provider<Vector2> vectors2) {
            return Provider.Create((v1, v2) => StaticVector2.Distance(v1, v2), vectors1, vectors2);
        }

        public static Provider<TFloat> DistanceToSquared(this Provider<Vector2> vectors1, Provider<Vector2> vectors2) {
            return Provider.Create((v1, v2) => StaticVector2.DistanceSquared(v1, v2), vectors1, vectors2);
        }

        public static Provider<Vector2> Add(this Provider<Vector2> v1s, Provider<Vector2> v2s) {
            return Provider.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<Vector2> Add(this Provider<Vector2> v1s, Vector2 v2) {
            return Provider.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<Vector2> Multiply(this Provider<Vector2> vs, TFloat multiplier) {
            return Provider.Create((v) => v * multiplier, vs);
        }

        public static Provider<Vector2> Divide(this Provider<Vector2> vs, TFloat divider) {
            return vs.Multiply(1 / divider);
        }

        public static Provider<Vector2> Negate(this Provider<Vector2> vs) {
            return Provider.Create((v) => -v, vs);
        }

        public static Provider<Vector2> Subtract(this Provider<Vector2> v1s, Provider<Vector2> v2s) {
            return Provider.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<Vector2> Subtract(this Provider<Vector2> v1s, Vector2 v2) {
            return Provider.Create((v1) => v1 - v2, v1s);
        }

        public static Provider<Vector2> Scale(this Provider<Vector2> vectors, TFloat scaleFactor) {
            return Provider.Create((v) => v * scaleFactor, vectors);
        }

        public static Provider<Vector2> Scale(this Provider<Vector2> vectors, Provider<TFloat> scaleFactors) {
            return Provider.Create((v, scaleFactor) => v * scaleFactor, vectors, scaleFactors);
        }

        public static Provider<Vector2> Scale(this Provider<Vector2> vectors, Vector2 scale) {
            return Provider.Create((v) => StaticVector2.Multiply(v, scale), vectors);
        }

        public static Provider<Vector2> Scale(this Provider<Vector2> vectors, Provider<Vector2> scales) {
            return Provider.Create((v, scale) => StaticVector2.Multiply(v, scale), vectors, scales);
        }

        public static Provider<Vector2> Normalize(this Provider<Vector2> vectors) {
            return Provider.Create((v) => StaticVector2.Normalize(v), vectors);
        }

#if FRAMEWORK_ARK || FRAMEWORK_XNA
        public static Provider<Vector2> Transform(this Provider<Vector2> vectors, Quaternion rotation) {
            return Provider.Create((v) => Vector2.Transform(v, rotation), vectors);
        }

        public static Provider<Vector2> Transform(this Provider<Vector2> vectors, Matrix matrix) {
            return Provider.Create((v) => Vector2.Transform(v, matrix), vectors);
        }

        public static Provider<Vector2> Transform(this Provider<Vector2> vectors, Provider<Quaternion> rotations) {
            return Provider.Create((v, rotation) => Vector2.Transform(v, rotation), vectors, rotations);
        }

        public static Provider<Vector2> Transform(this Provider<Vector2> vectors, Provider<Matrix> matrices) {
            return Provider.Create((v, matrix) => Vector2.Transform(v, matrix), vectors, matrices);
        }

        public static Provider<Vector2> Lerp(this Provider<Vector2> v1s, Provider<Vector2> v2s, Provider<TFloat> amounts) {
            return Provider.Create((v1, v2, amount) => Vector2.Lerp(v1, v2, amount), v1s, v2s, amounts);
        }
#endif
        public static Vector2Components ToComponents(this Provider<Vector2> vectors) {
            return new Vector2Components(vectors);
        }

        public static OrientedPosition2Components ToComponents(this Provider<OrientedPosition2> ops) {
            return OrientedPosition2Components.FromOrientedPositions2(ops);
        }

        public static Provider<Vector3> ToVectors3XY(this Provider<Vector2> vectors) {
            return Provider.Create((v) => new Vector3(v.X, v.Y, 0), vectors);
        }

        public static Provider<Vector3> ToVectors3XZ(this Provider<Vector2> vectors) {
            return Provider.Create((v) => new Vector3(v.X, 0, v.Y), vectors);
        }
        #endregion

        #region 3D
        public static Provider<TFloat> Length(this Provider<Vector3> vectors) {
            return Provider.Create((v) => v.Length(), vectors);
        }

        public static Provider<TFloat> LengthSquared(this Provider<Vector3> vectors) {
            return Provider.Create((v) => v.LengthSquared(), vectors);
        }

        public static Provider<TFloat> DistanceTo(this Provider<Vector3> vectors1, Provider<Vector3> vectors2) {
            return Provider.Create((v1, v2) => StaticVector3.Distance(v1, v2), vectors1, vectors2);
        }

        public static Provider<TFloat> DistanceToSquared(this Provider<Vector3> vectors1, Provider<Vector3> vectors2) {
            return Provider.Create((v1, v2) => StaticVector3.DistanceSquared(v1, v2), vectors1, vectors2);
        }

        public static Provider<Vector3> Add(this Provider<Vector3> v1s, Provider<Vector3> v2s) {
            return Provider.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<Vector3> Add(this Provider<Vector3> v1s, Vector3 v2) {
            return Provider.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<Vector3> Multiply(this Provider<Vector3> vs, TFloat multiplier) {
            return Provider.Create((v) => v * multiplier, vs);
        }

        public static Provider<Vector3> Divide(this Provider<Vector3> vs, TFloat divider) {
            return vs.Multiply(1 / divider);
        }

        public static Provider<Vector3> Negate(this Provider<Vector3> vs) {
            return Provider.Create((v) => -v, vs);
        }

        public static Provider<Vector3> Subtract(this Provider<Vector3> v1s, Provider<Vector3> v2s) {
            return Provider.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<Vector3> Subtract(this Provider<Vector3> v1s, Vector3 v2) {
            return Provider.Create((v1) => v1 - v2, v1s);
        }

        public static Provider<Vector3> Scale(this Provider<Vector3> vectors, TFloat scaleFactor) {
            return Provider.Create((v) => v * scaleFactor, vectors);
        }

        public static Provider<Vector3> Scale(this Provider<Vector3> vectors, Provider<TFloat> scaleFactors) {
            return Provider.Create((v, scaleFactor) => v * scaleFactor, vectors, scaleFactors);
        }

        public static Provider<Vector3> Scale(this Provider<Vector3> vectors, Vector3 scale) {
            return Provider.Create((v) => StaticVector3.Multiply(v, scale), vectors);
        }

        public static Provider<Vector3> Scale(this Provider<Vector3> vectors, Provider<Vector3> scales) {
            return Provider.Create((v, scale) => StaticVector3.Multiply(v, scale), vectors, scales);
        }

        public static Provider<Vector3> Normalize(this Provider<Vector3> vectors) {
            return Provider.Create((v) => StaticVector3.Normalize(v), vectors);
        }

#if FRAMEWORK_ARK || FRAMEWORK_XNA
        public static Provider<Vector3> Transform(this Provider<Vector3> vectors, Quaternion rotation) {
            return Provider.Create((v) => Vector3.Transform(v, rotation), vectors);
        }

        public static Provider<Vector3> Transform(this Provider<Vector3> vectors, Matrix matrix) {
            return Provider.Create((v) => Vector3.Transform(v, matrix), vectors);
        }

        public static Provider<Vector3> Transform(this Provider<Vector3> vectors, Provider<Quaternion> rotations) {
            return Provider.Create((v, rotation) => Vector3.Transform(v, rotation), vectors, rotations);
        }

        public static Provider<Vector3> Transform(this Provider<Vector3> vectors, Provider<Matrix> matrices) {
            return Provider.Create((v, matrix) => Vector3.Transform(v, matrix), vectors, matrices);
        }

        public static Provider<Vector3> Lerp(this Provider<Vector3> v1s, Provider<Vector3> v2s, Provider<TFloat> amounts) {
            return Provider.Create((v1, v2, amount) => Vector3.Lerp(v1, v2, amount), v1s, v2s, amounts);
        }
#endif

        public static Vector3Components ToComponents(this Provider<Vector3> vectors) {
            return new Vector3Components(vectors);
        }

        public static OrientedPosition3Components ToComponents(this Provider<OrientedPosition3> ops) {
            return OrientedPosition3Components.FromOrientedPositions3(ops);
        }

        public static Provider<Vector2> ToVectors2XY(this Provider<Vector3> vectors) {
            return Provider.Create((v) => new Vector2(v.X, v.Y), vectors);
        }

        public static Provider<Vector2> ToVectors2XZ(this Provider<Vector3> vectors) {
            return Provider.Create((v) => new Vector2(v.X, v.Z), vectors);
        }
        #endregion
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

namespace Ark.Animation { //.Pipes
    public static class Extensions_Pipes {
        #region Timers
        public static Provider<TFloat> Add(this Provider<TFloat> v1s, Provider<TFloat> v2s) {
            return Provider.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<TFloat> Add(this Provider<TFloat> v1s, TFloat v2) {
            return Provider.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<TFloat> Subtract(this Provider<TFloat> v1s, Provider<TFloat> v2s) {
            return Provider.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<TFloat> Subtract(this Provider<TFloat> v1s, TFloat v2) {
            return Provider.Create((v1) => v1 - v2, v1s);
        }

        public static Provider<TFloat> Accelerate(this Provider<TFloat> ts, TFloat multiplier) {
            TFloat t0 = ts.Value;
            return Provider.Create((t) => t0 + (t - t0) * multiplier, ts);
        }

        public static Provider<TFloat> Reset(this Provider<TFloat> timer) {
            TFloat t0 = timer.Value;
            return Provider.Create((t) => t - t0, timer);
        }

        public static Provider<DeltaT> ToDeltaTs(this Provider<TFloat> timer) {
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                TFloat oldTime = time;
                time = newTime;
                return (DeltaT)(newTime - oldTime);
            }, timer);
        }

        public static Provider<Tuple<TFloat, DeltaT>> AddDeltaTs(this Provider<TFloat> timer) {
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                TFloat oldTime = time;
                time = newTime;
                return new Tuple<TFloat, DeltaT>(newTime, newTime - oldTime);
            }, timer);
        }
        #endregion

        public static Provider<OrientedPosition2> AddOrientations(this Provider<Vector2> positions) {
            Vector2 position = positions.Value;
            TFloat orientation = 0;
            return Provider.Create((newPosition) => {
                Vector2 delta = newPosition - position;
                position = newPosition;
                if (!delta.IsZero()) {
                    orientation = delta.Angle();
                }
                return new OrientedPosition2(newPosition, orientation);
            }, positions);
        }
    }
}

