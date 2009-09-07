using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ark.XNA.Components {
    public class KeyboardControlledObject : GameComponent {
        Game _game;
        IHasChangeablePosition _object;
        float _speed;

        public KeyboardControlledObject(Game game, IHasChangeablePosition obj, float speed)
            : base(game) {
            _game = game;
            _object = obj;
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
                _object.Position += delta;
            }
        }
    }
}