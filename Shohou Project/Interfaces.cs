using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ark.Pipes;

namespace Ark.Animation { //.Pipes {
    public interface IHasDynamicPosition<T> {
        Provider<T> Position { get; }
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