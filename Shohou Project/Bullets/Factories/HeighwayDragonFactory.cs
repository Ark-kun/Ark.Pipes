using System;
using System.Collections.Generic;
using System.Linq;
using Ark.XNA.Sprites;
using Ark.XNA.Transforms;
using Microsoft.Xna.Framework;

namespace Ark.XNA.Bullets.Factories {
    public class HeighwayDragonFactory : HeighwayDragonBullet {
        public HeighwayDragonFactory(Game game, ITransform<Vector2> transform, StaticSprite bulletSprite, double startFireTime, Func<Vector2, bool> shouldDestroyBullet)
            : base(game, null, transform, bulletSprite, startFireTime, shouldDestroyBullet) {
        }
    }

    public class HeighwayDragonBullet : BulletFactoryBulletBase<Vector2> {
        Matrix _directionMatrix = Matrix.Identity;
        StaticSprite _bulletSprite;
        Double _lastFireTime;

        Func<Vector2, bool> _shouldDestroyBullet;

        static Matrix RotationPlus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(Math.PI / 4));
        static Matrix RotationMinus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(-Math.PI / 4));

        Vector2 _oldPosition;
        public HeighwayDragonBullet(Game game, IBulletFactory<Vector2> parent, ITransform<Vector2> relativeTransform, StaticSprite bulletSprite, double startFireTime, Func<Vector2, bool> shouldDestroyBullet)
            : base(game, parent == null ? relativeTransform : parent.Transform.Prepend(relativeTransform), parent) {
            _bulletSprite = bulletSprite;
            _lastFireTime = startFireTime;
            _shouldDestroyBullet = shouldDestroyBullet;

            if (parent == null) {
                _oldPosition = new Vector2(1, 0);
            } else if (parent is HeighwayDragonFactory || ((HeighwayDragonBullet)parent).BulletFactories.Any()) {
                _oldPosition = new Vector2(0.5f, 0.5f);
            } else {
                _oldPosition = new Vector2(0.5f, -0.5f);
            }
        }

        void Fire() {
            Matrix childMatrix;
            _oldPosition = Vector2.Transform(new Vector2(0.5f, 0), _directionMatrix);
            if (!Elements.Any() && !(this is HeighwayDragonFactory)) {
                _directionMatrix *= RotationPlus;
                childMatrix = _directionMatrix * Matrix.CreateRotationZ((float)(-Math.PI / 2));
            } else {
                _directionMatrix *= RotationMinus;
                childMatrix = _directionMatrix * Matrix.CreateRotationZ((float)(Math.PI / 2));
            }
            childMatrix.Translation = Vector3.Transform(Vector3.UnitX, _directionMatrix);
            _lastFireTime++;
            var newBullet = new HeighwayDragonBullet(Game, this, new XnaMatrixTransform(childMatrix), _bulletSprite, _lastFireTime, _shouldDestroyBullet);
            BulletFactories.Add(newBullet);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (gameTime.TotalGameTime.TotalSeconds - _lastFireTime > 1) {
                Fire();
            }
            var bulletsToDestroy = new List<IBulletFactoryBullet<Vector2>>();
            foreach (var bullet in BulletFactories) {
                bullet.Update(gameTime);
                if (_shouldDestroyBullet(bullet.Position)) {
                    bulletsToDestroy.Add(bullet);
                }
            }
            foreach (HeighwayDragonBullet bullet in bulletsToDestroy) {
                BulletFactories.Remove(bullet);
                foreach (var bulletFactory in bullet.BulletFactories) {
                    BulletFactories.Add(bulletFactory);
                }
            }
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);

            var alpha = gameTime.TotalGameTime.TotalSeconds - _lastFireTime;

            //alpha = 0.5 - 0.5 * Math.Cos(alpha * Math.PI);

            var positionAlpha = (float)(1 - alpha) * _oldPosition + (float)alpha * Vector2.Transform(new Vector2(0.5f, 0), _directionMatrix);

            var scale = (Transform.Transform(Vector2.Transform(Vector2.UnitX, _directionMatrix)) - Transform.Transform(Vector2.Transform(Vector2.Zero, _directionMatrix))).Length();

            Position = Transform.Transform(positionAlpha);

            _bulletSprite.Draw(Position, 0, scale * (float)(Math.Pow(2, -alpha / 2)));
            foreach (var bullet in BulletFactories) {
                bullet.Draw(gameTime);
            }

        }
    }
}
