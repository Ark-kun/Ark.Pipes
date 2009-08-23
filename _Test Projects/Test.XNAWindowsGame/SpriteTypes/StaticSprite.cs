using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame.SpriteTypes
{
    public class StaticSprite : IGameElement, IHasChangeablePosition
    {
        Texture2D _texture;
        Vector2 _origin;
        Vector2 _position;

        public StaticSprite(Texture2D texture, Vector2 origin)
        {
            _texture = texture;
            _origin = origin;
            _position = Vector2.Zero;
        }
        public StaticSprite(Texture2D texture, Vector2 origin, Vector2 position)
        {
            _texture = texture;
            _position = position;
            _origin = origin;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position - _origin, Color.White);
        }

        Vector2 IHasChangeablePosition.Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
            }
        }

    }
}
