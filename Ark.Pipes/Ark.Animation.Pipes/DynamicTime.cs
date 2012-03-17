using System;
using Ark.Animation;
using Ark.Pipes;
using Ark.Geometry;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
using StaticVector2 = Ark.Geometry.Primitives.Double.Vector2;
using StaticVector3 = Ark.Geometry.Primitives.Double.Vector3;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
using StaticVector2 = Ark.Geometry.Primitives.Single.Vector2;
using StaticVector3 = Ark.Geometry.Primitives.Single.Vector3;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
using StaticVector2 = Microsoft.Xna.Framework.Vector2;
using StaticVector3 = Microsoft.Xna.Framework.Vector3;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Ark.Geometry.Primitives;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
using StaticVector2 = Ark.Geometry.Primitives.XamlVector2;
using StaticVector3 = Ark.Geometry.Primitives.XamlVector3;
#else
#error Bad geometry framework
#endif

namespace Ark.Animation { //.Pipes
    public static class DynamicTime {
        public static Provider<TFloat> Add(this Provider<TFloat> v1s, Provider<TFloat> v2s) {
            return Provider.Create((v1, v2) => v1 + v2, v1s, v2s);
        }

        public static Provider<TFloat> Add(this Provider<TFloat> v1s, TFloat v2) {
            return Provider.Create((v1) => v1 + v2, v1s);
        }

        public static Provider<TFloat> Subtract(this Provider<TFloat> v1s, Provider<TFloat> v2s) {
            return Provider.Create((v1, v2) => v1 - v2, v1s, v2s);
        }

        public static Provider<TFloat> Subtract(this Provider<TFloat> v1s, TFloat v2) {
            return Provider.Create((v1) => v1 - v2, v1s);
        }

        public static Provider<TFloat> Accelerate(this Provider<TFloat> ts, TFloat multiplier) {
            TFloat t0 = ts.Value;
            return Provider.Create((t) => t0 + (t - t0) * multiplier, ts);
        }

        public static Provider<TFloat> Reset(this Provider<TFloat> timer) {
            TFloat t0 = timer.Value;
            return Provider.Create((t) => t - t0, timer);
        }

        public static Provider<DeltaT> ToDeltaTs(this Provider<TFloat> timer) {
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                TFloat oldTime = time;
                time = newTime;
                return (DeltaT)(newTime - oldTime);
            }, timer);
        }

        public static Provider<Tuple<TFloat, DeltaT>> AddDeltaTs(this Provider<TFloat> timer) {
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                TFloat oldTime = time;
                time = newTime;
                return new Tuple<TFloat, DeltaT>(newTime, newTime - oldTime);
            }, timer);
        }
    }
}

