using System.Collections.Generic;
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

namespace Ark.Physics { //.Pipes {
    public class MaterialPoint {
        protected Variable<Vector3> _position;
        protected Variable<Vector3> _velocity;
        protected Variable<Vector3> _acceleraton;
        protected Constant<TFloat> _mass;
        protected List<Provider<Vector3>> _forces;

        public MaterialPoint(TFloat mass, Vector3 position, Vector3 speed = new Vector3()) {
            _mass = new Constant<TFloat>(mass);
            _position = new Variable<Vector3>(position);
            _velocity = new Variable<Vector3>(speed);
            _acceleraton = new Variable<Vector3>();
            _forces = new List<Provider<Vector3>>();
        }

        public Provider<Vector3> Position {
            get { return _position; }
        }

        public Provider<Vector3> Velocity {
            get { return _velocity; }
        }

        public Provider<Vector3> Acceleration {
            get { return _acceleraton; }
        }

        public Provider<TFloat> Mass {
            get { return _mass; }
        }

        public List<Provider<Vector3>> Forces {
            get { return _forces; }
        }
    }
}
