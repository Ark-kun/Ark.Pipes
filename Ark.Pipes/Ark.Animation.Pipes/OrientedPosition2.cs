using Ark.Abstract;
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
    public struct OrientedPosition2 {
        public Vector2 Position;
        public TFloat Orientation;

        public OrientedPosition2(Vector2 position, TFloat orientation) {
            Position = position;
            Orientation = orientation;
        }

        public static OrientedPosition2 operator *(OrientedPosition2 op, TFloat dt) {
            return new OrientedPosition2(op.Position * dt, op.Orientation * dt);
        }

        public static OrientedPosition2 operator +(OrientedPosition2 op1, OrientedPosition2 op2) {
            return new OrientedPosition2(op1.Position + op2.Position, op1.Orientation + op2.Orientation);
        }

        public static OrientedPosition2 operator -(OrientedPosition2 op1, OrientedPosition2 op2) {
            return new OrientedPosition2(op1.Position - op2.Position, op1.Orientation - op2.Orientation);
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
            return Provider<OrientedPosition2>.Create((p, o) => new OrientedPosition2(p, o), Position, Orientation);
        }
    }
}