﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame
{
    public class CoolSprite : IGameElement
    {
        double angle;

        public CoolSprite()
        {
            RotationSpeed = 2 * Math.PI * 1.0 / 2; // 1/10; = 1/3s // 1/60 = 1/20s
        }

        public void Update(GameTime gameTime)
        {
            //angle += (RotationSpeed * gameTime.ElapsedRealTime.TotalSeconds) % 2 * Math.PI;
            angle += (RotationSpeed * gameTime.ElapsedGameTime.TotalSeconds); 
            angle %= 2 * Math.PI;
            //angle += (RotationSpeed * Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2 * Math.PI) * gameTime.ElapsedGameTime.TotalSeconds) % 2 * Math.PI;
        }

public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Sprite1 != null)
            {
                Vector2 origin = new Vector2() { X = Sprite1.Width / 2, Y = Sprite1.Height / 2 };
                spriteBatch.Draw(Sprite1, Position, null, Color.White, -(float)angle, origin, 0.5f, SpriteEffects.None, 0);                
            }
            if (Sprite2 != null)
            {
                Vector2 origin = new Vector2() { X = Sprite2.Width / 2, Y = Sprite2.Height / 2 };
                spriteBatch.Draw(Sprite2, Position, null, Color.White, (float)angle, origin, 0.5f, SpriteEffects.None, 0);
            }

        }


        public Vector2 Position { get; set; }

        public Texture2D Sprite1 { get; set; }
        public Texture2D Sprite2 { get; set; }

        public double RotationSpeed { get; set; }
    }
}