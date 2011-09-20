using Ark.Pipes.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ark.Pipes.Xna {
    public class XnaMouse : IMouse<Vector2> {
        static Provider<Vector2> _position = new Function<Vector2>(GetPosition);
        static Provider<bool> _leftButton = new Function<bool>(() => Mouse.GetState().LeftButton == ButtonState.Pressed);
        static Provider<bool> _middleButton = new Function<bool>(() => Mouse.GetState().MiddleButton == ButtonState.Pressed);
        static Provider<bool> _rightButton = new Function<bool>(() => Mouse.GetState().RightButton == ButtonState.Pressed);

        static Vector2 GetPosition() {
            var mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static Provider<Vector2> Position {
            get { return _position; }
        }

        public static Provider<bool> IsLeftButtonPressed {
            get { return _leftButton; }
        }

        public static Provider<bool> IsMiddleButtonPressed {
            get { return _middleButton; }
        }

        public static Provider<bool> IsRightButtonPressed {
            get { return _rightButton; }
        }


        Provider<Vector2> IMouse<Vector2>.Position {
            get { return _position; }
        }

        Provider<bool> IMouse<Vector2>.IsLeftButtonPressed {
            get { return _leftButton; }
        }

        Provider<bool> IMouse<Vector2>.IsMiddleButtonPressed {
            get { return _middleButton; }
        }

        Provider<bool> IMouse<Vector2>.IsRightButtonPressed {
            get { return _rightButton; }
        }
    }

    public class MousePosition : Provider<Vector2> {
        public override Vector2 GetValue() {
            var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }
    }
}