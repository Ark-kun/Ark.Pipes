using System.Collections.Generic;
using Ark.Borrowed.Net.Microsoft.Xna.Framework;

namespace Ark.Pipes.Physics {
    public class MaterialPoint {
        protected Variable<Vector3> _position;
        protected Variable<Vector3> _velocity;
        protected Variable<Vector3> _acceleraton;
        protected Constant<double> _mass;
        protected HashSet<Provider<Vector3>> _forces;

        public MaterialPoint(double mass, Vector3 position, Vector3 speed = new Vector3()) {
            _mass = new Constant<double>(mass);
            _position = new Variable<Vector3>(position);
            _velocity = new Variable<Vector3>(speed);
            _acceleraton = new Variable<Vector3>();
            _forces = new HashSet<Provider<Vector3>>();
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

        public Provider<double> Mass {
            get { return _mass; }
        }

        public HashSet<Provider<Vector3>> Forces {
            get { return _forces; }
        }
    }
}
