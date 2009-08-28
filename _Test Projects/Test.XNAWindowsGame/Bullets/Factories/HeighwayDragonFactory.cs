using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Test.XNAWindowsGame.Bullets.Factories {
    //public class HeighwayDragonFactory : IBulletFactory2D {
    //    List<IBullet> _bullets;

    //    IEnumerable<IBulletFactory> IContainer<IBulletFactory>.Elements {
    //        get {
    //            throw new NotImplementedException();
    //        }
    //    }

    //    IEnumerable<IBullet> IContainer<IBullet>.Elements {
    //        get {
    //            return _bullets;
    //        }
    //    }
    public class HeighwayDragonBullet : DrawableGameComponent, IBullet2D, IBulletFactory2D {
        List<HeighwayDragonBullet> _bullets = null;
        HeighwayDragonBullet _parent;
        Vector2 _directionTarget;
        Vector2 _positionTarget;
        SpriteInBatch _bulletSprite;
        Double _lastFireTime;

        static Matrix RotationPlus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(Math.PI / 4));
        static Matrix RotationMinus = Matrix.CreateScale((float)Math.Sqrt(1.0 / 2)) * Matrix.CreateRotationZ((float)(-Math.PI / 4));

        //public HeighwayDragonBullet(Game game)
        //    : base(game) {
        //}
        public HeighwayDragonBullet(Game game, HeighwayDragonBullet parent, Vector2 positionTarget, Vector2 directionTarget, SpriteInBatch bulletSprite, double startFireTime)
            : base(game) {
            _directionTarget = directionTarget;
            _positionTarget = positionTarget;
            _parent = parent;
            _bulletSprite = bulletSprite;
            _bullets = new List<HeighwayDragonBullet>();
            _lastFireTime = startFireTime;
        }

        void Fire() {
            Vector2 newPositionTarget;
            if (_bullets.Count == 0) {
                newPositionTarget = _positionTarget + Vector2.Transform(_directionTarget - _positionTarget, RotationPlus);
            } else {
                newPositionTarget = _positionTarget + Vector2.Transform(_directionTarget - _positionTarget, RotationMinus);
            }
            _lastFireTime++;
            var newBullet = new HeighwayDragonBullet(Game, this, newPositionTarget, _directionTarget, _bulletSprite, _lastFireTime);
            _directionTarget = newPositionTarget;
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
                return _parent.Transform;
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
            _bulletSprite.Draw((_positionTarget + _directionTarget) * 0.5f, 0, (float)((_directionTarget - _positionTarget).Length() / 20));
            foreach (var bullet in _bullets) {
                bullet.Draw(gameTime);
            }
        }
    }

    //public ITransform<Vector2> Transform {
    //    get { throw new NotImplementedException(); }
    //}
    //}
}
