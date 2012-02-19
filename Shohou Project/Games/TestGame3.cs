using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using Ark.Xna.Bullets;
using Ark.Xna.Bullets.Factories;
using Ark.Xna.Transforms;
using Ark.Xna.Sprites;
using Ark.Xna;
using Ark.Pipes;
using Ark.Xna.Components;
using Ark.Xna.Geometry;
using Ark.Xna.Geometry.Curves.Dynamic;
using Ark.Xna.Geometry.Curves;

namespace Ark.Shohou {
    public class TestGame3 : Microsoft.Xna.Framework.Game {
        private const int TargetFrameRate = 60;
        private const int BackBufferWidth = 1000;
        private const int BackBufferHeight = 1000;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Rectangle _screenRectangle;

        GameTimeProvider time;
        Time realTime;

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
        CurveMovement cm2;

        protected override void Initialize() {
            _screenRectangle = new Rectangle(_graphics.GraphicsDevice.Viewport.X, _graphics.GraphicsDevice.Viewport.Y, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);

            time = new GameTimeProvider();
            Components.Add(time);
            realTime = new Time();

            var bv = new DynamicBoundVector() {
                StartPoint = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 - 100, _screenRectangle.Top + _screenRectangle.Height - 200),
                EndPoint = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 + 100, _screenRectangle.Top + _screenRectangle.Height - 200)
            };
            inf = new LemniscateOfBernoulliCurve(bv);
            cm = new CurveMovement(inf) { Time = new Function<float>(() => time.Value * 0.004f) };
            cm2 = new CurveMovement(inf) { Time = new Function<float>(() => realTime.Value * 0.004f) };

            var cannon2 = new DynamicSprite(this) { Position = cm.Position, Texture = Content.Load<Texture2D>("Bullet 2"), Tint = Color.Red };
            Components.Add(cannon2);

            var cannon3 = new DynamicSprite(this) { Position = cm2.Position, Texture = Content.Load<Texture2D>("Bullet 2"), Tint = Color.Blue };
            Components.Add(cannon3);

            base.Initialize();
        }
#if !WINDOWS_PHONE || SILVERLIGHT
        public void CreateBullet1(float startTime = float.NegativeInfinity) {
#else
        public void CreateBullet1(float startTime) {
#endif
            Vector2 position;
            //position = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 - 20, _screenRectangle.Top + _screenRectangle.Height - 100);
            //position = inf.Evaluate(time.Value) + new Vector2(-20, 0);
            //position = cm.Position + new Vector2(-20, 0);
            position = cm.Position;
            var b = Bullets.CreateStraitConstantVelocityBullet(this, time, position, new Vector2(0, -0.5f), Content.Load<Texture2D>("Bullet 2"), startTime);
            var killerRect = _screenRectangle;
            //killerRect.Inflate(-100, -100);
            var killer = Bullets.CreateBoundingRectangleCondition(b.Position, killerRect);
            b.Updated += t => { if (!killer.Value) { b.Dispose(); } };
            Components.Add(b);
        }
#if !WINDOWS_PHONE || SILVERLIGHT
        public void CreateBullet2(float startTime = float.NegativeInfinity) {
#else
        public void CreateBullet2(float startTime) {
#endif
            Vector2 position;
            //position = new Vector2(_screenRectangle.Left + _screenRectangle.Width / 2 + 20, _screenRectangle.Top + _screenRectangle.Height - 100);
            //position = inf.Evaluate(time.Value) + new Vector2(+20, 0);
            //position = cm2.Position + new Vector2(+20, 0);
            position = cm2.Position;
            var b = Bullets.CreateStraitConstantVelocityBullet(this, realTime, position, new Vector2(0, -0.5f), Content.Load<Texture2D>("Bullet 2"), startTime);
            var killerRect = _screenRectangle;
            //killerRect.Inflate(-100, -100);
            var killer = Bullets.CreateBoundingRectangleCondition(b.Position, killerRect);
            b.Updated += t => { if (!killer.Value) { b.Dispose(); } };
            Components.Add(b);
        }

        protected override void Update(GameTime gameTime) {
            //if (ShouldFire1(gameTime)) CreateBullet1();
#if !WINDOWS_PHONE || SILVERLIGHT
            if (ShouldFire2(gameTime)) CreateBullet1();
#else
            if (ShouldFire2(gameTime)) CreateBullet1(float.NegativeInfinity);
#endif
            TryFire3();

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
            var touchState = TouchPanel.GetState();
            if (state.IsKeyDown(Keys.Space) || touchState.Any()) {
                if (gameTime.TotalGameTime - _lastFireTime > _fireDelay) {
                    _lastFireTime = gameTime.TotalGameTime;
                    return true;
                }
            }
            return false;
        }

        bool _keyPressed3 = false;
        float _fireDelay3 = 50;
        float _lastFireTime3;
        void TryFire3() {
            var state = Keyboard.GetState();
            var touchState = TouchPanel.GetState();
            if (state.IsKeyDown(Keys.Space) || touchState.Any()) {
                if (!_keyPressed3) {
                    _keyPressed3 = true;
                    if (_lastFireTime3 < realTime.Value - _fireDelay3) {
                        _lastFireTime3 = realTime.Value - _fireDelay3;
                    }
                }
                while (realTime.Value - _lastFireTime3 >= _fireDelay3) {
                    _lastFireTime3 += _fireDelay3;
                    CreateBullet2(_lastFireTime3);
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
#if !WINDOWS_PHONE || SILVERLIGHT
        public static DynamicSprite CreateStraitConstantVelocityBullet(Game game, Provider<float> time, Vector2 startPosition, Vector2 velocity, Texture2D texture, float startTime = float.NegativeInfinity) {
#else
        public static DynamicSprite CreateStraitConstantVelocityBullet(Game game, Provider<float> time, Vector2 startPosition, Vector2 velocity, Texture2D texture, float startTime) {
#endif
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

            public override Vector2 GetValue() {
                return _startPosition + _velocity * (_time.Value - _startTime);
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

            public override Vector2 GetValue() {
                return _position;
            }
        }
    }
}