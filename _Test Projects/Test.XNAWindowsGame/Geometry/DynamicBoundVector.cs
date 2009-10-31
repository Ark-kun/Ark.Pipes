using Microsoft.Xna.Framework;
using Ark.Pipes;

namespace Ark.XNA.Geometry {
    public class DynamicBoundVector : Provider<BoundVector> {
        public DynamicBoundVector() {
            StartPoint = Constant<Vector2>.Default;
            EndPoint = Constant<Vector2>.Default;
        }

        public Provider<Vector2> StartPoint { get; set; }
        public Provider<Vector2> EndPoint { get; set; }

        public override BoundVector Value {
            get {
                return new BoundVector(StartPoint, EndPoint);
            }
        }
    }    
}