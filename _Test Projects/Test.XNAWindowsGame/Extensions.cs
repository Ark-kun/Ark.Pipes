﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA {
    public static class Extensions {
        public static double Angle(this Vector2 v) {
            return Math.Sign(v.Y) * Math.Acos(v.X / v.Length());
        }

        public static Vector3 ToVector3(this Vector2 v) {
            return new Vector3(v.X, v.Y, 0);
        }

        public static Vector2 CenterOrigin(this Texture2D texture) {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public static void RemoveBorder(this Game game) {
            var handle = game.Window.Handle;
            if (handle != IntPtr.Zero) {
#if Windows 
                ((System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(handle)).FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
#endif
            }
        }
    }
}