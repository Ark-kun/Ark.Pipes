using Ark.Geometry;
using Ark.Geometry.Curves;
using Microsoft.Xna.Framework;

namespace Ark.Geometry.Curves {
    public interface ICurve<TVector> {
        TVector Evaluate(float param);
    }
}

namespace Ark.Geometry.Curves { //.Xna {
    public interface ICurve2D : ICurve<Vector2> {
    }

    public class LineCurve : ICurve2D {
        Vector2 _origin;
        Vector2 _direction;

        public LineCurve(Vector2 origin, Vector2 direction) {
            _origin = origin;
            _direction = direction;
            if (_direction != Vector2.Zero) {
                _direction.Normalize();
            }
        }

        public Vector2 Evaluate(float param) {
            return _origin + _direction * param;
        }
    }
}

namespace Ark.Geometry.Curves { //.Pipes.Xna {
    public class LineSectionCurve : ICurve2D {
        DynamicBoundVector _boundVector;

        public LineSectionCurve(DynamicBoundVector boundVector) {
            _boundVector = boundVector;
        }

        public Vector2 Evaluate(float param) {
            var startPoint = _boundVector.StartPoint;
            return startPoint + (_boundVector.EndPoint.Value - startPoint) * param;
        }
    }
}
