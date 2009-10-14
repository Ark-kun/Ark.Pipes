using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Pipes;

namespace Ark.XNA.Sprites {
    public class DynamicSprite : DrawableGameComponent, IHasChangeablePosition, IHasChangeableAngle, IHasChangeableTexture {
        private SpriteBatch _spriteBatch;

        public DynamicSprite(Game game, SpriteBatch spriteBatch)
            : base(game) {
            _spriteBatch = spriteBatch;
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
    }
}
