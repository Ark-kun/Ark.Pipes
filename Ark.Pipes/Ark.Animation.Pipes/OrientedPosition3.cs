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
    public struct OrientedPosition3 : IIsDerivativeOf<OrientedPosition3, TFloat>, IIsDerivativeOfEx<OrientedPosition3, DeltaT>, IAdditive<OrientedPosition3>, IMultiplicative<DeltaT, OrientedPosition3> {
        public Vector3 Position;
        public Quaternion Orientation;

        public OrientedPosition3(Vector3 position, Quaternion orientation) {
            Position = position;
            Orientation = orientation;
        }

        public OrientedPosition3(ref Vector3 position, ref Quaternion orientation) {
            Position = position;
            Orientation = orientation;
        }

        public OrientedPosition3 MakeStep(OrientedPosition3 state, TFloat arg, TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public void MakeStep(ref OrientedPosition3 state, ref TFloat arg, ref TFloat newArg, out OrientedPosition3 result) {
            DeltaT deltaArg = newArg - arg;
            MakeStep(ref state, ref deltaArg, out result);
        }

        public OrientedPosition3 MakeStep(OrientedPosition3 state, DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public void MakeStep(ref OrientedPosition3 state, ref DeltaT deltaArg, out OrientedPosition3 result) {
            Vector3.Multiply(ref Position, deltaArg, out result.Position);
            Vector3.Add(ref result.Position, ref state.Position, out result.Position);
            Quaternion.Multiply(ref Orientation, deltaArg, out result.Orientation);
            Quaternion.Add(ref result.Orientation, ref state.Orientation, out result.Orientation);
        }

        public OrientedPosition3 Plus(OrientedPosition3 value) {
            return this + value;
        }

        public void Plus(ref OrientedPosition3 value, out OrientedPosition3 result) {
            Add(ref this, ref value, out result);
        }

        public OrientedPosition3 MultipliedBy(DeltaT multiplier) {
            return this * multiplier;
        }

        public void MultipliedBy(ref DeltaT multiplier, out OrientedPosition3 result) {
            Multiply(ref this, multiplier, out result);
        }

        public static void Add(ref OrientedPosition3 value1, ref OrientedPosition3 value2, out OrientedPosition3 result) {
            Vector3.Add(ref value1.Position, ref value2.Position, out result.Position);
            Quaternion.Add(ref value1.Orientation, ref value2.Orientation, out result.Orientation);
        }

        public static void Multiply(ref OrientedPosition3 value, DeltaT multiplier, out OrientedPosition3 result) {
            Vector3.Multiply(ref value.Position, multiplier, out result.Position);
            Quaternion.Multiply(ref value.Orientation, multiplier, out result.Orientation);
        }

        public static OrientedPosition3 operator *(OrientedPosition3 op, DeltaT dt) {
            return new OrientedPosition3(op.Position * dt, op.Orientation * dt);
        }

        public static OrientedPosition3 operator +(OrientedPosition3 op1, OrientedPosition3 op2) {
            return new OrientedPosition3(op1.Position + op2.Position, op1.Orientation + op2.Orientation);
        }

        public static OrientedPosition3 operator -(OrientedPosition3 op1, OrientedPosition3 op2) {
            return new OrientedPosition3(op1.Position - op2.Position, op1.Orientation - op2.Orientation);
        }
    }

    public sealed class OrientedPosition3Components {
        public Provider<Vector3> Position;
        public Provider<Quaternion> Orientation;

        public OrientedPosition3Components() : this(Constant<Vector3>.Default, Constant<Quaternion>.Default) { }

        public OrientedPosition3Components(Provider<Vector3> position, Provider<Quaternion> orientation) {
            Position = position;
            Orientation = orientation;
        }

        public static OrientedPosition3Components FromOrientedPositions3(Provider<OrientedPosition3> orientedPositions) {
            return new OrientedPosition3Components() {
                Position = Provider<Vector3>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<Quaternion>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<OrientedPosition3> ToOrientedPositions3() {
            return Provider<OrientedPosition3>.Create((p, o) => new OrientedPosition3(p, o), Position, Orientation);
        }
    }
}
