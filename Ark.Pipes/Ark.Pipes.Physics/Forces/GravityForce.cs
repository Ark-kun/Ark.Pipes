using System;
using Ark.Borrowed.Net.Microsoft.Xna.Framework;

namespace Ark.Pipes.Physics.Forces {
    public class GravityForce : TwoBodyForce {
        public const double G = 6.6738480E-11; ///kg^-1 * m^3 * s^-2
        public GravityForce(MaterialPoint obj1, MaterialPoint obj2)
            : base(obj1, obj2) {
        }

        protected override double GetMagnitude() {
            Vector3 r = _obj2.Position.Value - _obj1.Position.Value;
            return G * _obj1.Mass * _obj2.Mass / r.LengthSquared;
        }
    }
}
