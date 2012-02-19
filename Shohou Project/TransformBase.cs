using System;
using Microsoft.Xna.Framework;
using Ark.Pipes;
using System.Collections.Generic;
using System.Linq;

namespace Ark.Xna.Transforms {
    public sealed class FunctionTransform<T> : ITransform<T> {
        Func<T, T> _transform;

        public FunctionTransform(Func<T, T> transform) {
            _transform = transform;
        }

        public T Transform(T value) {
            return _transform(value);
        }
    }

    public sealed class InvertibleFunctionTransform<T> : IInvertibleTransform<T> {
        Func<T, T> _transform;
        IInvertibleTransform<T> _inverseTransform;

        public InvertibleFunctionTransform(Func<T, T> transform, Func<T, T> inverse) {
            _transform = transform;
            _inverseTransform = new InvertibleFunctionTransform<T>(inverse, this);
        }

        public InvertibleFunctionTransform(Func<T, T> transform, IInvertibleTransform<T> inverseTransform) {
            _transform = transform;
            _inverseTransform = inverseTransform;
        }

        public T Transform(T value) {
            return _transform(value);
        }

        public IInvertibleTransform<T> Inverse {
            get { return _inverseTransform; }
        }
    }

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


    public static class Transform<T> {
        static IdentityTransform _identity = new IdentityTransform();

        public static IInvertibleTransform<T> Identity {
            get { return _identity; }
        }

        private class IdentityTransform : IInvertibleTransform<T> {
            public T Transform(T value) {
                return value;
            }

            public IInvertibleTransform<T> Inverse {
                get { return this; }
            }
        }
    }

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


    public class TransformStack<T> : ITransform<T> {
        IEnumerable<ITransform<T>> _transforms;

        public TransformStack(IEnumerable<ITransform<T>> transforms) {
            _transforms = transforms;
        }

        public T Transform(T value) {
            T current = value;
            foreach (var transform in _transforms) {
                current = transform.Transform(current);
            }
            return current;
        }
    }

    public class InvertibleTransformStack<T> : IInvertibleTransform<T> {
        List<IInvertibleTransform<T>> _transforms;
        IInvertibleTransform<T> _inverseTransform;

        public InvertibleTransformStack(IEnumerable<IInvertibleTransform<T>> transforms) {
            _transforms = new List<IInvertibleTransform<T>>(transforms);
        }

        private InvertibleTransformStack(IEnumerable<IInvertibleTransform<T>> transforms, IInvertibleTransform<T> inverseTransform)
            : this(transforms) {
            _inverseTransform = inverseTransform;
        }

        public T Transform(T value) {
            T current = value;
            foreach (var transform in _transforms) {
                current = transform.Transform(current);
            }
            return current;
        }

        public IInvertibleTransform<T> Inverse {
            get {
                if (_inverseTransform == null) {
                    _inverseTransform = new InvertibleTransformStack<T>(_transforms.Select(t => t.Inverse).Reverse(), this);
                }
                return _inverseTransform;
            }
        }
    }

    public static class Extensions {
        public static ITransform<T> Prepend<T>(this ITransform<T> t1, ITransform<T> t2) {
            return new FunctionTransform<T>((value) => t1.Transform(t2.Transform(value)));
        }

        public static ITransform<T> Append<T>(this ITransform<T> t1, ITransform<T> t2) {
            return new FunctionTransform<T>((value) => t2.Transform(t1.Transform(value)));
        }
    }
}