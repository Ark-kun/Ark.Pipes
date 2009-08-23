using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

