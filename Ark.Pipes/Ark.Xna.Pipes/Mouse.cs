using System;
using Ark.Pipes;
using Ark.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ark.Input { //.Pipes.Xna {
    public sealed class XnaMouse {
        bool _isDirty = true;
        MouseState _cachedValue;
        ITrigger _invalidationTrigger;

        Provider<Vector2> _position;
        Provider<bool> _leftButton;
        Provider<bool> _middleButton;
        Provider<bool> _rightButton;
        Provider<int> _scrollWheel;

        public XnaMouse(ITrigger trigger) {
            InvalidationTrigger = trigger;
            _position = Provider.Create(() => { Refresh(); return new Vector2(_cachedValue.X, _cachedValue.Y); });
            _leftButton = Provider.Create(() => { Refresh(); return _cachedValue.LeftButton == ButtonState.Pressed; });
            _middleButton = Provider.Create(() => { Refresh(); return _cachedValue.MiddleButton == ButtonState.Pressed; });
            _rightButton = Provider.Create(() => { Refresh(); return _cachedValue.RightButton == ButtonState.Pressed; });
            _scrollWheel = Provider.Create(() => { Refresh(); return _cachedValue.ScrollWheelValue; });
        }

        void Refresh() {
            if (_isDirty) {
                _cachedValue = Mouse.GetState();
                _isDirty = false;
            }
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

        public Provider<int> ScrollWheel {
            get { return _scrollWheel; }
        }

        void Invalidate() {
            _isDirty = true;
        }

        public ITrigger InvalidationTrigger {
            get { return _invalidationTrigger; }
            set {
                if (_invalidationTrigger != null) {
                    _invalidationTrigger.Triggered -= Invalidate;
                }
                _invalidationTrigger = value;
                if (_invalidationTrigger != null) {
                    _invalidationTrigger.Triggered += Invalidate;
                }
            }
        }
    }

    public class XNAMousePosition : Provider<Vector2> {
        public override Vector2 GetValue() {
            var mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();
            return new Vector2(mouseState.X, mouseState.Y);
        }
    }
}