using Ark.Geometry;
using Ark.Physics.Pipes;

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

namespace Ark.Physics.Forces.Pipes {
    public class ConstantFrictionForce : AmbientForce {
        TFloat _coeff;

        public ConstantFrictionForce(TFloat coeff) {
            _coeff = coeff;
        }

        public override Vector3 CalculateForceOnObject(MaterialPoint obj) {
            Vector3 res = obj.Velocity.Value;
            if (res.LengthSquared() > 0) {
                res.Normalize();
                res *= -_coeff;
            }
            return res; ;
        }
    }
}
