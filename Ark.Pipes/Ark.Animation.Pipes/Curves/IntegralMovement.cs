using System;
using Ark.Abstract;
using Ark.Animation;
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

namespace Ark.Geometry.Curves {
    public static class Extensions_IntegralCurves {
        public static Func<T, TDerivative> AsTimeCurve<T, TDerivative, T1, T2>(this Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2) {
            return (state) => curve(state, arg1, arg2);
        }

        public static Func<Provider<TFloat>, Provider<T>> AsTimerCurve<T, TDerivative, T1, T2>(this Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2)
            where TDerivative : IIsDerivativeOf<T, TFloat> {
            return (timer) => {
                T state = initialState;
                TFloat varValue = timer.Value;
                return Provider<T>.Create((p1, p2, t) => {
                    state = curve(state, p1, p2).MakeStep(ref state, ref varValue, ref t);
                    varValue = t;
                    return state;
                }, arg1, arg2, timer);
            };
        }

        public static Func<Provider<DeltaT>, Provider<T>> AsDeltaTimerCurve<T, TDerivative, T1, T2>(this Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            return (deltas) => {
                T state = initialState;
                return Provider<T>.Create((p1, p2, dt) => curve(state, p1, p2).MakeStep(ref state, ref dt), arg1, arg2, deltas);
            };
        }

    }

    public class IntegralMovement {
        //Cannot infer TDelta
        //public static Provider<T> Create<T, TDerivative, TDelta, TDeltaVar, T1, T2>(Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TDeltaVar> dparam)
        //    where TDelta : IInstanceDelta<T>
        //    where TDerivative : IInstanceMultiplicative<TDeltaVar, TDelta> {
        //    T state = initialState;
        //    return Provider<T>.Create((p1, p2, dt) => {
        //        return state = curve(state, p1, p2).MultipliedBy(ref dt).Plus(ref state);
        //    }, arg1, arg2, dparam);
        //}

        //Cannot infer TDeltaVar
        //public static Provider<T> Create<T, TVar, TDerivative, TDeltaVar, T1, T2>(Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TVar> variable)
        //    where TDeltaVar : IInstanceDelta<TVar>, new()
        //    where TDerivative : IIsDerivativeOf2<T, TVar, TDeltaVar> {
        //    T state = initialState;
        //    TVar varValue = variable.Value;
        //    return Provider<T>.Create((p1, p2, t) => {
        //        var dt = new TDeltaVar();
        //        dt.ConstructFromDifference(ref varValue, ref t);
        //        varValue = t;
        //        return state = curve(state, p1, p2).MultiplyByAndAdd(ref dt, ref state);
        //    }, arg1, arg2, variable);
        //}

        public static Provider<T> Create<T, TVar, TDerivative, T1, T2>(Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TVar> variable)
            where TDerivative : IIsDerivativeOf<T, TVar> {
            T state = initialState;
            TVar varValue = variable.Value;
            return Provider<T>.Create((p1, p2, t) => {
                state = curve(state, p1, p2).MakeStep(ref state, ref varValue, ref t);
                varValue = t;
                return state;
            }, arg1, arg2, variable);
        }

        //Cannot infer TVar?
        //Provider<TVar> -> Provider<TDeltaVar>
        public static Provider<T> Create<T, TVar, TDerivative, TDeltaVar, T1, T2>(Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TDeltaVar> deltas)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>
            where TDeltaVar : IDelta<TVar> {
            T state = initialState;
            return Provider<T>.Create((p1, p2, dt) => {
                state = curve(state, p1, p2).MakeStep(ref state, ref dt);
                return state;
            }, arg1, arg2, deltas);
        }

        //TDeltaVar = DeltaT
        public static Provider<T> Create<T, TDerivative, T1, T2>(Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<DeltaT> deltas)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            T state = initialState;
            return Provider<T>.Create((p1, p2, dt) => {
                state = curve(state, p1, p2).MakeStep(ref state, ref dt);
                return state;
            }, arg1, arg2, deltas);
        }

        //TVar = TFloat
        public static Provider<T> Create<T, TDerivative, T1, T2>(Func<T, T1, T2, TDerivative> curve, T initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TFloat> timer)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            T state = initialState;
            return Provider<T>.Create((p1, p2, dt) => {
                state = curve(state, p1, p2).MakeStep(ref state, ref dt);
                return state;
            }, arg1, arg2, timer.ToDeltaTs());
        }

        //T = Vector2
        public static Provider<Vector2> Create<T1, T2>(Func<Vector2, T1, T2, Vector2> curve, Vector2 initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TFloat> timer) {
            Vector2 state = initialState;
            return Provider<Vector2>.Create((p1, p2, dt) => {
                return state += curve(state, p1, p2) * dt;
            }, arg1, arg2, timer.ToDeltaTs());
        }

        //T = Vector3
        public static Provider<Vector3> Create<T1, T2>(Func<Vector3, T1, T2, Vector3> curve, Vector3 initialState, Provider<T1> arg1, Provider<T2> arg2, Provider<TFloat> timer) {
            Vector3 state = initialState;
            return Provider<Vector3>.Create((p1, p2, dt) => {
                return state += curve(state, p1, p2) * dt;
            }, arg1, arg2, timer.ToDeltaTs());
        }
    }
}