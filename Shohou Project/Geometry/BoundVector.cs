using Microsoft.Xna.Framework;

namespace Ark.Xna.Geometry {
    public struct BoundVector {
        public BoundVector(Vector2 startPoint, Vector2 endPoint) {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        Vector2 StartPoint;
        Vector2 EndPoint;
    }
}