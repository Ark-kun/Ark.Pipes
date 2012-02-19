using Microsoft.Xna.Framework;
using Ark.Pipes;

namespace Ark.XNA.Geometry {
    public class DynamicBoundVector : Provider<BoundVector> {
        public DynamicBoundVector() {
            StartPoint = Constant<Vector2>.Default;
            EndPoint = Constant<Vector2>.Default;
            _center = (Provider<Vector2>)(() => (StartPoint.Value + EndPoint.Value) / 2);
        }

        public Provider<Vector2> StartPoint { get; set; }
        public Provider<Vector2> EndPoint { get; set; }

        public override BoundVector GetValue() {
            return new BoundVector(StartPoint, EndPoint);
        }

        private Provider<Vector2> _center;
        public Provider<Vector2> Center {
            get {
                return _center;
            }
        }
    }
}