using System;
using Ark.Geometry.Curves;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Animation { //.Pipes.Xna {
    public class CurveMovement {
        ICurve2D _curve;

        public CurveMovement(ICurve2D curve) {
            _position = new Func<Vector2>(GetPosition);
            _curve = curve;
            Time = Constant<float>.Default;
        }

        Vector2 GetPosition() {
            return _curve.Evaluate(Time.Value);
        }

        Provider<Vector2> _position;
        public Provider<Vector2> Position {
            get {
                return _position;
            }
        }

        public Provider<float> Time { get; set; }
    }
}
