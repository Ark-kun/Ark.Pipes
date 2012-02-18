
#if FLOAT_GEOMETRY
using TType = System.Single;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector2;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector3;
using TQuaternion = Ark.Borrowed.Net.Microsoft.Xna.Framework.Quaternion;    
#else
using TType = System.Double;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector2;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector3;
using TQuaternion = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Quaternion;
#endif

namespace Ark.Pipes.Animation {
    //using OrientedPosition3ComponentsEx = OrientedPositionComponents<TVector3, TQuaternion, OrientedPosition3>;

    public struct OrientedPosition3 : IHasPosition<TVector3>, IHasOrientation<TQuaternion> {
        public TVector3 Position;
        public TQuaternion Orientation;

        TVector3 IHasPosition<TVector3>.Position {
            get { return Position; }
            set { Position = value; }
        }

        TQuaternion IHasOrientation<TQuaternion>.Orientation {
            get { return Orientation; }
            set { Orientation = value; }
        }
    }

    public sealed class OrientedPosition3Components {
        public Provider<TVector3> Position;
        public Provider<TQuaternion> Orientation;

        public static OrientedPosition3Components FromOrientedPositions3(Provider<OrientedPosition3> orientedPositions) {
            return new OrientedPosition3Components() {
                Position = Provider<TVector3>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<TQuaternion>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<OrientedPosition3> ToOrientedPositions3() {
            return Provider<OrientedPosition3>.Create((p, o) => new OrientedPosition3() { Position = p, Orientation = o }, Position, Orientation);
        }
    }
}
