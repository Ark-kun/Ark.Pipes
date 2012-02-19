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

namespace Ark.Animation.Pipes {
    //using OrientedPosition2ComponentsEx = OrientedPositionComponents<TVector2, T, OrientedPosition2>;

    public struct OrientedPosition2 : IHasPosition<Vector2>, IHasOrientation<TFloat> {
        public Vector2 Position;
        public TFloat Orientation;

        Vector2 IHasPosition<Vector2>.Position {
            get { return Position; }
            set { Position = value; }
        }

        TFloat IHasOrientation<TFloat>.Orientation {
            get { return Orientation; }
            set { Orientation = value; }
        }
    }

    public sealed class OrientedPosition2Components {
        public Provider<Vector2> Position;
        public Provider<TFloat> Orientation;

        public OrientedPosition2Components() : this(Constant<Vector2>.Default, Constant<TFloat>.Default) { }

        public OrientedPosition2Components(Provider<Vector2> position, Provider<TFloat> orientation) {
            Position = position;
            Orientation = orientation;
        }

        public static OrientedPosition2Components FromOrientedPositions2(Provider<OrientedPosition2> orientedPositions) {
            return new OrientedPosition2Components() {
                Position = Provider<Vector2>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<TFloat>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<OrientedPosition2> ToOrientedPositions2() {
            return Provider<OrientedPosition2>.Create((p, o) => new OrientedPosition2() { Position = p, Orientation = o }, Position, Orientation);
        }
    }
}