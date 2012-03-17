using Ark.Abstract;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
using StaticVector3 = Ark.Geometry.Primitives.Double.Vector3;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
using StaticVector3 = Ark.Geometry.Primitives.Single.Vector3;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
using StaticVector3 = Microsoft.Xna.Framework.Vector3;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows.Media.Media3D;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
using StaticVector3 = Ark.Geometry.XamlVector3;
#else
#error Bad geometry framework
#endif

namespace Ark.Animation {
    public struct PositionWithVelocity3 : IIsDerivativeOf<PositionWithVelocity3, TFloat>, IIsDerivativeOfEx<PositionWithVelocity3, DeltaT>, IAdditive<PositionWithVelocity3>, IMultiplicative<DeltaT, PositionWithVelocity3> {
        public Vector3 Position;
        public Vector3 Velocity;

        public PositionWithVelocity3(Vector3 position, Vector3 velocity) {
            Position = position;
            Velocity = velocity;
        }

        public PositionWithVelocity3(ref Vector3 position, ref Vector3 velocity) {
            Position = position;
            Velocity = velocity;
        }

        public PositionWithVelocity3 MakeStep(PositionWithVelocity3 state, TFloat arg, TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public void MakeStep(ref PositionWithVelocity3 state, ref TFloat arg, ref TFloat newArg, out PositionWithVelocity3 result) {
            DeltaT deltaArg = newArg - arg;
            MakeStep(ref state, ref deltaArg, out result);
        }

        public PositionWithVelocity3 MakeStep(PositionWithVelocity3 state, DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public void MakeStep(ref PositionWithVelocity3 state, ref DeltaT deltaArg, out PositionWithVelocity3 result) {
            StaticVector3.Multiply(ref Position, deltaArg, out result.Position);
            StaticVector3.Add(ref result.Position, ref state.Position, out result.Position);
            StaticVector3.Multiply(ref Velocity, deltaArg, out result.Velocity);
            StaticVector3.Add(ref result.Velocity, ref state.Velocity, out result.Velocity);
        }

        public PositionWithVelocity3 Plus(PositionWithVelocity3 value) {
            return this + value;
        }

        public void Plus(ref PositionWithVelocity3 value, out PositionWithVelocity3 result) {
            Add(ref this, ref value, out result);
        }

        public PositionWithVelocity3 MultipliedBy(DeltaT multiplier) {
            return this * multiplier;
        }

        public void MultipliedBy(ref DeltaT multiplier, out PositionWithVelocity3 result) {
            Multiply(ref this, multiplier, out result);
        }

        public static void Add(ref PositionWithVelocity3 value1, ref PositionWithVelocity3 value2, out PositionWithVelocity3 result) {
            StaticVector3.Add(ref value1.Position, ref value2.Position, out result.Position);
            StaticVector3.Add(ref value1.Velocity, ref value2.Velocity, out result.Velocity);
        }

        public static void Multiply(ref PositionWithVelocity3 value, DeltaT multiplier, out PositionWithVelocity3 result) {
            StaticVector3.Multiply(ref value.Position, multiplier, out result.Position);
            StaticVector3.Multiply(ref value.Velocity, multiplier, out result.Velocity);
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

    public struct OrientedPosition3WithVelocities : IIsDerivativeOf<OrientedPosition3WithVelocities, TFloat>, IIsDerivativeOfEx<OrientedPosition3WithVelocities, DeltaT>, IAdditive<OrientedPosition3WithVelocities>, IMultiplicative<DeltaT, OrientedPosition3WithVelocities> {
        public OrientedPosition3 Value;
        public OrientedPosition3 D;

        public OrientedPosition3WithVelocities(OrientedPosition3 value, OrientedPosition3 d) {
            Value = value;
            D = d;
        }

        public OrientedPosition3WithVelocities(ref OrientedPosition3 value, ref OrientedPosition3 d) {
            Value = value;
            D = d;
        }

        public OrientedPosition3WithVelocities MakeStep(OrientedPosition3WithVelocities state, TFloat arg, TFloat newArg) {
            return state + this * (newArg - arg);
        }

        public void MakeStep(ref OrientedPosition3WithVelocities state, ref TFloat arg, ref TFloat newArg, out OrientedPosition3WithVelocities result) {
            DeltaT deltaArg = newArg - arg;
            MakeStep(ref state, ref deltaArg, out result);
        }

        public OrientedPosition3WithVelocities MakeStep(OrientedPosition3WithVelocities state, DeltaT deltaArg) {
            return state + this * deltaArg;
        }

        public void MakeStep(ref OrientedPosition3WithVelocities state, ref DeltaT deltaArg, out OrientedPosition3WithVelocities result) {
            OrientedPosition3.Multiply(ref Value, deltaArg, out result.Value);
            OrientedPosition3.Add(ref result.Value, ref state.Value, out result.Value);
            OrientedPosition3.Multiply(ref D, deltaArg, out result.D);
            OrientedPosition3.Add(ref result.D, ref state.D, out result.D);
        }

        public OrientedPosition3WithVelocities Plus(OrientedPosition3WithVelocities value) {
            return this + value;
        }

        public void Plus(ref OrientedPosition3WithVelocities value, out OrientedPosition3WithVelocities result) {
            Add(ref this, ref value, out result);
        }

        public OrientedPosition3WithVelocities MultipliedBy(DeltaT multiplier) {
            return this * multiplier;
        }

        public void MultipliedBy(ref DeltaT multiplier, out OrientedPosition3WithVelocities result) {
            Multiply(ref this, multiplier, out result);
        }

        public static void Add(ref OrientedPosition3WithVelocities value1, ref OrientedPosition3WithVelocities value2, out OrientedPosition3WithVelocities result) {
            OrientedPosition3.Add(ref value1.Value, ref value2.Value, out result.Value);
            OrientedPosition3.Add(ref value1.D, ref value2.D, out result.D);
        }

        public static void Multiply(ref OrientedPosition3WithVelocities value, DeltaT multiplier, out OrientedPosition3WithVelocities result) {
            OrientedPosition3.Multiply(ref value.Value, multiplier, out result.Value);
            OrientedPosition3.Multiply(ref value.D, multiplier, out result.D);
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