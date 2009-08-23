using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Test.XNAWindowsGame.SpriteTypes;

namespace Test.XNAWindowsGame.Bullets
{
    class SimpleStraitBulletFactory
    {
        Texture2D _bulletSprite;
        Vector2 _spriteOrigin;
        Rectangle _originsSet;
        float _maxSpeed;

        public SimpleStraitBulletFactory(Texture2D bulletSprite, Rectangle originsSet, float maxSpeed)
        {
            _bulletSprite = bulletSprite;
            _originsSet = originsSet;
            _maxSpeed = maxSpeed;
            _spriteOrigin = new Vector2(_bulletSprite.Width / 2, _bulletSprite.Height / 2);
        }
        Random rnd = new Random(666);
        public IGameElement GenerateBullet()
        {
            Vector2 origin = new Vector2(_originsSet.X + rnd.Next(_originsSet.Width), _originsSet.Y + rnd.Next(_originsSet.Height));
            float angle =(float)( rnd.NextDouble() * 2 * Math.PI);
            float speed = (float)(rnd.NextDouble() * _maxSpeed);
            return new StraitLineBullet<StaticSprite>(new StaticSprite(_bulletSprite, _spriteOrigin), origin, angle, speed);
        }
    }
}
