using Ark.Borrowed.Net.Microsoft.Xna.Framework;

namespace Ark.Pipes.Physics.Forces {
    public abstract class AmbientForce {
        public Provider<Vector3> GetForceOnObject(MaterialPoint obj) {
            return new Function<Vector3>(() => CalculateForceOnObject(obj));
        }
        public abstract Vector3 CalculateForceOnObject(MaterialPoint obj);
    }
}
