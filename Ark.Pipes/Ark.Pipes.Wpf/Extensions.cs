using System.Windows;
using System.Windows.Media.Media3D;

using Ark.Pipes.Animation;

#if FLOAT_GEOMETRY
using TType = System.Single;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector2;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector3;
using TQuaternion = Ark.Borrowed.Net.Microsoft.Xna.Framework.Quaternion;    
#else
using TType = System.Double;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector2;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector3;
using TQuaternion = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Quaternion;
using System.Windows.Data;
using System.Windows.Controls;
#endif

namespace Ark.Pipes.Wpf {
    public static class Extensions {
        public static TVector2 ToVector2(this Point vector) {
            return new TVector2(vector.X, vector.Y);
        }

        public static TVector3 ToVector3(this Vector3D vector) {
            return new TVector3(vector.X, vector.Y, vector.Z);
        }

        public static Provider<TVector2> ToVectors2(this Provider<Point> vectors) {
            return Provider<TVector2>.Create((v) => v.ToVector2(), vectors);
        }

        public static Provider<TVector3> ToVectors3(this Provider<Vector3D> vectors) {
            return Provider<TVector3>.Create((v) => v.ToVector3(), vectors);
        }

        public static Vector2Components ToVector2Components(this Provider<Point> point) {
            return new Vector2Components(Provider<double>.Create((p) => p.X, point), Provider<double>.Create((p) => p.Y, point));
        }

        public static Vector3Components ToVector3Component(this Provider<Vector3D> point) {
            return new Vector3Components(Provider<double>.Create((p) => p.X, point), Provider<double>.Create((p) => p.Y, point), Provider<double>.Create((p) => p.Z, point));
        }

        public static void SetBinding<T>(this FrameworkElement element, DependencyProperty property, Provider<T> provider) {
            element.SetBinding(property, new Binding("Value") { Source = new NotifyPropertyChangedAdapter<T>(provider), Mode = BindingMode.OneWay });
        }

        public static void SetCanvasPosition(this FrameworkElement element, Provider<TVector2> position) {
            var components = position.ToComponents();

            element.SetBinding(Canvas.LeftProperty, new Binding("Value") { Source = new NotifyPropertyChangedAdapter<double>(components.X), Mode = BindingMode.OneWay });
            element.SetBinding(Canvas.TopProperty, new Binding("Value") { Source = new NotifyPropertyChangedAdapter<double>(components.Y), Mode = BindingMode.OneWay });
        }

        //TODO: Create a real provider
        public static TVector2 GetCanvasPosition(this FrameworkElement element) {
            return new TVector2((double)element.GetValue(Canvas.LeftProperty), (double)element.GetValue(Canvas.TopProperty));
        }
    }
}
