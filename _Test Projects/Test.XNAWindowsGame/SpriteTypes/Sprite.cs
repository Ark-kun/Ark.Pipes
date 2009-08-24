using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame
{
    public class Sprite
    {
        public Texture2D texture;
        public Vector2 origin;
        public float scale = 1.0f;
        public Color tint = Color.White;

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float angle){
            spriteBatch.Draw(texture, position, null, tint, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}
