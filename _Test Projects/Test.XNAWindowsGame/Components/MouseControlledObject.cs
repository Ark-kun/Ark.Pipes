using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ark.XNA.Components {
    public class MouseControlledObject : GameComponent {
        IHasChangeablePosition _object;

        public MouseControlledObject(Game game, IHasChangeablePosition obj)
            : base(game) {
            _object = obj;
        }
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            var mouseState = Mouse.GetState();
            _object.Position = new Vector2(mouseState.X, mouseState.Y);
        }
    }
}