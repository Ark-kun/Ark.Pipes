using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Test.XNAWindowsGame
{
    public class RotatingSprite : IGameElement
    {
        double angle;

        public RotatingSprite()
        {
            RotationSpeed = 2 * Math.PI * 1.0 / 4; // 1/10; = 1/3s // 1/60 = 1/20s
        }

        public void Update(GameTime gameTime)
        {
            //angle += (RotationSpeed * gameTime.ElapsedRealTime.TotalSeconds) % 2 * Math.PI;
            angle += (RotationSpeed * gameTime.ElapsedGameTime.TotalSeconds) % 2 * Math.PI;
            //angle += (RotationSpeed * Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2 * Math.PI) * gameTime.ElapsedGameTime.TotalSeconds) % 2 * Math.PI;
        }

     

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Sprite, Position, Color.White);
            if (Sprite != null)
            {
                //spriteBatch.Draw(Sprite, Position, Color.White);

                Vector2 origin = new Vector2() { X = Sprite.Width / 2, Y = Sprite.Height / 2 };
                //var destRect = new Rectangle() { X = (int)Position.X, Y = (int)Position.Y, Width = 10, Height = 10 };
                //var destRect = new Rectangle() { X = (int)Position.X, Y = (int)Position.Y, Width = 1, Height = 1 };
                //spriteBatch.Draw(Sprite, destRect, null, Color.White, (float)angle, origin, SpriteEffects.None, 0);

                spriteBatch.Draw(Sprite, Position, null, Color.White, (float)angle, origin, 1, SpriteEffects.None, 0);
                
            }

        }

        public Vector2 Position { get; set; }

        public Texture2D Sprite { get; set; }

        public double RotationSpeed { get; set; }
    }
}
