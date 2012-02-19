using System;
using System.Linq;
using System.Collections.Generic;
using Ark.Xna.Sprites;
using Ark.Xna.Transforms;
using Microsoft.Xna.Framework;

namespace Ark.Xna.Bullets.Factories {
    public class RadialBulletFactory : GameComponent {
        StaticSprite bulletSprite;

        int bulletNumber;
        float bulletSpeed;
        float waveFrequency;
        int waveNumber;
        double startTime;

        ITransform<Vector2> _transform;
        Func<Vector2, bool> shouldDestroyBullet;

        HashSet<Bullet1D> bullets = new HashSet<Bullet1D>();
        ITransform<Vector2>[] rayMatrices;
        int waveCount;

        public RadialBulletFactory(Game game, ITransform<Vector2> transform, double startTime, StaticSprite bulletSprite, int bulletNumber, float bulletSpeed, int waveNumber, float waveFrequency, Func<Vector2, bool> shouldDestroyBullet)
            : base(game) {
            this.bulletSprite = bulletSprite;
            this.bulletNumber = bulletNumber;
            this.bulletSpeed = bulletSpeed;
            this.waveNumber = waveNumber;
            this.waveFrequency = waveFrequency;
            this._transform = transform;
            this.startTime = startTime;
            this.shouldDestroyBullet = shouldDestroyBullet;

            rayMatrices = new ITransform<Vector2>[bulletNumber];
            for (int i = 0; i < bulletNumber; i++) {
                rayMatrices[i] = new XnaMatrixTransform(Matrix.CreateRotationZ((float)(2 * Math.PI * i / bulletNumber))).Append(_transform);
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            double totalSecondsPassed = gameTime.TotalGameTime.TotalSeconds - startTime;
            while (waveCount < waveNumber && waveCount < totalSecondsPassed * waveFrequency) {
                double waveStartTime = startTime + waveCount / waveFrequency;
                //Movements.Movement1D movement = (time) => time < waveStartTime ? 0 : bulletSpeed * (time - waveStartTime);
                ////Func<double, double> movement = (time) => time < waveStartTime ? 0 :Math.Sqrt(100* bulletSpeed * (time - waveStartTime));
                //Movements.Movement1D movement = Movements.ConditionalMovement(
                //    (time) => time < waveStartTime,
                //    Movements.StayAtStart,
                //    Movements.MoveAtConstantSpeed(bulletSpeed).TranslateTime(waveStartTime));

                //Movements.Movement1D movement = (time) => 200 * Math.Sin(time - waveStartTime);
                for (int i = 0; i < bulletNumber; i++) {
                    Movements.Movement1D movement = Movements.ConditionalMovement(
                        (time) => time < waveStartTime - (float)i / bulletNumber / waveFrequency,
                        Movements.StayAtStart,
                        Movements.MoveAtConstantSpeed(bulletSpeed).TranslateTime(waveStartTime - (float)i / bulletNumber / waveFrequency));
                    //bullets.Add(new Bullet1D(game, rayMatrices[i], bulletSprite, movement));
                    var m = Matrix.CreateRotationZ((float)(2 * Math.PI * ((float)i / bulletNumber + (float)waveCount / 50)));
                    //var t = new XnaMatrixTransform(m).Append(_transform);
                    var v = _transform.Transform(Vector2.Zero);
                    m.Translation = new Vector3(v.X, v.Y, 0);
                    var t = new XnaMatrixTransform(m);
                    var bullet = new Bullet1D(Game, null, t, bulletSprite, movement);
                    bullets.Add(bullet);
                    Game.Components.Add(bullet);
                }
                waveCount++;
            }
            var bulletsToRemove = bullets.Where((bullet) => shouldDestroyBullet(bullet.Position)).ToArray();
            foreach (var bullet in bulletsToRemove) {
                bullets.Remove(bullet);
                Game.Components.Remove(bullet);
            }
        }

    }

    public class Bullet1D : BulletBase<Vector2> {
        ITransform<Vector2> _transform;
        StaticSprite _bulletSprite;
        Movements.Movement1D _movement;
        float angle;
        Vector2 _oldPosition;

        public Bullet1D(Game game, IBulletFactory<Vector2> parent, ITransform<Vector2> relativeTransform, StaticSprite bulletSprite, Movements.Movement1D movement)
            : base(game, parent) {
            this._transform = parent == null ? relativeTransform : parent.Transform.Prepend(relativeTransform);
            this._bulletSprite = bulletSprite;
            this._movement = movement;
            _oldPosition = _transform.Transform(Vector2.Zero);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            var x = (float)_movement(gameTime.TotalGameTime.TotalSeconds);
            Position = _transform.Transform(Vector2.UnitX * x);
            var direction = (Position - _oldPosition);
            if (direction.Length() > 0) {
                angle = (float)direction.Angle();
            }
        }

        public override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
            _bulletSprite.Draw(Position, angle);
        }
    }
}
