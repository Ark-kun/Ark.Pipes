using Ark.Borrowed.Net.Microsoft.Xna.Framework;

namespace Ark.Pipes.Physics.Forces {
    public class EarthGravityForce : AmbientForce {
        const double g = 9.80665;
        public override Vector3 CalculateForceOnObject(MaterialPoint obj) {
            return new Vector3(0, 0, -obj.Mass * g);
        }
    }
}
