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
using Ark.Pipes;
using Ark.Pipes.Input;
using Ark.Xna.Bullets;
using Ark.Xna.Bullets.Factories;
using Ark.Xna.Transforms;
using Ark.Xna.Sprites;
using Ark.Xna;
using Ark.Xna.Components;
using Ark.Xna.Geometry;
using Ark.Xna.Geometry.Curves.Dynamic;
using Ark.Xna.Geometry.Curves;

namespace Ark.Shohou {
    //Testing DynamicFrame
    public class TestGame2 : Microsoft.Xna.Framework.Game {
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1000;
        private const int BackBufferHeight = 1000;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Rectangle _screenRectangle;

        Random _rnd = new Random();

        public TestGame2() {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = BackBufferWidth;
            _graphics.PreferredBackBufferHeight = BackBufferHeight;
        }

        protected override void LoadContent() {

            base.LoadContent();
        }

        protected override void Initialize() {
            _screenRectangle = new Rectangle(_graphics.GraphicsDevice.Viewport.X, _graphics.GraphicsDevice.Viewport.Y, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            var time = new GameTimeProvider();
            Components.Add(time);

            //var cursor = new CoolSprite(this, "Circle2", "Circle3");
            //cursor.Position = Ark.Pipes.Mouse.Position;
            //Components.Add(cursor);

            var rootTransform = new XnaMatrix3Transform(Matrix.CreateTranslation(-300, -300, 0) * Matrix.CreateRotationZ(1) * Matrix.CreateTranslation(300, 300, 0));
            var rootFrame = new DynamicFrame(rootTransform);

            var cursor3D = new Function<Vector2, Vector3>(v2 => v2.ToVector3(), XnaMouse.Default.Position);
            var cursorTransform = new TranslationTransform3D() { Translation = cursor3D };
            var cursorFrame = new DynamicFrame(rootFrame, cursorTransform);


            //var cursorSprite = new DynamicSprite(this) { Texture = Content.Load<Texture2D>("Bullet 2"), Position = XnaMouse.Default.Position };

            var cursorSpriteTransform = new FunctionTransform<Vector2>(v2 => cursorFrame.GetAbsoluteTransform().Transform(v2.ToVector3()).ToVector2());
            var cursorSprite = new TransformedSprite(this) { Texture = Content.Load<Texture2D>("Bullet 2"), Transform = cursorSpriteTransform };

            //var source = Ark.Pipes.Mouse.Position;

            ////immidiateTarget
            //Provider<Vector2> immidiateTarget = source.Value;

            ////singleHomingTarget
            //{
            //    Vector2 target = Vector2.Zero;
            //    bool locked = false;
            //    Provider<Vector2> singleHomingTarget = (Provider<Vector2>)(() => { if (!locked) { target = source; locked = true; } return target; });
            //}

            ////homingTarget
            //Provider<Vector2> homingTarget = source;

            

            Components.Add(cursorSprite);

            base.Initialize();
        }


        protected override void Update(GameTime gameTime) {

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        protected override bool BeginDraw() {
            _spriteBatch.Begin();

            return base.BeginDraw();
        }

        protected override void EndDraw() {
            _spriteBatch.End();

            base.EndDraw();
        }
    }
}