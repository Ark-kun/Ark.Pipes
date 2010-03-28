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

            var time = new Time();
            Components.Add(time);

            var cursor = new CoolSprite(this, "Circle2", "Circle3");
            cursor.Position = Ark.Pipes.Mouse.Position;
            Components.Add(cursor);

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