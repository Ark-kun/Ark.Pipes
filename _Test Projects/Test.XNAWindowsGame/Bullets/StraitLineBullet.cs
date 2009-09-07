using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA.Bullets {
    class StraitLineBullet<TBullet> : IGameElement where TBullet : IGameElement, IHasChangeablePosition {
        TBullet _bullet;
        Vector2 _origin;
        Vector2 _velocity;
        double _millisecondsPassed;

        public StraitLineBullet(TBullet bullet, Vector2 origin, Vector2 velocity) {
            _bullet = bullet;
            _origin = origin;
            _velocity = velocity;

            var rotatableBullet = _bullet as IHasChangeableAngle;
            if (rotatableBullet != null) {
                rotatableBullet.Angle = (float)(Math.Sign(_velocity.Y) * Math.Acos(_velocity.X / _velocity.Length()));
            }
        }

        public StraitLineBullet(TBullet bullet, Vector2 origin, float angle, float speed) {
            _bullet = bullet;
            _origin = origin;
            _velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));

            var rotatableBullet = _bullet as IHasChangeableAngle;
            if (rotatableBullet != null) {
                rotatableBullet.Angle = angle;
            }
        }

        public void Update(GameTime gameTime) {
            _millisecondsPassed += gameTime.ElapsedGameTime.TotalMilliseconds;
            _bullet.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            _bullet.Position = _origin + _velocity * (float)(_millisecondsPassed);

            _bullet.Draw(gameTime, spriteBatch);
        }
    }
}
