using Ark.Borrowed.Net.Microsoft.Xna.Framework._Double;

namespace Ark.Pipes.Physics.Forces {
    public abstract class TwoBodyForce : Provider<double> {
        protected MaterialPoint _obj1;
        protected MaterialPoint _obj2;
        Provider<Vector3> _forceOnObj1;
        Provider<Vector3> _forceOnObj2;

        public TwoBodyForce(MaterialPoint obj1, MaterialPoint obj2) {
            _obj1 = obj1;
            _obj2 = obj2;
            _forceOnObj1 = new Function<Vector3, Vector3, Vector3>((p1, p2) => {
                Vector3 _delta = p2 - p1;
                if (_delta == Vector3.Zero) {
                    return new Vector3();
                }
                _delta.Normalize();
                return GetMagnitude() * _delta;
            }, _obj1.Position, _obj2.Position);
            _forceOnObj2 = new Function<Vector3, Vector3, Vector3>((p1, p2) => {
                Vector3 _delta = p1 - p2;
                if (_delta == Vector3.Zero) {
                    return new Vector3();
                }
                _delta.Normalize();
                return GetMagnitude() * _delta;
            }, _obj1.Position, _obj2.Position);
        }

        //positive = attraction, negative - detraction
        protected abstract double GetMagnitude();

        public override double GetValue() {
            return GetMagnitude();
        }

        public Provider<Vector3> ForceOnObject1 {
            get { return _forceOnObj1; }
        }

        public Provider<Vector3> ForceOnObject2 {
            get { return _forceOnObj2; }
        }

        public double Magnitude {
            get {
                return GetMagnitude();
            }
        }
    }
}
