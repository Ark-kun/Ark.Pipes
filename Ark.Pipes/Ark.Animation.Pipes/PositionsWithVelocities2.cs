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
    public struct PositionWithVelocity2 : IIsDerivativeOf<PositionWithVelocity2, TFloat>, IIsDerivativeOfEx<PositionWithVelocity2, DeltaT>, IAdditive<PositionWithVelocity2>, IMultiplicative<DeltaT, PositionWithVelocity2> {
        public Vector2 Position;
        public Vector2 Velocity;

        public PositionWithVelocity2(Vector2 position, Vector2 velocity) {
            Position = position;
            Velocity = velocity;
        }

        public PositionWithVelocity2(ref Vector2 position, ref Vector2 velocity) {
            Position = position;
            Velocity = velocity;
        }

        public PositionWithVelocity2 MakeStep(PositionWithVelocity2 state, TFloat arg, TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public void MakeStep(ref PositionWithVelocity2 state, ref TFloat arg, ref TFloat newArg, out PositionWithVelocity2 result) {
            DeltaT deltaArg = newArg - arg;
            MakeStep(ref state, ref deltaArg, out result);
        }

        public PositionWithVelocity2 MakeStep(PositionWithVelocity2 state, DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public void MakeStep(ref PositionWithVelocity2 state, ref DeltaT deltaArg, out PositionWithVelocity2 result) {
            Vector2.Multiply(ref Position, deltaArg, out result.Position);
            Vector2.Add(ref result.Position, ref state.Position, out result.Position);
            Vector2.Multiply(ref Velocity, deltaArg, out result.Velocity);
            Vector2.Add(ref result.Velocity, ref state.Velocity, out result.Velocity);
        }

        public PositionWithVelocity2 Plus(PositionWithVelocity2 value) {
            return this + value;
        }

        public void Plus(ref PositionWithVelocity2 value, out PositionWithVelocity2 result) {
            Add(ref this, ref value, out result);
        }

        public PositionWithVelocity2 MultipliedBy(DeltaT multiplier) {
            return this * multiplier;
        }

        public void MultipliedBy(ref DeltaT multiplier, out PositionWithVelocity2 result) {
            Multiply(ref this, multiplier, out result);
        }

        public static void Add(ref PositionWithVelocity2 value1, ref PositionWithVelocity2 value2, out PositionWithVelocity2 result) {
            Vector2.Add(ref value1.Position, ref value2.Position, out result.Position);
            Vector2.Add(ref value1.Velocity, ref value2.Velocity, out result.Velocity);
        }

        public static void Multiply(ref PositionWithVelocity2 value, DeltaT multiplier, out PositionWithVelocity2 result) {
            Vector2.Multiply(ref value.Position, multiplier, out result.Position);
            Vector2.Multiply(ref value.Velocity, multiplier, out result.Velocity);
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

    public struct OrientedPosition2WithVelocities : IIsDerivativeOf<OrientedPosition2WithVelocities, TFloat>, IIsDerivativeOfEx<OrientedPosition2WithVelocities, DeltaT>, IAdditive<OrientedPosition2WithVelocities>, IMultiplicative<DeltaT, OrientedPosition2WithVelocities> {
        public OrientedPosition2 Value;
        public OrientedPosition2 D;

        public OrientedPosition2WithVelocities(OrientedPosition2 value, OrientedPosition2 d) {
            Value = value;
            D = d;
        }

        public OrientedPosition2WithVelocities(ref OrientedPosition2 value, ref OrientedPosition2 d) {
            Value = value;
            D = d;
        }

        public OrientedPosition2WithVelocities MakeStep(OrientedPosition2WithVelocities state, TFloat arg, TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public void MakeStep(ref OrientedPosition2WithVelocities state, ref TFloat arg, ref TFloat newArg, out OrientedPosition2WithVelocities result) {
            DeltaT deltaArg = newArg - arg;
            MakeStep(ref state, ref deltaArg, out result);
        }

        public OrientedPosition2WithVelocities MakeStep(OrientedPosition2WithVelocities state, DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public void MakeStep(ref OrientedPosition2WithVelocities state, ref DeltaT deltaArg, out OrientedPosition2WithVelocities result) {
            OrientedPosition2.Multiply(ref Value, deltaArg, out result.Value);
            OrientedPosition2.Add(ref result.Value, ref state.Value, out result.Value);
            OrientedPosition2.Multiply(ref D, deltaArg, out result.D);
            OrientedPosition2.Add(ref result.D, ref state.D, out result.D);
        }

        public OrientedPosition2WithVelocities Plus(OrientedPosition2WithVelocities value) {
            return this + value;
        }

        public void Plus(ref OrientedPosition2WithVelocities value, out OrientedPosition2WithVelocities result) {
            Add(ref this, ref value, out result);
        }

        public OrientedPosition2WithVelocities MultipliedBy(DeltaT multiplier) {
            return this * multiplier;
        }

        public void MultipliedBy(ref DeltaT multiplier, out OrientedPosition2WithVelocities result) {
            Multiply(ref this, multiplier, out result);
        }

        public static void Add(ref OrientedPosition2WithVelocities value1, ref OrientedPosition2WithVelocities value2, out OrientedPosition2WithVelocities result) {
            OrientedPosition2.Add(ref value1.Value, ref value2.Value, out result.Value);
            OrientedPosition2.Add(ref value1.D, ref value2.D, out result.D);
        }

        public static void Multiply(ref OrientedPosition2WithVelocities value, DeltaT multiplier, out OrientedPosition2WithVelocities result) {
            OrientedPosition2.Multiply(ref value.Value, multiplier, out result.Value);
            OrientedPosition2.Multiply(ref value.D, multiplier, out result.D);
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