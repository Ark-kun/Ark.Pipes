using System;
using Microsoft.Xna.Framework;

namespace Ark.XNA {
    public static class Extensions {
        public static double Angle(this Vector2 v) {
            return Math.Sign(v.Y) * Math.Acos(v.X / v.Length());
        }
    }
}