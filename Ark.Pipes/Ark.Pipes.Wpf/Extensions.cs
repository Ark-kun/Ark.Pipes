using System.Windows;
using System.Windows.Media.Media3D;

namespace Ark.Pipes.Wpf {
    static class Extensions {

        public static Vector2dComponents ToVector2dComponents(this Provider<Point> point) {
            return new Vector2dComponents(new Function<double>(() => point.Value.X), new Function<double>(() => point.Value.Y));
        }

        public static Vector3dComponents ToVector2dComponent(Provider<Vector3D> point) {
            return new Vector3dComponents(new Function<double>(() => point.Value.X), new Function<double>(() => point.Value.Y), new Function<double>(() => point.Value.Z));
        }
    }
}
