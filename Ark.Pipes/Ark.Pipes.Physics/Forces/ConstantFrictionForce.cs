
using Ark.Borrowed.Net.Microsoft.Xna.Framework;
namespace Ark.Pipes.Physics.Forces {
    public class ConstantFrictionForce : AmbientForce {
        double _coeff;

        public ConstantFrictionForce(double coeff) {
            _coeff = coeff;
        }

        public override Vector3 CalculateForceOnObject(MaterialPoint obj) {
            Vector3 res = obj.Velocity.Value;
            if (res.LengthSquared > 0) {
                res.Normalize();
                res *= -_coeff;
            }
            return res; ;
        }
    }
}
