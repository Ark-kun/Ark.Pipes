using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Ark.XNA.Bullets;
using Ark.XNA.Bullets.Factories;
using Ark.XNA.Transforms;
using Ark.XNA.Sprites;

namespace Test.XNAWindowsGame {
    public class MyGame : Microsoft.Xna.Framework.Game {
        private const int TargetFrameRate = 60;
        //private const int BackBufferWidth = 1280;
        //private const int BackBufferHeight = 720;
        private const int BackBufferWidth = 1000;
        private const int BackBufferHeight = 1000;

        GraphicsDeviceManager graphics;
        SharedSpriteBatch spriteBatch;

        Vector2 screenCenter;
        Rectangle screenRectangle;

        //SimpleRandomStraitBulletFactory bulletFactory;
        //HomingBulletFactory bulletFactory;
        //RadialBulletFactory bulletFactory;

        List<IGameElement> gameElements = new List<IGameElement>();

        public MyGame() {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;


        }

        protected override void LoadContent() {


            //////            //spriteTexture = Content.Load<Texture2D>("6861023");
            //////            //Texture2D spriteTexture1 = null;
            //////            //Texture2D spriteTexture2 = Content.Load<Texture2D>("Circle5");
            //////            //Texture2D spriteTexture1 = Content.Load<Texture2D>("Circle2");
            //////            //Texture2D spriteTexture2 = Content.Load<Texture2D>("Circle3");
            //////            //sprite = new RotatingSprite() { Sprite = spriteTexture1 };
            ////////            sprite = new CoolSprite(this, "Circle2", "Circle5");

            //////            //Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 1");
            ///////Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 2");
            //////            Texture2D bulletTexture3 = Content.Load<Texture2D>("Bullet 3");
            //////            //bulletFactory = new SimpleRandomStraitBulletFactory(bulletTexture1, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), 1.0001f);
            //////            //bulletFactory = new HomingBulletFactory(bulletTexture1, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), 1.0001f);
            //////            var bulletSprite = new Sprite() { texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2) };
            //////            var bulletSprite2 = new Sprite() { texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Red };
            //////            var bulletSprite3 = new Sprite() { texture = bulletTexture3, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Red };

            //////            screenRectangle = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            //////            Func<Vector2, bool> destroyer = v => !screenRectangle.Contains(new Point((int)v.X, (int)v.Y));
            //////            //bulletFactory = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0), 0, bulletSprite, 10, 10, int.MaxValue, 1, destroyer);
            //////            //bulletFactory = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0), 0, bulletSprite, 20, 50, int.MaxValue, 2f, destroyer);
            //////            var bulletFactory1 = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0), 0, bulletSprite, 20, 50, int.MaxValue, 2f, destroyer);
            //////            //var bulletFactory2 = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X-200, screenCenter.Y+50, 0), 0.5f, bulletSprite2, 20, 50, int.MaxValue, 3f, destroyer);
            //////            //var mat = Matrix.CreateScale(1,4,1);
            //////            var mat = Matrix.CreateRotationZ(1);
            //////            mat.Translation = new Vector3(screenCenter.X - 200, screenCenter.Y + 50, 0);
            //////            var bulletFactory2 = new RadialBulletFactory(this, mat, 0.5f, bulletSprite3, 20, 80, int.MaxValue, 3f, destroyer);
            //////            //this.Components.Add(bulletFactory1);
            //////            //this.Components.Add(bulletFactory2);




        }

