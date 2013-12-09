using System;
using Ark.Animation;
using Ark.Geometry;
using Microsoft.Xna.Framework;

namespace Ark.Animation {
    public static class Movements {
        public delegate bool TimeCondition(double t);
        public delegate double Movement1D(double t);

        public static readonly Movement1D StayAtStart = (t) => 0;

        public static Movement1D StayAt(double x) {
            return (t) => x;
        }

        public static Movement1D MoveAtConstantSpeed(double v) {
            return (t) => t * v;
        }

        public static Movement1D RotateAtConstantSpeed(double angularVelocity) {
            return (t) => t * (angularVelocity % (2 * Math.PI));
        }

        public static Movement1D TranslateTime(this Movement1D movement, double t0) {
            return (t) => movement(t - t0);
        }

        public static Movement1D TranslateCoordinate(this Movement1D movement, double x0) {
            return (t) => x0 + movement(t);
        }

        public static Movement1D ConditionalMovement(TimeCondition condition, Movement1D thenMovement, Movement1D elseMovement) {
            return (t) => condition(t) ? thenMovement(t) : elseMovement(t);
        }

        //public delegate TVector2 Movement2D<TVector2>(float t);

        //public delegate TOrientedPosition2 OrientedMovement2D<TOrientedPosition2>(float t);
    }
}
namespace Ark.Animation { //.Xna {
    public static class Movements_Xna {
        public static Func<float, OrientedPosition2> OrientedStraightLineConstantSpeed(Vector2 origin, Vector2 velocity) {
            float angle = (float)velocity.Angle();
            return (t) => new OrientedPosition2(origin + velocity * t, angle);
        }

        public static Func<float, Vector2> StraightLineConstantSpeed(Vector2 origin, Vector2 velocity) {
            return (t) => origin + velocity * t;
        }

        public static Func<float, Vector2> StraightLineConstantSpeed(Vector2 origin, float angle, float speed) {
            Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * speed;
            return StraightLineConstantSpeed(origin, velocity);
        }
    }
}