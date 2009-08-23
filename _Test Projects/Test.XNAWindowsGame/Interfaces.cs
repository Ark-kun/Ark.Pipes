using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IUpdatable
{
    void Update(GameTime gameTime);
}

public interface IGameElement : IUpdatable
{
    void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}

public interface IHasChangeablePosition {
    Vector2 Position { get; set; }
}
public interface IHasChangeableAngle
{
    float Angle { get; set; }
}
public interface IHasChangeableTexture
{
    Texture2D Texture { get; set; }
}

