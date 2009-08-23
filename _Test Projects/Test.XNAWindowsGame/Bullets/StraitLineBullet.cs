using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame.Bullets
{
    class StraitLineBullet<TBullet> : IGameElement where TBullet : IGameElement, IHasChangeablePosition
    {
        TBullet _bullet;
        Vector2 _origin;
        Vector2 _direction;
        float _speed;
        double _millisecondsPassed;

        public StraitLineBullet(TBullet bullet, Vector2 origin, Vector2 direction, float speed)
        {
            _bullet = bullet;
            _origin = origin;
            _direction = direction;
            _speed = speed;

            var rotatableBullet = _bullet as IHasChangeableAngle;
            if (rotatableBullet != null)
            {
                rotatableBullet.Angle = (float)(Math.Sign(_direction.Y) * Math.Acos(_direction.X / _direction.Length()));
            }
        }

        public StraitLineBullet(TBullet bullet, Vector2 origin, float angle, float speed)
        {
            _bullet = bullet;
            _origin = origin;
            _direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            _speed = speed;

            var rotatableBullet = _bullet as IHasChangeableAngle;
            if (rotatableBullet != null)
            {
                rotatableBullet.Angle = angle;
            }
        }

        public void Update(GameTime gameTime)
        {
            _millisecondsPassed += gameTime.ElapsedGameTime.TotalMilliseconds;
            _bullet.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _bullet.Position = _origin + _direction * (float)(_speed * _millisecondsPassed);

            _bullet.Draw(gameTime, spriteBatch);
        }
    }
}
