using Microsoft.Xna.Framework;
using System;

namespace Ark.Pipes.Xna {
    public static class Mouse {
        static Provider<Vector2> _p = new Function<Vector2>(GetPosition);

        static Vector2 GetPosition() {
            var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static Provider<Vector2> Position {
            get {
                return _p;
            }
        }
    }

    public class MousePosition : Provider<Vector2> {
        public override Vector2 GetValue() {
            var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }
    }
}