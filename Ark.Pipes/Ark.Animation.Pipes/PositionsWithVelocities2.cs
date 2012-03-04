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
    public struct PositionWithVelocity2 : IIsDerivativeOf<PositionWithVelocity2, TFloat>, IIsDerivativeOfEx<PositionWithVelocity2, DeltaT> {
        public Vector2 Position;
        public Vector2 Velocity;

        public PositionWithVelocity2(Vector2 position, Vector2 velocity) {
            Position = position;
            Velocity = velocity;
        }

        public PositionWithVelocity2 MakeStep(ref PositionWithVelocity2 state, ref TFloat arg, ref TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public PositionWithVelocity2 MakeStep(ref PositionWithVelocity2 state, ref DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public static PositionWithVelocity2 operator *(PositionWithVelocity2 op, DeltaT dt) {
            return new PositionWithVelocity2(op.Position * dt, op.Velocity * dt);
        }

        public static PositionWithVelocity2 operator +(PositionWithVelocity2 op1, PositionWithVelocity2 op2) {
            return new PositionWithVelocity2(op1.Position + op2.Position, op1.Velocity + op2.Velocity);
        }

        public static PositionWithVelocity2 operator -(PositionWithVelocity2 op1, PositionWithVelocity2 op2) {
            return new PositionWithVelocity2(op1.Position - op2.Position, op1.Velocity - op2.Velocity);
        }
    }

    public struct OrientedPosition2WithVelocities : IIsDerivativeOf<OrientedPosition2WithVelocities, TFloat>, IIsDerivativeOfEx<OrientedPosition2WithVelocities, DeltaT> {
        public OrientedPosition2 Value;
        public OrientedPosition2 D;

        public OrientedPosition2WithVelocities(OrientedPosition2 value, OrientedPosition2 d) {
            Value = value;
            D = d;
        }

        public OrientedPosition2WithVelocities MakeStep(ref OrientedPosition2WithVelocities state, ref TFloat arg, ref TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public OrientedPosition2WithVelocities MakeStep(ref OrientedPosition2WithVelocities state, ref DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public static OrientedPosition2WithVelocities operator *(OrientedPosition2WithVelocities op, DeltaT dt) {
            return new OrientedPosition2WithVelocities(op.Value * dt, op.D * dt);
        }

        public static OrientedPosition2WithVelocities operator +(OrientedPosition2WithVelocities op1, OrientedPosition2WithVelocities op2) {
            return new OrientedPosition2WithVelocities(op1.Value + op2.Value, op1.D + op2.D);
        }

        public static OrientedPosition2WithVelocities operator -(OrientedPosition2WithVelocities op1, OrientedPosition2WithVelocities op2) {
            return new OrientedPosition2WithVelocities(op1.Value - op2.Value, op1.D - op2.D);
        }
    }
}