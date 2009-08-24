﻿
namespace Ark.XNA.Bullets
{
    public static class Movements
    {
        public delegate double Movement1D(double t);
        public delegate bool Condition1D(double t);

        public static Movement1D StayAtStart = (t) => 0;

        public static Movement1D StayAt(double x)
        {
            return (t) => x;
        }

        public static Movement1D MoveAtConstantSpeed(double v)
        {
            return (t) => t * v;
        }
        public static Movement1D TranslateTime(this Movement1D movement, double t0) {
            return (t) => movement(t - t0);
        }
        public static Movement1D TranslateCoordinate(this Movement1D movement, double x0)
        {
            return (t) => x0+movement(t);
        }

        public static Movement1D ConditionalMovement(Condition1D condition, Movement1D thenMovement, Movement1D elseMovement)
        {
            return (t) => condition(t) ? thenMovement(t) : elseMovement(t);
        }

    }
}