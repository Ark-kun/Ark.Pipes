using Ark.Pipes;

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

namespace Ark.Animation { //.Pipes {
    //using OrientedPosition3ComponentsEx = OrientedPositionComponents<TVector3, TQuaternion, OrientedPosition3>;

    public struct OrientedPosition3 : IHasChangeablePosition<Vector3>, IHasChangeableOrientation<Quaternion> {
        public Vector3 Position;
        public Quaternion Orientation;

        public OrientedPosition3(Vector3 position, Quaternion orientation) {
            Position = position;
            Orientation = orientation;
        }

        Vector3 IHasChangeablePosition<Vector3>.Position {
            get { return Position; }
            set { Position = value; }
        }

        Quaternion IHasChangeableOrientation<Quaternion>.Orientation {
            get { return Orientation; }
            set { Orientation = value; }
        }
    }

    public sealed class OrientedPosition3Components {
        public Provider<Vector3> Position;
        public Provider<Quaternion> Orientation;

        public OrientedPosition3Components() : this(Constant<Vector3>.Default, Constant<Quaternion>.Default) { }

        public OrientedPosition3Components(Provider<Vector3> position, Provider<Quaternion> orientation) {
            Position = position;
            Orientation = orientation;
        }

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
