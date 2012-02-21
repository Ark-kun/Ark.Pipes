using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Geometry {
    public static class Extensions_Xna {
        public static Point ToPoint(this Vector2 v) {
            return new Point((int)v.X, (int)v.Y);
        }

        public static Vector2 ToVector2(this Point p) {
            return new Vector2(p.X, p.Y);
        }

        public static Vector2 Center(this Texture2D texture) {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }
    }
}