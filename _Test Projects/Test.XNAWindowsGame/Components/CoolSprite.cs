using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA.Components {
    public class CoolSprite : DrawableGameComponent, IHasChangeablePosition {
        double _angle;
        Game _game;
        SharedSpriteBatch _spriteBatch;
        Texture2D _sprite1;
        Texture2D _sprite2;
        string _spriteFile1 = null;
        string _spriteFile2 = null;

        public CoolSprite(Game game, string spriteFile1, string spriteFile2)
            : base(game) {
            _game = game;
            _spriteFile1 = spriteFile1;
            _spriteFile2 = spriteFile2;
            _spriteBatch = (SharedSpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            this.RotationSpeed = 2 * Math.PI * 1.0 / 2; // 1/10; = 1/3s // 1/60 = 1/20s
        }

        protected override void LoadContent() {
            base.LoadContent();
            _sprite1 = _game.Content.Load<Texture2D>(_spriteFile1);
            _sprite2 = _game.Content.Load<Texture2D>(_spriteFile2);
        }

        public override void Update(GameTime gameTime) {
            //angle += (RotationSpeed * gameTime.ElapsedRealTime.TotalSeconds) % 2 * Math.PI;
            _angle += (RotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            _angle %= 2 * Math.PI;
            //angle += (RotationSpeed * Math.Sin(gameTime.TotalGameTime.TotalSeconds * 2 * Math.PI) * gameTime.ElapsedGameTime.TotalSeconds) % 2 * Math.PI;
        }

        public override void Draw(GameTime gameTime) {
            _spriteBatch.SharedBegin();
            if (Sprite1 != null) {
                Vector2 origin = new Vector2() { X = Sprite1.Width / 2, Y = Sprite1.Height / 2 };
                _spriteBatch.Draw(Sprite1, Position, null, Color.White, -(float)_angle, origin, 0.5f, SpriteEffects.None, 0);
            }
            if (Sprite2 != null) {
                Vector2 origin = new Vector2() { X = Sprite2.Width / 2, Y = Sprite2.Height / 2 };
                _spriteBatch.Draw(Sprite2, Position, null, Color.White, (float)_angle, origin, 0.5f, SpriteEffects.None, 0);
            }
            _spriteBatch.SharedEnd();
        }

        public Vector2 Position { get; set; }

        public Texture2D Sprite1 {
            get {
                return _sprite1;
            }
            set { _sprite1 = value; }
        }
        public Texture2D Sprite2 {
            get {
                return _sprite2;
            }
            set { _sprite2 = value; }
        }

        public double RotationSpeed { get; set; }
    }
}
