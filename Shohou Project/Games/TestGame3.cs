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
    public class TestGame3 : Microsoft.Xna.Framework.Game {
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1000;
        private const int BackBufferHeight = 1000;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Rectangle _screenRectangle;

        GameTimeProvider time;

        Random _rnd = new Random();

        public TestGame3() {
            Content.RootDirectory = "Content";
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = BackBufferWidth;
            _graphics.PreferredBackBufferHeight = BackBufferHeight;
        }

        protected override void LoadContent() {

            base.LoadContent();
        }

        LemniscateOfBernoulliCurve inf;
        CurveMovement cm;

        protected override void Initialize() {
            _screenRectangle = new Rectangle(_graphics.GraphicsDevice.Viewport.X, _graphics.GraphicsDevice.Viewport.Y, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            time = new GameTimeProvider();
            Components.Add(time);

            var bv = new DynamicBoundVector() {
                StartPoint = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 - 100, _screenRectangle.Top + _screenRectangle.Height - 200),
                EndPoint = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 + 100, _screenRectangle.Top + _screenRectangle.Height - 200)
            };
            inf = new LemniscateOfBernoulliCurve(bv);
            cm = new CurveMovement(inf) { Time = time };
            base.Initialize();
        }

        public void CreateBullet1(float startTime = float.NegativeInfinity) {
            Vector2 position;
            //position = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 - 20, _screenRectangle.Top + _screenRectangle.Height - 100);
            //position = inf.Evaluate(time.Value) + new Vector2(-20, 0);
            position = cm.Position + new Vector2(-20, 0);
            var b = Bullets.CreateStraitConstantVelocityBullet(this, time, position, new Vector2(0, -0.5f), Content.Load<Texture2D>("Bullet 2"), startTime);
            var killerRect = _screenRectangle;
            //killerRect.Inflate(-100, -100);
            var killer = Bullets.CreateBoundingRectangleCondition(b.Position, killerRect);
            b.Updated += t => { if (!killer.Value) { b.Dispose(); } };
            Components.Add(b);
        }
        public void CreateBullet2(float startTime = float.NegativeInfinity) {
            Vector2 position;
            //position = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 + 20, _screenRectangle.Top + _screenRectangle.Height - 100);
            //position = inf.Evaluate(time.Value) + new Vector2(+20, 0);
            position = cm.Position + new Vector2(+20, 0);
            var b = Bullets.CreateStraitConstantVelocityBullet(this, time, position, new Vector2(0, -0.5f), Content.Load<Texture2D>("Bullet 2"), startTime);
            var killerRect = _screenRectangle;
            //killerRect.Inflate(-100, -100);
            var killer = Bullets.CreateBoundingRectangleCondition(b.Position, killerRect);
            b.Updated += t => { if (!killer.Value) { b.Dispose(); } };
            Components.Add(b);
        }

        protected override void Update(GameTime gameTime) {
            //if (ShouldFire1(gameTime)) CreateBullet1();
            if (ShouldFire2(gameTime)) CreateBullet1();
            TryFire3(gameTime);

            base.Update(gameTime);
        }

        bool _keyPressed = false;
        bool ShouldFire1(GameTime gameTime) {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space)) {
                if (!_keyPressed) {
                    _keyPressed = true;
                    return true;
                }
            } else {
                _keyPressed = false;
            }
            return false;
        }

        TimeSpan _lastFireTime;
        TimeSpan _fireDelay = new TimeSpan(0, 0, 0, 0, 50);
        bool ShouldFire2(GameTime gameTime) {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space)) {
                if (gameTime.TotalGameTime - _lastFireTime > _fireDelay) {
                    _lastFireTime = gameTime.TotalGameTime;
                    return true;
                }
            }
            return false;
        }

        bool _keyPressed3 = false;
        TimeSpan _lastFireTime3;
        void TryFire3(GameTime gameTime) {
            var state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space)) {
                if (!_keyPressed3) {
                    _keyPressed3 = true;
                    if (gameTime.TotalGameTime - _lastFireTime3 >= _fireDelay) {
                        _lastFireTime3 = gameTime.TotalGameTime - _fireDelay;
                    }
                }
                while (gameTime.TotalGameTime - _lastFireTime3 >= _fireDelay) {
                    _lastFireTime3 += _fireDelay;
                    if (state.IsKeyDown(Keys.Space)) {
                        CreateBullet2((float)_lastFireTime3.TotalMilliseconds);
                    }
                }
            } else {
                _keyPressed3 = false;
            }
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

    public class Bullets {

        public static DynamicSprite CreateStraitConstantVelocityBullet(Game game, Provider<float> time, Vector2 startPosition, Vector2 velocity, Texture2D texture, float startTime = float.NegativeInfinity) {
            if (float.IsNegativeInfinity(startTime)) {
                startTime = time;
            }
            var bullet = new DynamicSprite(game) { Origin = texture.CenterOrigin(), Texture = texture };
            bullet.Position = CreateConstantVelocityMovement2D(time, startTime, startPosition, velocity);
            return bullet;
        }

        public class ConstantVelocityMovement2D : Provider<Vector2> {
            Vector2 _startPosition;
            Vector2 _velocity;
            Provider<float> _time;
            float _startTime;

            public ConstantVelocityMovement2D(Provider<float> time, float startTime, Vector2 startPosition, Vector2 velocity) {
                _startPosition = startPosition;
                _velocity = velocity;
                _time = time;
                _startTime = startTime;
            }

            public override Vector2 Value {
                get {
                    return _startPosition + _velocity * (_time.Value - _startTime);
                }
            }
        }

        public static Provider<Vector2> CreateConstantVelocityMovement2D(Provider<float> time, float startTime, Vector2 startPosition, Vector2 velocity) {
            return (Provider<Vector2>)(() => startPosition + velocity * (time.Value - startTime));
        }

        public static Provider<bool> CreateBoundingRectangleCondition(Provider<Vector2> position, Rectangle boundingRectangle) {
            return (Provider<bool>)(() => (bool)(boundingRectangle.Contains(position.Value.ToPoint())));
        }

        public class SimpleVelocityBasedMovement2D : Provider<Vector2> {
            Vector2 _position;
            float _lastUpdateTime;
            Provider<float> _time;

            public SimpleVelocityBasedMovement2D(Provider<float> time, Vector2 startPosition, Provider<Vector2> velocity) {
                _position = startPosition;
                Velocity = velocity;
                _time = time;
                _lastUpdateTime = time.Value;
            }

            public void Update() {
                var currentTime = _time.Value;
                _position += Velocity.Value * (currentTime - _lastUpdateTime);
                _lastUpdateTime = currentTime;
            }

            public Provider<Vector2> Velocity { get; set; }

            public override Vector2 Value {
                get {
                    return _position;
                }
            }
        }
    }
}