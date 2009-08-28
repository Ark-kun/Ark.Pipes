using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Test.XNAWindowsGame.SpriteTypes;
using System.Collections.Generic;
using Ark.XNA.Bullets;

namespace Test.XNAWindowsGame.Bullets
{
    class RadialBulletFactory : DrawableGameComponent
    {
        Sprite bulletSprite;

        int bulletNumber;
        float bulletSpeed;
        float waveFrequency;
        int waveNumber;
        double startTime;

        Matrix transform;
        Func<Vector2, bool> shouldDestroyBullet;
        SpriteBatch spriteBatch;

        List<Bullet1D> bullets = new List<Bullet1D>();
        Matrix[] rayMatrices;
        int waveCount;

        public RadialBulletFactory(Game game, Matrix transform, double startTime, Sprite bulletSprite, int bulletNumber, float bulletSpeed, int waveNumber, float waveFrequency, Func<Vector2, bool> shouldDestroyBullet)
            : base(game)
        {
            this.bulletSprite = bulletSprite;
            this.bulletNumber = bulletNumber;
            this.bulletSpeed = bulletSpeed;
            this.waveNumber = waveNumber;
            this.waveFrequency = waveFrequency;
            this.transform = transform;
            this.startTime = startTime;
            this.shouldDestroyBullet = shouldDestroyBullet;

            this.spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            rayMatrices = new Matrix[bulletNumber];
            for (int i = 0; i < bulletNumber; i++)
            {
                rayMatrices[i] = Matrix.CreateRotationZ((float)(2 * Math.PI * i / bulletNumber)) * transform;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            double totalSecondsPassed = gameTime.TotalGameTime.TotalSeconds - startTime;
            while (waveCount < waveNumber && waveCount < totalSecondsPassed * waveFrequency)
            {
                double waveStartTime = startTime + waveCount / waveFrequency;
                //Movements.Movement1D movement = (time) => time < waveStartTime ? 0 : bulletSpeed * (time - waveStartTime);
                ////Func<double, double> movement = (time) => time < waveStartTime ? 0 :Math.Sqrt(100* bulletSpeed * (time - waveStartTime));
                //Movements.Movement1D movement = Movements.ConditionalMovement(
                //    (time) => time < waveStartTime,
                //    Movements.StayAtStart,
                //    Movements.MoveAtConstantSpeed(bulletSpeed).TranslateTime(waveStartTime));
                Movements.Movement1D movement = (time) => 200 * Math.Sin(time - waveStartTime);
                for (int i = 0; i < bulletNumber; i++)
                {
                    //bullets.Add(new Bullet1D(game, rayMatrices[i], bulletSprite, movement));
                    Matrix m = Matrix.CreateRotationZ((float)(2 * Math.PI * ((float)i / bulletNumber + (float)waveCount / 50))) * transform;
                    bullets.Add(new Bullet1D(Game, m, bulletSprite, movement));
                }
                waveCount++;
            }
            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
            }
            bullets.RemoveAll(bullet => shouldDestroyBullet(bullet.position));
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            spriteBatch.Begin();
            foreach (var bullet in bullets)
            {
                bullet.Draw(gameTime);
            }
            spriteBatch.End();
        }
    }
    public class Bullet1D : DrawableGameComponent
    {
        Matrix transform;
        Sprite bulletSprite;
        Movements.Movement1D movement;
        SpriteBatch spriteBatch;

        public Bullet1D(Game game, Matrix transform, Sprite bulletSprite, Movements.Movement1D movement)
            : base(game)
        {
            this.transform = transform;
            this.bulletSprite = bulletSprite;
            this.movement = movement;
            this.spriteBatch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
        }

        public Vector2 position;
        float angle;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            var x = (float)movement(gameTime.TotalGameTime.TotalSeconds);
            //var ownPosition = new Vector2((float)x, 0);
            //this.position = Vector2.Transform(ownPosition, transform);
            this.position = new Vector2(x * transform.M11 + transform.M41, x * transform.M12 + transform.M42);
            var direction = new Vector2(transform.M11, transform.M12);
            this.angle = (float)direction.Angle();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            bulletSprite.Draw(spriteBatch, position, angle);
        }
    }
}
