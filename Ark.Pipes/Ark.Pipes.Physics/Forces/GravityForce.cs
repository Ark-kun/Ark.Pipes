using System;
using Ark.Pipes.Animation;

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

namespace Ark.Pipes.Physics.Forces {
    public class GravityForce : TwoBodyForce {
        public const TFloat G = (TFloat)6.6738480E-11; ///kg^-1 * m^3 * s^-2
        public GravityForce(MaterialPoint obj1, MaterialPoint obj2)
            : base(obj1, obj2) {
        }

        protected override TFloat GetMagnitude() {
            Vector3 r = _obj2.Position.Value - _obj1.Position.Value;
            return G * _obj1.Mass * _obj2.Mass / r.LengthSquared();
        }
    }
}
