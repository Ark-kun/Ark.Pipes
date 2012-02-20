using System;
using System.Collections.Generic;
using System.Linq;

namespace Ark.Geometry.Transforms {
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
        public static FunctionTransform<T> Prepend<T>(this ITransform<T> t1, ITransform<T> t2) {
            return new FunctionTransform<T>((value) => t1.Transform(t2.Transform(value)));
        }

        public static FunctionTransform<T> Append<T>(this ITransform<T> t1, ITransform<T> t2) {
            return new FunctionTransform<T>((value) => t2.Transform(t1.Transform(value)));
        }
    }
}