﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public interface IUpdatable {
    void Update(GameTime gameTime);
}

public interface IGameElement : IUpdatable {
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
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

public interface IBullet : IDrawable, IUpdateable { }
public interface IBullet2D : IBullet, IHasParent<IBulletFactory2D> { }
public interface IBulletFactory : IContainer<IBulletFactory>, IContainer<IBullet> { }
public interface IBulletFactory2D : IBulletFactory, IHasTransform<Vector2> { };

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