using System;
using Ark.Animation.Xna;
using Ark.Geometry;
using Ark.Graphics.Pipes.Xna;
using Ark.Pipes;
using Ark.Xna;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Bullets.Pipes.Xna {
    public class HomingBulletFactory {
        DynamicSprite _bulletSprite;
        Rectangle _originsSet;
        float _maxSpeed;
        Provider<Vector2> _target = Constant<Vector2>.Default;
        //Random rnd = new Random(666);
        Random rnd = new Random();
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
            Vector2 velocity = direction * speed;
            
            Func<float, Vector2> movement = Ark.Animation.Xna.Movements.StraightLineConstantSpeed(origin, velocity);
            _bulletSprite.Position = new Function<float, Vector2>(movement, new Time());
            _bulletSprite.Angle = (float)direction.Angle();
            return _bulletSprite;
        }

        public Provider<Vector2> Target {
            get {
                return _target;
            }
            set {
                _target = value;
            }
        }
    }
}
