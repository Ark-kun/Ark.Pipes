using Ark.Pipes;
using Ark.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ark.Input { //.Pipes.Xna {
    public class KeyboardControlledPosition : GameComponent {
        Game _game;
        float _speed;
        Variable<Vector2> _position = new Variable<Vector2>();

        public KeyboardControlledPosition(Game game, float speed)
            : base(game) {
            _game = game;
            _speed = speed;
        }
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 delta = new Vector2();

            if (keyboardState.IsKeyDown(Keys.Left) ||
                keyboardState.IsKeyDown(Keys.A)) {
                delta.X -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.Right) ||
                keyboardState.IsKeyDown(Keys.D)) {
                delta.X += 1;
            }
            if (keyboardState.IsKeyDown(Keys.Up) ||
                keyboardState.IsKeyDown(Keys.W)) {
                delta.Y -= 1;
            }
            if (keyboardState.IsKeyDown(Keys.Down) ||
                keyboardState.IsKeyDown(Keys.S)) {
                delta.Y += 1;
            }
            if (delta.LengthSquared() > 0) {
                delta.Normalize();
                delta *= (float)(_speed * gameTime.ElapsedGameTime.TotalSeconds);
                _position.Value += delta;
            }
        }

        public Provider<Vector2> Position {
            get { return _position; }
        }
    }
}