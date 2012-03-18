using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
using Matrix3 = Ark.Geometry.Primitives.Double.Matrix;
using StaticVector2 = Ark.Geometry.Primitives.Double.Vector2;
using StaticVector3 = Ark.Geometry.Primitives.Double.Vector3;
using StaticMatrix = Ark.Geometry.Primitives.Double.Matrix;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
using Matrix3 = Ark.Geometry.Primitives.Single.Matrix;
using StaticVector2 = Ark.Geometry.Primitives.Single.Vector2;
using StaticVector3 = Ark.Geometry.Primitives.Single.Vector3;
using StaticMatrix = Ark.Geometry.Primitives.Single.Matrix;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
using Matrix3 = Microsoft.Xna.Framework.Matrix;
using StaticVector2 = Microsoft.Xna.Framework.Vector2;
using StaticVector3 = Microsoft.Xna.Framework.Vector3;
using StaticMatrix = Microsoft.Xna.Framework.Matrix;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
using Matrix3 = System.Windows.Media.Media3D.Matrix3D;
using StaticVector2 = Ark.Geometry.Primitives.XamlVector2;
using StaticVector3 = Ark.Geometry.Primitives.XamlVector3;
using StaticMatrix = Ark.Geometry.Primitives.XamlMatrix;
#else
#error Bad geometry framework
#endif

namespace Ark.Geometry { //.Pipes
    public static class DynamicMatrix {
        public static Provider<Matrix3> CreateFromAxisAngle(Vector3 axis, Provider<TFloat> angles) {
            return Provider.Create((angle) => StaticMatrix.CreateFromAxisAngle(axis, angle), angles);
        }

        public static Provider<Matrix3> CreateFromAxisAngle(Provider<Vector3> axes, Provider<TFloat> angles) {
            return Provider.Create((axis, angle) => StaticMatrix.CreateFromAxisAngle(axis, angle), axes, angles);
        }

        public static Provider<Matrix3> CreateFromQuaternion(Provider<Quaternion> quaternions) {
            return Provider.Create((quaternion) => StaticMatrix.CreateFromQuaternion(quaternion), quaternions);
        }

        public static Provider<Matrix3> CreateRotationX(Provider<TFloat> angles) {
            return Provider.Create((angle) => StaticMatrix.CreateRotationX(angle), angles);
        }

        public static Provider<Matrix3> CreateRotationY(Provider<TFloat> angles) {
            return Provider.Create((angle) => StaticMatrix.CreateRotationY(angle), angles);
        }

        public static Provider<Matrix3> CreateRotationZ(Provider<TFloat> angles) {
            return Provider.Create((angle) => StaticMatrix.CreateRotationZ(angle), angles);
        }
    }
}