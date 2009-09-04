using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.XNA.Transforms;

namespace Test.XNAWindowsGame.Bullets.Factories {
    public class HeighwayDragonFactory : HeighwayDragonBullet {
        ITransform<Vector2> _parentTransform;
        public HeighwayDragonFactory(Game game, ITransform<Vector2> transform, SpriteInBatch bulletSprite, double startFireTime, Func<Vector2, bool> shouldDestroyBullet)
            : base(game, null, transform, bulletSprite, startFireTime, shouldDestroyBullet) {

            _parentTransform = transform;
        }
    }
    public class HeighwayDragonBullet : DrawableGameComponent, IBulletFactory<Vector2>, IBullet<Vector2> {
        List<IBullet<Vector2>> _bullets = null;
        IBulletFactory<Vector2> _parent;
        ITransform<Vector2> _transform;
        Matrix _directionMatrix = Matrix.Identity;

        SpriteInBatch _bulletSprite;
        Double _lastFireTime;

        Func<Vector2, bool> _shouldDestroyBullet;

        static Matrix RotationPlus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(Math.PI / 4));
        static Matrix RotationMinus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(-Math.PI / 4));

        Vector2 _oldPosition;
        public HeighwayDragonBullet(Game game, IBulletFactory<Vector2> parent, ITransform<Vector2> relativeTransform, SpriteInBatch bulletSprite, double startFireTime, Func<Vector2, bool> shouldDestroyBullet)
            : base(game) {
            _transform = parent == null ? relativeTransform : parent.Transform.Prepend(relativeTransform);
            _parent = parent;
            _bulletSprite = bulletSprite;
            _bullets = new List<IBullet<Vector2>>();
            _lastFireTime = startFireTime;
            _shouldDestroyBullet = shouldDestroyBullet;

            if (parent == null) {
                _oldPosition = new Vector2(1, 0);
            } else if (((IContainer<IBullet<Vector2>>)parent).Elements.Count() > 0 || parent is HeighwayDragonFactory) {
                _oldPosition = new Vector2(0.5f, 0.5f);
            } else {
                _oldPosition = new Vector2(0.5f, -0.5f);
            }
        }

        void Fire() {
            Matrix childMatrix;
            _oldPosition = Vector2.Transform(new Vector2(0.5f, 0), _directionMatrix);
            if (_bullets.Count == 0 && !(this is HeighwayDragonFactory)) {
                _directionMatrix *= RotationPlus;
                childMatrix = _directionMatrix * Matrix.CreateRotationZ((float)(-Math.PI / 2));
            } else {
                _directionMatrix *= RotationMinus;
                childMatrix = _directionMatrix * Matrix.CreateRotationZ((float)(Math.PI / 2));
            }
            childMatrix.Translation = Vector3.Transform(new Vector3(1, 0, 0), _directionMatrix);
            _lastFireTime++;
            var newBullet = new HeighwayDragonBullet(Game, this, new XnaMatrixTransform(childMatrix), _bulletSprite, _lastFireTime, _shouldDestroyBullet);
            _bullets.Add(newBullet);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.TotalSeconds - _lastFireTime > 1) {
                Fire();
            }
            var bulletsToDestroy = new List<IBullet<Vector2>>();
            foreach (var bullet in _bullets) {
                bullet.Update(gameTime);
                if (_shouldDestroyBullet(bullet.Position)) {
                    bulletsToDestroy.Add(bullet);
                }
            }
            foreach (var bullet in bulletsToDestroy) {
                _bullets.Remove(bullet);
                _bullets.AddRange(((IContainer<IBullet<Vector2>>)bullet).Elements);
            }
        }

        Vector2 position;
        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            var alpha = gameTime.TotalGameTime.TotalSeconds - _lastFireTime;

            //alpha = 0.5 - 0.5 * Math.Cos(alpha * Math.PI);

            var positionAlpha = (float)(1 - alpha) * _oldPosition + (float)alpha * Vector2.Transform(new Vector2(0.5f, 0), _directionMatrix);

            var scale = (_transform.Transform(Vector2.Transform(new Vector2(1, 0), _directionMatrix)) - _transform.Transform(Vector2.Transform(new Vector2(0, 0), _directionMatrix))).Length();

            position = _transform.Transform(positionAlpha);

            _bulletSprite.Draw(position, 0, _bulletSprite.scale * scale * (float)(Math.Pow(2, -alpha / 2)));
            foreach (var bullet in _bullets) {
                bullet.Draw(gameTime);
            }

        }

        public Vector2 Position {
            get {
                return position;
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
    }
}
