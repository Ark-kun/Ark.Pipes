using Ark.Pipes;

namespace Ark.Animation { //.Pipes {
    //using OrientedPosition2ComponentsEx = OrientedPositionComponents<TVector2, T, OrientedPosition2>;
    //using OrientedPosition3ComponentsEx = OrientedPositionComponents<TVector3, TQuaternion, OrientedPosition3>;
    public class OrientedPositionComponents<TPosition, TOrientation, TOrientedPosition> where TOrientedPosition : IHasChangeablePosition<TPosition>, IHasChangeableOrientation<TOrientation>, new() {
        public Provider<TPosition> Position;
        public Provider<TOrientation> Orientation;

        public OrientedPositionComponents() : this(Constant<TPosition>.Default, Constant<TOrientation>.Default) { }

        public OrientedPositionComponents(Provider<TPosition> position, Provider<TOrientation> orientation) {
            Position = position;
            Orientation = orientation;
        }

        //public static OrientedPositionComponents<TPosition, TOrientation, TOrientedPosition> FromOrientedPositions<T>(T orientedPositions) where T : Provider<TOrientedPosition> {
        public static OrientedPositionComponents<TPosition, TOrientation, TOrientedPosition> FromOrientedPositions(Provider<TOrientedPosition> orientedPositions) {
            return new OrientedPositionComponents<TPosition, TOrientation, TOrientedPosition>() {
                Position = Provider<TPosition>.Create((op) => op.Position, orientedPositions),
                Orientation = Provider<TOrientation>.Create((op) => op.Orientation, orientedPositions)
            };
        }

        public Provider<TOrientedPosition> ToOrientedPositions() {
            return Provider<TOrientedPosition>.Create((p, o) => new TOrientedPosition() { Position = p, Orientation = o }, Position, Orientation);
        }
    }
}
