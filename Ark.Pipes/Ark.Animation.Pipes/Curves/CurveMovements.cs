﻿using System;
using Ark.Animation;
using Ark.Geometry;
using Ark.Geometry.Curves;
using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

#if FRAMEWORK_ARK && FLOAT_TYPE_DOUBLE
using Ark.Geometry.Primitives.Double;
#elif FRAMEWORK_ARK && FLOAT_TYPE_SINGLE
using Ark.Geometry.Primitives.Single;
#elif FRAMEWORK_XNA && FLOAT_TYPE_SINGLE
using Microsoft.Xna.Framework;
#elif FRAMEWORK_WPF && FLOAT_TYPE_DOUBLE
using System.Windows;
using System.Windows.Media.Media3D;
using Vector2 = System.Windows.Vector;
using Vector3 = System.Windows.Media.Media3D.Vector3D;
#else
#error Bad geometry framework
#endif

namespace Ark.Animation { //.Pipes {
    public class CurveMovement {
        public static Provider<TResult> Create<TResult, T1>(Func<T1, float, TResult> curve, Provider<T1> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2>(Func<T1, T2, float, TResult> curve, Provider<T1> param1, Provider<T2> param2, Provider<float> time) {
            return Provider<TResult>.Create((p1, p2, t) => curve(p1, p2, t), param1, param2, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2>(Func<Tuple<T1, T2>, float, TResult> curve, Provider<Tuple<T1, T2>> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2, T3>(Func<T1, T2, T3, float, TResult> curve, Provider<T1> param1, Provider<T2> param2, Provider<T3> param3, Provider<float> time) {
            return Provider<TResult>.Create((p1, p2, p3, t) => curve(p1, p2, p3, t), param1, param2, param3, time);
        }

        public static Provider<TResult> Create<TResult, T1, T2, T3>(Func<Tuple<T1, T2, T3>, float, TResult> curve, Provider<Tuple<T1, T2, T3>> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }

        //public static Provider<TResult> Create<TResult, T1, T2, T3, T4>(Func<T1, T2, T3, T4, float, TResult> curve, Provider<T1> param1, Provider<T2> param2, Provider<T3> param3, Provider<T4> param4, Provider<float> time) {
        //    return Provider<TResult>.Create((p1, p2, p3, p4, t) => curve(p1, p2, p3, p4, t), param1, param2, param3, param4, time);
        //}

        public static Provider<TResult> Create<TResult, T1, T2, T3, T4>(Func<Tuple<T1, T2, T3, T4>, float, TResult> curve, Provider<Tuple<T1, T2, T3, T4>> parameters, Provider<float> time) {
            return Provider<TResult>.Create((p, t) => curve(p, t), parameters, time);
        }
    }
}