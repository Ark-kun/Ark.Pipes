using System;
using Ark.Pipes;
using Microsoft.Xna.Framework;

namespace Ark.Xna.Geometry {
    public class RandomVectorInsideRectangle : Provider<Vector2> {
        private Random _rnd = new Random();
        private Rectangle _rect;

        public RandomVectorInsideRectangle(Rectangle rect) {
            _rect = rect;
        }

        public override Vector2 GetValue() {
            return new Vector2(_rect.X + _rnd.Next(_rect.Width), _rect.Y + _rnd.Next(_rect.Height));
        }
    }
}
