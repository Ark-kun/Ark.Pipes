using Ark.Animation.Pipes;
using Ark.Geometry.Transforms;
using Microsoft.Xna.Framework;

namespace Ark.Animation.Bullets.Xna {
    public interface IBullet<T> : IDrawable, IUpdateable, IHasPosition<T>, IHasParent<IBulletFactory<T>> { }

    public interface IBulletFactory<T> : IUpdateable, IContainer<IBulletFactory<T>>, IContainer<IBullet<T>>, IHasTransform<T> { }

    public interface IBulletFactoryBullet<T> : IBulletFactory<T>, IBullet<T> { }

    public interface IBullet2D : IBullet<Vector2> { }

    public interface IBulletFactory2D : IBulletFactory<Vector2> { }
}