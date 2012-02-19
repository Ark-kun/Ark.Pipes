using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Pipes;

namespace Ark.Animation.Pipes {
    public interface IHasPosition<T> {
        Provider<T> Position { get; }
    }

    public interface IHasChangeableAngle {
        Provider<float> Angle { get; set; }
    }

    public interface ITimeDependent {
        Provider<float> Time { get; set; }
    }
}

namespace Ark.Animation.Pipes.Xna {
    public interface IHasChangeablePosition {
        Provider<Vector2> Position { get; set; }
    }

    public interface IHasChangeableTexture {
        Provider<Texture2D> Texture { get; set; }
    }
}

namespace Ark.Animation {
    public interface IContainer<TElement> {
        IEnumerable<TElement> Elements { get; }
    }

    public interface IHasParent<T> {
        T Parent { get; }
    }
}