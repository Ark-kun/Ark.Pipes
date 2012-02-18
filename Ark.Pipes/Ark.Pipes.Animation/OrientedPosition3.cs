#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
#else
#error Bad geometry framework
#endif

namespace Ark.Pipes.Animation {
    //using OrientedPosition3ComponentsEx = OrientedPositionComponents<TVector3, TQuaternion, OrientedPosition3>;

    public struct OrientedPosition3 : IHasPosition<Vector3>, IHasOrientation<Quaternion> {
        public Vector3 Position;
        public Quaternion Orientation;

        Vector3 IHasPosition<Vector3>.Position {
            get { return Position; }
            set { Position = value; }
        }

        Quaternion IHasOrientation<Quaternion>.Orientation {
            get { return Orientation; }
            set { Orientation = value; }
        }
    }

    public sealed class OrientedPosition3Components {
        public Provider<Vector3> Position;
        public Provider<Quaternion> Orientation;

        public static OrientedPosition3Components FromOrientedPositions3(Provider<OrientedPosition3> orientedPositions) {
            return new OrientedPosition3Components() {
                Position = Provider<Vector3>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<Quaternion>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<OrientedPosition3> ToOrientedPositions3() {
            return Provider<OrientedPosition3>.Create((p, o) => new OrientedPosition3() { Position = p, Orientation = o }, Position, Orientation);
        }
    }
}
