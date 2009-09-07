using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA.Transforms {
    public class XnaMatrixTransform : IInversibleTransform<Vector2> {
        Matrix _matrix;
        XnaMatrixTransform _inverse;

        public XnaMatrixTransform(Matrix matrix) {
            _matrix = matrix;
        }

        public IInversibleTransform<Vector2> Inverse {
            get {
                if (_inverse == null) {
                    _inverse = new XnaMatrixTransform(Matrix.Invert(_matrix));
                }
                return _inverse;
            }
        }

        public Vector2 Transform(Vector2 value) {
            return Vector2.Transform(value, _matrix);
        }

    }
}