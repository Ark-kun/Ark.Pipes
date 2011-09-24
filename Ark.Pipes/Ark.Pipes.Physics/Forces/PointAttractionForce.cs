using Ark.Borrowed.Net.Microsoft.Xna.Framework;

namespace Ark.Pipes.Physics.Forces {
    public class PointAttractionForce : AmbientForce {
        Property<Vector3> _attractionPoint;
        double _coeff;

        public PointAttractionForce(Provider<Vector3> attractionPoint, double coeff = 1.0) {
            _attractionPoint = new Property<Vector3>(attractionPoint);
            _coeff = coeff;
        }

        public override Vector3 CalculateForceOnObject(MaterialPoint obj) {
            Vector3 p = _attractionPoint.Value;
            Vector3 o = obj.Position.Value;
            Vector3 v = obj.Velocity.Value;
            Vector3 delta = p - o;
            //Vector3 delta = new Vector3(100,0,100) - o;
            Vector3 res = Vector3.Zero;
            //return _coeff * delta * delta.Length;
            //return _coeff * delta;
            //return _coeff * delta / delta.Length;

            //4
            //res = _coeff * delta;            
            //double dot = delta.X * v.X + delta.Y * v.Y;
            //if (delta != Vector3.Zero && v != Vector3.Zero && dot < 0) {
            //    res += 5 * dot / (delta.Length * v.Length) * v;
            //}

            //5
            res = _coeff * (delta - v) * obj.Mass;
            //res = (delta - v);
            //res = delta / (delta.Length * 0.001) - v / Math.Pow(delta.Length * 0.001, 2);

            //6 cool bug
            //double tmp = 0.001 * delta.Length;
            //if (tmp != 0) {
            //    res = (delta / tmp - v / (tmp * tmp)) * obj.Mass;
            //}

            //7 cool bug 1.1
            //double tmp = 0.01 * delta.Length;
            //if (tmp != 0) {
            //    res = (delta / tmp - v / (tmp * tmp)) * obj.Mass;
            //}
            return res;
        }

        Property<Vector3> AttractionPoint {
            get { return _attractionPoint; }
            set { _attractionPoint.Provider = value.Provider; }
        }
    }
}
