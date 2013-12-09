using System;
using System.Collections.Generic;
using System.Linq;
using Ark.Animation.Bullets;
using Ark.Geometry.Transforms;
using Ark.Graphics;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Bullets { //.Pipes.Xna {
    public class LevyCCurveFactory : LevyCCurveBullet {
        ITransform<Vector2> _parentTransform;
        public LevyCCurveFactory(Game game, ITransform<Vector2> transform, StaticSprite bulletSprite, double startFireTime)
            : base(game, null, transform, bulletSprite, startFireTime) {

            _parentTransform = transform;
        }
    }
    public class LevyCCurveBullet : DrawableGameComponent, IBullet2D, IBulletFactory2D {
        HashSet<LevyCCurveBullet> _bullets = null;
        IBulletFactory<Vector2> _parent;
        ITransform<Vector2> _transform;
        Matrix _childMatrix = Matrix.CreateTranslation(1, 0, 0);

        StaticSprite _bulletSprite;
        Double _lastFireTime;

        static Matrix RotationPlus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(Math.PI / 4));
        static Matrix RotationMinus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(-Math.PI / 4));

        public LevyCCurveBullet(Game game, IBulletFactory2D parent, ITransform<Vector2> relativeTransform, StaticSprite bulletSprite, double startFireTime)
            : base(game) {
            _transform = parent == null ? relativeTransform : parent.Transform.Prepend(relativeTransform);
            _parent = parent;
            _bulletSprite = bulletSprite;
            _bullets = new HashSet<LevyCCurveBullet>();
            _lastFireTime = startFireTime;
        }

        void Fire() {
            if (_bullets.Count == 0) {
                _childMatrix *= RotationPlus;
            } else {
                _childMatrix *= RotationMinus;
            }
            _lastFireTime++;
            var newBullet = new LevyCCurveBullet(Game, this, new XnaMatrixTransform(_childMatrix), _bulletSprite, _lastFireTime);
            _bullets.Add(newBullet);
        }


        IEnumerable<IBulletFactory<Vector2>> IContainer<IBulletFactory<Vector2>>.Elements {
            get {
                return _bullets.Cast<IBulletFactory<Vector2>>();
            }
        }

        IEnumerable<IBullet<Vector2>> IContainer<IBullet<Vector2>>.Elements {
            get {
                return _bullets.Cast<IBullet<Vector2>>();
            }
        }

        public IBulletFactory<Vector2> Parent {
            get {
                return _parent;
            }
        }

        public ITransform<Vector2> Transform {
            get {
                return _transform;
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.TotalSeconds - _lastFireTime > 1) {
                Fire();
            }
            foreach (var bullet in _bullets) {
                bullet.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            //_bulletSprite.Draw(_positionTarget, 0);
            //_bulletSprite.Draw(_positionTarget, 0, (float)((_directionTarget - _positionTarget).Length() / 10));
            //_bulletSprite.Draw((_positionTarget + _directionTarget) * 0.5f, 0, (float)((_directionTarget - _positionTarget).Length() / 20));
            //_bulletSprite.Draw(_positionTarget, 0, (float)(40 * Math.Pow(2, -gameTime.TotalGameTime.TotalSeconds / 2)));
            //var alpha = gameTime.TotalGameTime.Milliseconds / 1000.0;
            //var alpha = gameTime.TotalGameTime.Milliseconds / 900;
            //alpha = alpha > 1 ? 1 : alpha;
            //var alpha = gameTime.TotalGameTime.TotalSeconds - _lastFireTime;
            //_bulletSprite.Draw((_positionTarget + (float)(1 - alpha) * _oldDirectionTarget + (float)alpha * _directionTarget) * 0.5f, 0, (float)(25.0 * Math.Pow(2, -gameTime.TotalGameTime.TotalSeconds / 2)));

            var position = new Vector2(0, 0);
            position = _transform.Transform(position);
            _bulletSprite.Draw(position, 0);
            foreach (var bullet in _bullets) {
                bullet.Draw(gameTime);
            }
            _position = position;
        }

        Provider<Vector2> _position = Vector2.Zero;
        public Provider<Vector2> Position {
            get {
                return _position;
            }
        }

    }
}
