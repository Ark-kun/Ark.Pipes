using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
using StaticVector3 = Ark.Geometry.Primitives.Double.Vector3;
using StaticQuaternion = Ark.Geometry.Primitives.Double.Quaternion;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
using StaticVector3 = Ark.Geometry.Primitives.Single.Vector3;
using StaticQuaternion = Ark.Geometry.Primitives.Single.Quaternion;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
using StaticVector3 = Microsoft.Xna.Framework.Vector3;
using StaticQuaternion = Microsoft.Xna.Framework.Quaternion;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows.Media.Media3D;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
using StaticVector3 = Ark.Geometry.Primitives.XamlVector3;
using StaticQuaternion = Ark.Geometry.Primitives.XamlQuaternion;
#else
#error Bad geometry framework
#endif

namespace Ark.Animation { //.Pipes
    public static class DynamicQuaternion {
        public static Provider<Quaternion> CreateFromAxisAngle(Vector3 axis, Provider<TFloat> angles) {
            return Provider.Create(( angle) => StaticQuaternion.CreateFromAxisAngle(axis, angle), angles);
        }

        public static Provider<Quaternion> CreateFromAxisAngle(Provider<Vector3> axes, Provider<TFloat> angles) { 
            return Provider.Create((axis, angle) => StaticQuaternion.CreateFromAxisAngle(axis, angle), axes, angles);
        }
    }
}