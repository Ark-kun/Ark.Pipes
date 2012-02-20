using System;
using Ark.Animation.Pipes;
using Ark.Geometry.Pipes;
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
        public Vector2 Position(Vector2 origin, Vector2 direction, float t) {
            return origin + direction * t;
        }

        public OrientedPosition2 OrientedPosition(Vector2 origin, Vector2 direction, float t) {
            Vector2 position = Position(origin, direction, t);
            TFloat orientation = (TFloat)direction.Angle();
            return new OrientedPosition2(position, orientation);
        }

        public Vector2 Position(Tuple<Vector2, Vector2> parameters, float t) {
            return Position(parameters.Item1, parameters.Item2, t);
        }

        public OrientedPosition2 OrientedPosition(Tuple<Vector2, Vector2> parameters, float t) {
            return OrientedPosition(parameters.Item1, parameters.Item2, t);
        }
    }

    public class Orbit {
        public Vector2 Position(Vector2 center, TFloat radius, TFloat angularVelocity, TFloat phase, float t) {
            TFloat angle = phase + angularVelocity * t;
            return center + new Vector2((TFloat)Math.Cos(angle), (TFloat)Math.Sin(angle)) * radius;
        }

        public OrientedPosition2 OrientedPosition(Vector2 center, TFloat radius, TFloat angularVelocity, float t) {
            return OrientedPosition(center, radius, angularVelocity, 0, t);
        }

        public OrientedPosition2 OrientedPosition(Vector2 center, TFloat radius, TFloat angularVelocity, TFloat phase, float t) {
            Vector2 position = Position(center, radius, angularVelocity, phase, t);
            TFloat orientation = phase + angularVelocity * t + Math.Sign(angularVelocity) * (TFloat)Math.PI / 2;
            return new OrientedPosition2(position, orientation);
        }

        public Vector2 Position(Tuple<Vector2, TFloat, TFloat, TFloat> parameters, float t) {
            return Position(parameters.Item1, parameters.Item2, parameters.Item3, parameters.Item4, t);
        }

        public OrientedPosition2 OrientedPosition(Tuple<Vector2, TFloat, TFloat, TFloat> parameters, float t) {
            return OrientedPosition(parameters.Item1, parameters.Item2, parameters.Item3, parameters.Item4, t);
        }

        public OrientedPosition2 OrientedPosition(Tuple<Vector2, TFloat, TFloat> parameters, float t) {
            return OrientedPosition(parameters.Item1, parameters.Item2, parameters.Item3, t);
        }
    }

    public class InfinitySign {
        public Vector2 CanonicalPosition(float t) {
            double a = t;
            double sqrt2 = Math.Sqrt(2);
            var sinA = Math.Sin(a);
            var cosA = Math.Cos(a);
            var denominator = 2 * sqrt2 * cosA - 3;
            var sinB = sinA / denominator;
            var cosB = (2 * sqrt2 - 3 * cosA) / denominator;
            var C = new Vector2((float)cosA - 1, (float)sinA);
            var D = new Vector2((float)cosB + 1, (float)sinB);
            var E = (C + D) / 2;

            return E;
        }

        public Vector2 Position(Vector2 focus1, Vector2 focus2, float t) {
            Vector2 r1 = focus1;
            Vector2 r2 = focus2;
            var direction = r2 - r1;
            var scale = direction.Length() / 2;
            var center = (r1 + r2) / 2;
            var result = CanonicalPosition(t);
            result = center + result.Rotate(direction.Angle()) * scale;
            return result;
        }

        public Vector2 Position(Tuple<Vector2, Vector2> parameters, float t) {
            return Position(parameters.Item1, parameters.Item2, t);
        }
    }
}

namespace Ark.Geometry.Curves.Pipes {
    public class CurveMovement {
        public static Provider<TResult> Create<TResult, T1>(Func<T1, float, TResult> curve, Provider<T1> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2>(Func<T1, T2, float, TResult> curve, Provider<T1> param1, Provider<T2> param2, Provider<float> time) {
            return Provider<TResult>.Create((p1, p2, t) => curve(p1, p2, t), param1, param2, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2>(Func<Tuple<T1, T2>, float, TResult> curve, Provider<Tuple<T1, T2>> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2, T3>(Func<T1, T2, T3, float, TResult> curve, Provider<T1> param1, Provider<T2> param2, Provider<T3> param3, Provider<float> time) {
            return Provider<TResult>.Create((p1, p2, p3, t) => curve(p1, p2, p3, t), param1, param2, param3, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2, T3>(Func<Tuple<T1, T2, T3>, float, TResult> curve, Provider<Tuple<T1, T2, T3>> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }

        //public static Provider<TResult> Create<TResult, T1, T2, T3, T4>(Func<T1, T2, T3, T4, float, TResult> curve, Provider<T1> param1, Provider<T2> param2, Provider<T3> param3, Provider<T4> param4, Provider<float> time) {
        //    return Provider<TResult>.Create((p1, p2, p3, p4, t) => curve(p1, p2, p3, p4, t), param1, param2, param3, param4, time);
        //}

        public static Provider<TResult> Create<TResult, T1, T2, T3, T4>(Func<Tuple<T1, T2, T3, T4>, float, TResult> curve, Provider<Tuple<T1, T2, T3, T4>> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }
    }
}