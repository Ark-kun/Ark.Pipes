using System.Windows;
using System.Windows.Input;
using Ark.Pipes;

namespace Ark.Input { //.Pipes.Wpf {
    public class WpfMouse : IMouse<Point> {
        IInputElement _canvas;
        Provider<Point> _position;
        Provider<bool> _leftButton;
        Provider<bool> _middleButton;
        Provider<bool> _rightButton;

        public WpfMouse(IInputElement canvas) {
            _canvas = canvas;
            _position = new Function<Point>(() => Mouse.GetPosition(_canvas));
            _leftButton = new Function<bool>(() => Mouse.LeftButton == MouseButtonState.Pressed);
            _middleButton = new Function<bool>(() => Mouse.MiddleButton == MouseButtonState.Pressed);
            _rightButton = new Function<bool>(() => Mouse.RightButton == MouseButtonState.Pressed);
        }

        public Provider<Point> Position {
            get { return _position; }
        }

        public Provider<bool> IsLeftButtonPressed {
            get { return _leftButton; }
        }

        public Provider<bool> IsMiddleButtonPressed {
            get { return _leftButton; }
        }

        public Provider<bool> IsRightButtonPressed {
            get { return _rightButton; }
        }
    }   
}
