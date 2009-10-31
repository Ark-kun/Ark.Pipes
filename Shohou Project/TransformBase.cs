using System;
using Microsoft.Xna.Framework;
using Ark.Pipes;

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

    public class UpdateableFunctionTransform<T> : GameComponent, ITransform<T> {
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

    public class TranslationTransform2D : ITransform<Vector2> {
        Provider<Vector2> _translation = Vector2.Zero;

        public Vector2 Transform(Vector2 value) {
            return value + _translation;
        }

        public Provider<Vector2> Translation {
            get {
                return _translation;
            }
            set {
                _translation = value;
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