using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ark.XNA {
    public interface IUpdatable {
        void Update(GameTime gameTime);
    }

    public interface IGameElement : IUpdatable {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }

    public interface IHasPosition {
        Vector2 Position { get; }
    }

    public interface IHasPosition<T> {
        T Position { get; }
    }

    public interface IHasChangeablePosition {
        Vector2 Position { get; set; }
    }

    public interface IHasChangeableAngle {
        float Angle { get; set; }
    }

    public interface IHasChangeableTexture {
        Texture2D Texture { get; set; }
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