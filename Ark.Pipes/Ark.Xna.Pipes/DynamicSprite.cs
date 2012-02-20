using Ark.Pipes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Graphics.Pipes.Xna {
    public class DynamicSprite : DrawableGameComponent {
        private SpriteBatch _spriteBatch;

        public DynamicSprite(Game game)
            : base(game) {
            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            Position = Vector2.Zero;
            Origin = Vector2.Zero;
            Texture = Constant<Texture2D>.Default;
            Angle = 0;
            Scale = 1;
            Tint = Color.White;
        }

        public void Draw() {
            _spriteBatch.Draw(Texture, Position, null, Tint, Angle, Origin, Scale, SpriteEffects.None, 0);
        }

        public Provider<Vector2> Position { get; set; }
        public Provider<Vector2> Origin { get; set; }
        public Provider<Texture2D> Texture { get; set; }
        public Provider<float> Angle { get; set; }
        public Provider<float> Scale { get; set; }
        public Provider<Color> Tint { get; set; }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            Draw();
        }

        public delegate void GameTimeEvent(GameTime gameTime);
        public event GameTimeEvent Updated;
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (Updated != null) {
                Updated(gameTime);
            }
        }
    }
}
