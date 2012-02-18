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
        Provider<TType> _length;

        public Vector2Components()
            : this(Constant<TType>.Default, Constant<TType>.Default) {
        }

        public static Vector2Components From<TPoint>(Provider<TPoint> point) {
            var x = Provider<TType>.Create(p => (TType)(((dynamic)p).X), point);
            var y = Provider<TType>.Create(p => (TType)(((dynamic)p).Y), point);
            return new Vector2Components(x, y);
        }

        public Vector2Components(Provider<TType> x, Provider<TType> y) {
            _x = x;
            _y = y;
            _length = Provider<TType>.Create((X, Y) => Math.Sqrt(X * X + Y * Y), _x, _y);
        }

        public Provider<TType> Length {
            get { return _length; }
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
