using System;
using Ark.Pipes;
using Ark.Input.Pipes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ark.Input.Pipes.Xna {
    public sealed class XnaMouse : IMouse<Vector2> {
        static Provider<Vector2> _staticPosition = Provider<Vector2>.Create(GetPosition);
        static Provider<bool> _staticLeftButton = Provider<bool>.Create(() => Mouse.GetState().LeftButton == ButtonState.Pressed);
        static Provider<bool> _staticMiddleButton = Provider<bool>.Create(() => Mouse.GetState().MiddleButton == ButtonState.Pressed);
        static Provider<bool> _staticRightButton = Provider<bool>.Create(() => Mouse.GetState().RightButton == ButtonState.Pressed);
        static XnaMouse _default = new XnaMouse();

        Provider<Vector2> _position;
        Provider<bool> _leftButton;
        Provider<bool> _middleButton;
        Provider<bool> _rightButton;

        XnaMouse() {
            _position = _staticPosition;
            _leftButton = _staticLeftButton;
            _middleButton = _staticMiddleButton;
            _rightButton = _staticRightButton;
        }

        public XnaMouse(Action<Action> trigger) {
            _position = Provider<Vector2>.Create(GetPosition, trigger);
            _leftButton = Provider<bool>.Create(() => Mouse.GetState().LeftButton == ButtonState.Pressed, trigger);
            _middleButton = Provider<bool>.Create(() => Mouse.GetState().MiddleButton == ButtonState.Pressed, trigger);
            _rightButton = Provider<bool>.Create(() => Mouse.GetState().RightButton == ButtonState.Pressed, trigger);
        }

        static Vector2 GetPosition() {
            var mouseState = Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }

        public static XnaMouse Default {
            get { return _default; }
        }

        public Provider<Vector2> Position {
            get { return _position; }
        }

        public Provider<bool> IsLeftButtonPressed {
            get { return _leftButton; }
        }

        public Provider<bool> IsMiddleButtonPressed {
            get { return _middleButton; }
        }

        public Provider<bool> IsRightButtonPressed {
            get { return _rightButton; }
        }
    }

    public class XNAMousePosition : Provider<Vector2> {
        public override Vector2 GetValue() {
            var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }
    }
}