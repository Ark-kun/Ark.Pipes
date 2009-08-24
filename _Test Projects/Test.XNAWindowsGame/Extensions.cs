using System;
using Microsoft.Xna.Framework;

public static class Extensions
{
    public static double Angle(this Vector2 v){
        return Math.Sign(v.Y) * Math.Acos(v.X / v.Length());
}
}