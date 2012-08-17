using Ark.Geometry;
using Ark.Pipes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Graphics { //.Pipes.Xna {
    public class DynamicSprite : DrawableGameComponent {
        private SpriteBatch _spriteBatch;

        Property<Vector2> _position = new Property<Vector2>();
        Property<Texture2D> _texture = new Property<Texture2D>();
        Property<float> _angle = new Property<float>();
        Property<float> _scale = new Property<float>();
        Property<Color> _tint = new Property<Color>();

        Vector2 _positionCache;
        Texture2D _textureCache;
        float _angleCache;
        float _scaleCache;
        Color _tintCache;

        public DynamicSprite(Game game)
            : base(game) {
            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            Position = Vector2.Zero;
            Texture = Constant<Texture2D>.Default;
            Angle = 0;
            Scale = 1;
            Tint = Color.White;
        }

        public void Draw() {
            _spriteBatch.Draw(_textureCache, _positionCache, null, _tintCache, _angleCache, _textureCache.Center(), _scaleCache, SpriteEffects.None, 0);
        }

        public Property<Vector2> Position {
            get { return _position; }
            set { _position.Provider = value; }
        }

        public Property<Texture2D> Texture {
            get { return _texture; }
            set { _texture.Provider = value; }
        }

        public Property<float> Angle {
            get { return _angle; }
            set { _angle.Provider = value; }
        }

        public Property<float> Scale {
            get { return _scale; }
            set { _scale.Provider = value; }
        }

        public Property<Color> Tint {
            get { return _tint; }
            set { _tint.Provider = value; }
        }
        
        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            Draw();
        }

        public override void Update(GameTime gameTime) {
            _positionCache = _position;
            _textureCache = _texture;
            _angleCache = _angle;
            _scaleCache = _scale;
            _tintCache = _tint;

            base.Update(gameTime);
        }
    }
}
