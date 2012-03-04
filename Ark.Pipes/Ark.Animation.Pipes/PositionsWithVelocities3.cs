using Ark.Abstract;

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

namespace Ark.Animation {
    public struct PositionWithVelocity3 : IIsDerivativeOf<PositionWithVelocity3, TFloat>, IIsDerivativeOfEx<PositionWithVelocity3, DeltaT> {
        public Vector3 Position;
        public Vector3 Velocity;

        public PositionWithVelocity3(Vector3 position, Vector3 velocity) {
            Position = position;
            Velocity = velocity;
        }

        public PositionWithVelocity3 MakeStep(ref PositionWithVelocity3 state, ref TFloat arg, ref TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public PositionWithVelocity3 MakeStep(ref PositionWithVelocity3 state, ref DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public static PositionWithVelocity3 operator *(PositionWithVelocity3 op, DeltaT dt) {
            return new PositionWithVelocity3(op.Position * dt, op.Velocity * dt);
        }

        public static PositionWithVelocity3 operator +(PositionWithVelocity3 op1, PositionWithVelocity3 op2) {
            return new PositionWithVelocity3(op1.Position + op2.Position, op1.Velocity + op2.Velocity);
        }

        public static PositionWithVelocity3 operator -(PositionWithVelocity3 op1, PositionWithVelocity3 op2) {
            return new PositionWithVelocity3(op1.Position - op2.Position, op1.Velocity - op2.Velocity);
        }
    }

    public struct OrientedPosition3WithVelocities : IIsDerivativeOf<OrientedPosition3WithVelocities, TFloat>, IIsDerivativeOfEx<OrientedPosition3WithVelocities, DeltaT> {
        public OrientedPosition3 Value;
        public OrientedPosition3 D;

        public OrientedPosition3WithVelocities(OrientedPosition3 value, OrientedPosition3 d) {
            Value = value;
            D = d;
        }

        public OrientedPosition3WithVelocities MakeStep(ref OrientedPosition3WithVelocities state, ref TFloat arg, ref TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public OrientedPosition3WithVelocities MakeStep(ref OrientedPosition3WithVelocities state, ref DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public static OrientedPosition3WithVelocities operator *(OrientedPosition3WithVelocities op, DeltaT dt) {
            return new OrientedPosition3WithVelocities(op.Value * dt, op.D * dt);
        }

        public static OrientedPosition3WithVelocities operator +(OrientedPosition3WithVelocities op1, OrientedPosition3WithVelocities op2) {
            return new OrientedPosition3WithVelocities(op1.Value + op2.Value, op1.D + op2.D);
        }

        public static OrientedPosition3WithVelocities operator -(OrientedPosition3WithVelocities op1, OrientedPosition3WithVelocities op2) {
            return new OrientedPosition3WithVelocities(op1.Value - op2.Value, op1.D - op2.D);
        }
    }
}