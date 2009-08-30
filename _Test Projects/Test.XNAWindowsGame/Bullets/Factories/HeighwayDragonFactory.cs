using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.XNA.Transforms;

namespace Test.XNAWindowsGame.Bullets.Factories {
    public class HeighwayDragonFactory : HeighwayDragonBullet {
        ITransform<Vector2> _parentTransform;
        public HeighwayDragonFactory(Game game, ITransform<Vector2> transform, SpriteInBatch bulletSprite, double startFireTime)
            : base(game, null, transform, bulletSprite, startFireTime) {

            _parentTransform = transform;

        }
    }
    public class HeighwayDragonBullet : DrawableGameComponent, IBullet2D, IBulletFactory2D {
        List<HeighwayDragonBullet> _bullets = null;
        IBulletFactory2D _parent;
        ITransform<Vector2> _transform;
        Matrix _childMatrix = Matrix.CreateTranslation(1, 0, 0);

        SpriteInBatch _bulletSprite;
        Double _lastFireTime;

        static Matrix RotationPlus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(Math.PI / 4));
        static Matrix RotationMinus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(-Math.PI / 4));

        public HeighwayDragonBullet(Game game, IBulletFactory2D parent, ITransform<Vector2> relativeTransform, SpriteInBatch bulletSprite, double startFireTime)
            : base(game) {
            //_transform = parent == null ? relativeTransform : parent.Transform.Append(relativeTransform);
            _transform = parent == null ? relativeTransform : parent.Transform.Prepend(relativeTransform);
            _parent = parent;
            _bulletSprite = bulletSprite;
            _bullets = new List<HeighwayDragonBullet>();
            _lastFireTime = startFireTime;
        }

        Vector2 _oldDirectionTarget;
        void Fire() {
            if (_bullets.Count == 0) {
                _childMatrix *= RotationPlus ;
            } else {
                _childMatrix *= RotationMinus;
            }
            _lastFireTime++;
            var newBullet = new HeighwayDragonBullet(Game, this, new XnaMatrixTransform(_childMatrix), _bulletSprite, _lastFireTime);
            _bullets.Add(newBullet);
        }


        IEnumerable<IBulletFactory> IContainer<IBulletFactory>.Elements {
            get {
                return _bullets.Cast<IBulletFactory>();
            }
        }

        IEnumerable<IBullet> IContainer<IBullet>.Elements {
            get {
                return _bullets.Cast<IBullet>();
            }
        }

        public IBulletFactory2D Parent {
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
            if (_parent != null) {
                position = position;
            }
            _bulletSprite.Draw(position, 0);
            foreach (var bullet in _bullets) {
                bullet.Draw(gameTime);
            }
        }
    }
}
