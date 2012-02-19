using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.Xna.Transforms {
    //public class XnaMatrixTransform : IInvertibleTransform<Vector2>, IInvertibleTransform<Vector3> {
    public class XnaMatrixTransform : IInvertibleTransform<Vector2> {
        Matrix _matrix;
        XnaMatrixTransform _inverse;

        public XnaMatrixTransform(Matrix matrix) {
            _matrix = matrix;
        }

        public IInvertibleTransform<Vector2> Inverse {
            get {
                if (_inverse == null) {
                    _inverse = new XnaMatrixTransform(Matrix.Invert(_matrix));
                }
                return _inverse;
            }
        }

        //IInvertibleTransform<Vector3> IInvertibleTransform<Vector3>.Inverse {
        //    get {
        //        if (_inverse == null) {
        //            _inverse = new XnaMatrixTransform(Matrix.Invert(_matrix));
        //        }
        //        return _inverse;
        //    }
        //}

        public Vector2 Transform(Vector2 value) {
            return Vector2.Transform(value, _matrix);
        }

        //public Vector3 Transform(Vector3 value) {
        //    return Vector3.Transform(value, _matrix);
        //}
    }

    public class XnaMatrix3Transform : IInvertibleTransform<Vector3> {
        Matrix _matrix;
        XnaMatrix3Transform _inverse;

        public XnaMatrix3Transform(Matrix matrix) {
            _matrix = matrix;
        }

        public IInvertibleTransform<Vector3> Inverse {
            get {
                if (_inverse == null) {
                    _inverse = new XnaMatrix3Transform(Matrix.Invert(_matrix));
                }
                return _inverse;
            }
        }

        public Vector3 Transform(Vector3 value) {
            return Vector3.Transform(value, _matrix);
        }
    }
}