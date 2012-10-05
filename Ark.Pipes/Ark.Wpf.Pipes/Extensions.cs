using System.Windows.Data;
using System.Windows.Controls;
using Ark.Geometry;
using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
#else
#error Bad geometry framework
#endif

namespace Ark.Geometry { //.Pipes.Wpf {
#if FRAMEWORK_WPF
    public static class Extensions_Wpf {
        public static Vector2 ToVector2(this Point vector) {
            return new Vector2((TFloat)vector.X, (TFloat)vector.Y);
        }

        public static Vector3 ToVector3(this Vector3D vector) {
            return new Vector3((TFloat)vector.X, (TFloat)vector.Y, (TFloat)vector.Z);
        }

        public static Provider<Vector2> ToVectors2(this Provider<Point> vectors) {
            return Provider.Create((v) => v.ToVector2(), vectors);
        }

        public static Provider<Vector3> ToVectors3(this Provider<Vector3D> vectors) {
            return Provider.Create((v) => v.ToVector3(), vectors);
        }

        public static Vector2Components ToVector2Components(this Provider<Point> vectors) {
            return vectors.ToVectors2().ToComponents();
        }

        public static Vector3Components ToVector3Component(this Provider<Vector3D> vectors) {
            return vectors.ToVectors3().ToComponents();
        }
    }
#endif
}

namespace Ark.Wpf { //.Pipes {
    public static class Extensions {
        public static void SetBinding<T>(this FrameworkElement element, DependencyProperty property, Provider<T> provider) {
            element.SetBinding(property, new Binding("Value") { Source = new NotifyPropertyChangedAdapter<T>(provider), Mode = BindingMode.OneWay });
        }

        public static void SetCanvasPosition(this FrameworkElement element, Provider<Vector2> position) {
            var components = position.ToComponents();

            element.SetBinding(Canvas.LeftProperty, new Binding("Value") { Source = new NotifyPropertyChangedAdapter<TFloat>(components.X), Mode = BindingMode.OneWay });
            element.SetBinding(Canvas.TopProperty, new Binding("Value") { Source = new NotifyPropertyChangedAdapter<TFloat>(components.Y), Mode = BindingMode.OneWay });
        }

        //TODO: Create a real provider
        public static Vector2 GetCanvasPosition(this FrameworkElement element) {
            return new Vector2((TFloat)element.GetValue(Canvas.LeftProperty), (TFloat)element.GetValue(Canvas.TopProperty));
        }
    }
}
