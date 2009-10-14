using System;
using Ark.XNA.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA.Bullets {
    class HomingBulletFactory {
        DynamicSprite _bulletSprite;
        Rectangle _originsSet;
        float _maxSpeed;
        Vector2 _target = Vector2.Zero;
        Random rnd = new Random(666);
        Game _game;

        public HomingBulletFactory(Game game, DynamicSprite bulletSprite, Rectangle originsSet, float maxSpeed) {
            _game = game;
            _bulletSprite = bulletSprite;
            _originsSet = originsSet;
            _maxSpeed = maxSpeed;
        }


        public DrawableGameComponent GenerateBullet() {
            Vector2 origin = new Vector2(_originsSet.X + rnd.Next(_originsSet.Width), _originsSet.Y + rnd.Next(_originsSet.Height));
            Vector2 direction = _target - origin;
            direction.Normalize();
            float speed = (float)(rnd.NextDouble() * _maxSpeed);
            direction *= speed;

            return new StraitLineBullet<DynamicSprite>(_game, _bulletSprite, origin, direction);
        }

        public Vector2 Target {
            get {
                return _target;
            }
            set {
                _target = value;
            }
        }
    }
}
