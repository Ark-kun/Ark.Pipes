using Ark.Animation;
using Ark.Geometry;
using Ark.Physics.Pipes;
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

namespace Ark.Physics.Forces.Pipes {
    public abstract class TwoBodyForce : Provider<TFloat> {
        protected MaterialPoint _obj1;
        protected MaterialPoint _obj2;
        Provider<Vector3> _forceOnObj1;
        Provider<Vector3> _forceOnObj2;

        public TwoBodyForce(MaterialPoint obj1, MaterialPoint obj2) {
            _obj1 = obj1;
            _obj2 = obj2;
            _forceOnObj1 = new Function<Vector3, Vector3, Vector3>((p1, p2) => {
                Vector3 _delta = p2 - p1;
                if (_delta.IsZero()) {
                    return new Vector3();
                }
                _delta.Normalize();
                return GetMagnitude() * _delta;
            }, _obj1.Position, _obj2.Position);
            _forceOnObj2 = new Function<Vector3, Vector3, Vector3>((p1, p2) => {
                Vector3 _delta = p1 - p2;
                if (_delta.IsZero()) {
                    return new Vector3();
                }
                _delta.Normalize();
                return GetMagnitude() * _delta;
            }, _obj1.Position, _obj2.Position);
        }

        //positive = attraction, negative - detraction
        protected abstract TFloat GetMagnitude();

        public override TFloat GetValue() {
            return GetMagnitude();
        }

        public Provider<Vector3> ForceOnObject1 {
            get { return _forceOnObj1; }
        }

        public Provider<Vector3> ForceOnObject2 {
            get { return _forceOnObj2; }
        }

        public TFloat Magnitude {
            get {
                return GetMagnitude();
            }
        }
    }
}
