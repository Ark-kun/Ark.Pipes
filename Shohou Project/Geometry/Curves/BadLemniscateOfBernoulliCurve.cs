using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ark.Xna.Geometry.Curves {
    namespace Dynamic {
        public class BadLemniscateOfBernoulliCurve : ICurve2D {
            DynamicBoundVector _boundVector;

            public BadLemniscateOfBernoulliCurve(DynamicBoundVector boundVector) {
                _boundVector = boundVector;
            }

            public Vector2 Evaluate(float param) {
                //Vector2 r1 = _boundVector.StartPoint;
                //Vector2 r1 = Vector2.Zero;
                Vector2 r1 = new Vector2(400, 400);
                Vector2 r2 = _boundVector.EndPoint;
                var direction = r2 - r1;
                var a = direction.Length() / 2;
                var c = (r1 + r2) / 2;
                var m = Matrix.CreateRotationZ((float)direction.Angle());
                m.Translation = new Vector3(c.X, c.Y, 0);
                double p = param * 2;// / 4;
                p -= Math.Round(p / (2 * Math.PI)) * 2 * Math.PI;
                if (p > -Math.PI / 4 && p < Math.PI / 4) {
                    p = p;
                } else if (p > Math.PI / 4 && p < 3 * Math.PI / 4) {
                    p = -1 * Math.PI / 2 - p;
                } else if (p > -3 * Math.PI / 4 && p < -Math.PI / 4) {
                    p = -3 * Math.PI / 2 - p;
                } else {
                    p = Math.PI + p;
                }

                p -= Math.Round(p / Math.PI) * Math.PI;
                if (p < -Math.PI / 4 || p > Math.PI / 4) {
                    p = - Math.PI / 2 - p;
                }

                return Vector2.Transform((float)(a * Math.Sqrt(2 * Math.Cos(2 * p))) * (new Vector2((float)Math.Cos(p), (float)Math.Sin(p))), m);
                //return Vector2.Transform(a * (new Vector2((float)Math.Cos(p), (float)Math.Sin(p))), m);
            }
        }
    }
}
