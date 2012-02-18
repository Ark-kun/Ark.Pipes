using Ark.Borrowed.Net.Microsoft.Xna.Framework._Double;

namespace Ark.Pipes.Physics.Forces {
    public class ViscousFrictionForce : AmbientForce {
        double _coeff;

        public ViscousFrictionForce(double coeff) {
            _coeff = coeff;
        }

        public override Vector3 CalculateForceOnObject(MaterialPoint obj) {
            return -_coeff * obj.Velocity.Value;
        }
    }
}
