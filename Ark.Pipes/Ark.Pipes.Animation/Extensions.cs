using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if FLOAT_GEOMETRY
using TType = System.Single;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector2;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework.Vector3;
using TQuaternion = Ark.Borrowed.Net.Microsoft.Xna.Framework.Quaternion;    
#else
using TType = System.Double;
using TVector2 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector2;
using TVector3 = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Vector3;
using TQuaternion = Ark.Borrowed.Net.Microsoft.Xna.Framework._Double.Quaternion;
#endif

namespace Ark.Pipes.Animation {
    public static class Extensions {
        public static Provider<TVector2> Add(this Provider<TVector2> v1s, Provider<TVector2> v2s) {
            return Provider<TVector2>.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<TVector2> Add(this Provider<TVector2> v1s, TVector2 v2) {
            return Provider<TVector2>.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<TVector2> Multiply(this Provider<TVector2> vs, TType multiplier) {
            return Provider<TVector2>.Create((v) => v * multiplier, vs);
        }

        public static Provider<TVector2> Divide(this Provider<TVector2> vs, TType divider) {
            return vs.Multiply(1 / divider);
        }

        public static Provider<TVector2> Negate(this Provider<TVector2> vs) {
            return Provider<TVector2>.Create((v) => -v, vs);
        }

        public static Provider<TVector2> Subtract(this Provider<TVector2> v1s, Provider<TVector2> v2s) {
            return Provider<TVector2>.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<TVector2> Subtract(this Provider<TVector2> v1s, TVector2 v2) {
            return Provider<TVector2>.Create((v1) => v1 - v2, v1s);
        }

        public static Vector2Components ToComponents(this Provider<TVector2> vectors) {
            return new Vector2Components(vectors);
        }

        public static TVector3 ToVector3XY(this TVector2 v) {
            return new TVector3(v.X, v.Y, 0);
        }

        public static Provider<TVector3> ToVectors3XY(this Provider<TVector2> vectors) {
            return Provider<TVector3>.Create((v) => new TVector3(v.X, v.Y, 0), vectors);
        }

        public static TVector3 ToVector3XZ(this TVector2 v) {
            return new TVector3(v.X, 0, v.Y);
        }

        public static Provider<TVector3> ToVectors3XZ(this Provider<TVector2> vectors) {
            return Provider<TVector3>.Create((v) => new TVector3(v.X, 0, v.Y), vectors);
        }

        public static Provider<TVector3> Add(this Provider<TVector3> v1s, Provider<TVector3> v2s) {
            return Provider<TVector3>.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<TVector3> Add(this Provider<TVector3> v1s, TVector3 v2) {
            return Provider<TVector3>.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<TVector3> Multiply(this Provider<TVector3> vs, TType multiplier) {
            return Provider<TVector3>.Create((v) => v * multiplier, vs);
        }

        public static Provider<TVector3> Divide(this Provider<TVector3> vs, TType divider) {
            return vs.Multiply(1 / divider);
        }

        public static Provider<TVector3> Negate(this Provider<TVector3> vs) {
            return Provider<TVector3>.Create((v) => -v, vs);
        }

        public static Provider<TVector3> Subtract(this Provider<TVector3> v1s, Provider<TVector3> v2s) {
            return Provider<TVector3>.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<TVector3> Subtract(this Provider<TVector3> v1s, TVector3 v2) {
            return Provider<TVector3>.Create((v1) => v1 - v2, v1s);
        }

        public static Vector3Components ToComponents(this Provider<TVector3> vectors) {
            return new Vector3Components(vectors);
        }

        public static TVector2 ToVectors2XY(this TVector3 v) {
            return new TVector2(v.X, v.Y);
        }
        public static Provider<TVector2> ToVectors2XY(this Provider<TVector3> vectors) {
            return Provider<TVector2>.Create((v) => new TVector2(v.X, v.Y), vectors);
        }

        public static TVector2 ToVector2XZ(this TVector3 v) {
            return new TVector2(v.X, v.Z);
        }
        public static Provider<TVector2> ToVectors2XZ(this Provider<TVector3> vectors) {
            return Provider<TVector2>.Create((v) => new TVector2(v.X, v.Z), vectors);
        }
    }
}
