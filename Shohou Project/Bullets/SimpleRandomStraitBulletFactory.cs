using System;
using Ark.Graphics.Pipes.Xna;
using Ark.Animation.Xna;
using Microsoft.Xna.Framework;
using Ark.Pipes;
using Ark.Xna;

namespace Ark.Animation.Bullets.Pipes.Xna {
    class SimpleRandomStraitBulletFactory {
        DynamicSprite _bulletSprite;
        Vector2 _spriteOrigin;
        Rectangle _originsSet;
        float _maxSpeed;
        Game _game;

        public SimpleRandomStraitBulletFactory(Game game, DynamicSprite bulletSprite, Rectangle originsSet, float maxSpeed) {
            _game = game;
            _bulletSprite = bulletSprite;
            _originsSet = originsSet;
            _maxSpeed = maxSpeed;
        }

        Random rnd = new Random(666);
        public DrawableGameComponent GenerateBullet() {
            Vector2 origin = new Vector2(_originsSet.X + rnd.Next(_originsSet.Width), _originsSet.Y + rnd.Next(_originsSet.Height));
            float angle = (float)(rnd.NextDouble() * 2 * Math.PI);
            float speed = (float)(rnd.NextDouble() * _maxSpeed);
            
            Func<float, Vector2> movement = Ark.Animation.Xna.Movements.StraightLineConstantSpeed(origin, angle, speed);
            _bulletSprite.Position = new Function<float, Vector2>(movement, new Timer());
            _bulletSprite.Angle = angle;
            return _bulletSprite;
        }
    }
}
