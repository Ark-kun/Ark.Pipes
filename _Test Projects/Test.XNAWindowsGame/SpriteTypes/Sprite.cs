using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame {
    public class Sprite {
        public Texture2D texture;
        public Vector2 origin;
        public float angle = 1.0f;
        public float scale = 1.0f;
        public Color tint = Color.White;

        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            spriteBatch.Draw(texture, position, null, tint, angle, origin, scale, SpriteEffects.None, 0);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float angle) {
            spriteBatch.Draw(texture, position, null, tint, angle, origin, scale, SpriteEffects.None, 0);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float angle, float scale) {
            spriteBatch.Draw(texture, position, null, tint, angle, origin, scale, SpriteEffects.None, 0);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float angle, float scale, Color tint) {
            spriteBatch.Draw(texture, position, null, tint, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
    public class SpriteInBatch : Sprite {
        public SharedSpriteBatch spriteBatch;

        public void Draw(Vector2 position) {
            Draw(position, angle, scale, tint);
        }
        public void Draw(Vector2 position, float angle) {
            Draw(position, angle, scale, tint);
        }
        public void Draw(Vector2 position, float angle, float scale) {
            Draw(position, angle, scale, tint);
        }
        public void Draw(Vector2 position, float angle, float scale, Color tint) {
            spriteBatch.SharedBegin();
            Draw(spriteBatch, position, angle, scale, tint);
            spriteBatch.SharedEnd();
        }
    }
}
