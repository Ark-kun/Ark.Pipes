using System;
using Ark.XNA.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA.Bullets {
    class HomingBulletFactory {
        Texture2D _bulletSprite;
        Vector2 _spriteOrigin;
        Rectangle _originsSet;
        float _maxSpeed;
        Vector2 _target = Vector2.Zero;
        Random rnd = new Random(666);

        public HomingBulletFactory(Texture2D bulletSprite, Rectangle originsSet, float maxSpeed) {
            _bulletSprite = bulletSprite;
            _originsSet = originsSet;
            _maxSpeed = maxSpeed;
            _spriteOrigin = new Vector2(_bulletSprite.Width / 2, _bulletSprite.Height / 2);
        }


        public IGameElement GenerateBullet() {
            Vector2 origin = new Vector2(_originsSet.X + rnd.Next(_originsSet.Width), _originsSet.Y + rnd.Next(_originsSet.Height));
            Vector2 direction = _target - origin;
            direction.Normalize();
            float speed = (float)(rnd.NextDouble() * _maxSpeed);
            direction *= speed;

            return new StraitLineBullet<StaticSprite>(new StaticSprite(_bulletSprite, _spriteOrigin), origin, direction);
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
