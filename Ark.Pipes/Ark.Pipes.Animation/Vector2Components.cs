using System;

#if FLOAT_GEOMETRY
using TType = System.Single;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector2;
#else
using TType = System.Double;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector2;
#endif

namespace Ark.Pipes {
    public class Vector2Components {
        Property<TType> _x;
        Property<TType> _y;

        public Vector2Components()
            : this(Constant<TType>.Default, Constant<TType>.Default) {
        }

        public Vector2Components(Provider<TType> x, Provider<TType> y) {
            _x = x;
            _y = y;
        }

        public Vector2Components(Provider<TVector2> vectors) {
            _x = Provider<TType>.Create((v) => v.X, vectors);
            _y = Provider<TType>.Create((v) => v.Y, vectors);
        }

        public static Vector2Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider<TType>.Create(p => (TType)(((dynamic)p).X), point);
            var y = Provider<TType>.Create(p => (TType)(((dynamic)p).Y), point);
            return new Vector2Components(x, y);
        }

        public Provider<TVector2> ToVectors2() {
            return Provider<TVector2>.Create((x, y) => new TVector2(x, y), _x, _y);
        }

        public Property<TType> X {
            get { return _x; }
            set { _x.Provider = value.Provider; }
        }

        public Property<TType> Y {
            get { return _y; }
            set { _y.Provider = value.Provider; }
        }
    }
}
