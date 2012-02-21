using System;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Geometry.Transforms.Xna {
    public sealed class UpdateableFunctionTransform<T> : GameComponent, ITransform<T> {
        Func<double, Func<T, T>> _transformFactory = null;
        Func<T, T> _transform = null;

        public UpdateableFunctionTransform(Game game, Func<double, Func<T, T>> transformFactory)
            : base(game) {
            _transformFactory = transformFactory;
        }

        public T Transform(T value) {
            return _transform(value);
        }
        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            _transform = _transformFactory(gameTime.TotalGameTime.TotalSeconds);
        }
    }
}

namespace Ark.Geometry.Transforms.Pipes.Xna {
    public class TranslationTransform2D : IInvertibleTransform<Vector2> {
        Provider<Vector2> _translation;
        IInvertibleTransform<Vector2> _inverseTransform;

        public TranslationTransform2D()
            : this(Vector2.Zero) {
        }
        public TranslationTransform2D(Provider<Vector2> translation) {
            _translation = translation;
            _inverseTransform = new InvertibleFunctionTransform<Vector2>(v => v - _translation, this);
        }

        public Vector2 Transform(Vector2 value) {
            return value + _translation;
        }

        public Provider<Vector2> Translation {
            get { return _translation; }
            set { _translation = value; }
        }

        public IInvertibleTransform<Vector2> Inverse {
            get { return _inverseTransform; }
        }
    }

    public class TranslationTransform3D : IInvertibleTransform<Vector3> {
        Provider<Vector3> _translation;
        IInvertibleTransform<Vector3> _inverseTransform;

        public TranslationTransform3D()
            : this(Vector3.Zero) {
        }
        public TranslationTransform3D(Provider<Vector3> translation) {
            _translation = translation;
            _inverseTransform = new InvertibleFunctionTransform<Vector3>(v => v - _translation, this);
        }

        public Vector3 Transform(Vector3 value) {
            return value + _translation;
        }

        public Provider<Vector3> Translation {
            get { return _translation; }
            set { _translation = value; }
        }

        public IInvertibleTransform<Vector3> Inverse {
            get { return _inverseTransform; }
        }
    }
}