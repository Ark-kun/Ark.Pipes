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
using Ark.XNA;
using Ark.Pipes;
using Ark.XNA.Components;
using Ark.XNA.Geometry;
using Ark.XNA.Geometry.Curves.Dynamic;
using Ark.XNA.Geometry.Curves;

namespace Ark.Shohou {
    public class TestGame1 : Microsoft.Xna.Framework.Game {
        private const int TargetFrameRate = 60;
        //private const int BackBufferWidth = 1280;
        //private const int BackBufferHeight = 720;
        private const int BackBufferWidth = 1000;
        private const int BackBufferHeight = 1000;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 screenCenter;
        Rectangle screenRectangle;

        public TestGame1() {
            Content.RootDirectory = "Content";
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
        }

        protected override void LoadContent() {
            //((System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(this.Window.Handle)).FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;


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



            base.LoadContent();
        }
        //public const int HORZSIZE = 4;
        //public const int VERTSIZE = 6;

        //public const int LOGPIXELSX = 0x58;
        //public const int LOGPIXELSY = 90;

        //[System.Security.SuppressUnmanagedCodeSecurity, System.Security.SecurityCritical, System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        //public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        //[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //public static extern IntPtr GetDC(IntPtr hWnd);

        protected override void Initialize() {
            screenCenter = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            //var xxx = GetDeviceCaps(GetDC(IntPtr.Zero), HORZSIZE);

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.Services.AddService(typeof(SpriteBatch), spriteBatch);

            Texture2D bulletTexture1 = Content.Load<Texture2D>("Bullet 1");
            Texture2D bulletTexture2 = Content.Load<Texture2D>("Bullet 2");
            Texture2D bulletTexture3 = Content.Load<Texture2D>("Bullet 3");

            float scale = 400;

            var bulletSpriteInBatchNew1 = new StaticSprite(spriteBatch, bulletTexture2, (1.0f / 20) * 20 / 15);
            var bulletSpriteInBatchNew2 = new StaticSprite(spriteBatch, bulletTexture2, (1.0f / 20) * 20 / 15, Color.Black);
            var bulletSpriteInBatchNew3 = new StaticSprite(spriteBatch, bulletTexture2, (1.0f / 20) * 20 / 15, Color.Blue);
            var bulletSpriteInBatchNew4 = new StaticSprite(spriteBatch, bulletTexture2, (1.0f / 20) * 20 / 15, Color.Red);

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
            //Components.Add(new MouseControlledObject(this, tt2D));
            tt2D.Translation = Ark.Pipes.Mouse.Position;

            var bulletSpriteInBatchRadial1 = new StaticSprite(spriteBatch, bulletTexture3, 1.0f, Color.Red);
            //var bulletFactoryRadial1 = new RadialBulletFactory(this, translationMatrix, 0, bulletSpriteInBatchRadial1, 20, 50, int.MaxValue, 2f, destroyer);
            var bulletFactoryRadial1 = new RadialBulletFactory(this, tt2D, 0, bulletSpriteInBatchRadial1, 20, 50, int.MaxValue, 2f, destroyer);
            //this.Components.Add(bulletFactoryRadial1);


            //Components.Add(new FpsCounter(this, spriteBatch, screenCenter));

            //var cursor = new CoolSprite(this, "Circle2", "Circle5");
            var cursor = new CoolSprite(this, "Circle2", "Circle3");
            Components.Add(cursor);
            //Components.Add(new MouseControlledObject(this, cursor));

            cursor.Position = Ark.Pipes.Mouse.Position;
            //Components.Add(new KeyboardControlledObject(this, cursor, 1000));

            //var ds1 = new DynamicSprite(this, spriteBatch) { Texture = bulletTexture3 };
            //hbf = new HomingBulletFactory(this, ds1, screenRectangle, 0.1f);
            ////hbf = new HomingBulletFactory(this, ds1, screenRectangle, 0.00000000000001f);
            ////hbf.Target = Ark.Pipes.Mouse.Position;

            var time = new Time();
            Components.Add(time);

            Random rnd = new Random();
            var randomVector = new RandomVectorInsideRectangle(screenRectangle);
            for (int i = 0; i < 200; i++) {
                var line = new DynamicBoundVector() { StartPoint = randomVector.Value, EndPoint = Ark.Pipes.Mouse.Position };
                //var curve = new LineSectionCurve(line);
                //lemniscate of Bernoulli 
                var curve = new LemniscateOfBernoulliCurve(line);
                //linear
                //var movement = new CurveMovement(curve) { Time = new Function<float>(() => time.Value * 0.001f) };
                //linear fast
                //var movement = new CurveMovement(curve) { Time = new Function<float>(() => time.Value * 0.01f) };
                //linear sine
                //var movement = new CurveMovement(curve) { Time = new Function<float>(() => 1 + (float)Math.Cos(time.Value * 0.001f)) };
                //linear chaotic phase
                //var movement = new CurveMovement(curve) { Time = new Function<float>(() => time.Value * 0.001f + (float)rnd.NextDouble()) };
                //linear random phase
                //float phase = (float)rnd.NextDouble(); //small delta
                float phase = (float)(rnd.NextDouble() * (2 * Math.PI)); //full delta
                var movement = new CurveMovement(curve) { Time = new Function<float>(() => time.Value * 0.001f + phase) };

                var bullet = new DynamicSprite(this, spriteBatch) { Position = movement.Position, Texture = bulletTexture2, Origin = bulletTexture2.CenterOrigin() };
                Components.Add(bullet);
            }

            //song1 = Content.Load<Song>("All Reflexive Arcade Games 1_0crk_xm");
            //song2 = Content.Load<Song>("ACDSee 8_0_14crk_xm");
            //base.Initialize();
            //MediaPlayer.Play(song1);

            base.Initialize();
        }
        Song song1;
        Song song2;
        //HomingBulletFactory hbf;
        protected override void Update(GameTime gameTime) {
            //if (song1 == null) {
                //(new System.Threading.Thread(() => { song1 = Content.Load<Song>("All Reflexive Arcade Games 1_0crk_xm"); MediaPlayer.Play(song1); }) { Priority =System.Threading.ThreadPriority.Lowest }).Start();
                //song1 = Content.Load<Song>("All Reflexive Arcade Games 1_0crk_xm");
                //MediaPlayer.Play(song1);
            //}
            //Components.OfType<HomingBulletFactory>()
            //Components.Add(hbf.GenerateBullet());
            if (Microsoft.Xna.Framework.Input.Mouse.GetState().LeftButton == ButtonState.Pressed) {
                MediaPlayer.Pause();
            } else {
                //if (MediaPlayer.State == MediaState.Paused) {
                //    MediaPlayer.Resume();
                //}
            }
            if (Microsoft.Xna.Framework.Input.Mouse.GetState().RightButton == ButtonState.Pressed) {
                if (song2 != null) {
                    MediaPlayer.Play(song2);
                    song2 = null;
                }
            }

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);

        }

        protected override bool BeginDraw() {
            spriteBatch.Begin();
            return base.BeginDraw();
        }

        protected override void EndDraw() {
            spriteBatch.End();
            base.EndDraw();
        }
    }
}