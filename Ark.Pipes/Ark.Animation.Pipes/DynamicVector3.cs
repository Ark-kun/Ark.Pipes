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
    public static class DynamicVector3 {
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
    }
}