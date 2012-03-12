using Ark.Abstract;
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

namespace Ark.Animation { //.Pipes {
    public struct OrientedPosition2 : IIsDerivativeOf<OrientedPosition2, TFloat>, IIsDerivativeOfEx<OrientedPosition2, DeltaT>, IAdditive<OrientedPosition2>, IMultiplicative<DeltaT, OrientedPosition2> {
        public Vector2 Position;
        public TFloat Orientation;

        public OrientedPosition2(Vector2 position, TFloat orientation) {
            Position = position;
            Orientation = orientation;
        }

        public OrientedPosition2(ref Vector2 position, ref TFloat orientation) {
            Position = position;
            Orientation = orientation;
        }

        public OrientedPosition2 MakeStep(OrientedPosition2 state, TFloat arg, TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public void MakeStep(ref OrientedPosition2 state, ref TFloat arg, ref TFloat newArg, out OrientedPosition2 result) {
            DeltaT deltaArg = newArg - arg;
            MakeStep(ref state, ref deltaArg, out result);
        }

        public OrientedPosition2 MakeStep(OrientedPosition2 state, DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public void MakeStep(ref OrientedPosition2 state, ref DeltaT deltaArg, out OrientedPosition2 result) {
            Vector2.Multiply(ref Position, deltaArg, out result.Position);
            Vector2.Add(ref result.Position, ref state.Position, out result.Position);
            result.Orientation = Orientation + state.Orientation;
        }

        public OrientedPosition2 Plus(OrientedPosition2 value) {
            return this + value;
        }

        public void Plus(ref OrientedPosition2 value, out OrientedPosition2 result) {
            Add(ref this, ref value, out result);
        }

        public OrientedPosition2 MultipliedBy(DeltaT multiplier) {
            return this * multiplier;
        }

        public void MultipliedBy(ref DeltaT multiplier, out OrientedPosition2 result) {
            Multiply(ref this, multiplier, out result);
        }

        public static void Add(ref OrientedPosition2 value1, ref OrientedPosition2 value2, out OrientedPosition2 result) {
            Vector2.Add(ref value1.Position, ref value2.Position, out result.Position);
            result.Orientation = value1.Orientation + value2.Orientation;
        }

        public static void Multiply(ref OrientedPosition2 value, DeltaT multiplier, out OrientedPosition2 result) {
            Vector2.Multiply(ref value.Position, multiplier, out result.Position);
            result.Orientation = value.Orientation * multiplier;
        }

        public static OrientedPosition2 operator *(OrientedPosition2 op, DeltaT dt) {
            return new OrientedPosition2(op.Position * dt, op.Orientation * dt);
        }

        public static OrientedPosition2 operator +(OrientedPosition2 op1, OrientedPosition2 op2) {
            return new OrientedPosition2(op1.Position + op2.Position, op1.Orientation + op2.Orientation);
        }

        public static OrientedPosition2 operator -(OrientedPosition2 op1, OrientedPosition2 op2) {
            return new OrientedPosition2(op1.Position - op2.Position, op1.Orientation - op2.Orientation);
        }
    }

    public sealed class OrientedPosition2Components {
        public Provider<Vector2> Position;
        public Provider<TFloat> Orientation;

        public OrientedPosition2Components() : this(Constant<Vector2>.Default, Constant<TFloat>.Default) { }

        public OrientedPosition2Components(Provider<Vector2> position, Provider<TFloat> orientation) {
            Position = position;
            Orientation = orientation;
        }

        public static OrientedPosition2Components FromOrientedPositions2(Provider<OrientedPosition2> orientedPositions) {
            return new OrientedPosition2Components() {
                Position = Provider<Vector2>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<TFloat>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<OrientedPosition2> ToOrientedPositions2() {
            return Provider<OrientedPosition2>.Create((p, o) => new OrientedPosition2(p, o), Position, Orientation);
        }
    }
}