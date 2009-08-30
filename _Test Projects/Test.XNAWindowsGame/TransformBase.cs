using System;
namespace Ark.XNA.Transforms {
    public class FunctionTransform<T> : ITransform<T> {
        Func<T, T> _transform;
        public FunctionTransform(Func<T, T> transform) {
            _transform = transform;
        }

        public T Transform(T value) {
            return _transform(value);
        }
    }

    public class IdentityTransform<T> : ITransform<T> {
        public T Transform(T value) {
            return value;
        }
    }

    public static class Transform<T> {
        static IdentityTransform<T> _identity = new IdentityTransform<T>();

        public static ITransform<T> Identity {
            get {
                return _identity;
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