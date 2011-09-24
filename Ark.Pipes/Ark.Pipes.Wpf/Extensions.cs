using System.Windows;
using System.Windows.Media.Media3D;

namespace Ark.Pipes.Wpf {
    static class Extensions {

        public static Vector2Components ToVector2Components(this Provider<Point> point) {
            return new Vector2Components(new Function<double>(() => point.Value.X), new Function<double>(() => point.Value.Y));
        }

        public static Vector3Components ToVector2Component(Provider<Vector3D> point) {
            return new Vector3Components(new Function<double>(() => point.Value.X), new Function<double>(() => point.Value.Y), new Function<double>(() => point.Value.Z));
        }
    }
}
