using System;
using Ark.Geometry.Curves.Xna;
using Ark.Geometry.Pipes.Xna;
using Ark.Geometry.Xna;
using Microsoft.Xna.Framework;

namespace Ark.Geometry.Curves.Pipes.Xna {
    public class LemniscateOfBernoulliCurve : ICurve2D {
        DynamicBoundVector _boundVector;

        public LemniscateOfBernoulliCurve(DynamicBoundVector boundVector) {
            _boundVector = boundVector;
        }

        public Vector2 Evaluate(float param) {
            Vector2 r1 = _boundVector.StartPoint;
            //Vector2 r1 = Vector2.Zero;
            //Vector2 r1 = new Vector2(400, 400);
            Vector2 r2 = _boundVector.EndPoint;
            var direction = r2 - r1;
            var scale = direction.Length() / 2;
            var center = (r1 + r2) / 2;
            //var q = new Quaternion(Vector3.UnitY, (float)direction.Angle());                
            //q.Normalize();

            var m = Matrix.CreateRotationZ((float)direction.Angle());
            m.Translation = new Vector3(center.X, center.Y, 0);

            double a = param;

            var sinA = Math.Sin(a);
            var cosA = Math.Cos(a);
            var denominator = Math.Sqrt(8) * cosA - 3;
            var sinB = sinA / denominator;
            var cosB = (Math.Sqrt(8) - 3 * cosA) / denominator;
            var C = new Vector2((float)cosA - 1, (float)sinA);
            var D = new Vector2((float)cosB + 1, (float)sinB);
            var E = (C + D) / 2;

            return Vector2.Transform((float)scale * E, m);
            //return center + (float)scale * Vector2.Transform(E, q);
        }
    }
}
