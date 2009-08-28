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

namespace Test.XNAWindowsGame
{
    public class MyGame : Microsoft.Xna.Framework.Game
    {
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1280;
        private const int BackBufferHeight = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont someFont;
        int frameCount = 0;
        Vector2 screenCenter;
        Rectangle screenRectangle;
        Color fpsColor = Color.BlanchedAlmond;

        //SimpleRandomStraitBulletFactory bulletFactory;
        //HomingBulletFactory bulletFactory;
        //RadialBulletFactory bulletFactory;

        List<IGameElement> gameElements = new List<IGameElement>();

        public MyGame()
        {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
        }

        protected override void LoadContent()
        {
            //this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spriteBatch = new SharedSpriteBatch(GraphicsDevice);
            this.someFont = Content.Load<SpriteFont>("Some Font");
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            //spriteTexture = Content.Load<Texture2D>("6861023");
            //Texture2D spriteTexture1 = null;
            //Texture2D spriteTexture2 = Content.Load<Texture2D>("Circle5");
            Texture2D spriteTexture1 = Content.Load<Texture2D>("Circle2");
            Texture2D spriteTexture2 = Content.Load<Texture2D>("Circle3");
            //sprite = new RotatingSprite() { Sprite = spriteTexture1 };
            sprite = new CoolSprite() { Sprite1 = spriteTexture1, Sprite2 = spriteTexture2 };
            gameElements.Add(sprite);

            //Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 1");
            Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 2");
            Texture2D bulletTexture3 = Content.Load<Texture2D>("Bullet 3");
            //bulletFactory = new SimpleRandomStraitBulletFactory(bulletTexture1, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), 1.0001f);
            //bulletFactory = new HomingBulletFactory(bulletTexture1, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), 1.0001f);
            var bulletSprite = new Sprite() { texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2) };
            var bulletSprite2 = new Sprite() { texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Red };
            var bulletSprite3 = new Sprite() { texture = bulletTexture3, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Red };

            screenRectangle = new Rectangle(graphics.GraphicsDevice.Viewport.X, graphics.GraphicsDevice.Viewport.Y, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
            Func<Vector2, bool> destroyer = v => !screenRectangle.Contains(new Point((int)v.X, (int)v.Y));
            //bulletFactory = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0), 0, bulletSprite, 10, 10, int.MaxValue, 1, destroyer);
            //bulletFactory = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0), 0, bulletSprite, 20, 50, int.MaxValue, 2f, destroyer);
            var bulletFactory1 = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X, screenCenter.Y, 0), 0, bulletSprite, 20, 50, int.MaxValue, 2f, destroyer);
            //var bulletFactory2 = new RadialBulletFactory(this, Matrix.CreateTranslation(screenCenter.X-200, screenCenter.Y+50, 0), 0.5f, bulletSprite2, 20, 50, int.MaxValue, 3f, destroyer);
            //var mat = Matrix.CreateScale(1,4,1);
            var mat = Matrix.CreateRotationZ(1);
            mat.Translation = new Vector3(screenCenter.X-200, screenCenter.Y+50,0);
            var bulletFactory2 = new RadialBulletFactory(this, mat, 0.5f, bulletSprite3, 20, 80, int.MaxValue, 3f, destroyer);
            //this.Components.Add(bulletFactory1);
            //this.Components.Add(bulletFactory2);

            var bulletSpriteInBatch = new SpriteInBatch() { spriteBatch = (SharedSpriteBatch)spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2) };
            var bulletSpriteInBatch2 = new SpriteInBatch() { spriteBatch = (SharedSpriteBatch)spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Black };
            var bulletSpriteInBatch3 = new SpriteInBatch() { spriteBatch = (SharedSpriteBatch)spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Blue };
            var bulletSpriteInBatch4 = new SpriteInBatch() { spriteBatch = (SharedSpriteBatch)spriteBatch, texture = bulletTexture1, origin = new Vector2(bulletTexture1.Width / 2, bulletTexture1.Height / 2), tint = Color.Red };
            //var bulletFactoryHeighway = new HeighwayDragonBullet(this, null, new Vector2(200, 250), new Vector2(800, 250), bulletSpriteInBatch, 1);
            var bulletFactoryHeighway1 = new HeighwayDragonBullet(this, null, new Vector2(600, 250), new Vector2(900, 250), bulletSpriteInBatch, 1);
            var bulletFactoryHeighway2 = new HeighwayDragonBullet(this, null, new Vector2(600, 250), new Vector2(300, 250), bulletSpriteInBatch2, 1);
            var bulletFactoryHeighway3 = new HeighwayDragonBullet(this, null, new Vector2(600, 250), new Vector2(600, -50), bulletSpriteInBatch3, 1);
            var bulletFactoryHeighway4 = new HeighwayDragonBullet(this, null, new Vector2(600, 250), new Vector2(600, 550), bulletSpriteInBatch4, 1);
            //this.Components.Add(bulletFactoryHeighway);
            this.Components.Add(bulletFactoryHeighway1);
            this.Components.Add(bulletFactoryHeighway2);
            this.Components.Add(bulletFactoryHeighway3);
            this.Components.Add(bulletFactoryHeighway4);
        }

        protected override void Initialize()
        {
            screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }


        protected override void UnloadContent()
        {
        }

        double lastBulletTime;
        protected override void Update(GameTime gameTime)
        {
            HandleInput();
            GetInput();

            //if (gameTime.TotalGameTime.TotalMilliseconds - lastBulletTime > 100)
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        gameElements.Add(bulletFactory.GenerateBullet());
            //    }
            //    lastBulletTime = gameTime.TotalGameTime.TotalMilliseconds;
            //}


            foreach (var ge in gameElements)
            {
                ge.Update(gameTime);
            }

            base.Update(gameTime);
        }

        private void HandleInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            bool continuePressed = keyboardState.IsKeyDown(Keys.Space);
        }


        //RotatingSprite sprite;
        CoolSprite sprite;


        private void GetInput()
        {
            // Get input state.
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 spritePosition = sprite.Position;
            var mouseState = Mouse.GetState();

            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            spritePosition = new Vector2() { X = mouseState.X, Y = mouseState.Y };
            //}
            //bulletFactory.Target = new Vector2() { X = mouseState.X, Y = mouseState.Y };

            // If any digital horizontal movement input is found, override the analog movement.
            if (keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.A))
            {
                spritePosition.X -= 10.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Right) ||
                     keyboardState.IsKeyDown(Keys.D))
            {
                spritePosition.X += 10.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.W))
            {
                spritePosition.Y -= 10.0f;
            }
            if (keyboardState.IsKeyDown(Keys.Down) ||
                     keyboardState.IsKeyDown(Keys.S))
            {
                spritePosition.Y += 10.0f;
            }
            sprite.Position = spritePosition;
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            frameCount += 1;
            var FPS = frameCount / gameTime.TotalRealTime.TotalSeconds;

            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.DrawString(someFont, FPS.ToString(), screenCenter, fpsColor);
            //spriteBatch.DrawString(someFont, gameTime.TotalGameTime.TotalSeconds.ToString(), fpsPosition, fpsColor);

            foreach (var ge in gameElements)
            {
                ge.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new MyGame())
            {
                game.Run();
            }
        }
    }
}