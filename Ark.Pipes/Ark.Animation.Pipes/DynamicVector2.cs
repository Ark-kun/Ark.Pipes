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

namespace Ark.Geometry { //.Pipes
    public static class DynamicVector2 {
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