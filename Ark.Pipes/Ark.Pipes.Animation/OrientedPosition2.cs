
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
    //using OrientedPosition2ComponentsEx = OrientedPositionComponents<TVector2, T, OrientedPosition2>;

    public struct OrientedPosition2 : IHasPosition<TVector2>, IHasOrientation<TType> {
        public TVector2 Position;
        public TType Orientation;

        TVector2 IHasPosition<TVector2>.Position {
            get { return Position; }
            set { Position = value; }
        }

        TType IHasOrientation<TType>.Orientation {
            get { return Orientation; }
            set { Orientation = value; }
        }
    }

    public sealed class OrientedPosition2Components {
        public Provider<TVector2> Position;
        public Provider<TType> Orientation;

        public static OrientedPosition2Components FromOrientedPositions2(Provider<OrientedPosition2> orientedPositions) {
            return new OrientedPosition2Components() {
                Position = Provider<TVector2>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<TType>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<OrientedPosition2> ToOrientedPositions2() {
            return Provider<OrientedPosition2>.Create((p, o) => new OrientedPosition2() { Position = p, Orientation = o }, Position, Orientation);
        }
    }
}