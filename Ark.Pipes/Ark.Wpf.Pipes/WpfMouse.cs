using System.Windows;
using System.Windows.Input;
using Ark.Pipes;

namespace Ark.Input { //.Pipes.Wpf {
    public class WpfMouse {
        IInputElement _canvas;
        Provider<Point> _position;
        Provider<bool> _leftButton;
        Provider<bool> _middleButton;
        Provider<bool> _rightButton;

        public WpfMouse(IInputElement canvas) {
            _canvas = canvas;
            _position = Provider.Create(() => Mouse.GetPosition(_canvas));
            _leftButton = Provider.Create(() => Mouse.LeftButton == MouseButtonState.Pressed);
            _middleButton = Provider.Create(() => Mouse.MiddleButton == MouseButtonState.Pressed);
            _rightButton = Provider.Create(() => Mouse.RightButton == MouseButtonState.Pressed);
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
