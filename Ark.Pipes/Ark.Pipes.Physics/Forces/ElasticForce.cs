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
    public class ElasticForce : TwoBodyForce {
        TFloat _springConstant;
        TFloat _length;

        public ElasticForce(TFloat elasticity, MaterialPoint obj1, MaterialPoint obj2)
            : this(elasticity, obj1, obj2, (obj1.Position.Value - obj2.Position.Value).Length()) {
        }

        public ElasticForce(TFloat springConstant, MaterialPoint obj1, MaterialPoint obj2, TFloat length)
            : base(obj1, obj2) {
            _springConstant = springConstant;
            _length = length;
        }

        protected override TFloat GetMagnitude() {
            return _springConstant * ((_obj1.Position.Value - _obj2.Position.Value).Length() - _length);
        }
    }
}
