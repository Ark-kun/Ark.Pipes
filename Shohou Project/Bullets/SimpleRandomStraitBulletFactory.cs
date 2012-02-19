﻿using System;
using Ark.Graphics.Sprites.Pipes.Xna;
using Microsoft.Xna.Framework;

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
            return new StraitLineBullet<DynamicSprite>(_game, _bulletSprite, origin, angle, speed);
        }
    }
}
