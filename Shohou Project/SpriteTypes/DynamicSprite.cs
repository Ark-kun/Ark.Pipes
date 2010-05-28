using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Pipes;

namespace Ark.XNA.Sprites {
    public class DynamicSprite : DrawableGameComponent, IHasChangeablePosition, IHasChangeableAngle, IHasChangeableTexture {
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

        //public void Draw(Vector2 position) {
        //    _spriteBatch.Draw(Texture, position, null, Tint, Angle, Origin, Scale, SpriteEffects.None, 0);
        //}

        //public void Draw(Vector2 position, float angle) {
        //    _spriteBatch.Draw(Texture, position, null, Tint, angle, Origin, Scale, SpriteEffects.None, 0);
        //}

        //public void Draw(Vector2 position, float angle, float scale) {
        //    _spriteBatch.Draw(Texture, position, null, Tint, angle, Origin, scale, SpriteEffects.None, 0);
        //}

        //public void Draw(Vector2 position, float angle, float scale, Color tint) {
        //    _spriteBatch.Draw(Texture, position, null, tint, angle, Origin, scale, SpriteEffects.None, 0);
        //}

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
