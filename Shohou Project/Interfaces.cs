using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Pipes;

namespace Ark.XNA {

    public interface IHasPosition<T> {
        Provider<T> Position { get; }
    }

    public interface IHasChangeablePosition {
        Provider<Vector2> Position { get; set; }
    }

    public interface IHasChangeableAngle {
        Provider<float> Angle { get; set; }
    }

    public interface IHasChangeableTexture {
        Provider<Texture2D> Texture { get; set; }
    }

    public interface ITimeDependent {
        Provider<float> Time { get; set; }
    }

    public interface IContainer<TElement> {
        IEnumerable<TElement> Elements { get; }
    }

    namespace Bullets {
        public interface IBullet<T> : IDrawable, IUpdateable, IHasPosition<T>, IHasParent<IBulletFactory<T>> { }

        public interface IBulletFactory<T> : IUpdateable, IContainer<IBulletFactory<T>>, IContainer<IBullet<T>>, IHasTransform<T> { }

        public interface IBulletFactoryBullet<T> : IBulletFactory<T>, IBullet<T> { }

        public interface IBullet2D : IBullet<Vector2> { }

        public interface IBulletFactory2D : IBulletFactory<Vector2> { }
    }

    public interface ITransform<T> {
        T Transform(T value);
    }
    public interface IInversibleTransform<T> : ITransform<T> {
        IInversibleTransform<T> Inverse { get; }
    }

    public interface IHasParent<T> {
        T Parent { get; }
    }

    public interface IHasTransform<T> {
        ITransform<T> Transform { get; }
    }
}