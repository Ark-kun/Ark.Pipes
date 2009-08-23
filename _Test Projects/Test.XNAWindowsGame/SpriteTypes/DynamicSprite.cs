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
        public void Dispose()
        {            
        }


        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public Texture2D Texture { get; set; }
        public float Angle { get; set; }
    }
}
