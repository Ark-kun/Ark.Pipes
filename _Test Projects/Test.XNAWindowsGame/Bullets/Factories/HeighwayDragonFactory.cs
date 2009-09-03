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
        Matrix _directionMatrix = Matrix.Identity;

        SpriteInBatch _bulletSprite;
        Double _lastFireTime;

        static Matrix RotationPlus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(Math.PI / 4));
        static Matrix RotationMinus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(-Math.PI / 4));

        Vector2 _oldPosition;
        public HeighwayDragonBullet(Game game, IBulletFactory2D parent, ITransform<Vector2> relativeTransform, SpriteInBatch bulletSprite, double startFireTime)
            : base(game) {
            _transform = parent == null ? relativeTransform : parent.Transform.Prepend(relativeTransform);
            _parent = parent;
            _bulletSprite = bulletSprite;
            _bullets = new List<HeighwayDragonBullet>();
            _lastFireTime = startFireTime;

            if(parent == null ){
                _oldPosition = new Vector2(1, 0);
            } else if (((IContainer<IBullet>)parent).Elements.Count() > 0 || parent is HeighwayDragonFactory) {
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
            var newBullet = new HeighwayDragonBullet(Game, this, new XnaMatrixTransform(childMatrix), _bulletSprite, _lastFireTime);
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

            var alpha = gameTime.TotalGameTime.TotalSeconds - _lastFireTime;

            //alpha = 0.5 - 0.5 * Math.Cos(alpha * Math.PI);

            var positionAlpha = (float)(1 - alpha) * _oldPosition + (float)alpha * Vector2.Transform(new Vector2(0.5f, 0), _directionMatrix);

            _bulletSprite.Draw(_transform.Transform(positionAlpha), 0, _bulletSprite.scale * (float)(Math.Pow(2, -gameTime.TotalGameTime.TotalSeconds / 2)));
            foreach (var bullet in _bullets) {
                bullet.Draw(gameTime);
            }
        }
    }
}
