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
using Test.XNAWindowsGame.Bullets;
using Test.XNAWindowsGame.Bullets.Factories;

namespace Test.XNAWindowsGame {
    public class MyGame : Microsoft.Xna.Framework.Game {
        private const int TargetFrameRate = 60;
        //private const int BackBufferWidth = 1280;
        //private const int BackBufferHeight = 720;
        private const int BackBufferWidth = 800;
        private const int BackBufferHeight = 800;

        GraphicsDeviceManager graphics;
        SharedSpriteBatch spriteBatch;

        SpriteFont someFont;
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
            //this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spriteBatch = new SharedSpriteBatch(GraphicsDevice);

            this.Services.AddService(typeof(SpriteBatch), spriteBatch);
            
            Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 2");
            var bulletSpriteInBatch = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2) };
            var bulletSpriteInBatch2 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Black };
            var bulletSpriteInBatch3 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Blue };
            var bulletSpriteInBatch4 = new SpriteInBatch() { spriteBatch = spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Red };
            //var bulletFactoryHeighway = new HeighwayDragonBullet(this, null, new Vector2(200, 250), new Vector2(800, 250), bulletSpriteInBatch, 1);
            //var bulletFactoryHeighway1 = new HeighwayDragonBullet(this, null, new Vector2(400, 400), new Vector2(700, 400), bulletSpriteInBatch, 1);
            //var bulletFactoryHeighway2 = new HeighwayDragonBullet(this, null, new Vector2(400, 400), new Vector2(100, 400), bulletSpriteInBatch2, 1);
            //var bulletFactoryHeighway3 = new HeighwayDragonBullet(this, null, new Vector2(400, 400), new Vector2(400, 100), bulletSpriteInBatch3, 1);
            //var bulletFactoryHeighway4 = new HeighwayDragonBullet(this, null, new Vector2(400, 400), new Vector2(400, 700), bulletSpriteInBatch4, 1);
            //this.Components.Add(bulletFactoryHeighway);
            //this.Components.Add(bulletFactoryHeighway1);
            //this.Components.Add(bulletFactoryHeighway2);
            //this.Components.Add(bulletFactoryHeighway3);
            //this.Components.Add(bulletFactoryHeighway4);
            //var bulletFactoryHeighway1 = new HeighwayDragonFactory(this, new XnaMatrixTransform(Matrix.CreateTranslation(400, 400,0)), bulletSpriteInBatch, 1);
            //var bulletFactoryHeighway1 = new HeighwayDragonFactory(this, Ark.XNA.Transforms.Transform<Vector2>.Identity, bulletSpriteInBatch, 1);
            var m = Matrix.CreateScale(100);
            m.Translation = new Vector3(400,400,0);
            var bulletFactoryHeighway1 = new HeighwayDragonFactory(this, new XnaMatrixTransform(m), bulletSpriteInBatch, 1);
            this.Components.Add(bulletFactoryHeighway1);



            screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
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