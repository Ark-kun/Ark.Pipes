using Ark.Animation.Pipes.Xna;
using Ark.Geometry.Transforms;
using Ark.Pipes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Graphics.Sprites.Pipes.Xna {
    public class TransformedSprite : DrawableGameComponent, IHasChangeableTexture {
        private SpriteBatch _spriteBatch;

        public TransformedSprite(Game game)
            : base(game) {
            _spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

            Transform = Transform<Vector2>.Identity;
            Origin = Vector2.Zero;
            Texture = Constant<Texture2D>.Default;
            Tint = Color.White;
        }

        public void Draw() {
            var trans = Transform;
            Texture2D tex = Texture;
            var p00 = trans.Transform(Vector2.Zero);
            var p01 = trans.Transform(new Vector2(0, tex.Height));
            var p10 = trans.Transform(new Vector2(tex.Width, 0));
            var p11 = trans.Transform(new Vector2(tex.Width, tex.Height));
            var dst = new Rectangle((int)p00.X, (int)p00.Y, (int)((p10 - p00).Length()), (int)((p01 - p00).Length()));
            var angle = (float)(System.Math.Atan2(p11.X - p00.X, p11.Y - p00.Y) - System.Math.Atan2(tex.Width, tex.Height));
            _spriteBatch.Draw(tex, dst, null, Tint, angle, Origin, SpriteEffects.None, 0);
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

        public ITransform<Vector2> Transform { get; set; }
        public Provider<Vector2> Origin { get; set; }
        public Provider<Texture2D> Texture { get; set; }
        public Provider<Color> Tint { get; set; }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            Draw();
        }
    }
}
