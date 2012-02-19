using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Xna;
using Ark.Pipes;

namespace Ark.Xna.Sprites {
    public class StaticSprite {
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;
        private Vector2 _origin;
        private float _angle = 0;
        private float _scale = 1;
        private Color _tint = Color.White;

        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture)
            : this(spriteBatch, texture, 1) {
        }
        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture, float scale)
            : this(spriteBatch, texture, scale, Color.White) {
        }

        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture, float scale, Color tint)
            : this(spriteBatch, texture, scale, tint, texture.CenterOrigin()) {
        }

        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture, float scale, Color tint, Vector2 origin)
            : this(spriteBatch, texture, scale, tint, origin, 0) {
        }

        public StaticSprite(SpriteBatch spriteBatch, Texture2D texture, float scale, Color tint, Vector2 origin, float angle) {
            _angle = angle;
            _origin = origin;
            _scale = scale;
            _spriteBatch = spriteBatch;
            _texture = texture;
            _tint = tint;
        }

        public void Draw(Vector2 position) {
            _spriteBatch.Draw(_texture, position, null, _tint, _angle, _origin, _scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, float angle) {
            _spriteBatch.Draw(_texture, position, null, _tint, angle, _origin, _scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, float angle, float scale) {
            _spriteBatch.Draw(_texture, position, null, _tint, angle, _origin, scale, SpriteEffects.None, 0);
        }

        public void Draw(Vector2 position, float angle, float scale, Color tint) {
            _spriteBatch.Draw(_texture, position, null, tint, angle, _origin, scale, SpriteEffects.None, 0);
        }
    }
}
