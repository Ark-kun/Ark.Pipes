using System;
using Ark.Animation.Pipes;
using Ark.Animation.Pipes.Xna;
using Ark.Geometry.Xna;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Bullets.Pipes.Xna {
    public class StraitLineBullet<TBullet> : DrawableGameComponent where TBullet : IUpdateable, IDrawable, IHasChangeablePosition {
        TBullet _bullet;
        Vector2 _origin;
        Vector2 _velocity;
        //double _millisecondsPassed;
        double _startTime = double.NaN;

        public StraitLineBullet(Game game, TBullet bullet, Vector2 origin, Vector2 velocity)
            : base(game) {
            _bullet = bullet;
            _origin = origin;
            _velocity = velocity;

            var rotatableBullet = _bullet as IHasChangeableAngle;
            if (rotatableBullet != null) {
                rotatableBullet.Angle = (float)_velocity.Angle();
            }
        }

        public StraitLineBullet(Game game, TBullet bullet, Vector2 origin, float angle, float speed)
            : base(game) {
            _bullet = bullet;
            _origin = origin;
            _velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));

            var rotatableBullet = _bullet as IHasChangeableAngle;
            if (rotatableBullet != null) {
                //rotatableBullet.Angle = angle;
            }
        }

        public override void Update(GameTime gameTime) {
            //_millisecondsPassed += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (double.IsNaN(_startTime)) {
                _startTime = gameTime.TotalGameTime.TotalMilliseconds;
            }
            _bullet.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            _bullet.Position = _origin + _velocity * (float)(gameTime.TotalGameTime.TotalMilliseconds - _startTime);
            _bullet.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
