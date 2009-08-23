using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame
{
    public class DynamicSprite : IGameElement
    {
        public void Update(GameTime gameTime)
        {
        }   

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                spriteBatch.Draw(Texture, Position, null, Color.White, Angle, Origin, 1, SpriteEffects.None, 0);
                
            }

        }

        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Texture2D Texture { get; set; }
        public float Angle { get; set; }
    }
}