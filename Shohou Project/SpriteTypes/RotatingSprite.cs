using System;
using Ark.Animation.Pipes.Xna;
using Ark.Pipes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Graphics.Sprites.Pipes.Xna {
    public class RotatingSprite : DrawableGameComponent, IHasChangeablePosition {
        double angle;
        SpriteBatch spriteBatch;

        public RotatingSprite(Game game)
            : base(game) {
            RotationSpeed = 2 * Math.PI * 1.0 / 4; // 1/10; = 1/3s // 1/60 = 1/20s
            spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
        }

        public override void Update(GameTime gameTime) {
            //angle += (RotationSpeed * gameTime.ElapsedRealTime.TotalSeconds) % 2 * Math.PI;
            angle += (RotationSpeed * gameTime.ElapsedGameTime.TotalSeconds) % 2 * Math.PI;
            //angle += (RotationSpeed * Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2 * Math.PI) * gameTime.ElapsedGameTime.TotalSeconds) % 2 * Math.PI;
        }

        public override void Draw(GameTime gameTime) {
            //spriteBatch.Draw(Sprite, Position, Color.White);
            if (Sprite != null) {
                //spriteBatch.Draw(Sprite, Position, Color.White);

                Vector2 origin = new Vector2() { X = Sprite.Width / 2, Y = Sprite.Height / 2 };
                //var destRect = new Rectangle() { X = (int)Position.X, Y = (int)Position.Y, Width = 10, Height = 10 };
                //var destRect = new Rectangle() { X = (int)Position.X, Y = (int)Position.Y, Width = 1, Height = 1 };
                //spriteBatch.Draw(Sprite, destRect, null, Color.White, (float)angle, origin, SpriteEffects.None, 0);

                spriteBatch.Draw(Sprite, Position, null, Color.White, (float)angle, origin, 1, SpriteEffects.None, 0);
            }
        }

        public Provider<Vector2> Position { get; set; }

        public Texture2D Sprite { get; set; }

        public double RotationSpeed { get; set; }
    }
}