        protected override void Initialize() {
            screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            this.spriteBatch = new SharedSpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 1");
            Texture2D bulletTexture2 = Content.Load<Texture2D>("Bullet 2");
            Texture2D bulletTexture3 = Content.Load<Texture2D>("Bullet 3");

            float scale = 400;

            var bulletSpriteInBatchNew1 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture2, origin = new Vector2(bulletTexture2.Width / 2, bulletTexture2.Height / 2), scale = (1.0f / 20) * 20 / 15, tint = Color.White };
            var bulletSpriteInBatchNew2 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture2, origin = new Vector2(bulletTexture2.Width / 2, bulletTexture2.Height / 2), scale = (1.0f / 20) * 20 / 15, tint = Color.Black };
            var bulletSpriteInBatchNew3 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture2, origin = new Vector2(bulletTexture2.Width / 2, bulletTexture2.Height / 2), scale = (1.0f / 20) * 20 / 15, tint = Color.Blue };
            var bulletSpriteInBatchNew4 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture2, origin = new Vector2(bulletTexture2.Width / 2, bulletTexture2.Height / 2), scale = (1.0f / 20) * 20 / 15, tint = Color.Red };

            var scaleMatrix = Matrix.CreateScale(scale);
            var translationMatrix = Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0);

            //var rotator = new UpdateableFunctionTransform<Vector2>(this, (t) => (v) => Vector2.Transform(Vector2.Transform(v, Matrix.CreateScale((float)(Math.Sqrt(2) * t))), Matrix.CreateRotationZ((float)(Math.PI / 4 * t))));
            //var antiScaler = new UpdateableFunctionTransform<Vector2>(this, (t) => (v) => Vector2.Transform(Vector2.Transform(Vector2.Transform(v, Matrix.CreateScale((float)(Math.Pow(2.0, (t < 5 ? 0 : t - 5) / 2)))), Matrix.Identity), translationMatrix));
            var rotator = new UpdateableFunctionTransform<Vector2>(this, (t) => (v) => Vector2.Transform(Vector2.Transform(Vector2.Transform(v, Matrix.CreateScale((float)(Math.Pow(2.0, (t < 6 ? 0 : t - 6) / 2)))), Matrix.CreateRotationZ((float)(Math.PI / 4 * t))), translationMatrix));
            //var identity = new UpdateableFunctionTransform<Vector2>(this, (t) => (v) => Vector2.Transform(Vector2.Transform(v, Matrix.Identity), translationMatrix));

            Components.Add(rotator);

            //HeighwayDragonFactories
            var m1 = scaleMatrix * Matrix.CreateRotationZ((float)(0 * 0.5 * Math.PI));
            var m2 = scaleMatrix * Matrix.CreateRotationZ((float)(1 * 0.5 * Math.PI));
            var m3 = scaleMatrix * Matrix.CreateRotationZ((float)(2 * 0.5 * Math.PI));
            var m4 = scaleMatrix * Matrix.CreateRotationZ((float)(3 * 0.5 * Math.PI));

            Func<Vector2, bool> killer = (v) => (v - screenCenter).Length() > screenCenter.Length() + 20;

            var bulletFactoryHeighway1 = new HeighwayDragonFactory(this, new XnaMatrixTransform(m1).Append(rotator), bulletSpriteInBatchNew1, 1, killer);
            var bulletFactoryHeighway2 = new HeighwayDragonFactory(this, new XnaMatrixTransform(m2).Append(rotator), bulletSpriteInBatchNew2, 1, killer);
            var bulletFactoryHeighway3 = new HeighwayDragonFactory(this, new XnaMatrixTransform(m3).Append(rotator), bulletSpriteInBatchNew3, 1, killer);
            var bulletFactoryHeighway4 = new HeighwayDragonFactory(this, new XnaMatrixTransform(m4).Append(rotator), bulletSpriteInBatchNew4, 1, killer);

            //this.Components.Add(bulletFactoryHeighway1);
            //this.Components.Add(bulletFactoryHeighway2);
            //this.Components.Add(bulletFactoryHeighway3);
            //this.Components.Add(bulletFactoryHeighway4);

            //RadialBulletFactories
            screenRectangle = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Func<Vector2, bool> destroyer = v => !screenRectangle.Contains(new Point((int)v.X, (int)v.Y));

            var tt2D = new TranslationTransform2D();
            Components.Add(new MouseControlledObject(this, tt2D));

            var bulletSpriteInBatchRadial1 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture3, origin = new Vector2(bulletTexture3.Width / 2, bulletTexture3.Height / 2), scale = 1.0f, tint = Color.Red };
            //var bulletFactoryRadial1 = new RadialBulletFactory(this, translationMatrix, 0, bulletSpriteInBatchRadial1, 20, 50, int.MaxValue, 2f, destroyer);
            var bulletFactoryRadial1 = new RadialBulletFactory(this, tt2D, 0, bulletSpriteInBatchRadial1, 20, 50, int.MaxValue, 2f, destroyer);
            this.Components.Add(bulletFactoryRadial1);


            //Components.Add(new FpsCounter(this, spriteBatch, screenCenter));

            //var cursor = new CoolSprite(this, "Circle2", "Circle5");
            var cursor = new CoolSprite(this, "Circle2", "Circle3");
            Components.Add(cursor);
            Components.Add(new MouseControlledObject(this, cursor));
            //Components.Add(new KeyboardControlledObject(this, cursor, 1000));   



            base.Initialize();
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }

    static class Program {
        static void Main(string[] args) {
            using (Game game = new MyGame()) {
                game.Run();
            }
        }
    }
}