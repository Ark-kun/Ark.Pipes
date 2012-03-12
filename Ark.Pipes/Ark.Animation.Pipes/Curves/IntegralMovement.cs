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
        public static Func<T, TDerivative> AsTimeCurve<T, TDerivative, T1, T2>(this Func<T, TDerivative> curve, T initialState) {
            return (state) => curve(state);
        }

        public static Func<Provider<TFloat>, Provider<T>> AsTimerCurve<T, TDerivative>(this Func<T, TDerivative> curve, T initialState)
            where TDerivative : IIsDerivativeOf<T, TFloat> {
            return (timer) => {
                T state = initialState;
                TFloat varValue = timer.Value;
                return Provider<T>.Create((t) => {
                    state = curve(state).MakeStep(ref state, ref varValue, ref t);
                    varValue = t;
                    return state;
                }, timer);
            };
        }

        public static Func<Provider<DeltaT>, Provider<T>> AsDeltaTimerCurve<T, TDerivative>(this Func<T, TDerivative> curve, T initialState)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            return (deltas) => {
                T state = initialState;
                return Provider<T>.Create((dt) => curve(state).MakeStep(ref state, ref dt), deltas);
            };
        }

    }

    public class IntegralMovement {
        //Cannot infer TDelta
        //public static Provider<T> Create<T, TDerivative, TDelta, TDeltaVar>(Func<T, TDerivative> curve, T initialState, Provider<TDeltaVar> dparam)
        //    where TDelta : IInstanceDelta<T>
        //    where TDerivative : IInstanceMultiplicative<TDeltaVar, TDelta> {
        //    T state = initialState;
        //    return Provider<T>.Create((dt) => {
        //        return state = curve(state).MultipliedBy(ref dt).Plus(ref state);
        //    }, dparam);
        //}

        //Cannot infer TDeltaVar
        //public static Provider<T> Create<T, TVar, TDerivative, TDeltaVar>(Func<T, TDerivative> curve, T initialState, Provider<TVar> variable)
        //    where TDeltaVar : IInstanceDelta<TVar>, new()
        //    where TDerivative : IIsDerivativeOf2<T, TVar, TDeltaVar> {
        //    T state = initialState;
        //    TVar varValue = variable.Value;
        //    return Provider<T>.Create((t) => {
        //        var dt = new TDeltaVar();
        //        dt.ConstructFromDifference(ref varValue, ref t);
        //        varValue = t;
        //        return state = curve(state).MultiplyByAndAdd(ref dt, ref state);
        //    }, variable);
        //}

        public static Provider<T> Create<T, TVar, TDerivative>(Func<T, TDerivative> curve, T initialState, Provider<TVar> variable)
            where TDerivative : IIsDerivativeOf<T, TVar> {
            T state = initialState;
            TVar value = variable.Value;
            return Provider<T>.Create((newValue) => {
                state = curve(state).MakeStep(ref state, ref value, ref newValue);
                value = newValue;
                return state;
            }, variable);
        }

        public static Provider<T> Create<T, TVar, TDerivative>(Func<T, TVar, TDerivative> curve, T initialState, Provider<TVar> variable)
            where TDerivative : IIsDerivativeOf<T, TVar> {
            T state = initialState;
            TVar value = variable.Value;
            return Provider<T>.Create((newValue) => {
                state = curve(state, value).MakeStep(ref state, ref value, ref newValue);
                value = newValue;
                return state;
            }, variable);
        }

        //Cannot infer TVar?
        //Provider<TVar> -> Provider<TDeltaVar>
        public static Provider<T> Create<T, TVar, TDerivative, TDeltaVar>(Func<T, TDerivative> curve, T initialState, Provider<TDeltaVar> deltas)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>
            where TDeltaVar : IDelta<TVar> {
            T state = initialState;
            return Provider<T>.Create((dt) => {
                state = curve(state).MakeStep(ref state, ref dt);
                return state;
            }, deltas);
        }

        //TDeltaVar = DeltaT
        public static Provider<T> Create<T, TDerivative>(Func<T, TDerivative> curve, T initialState, Provider<DeltaT> deltas)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            T state = initialState;
            return Provider<T>.Create((dt) => {
                state = curve(state).MakeStep(ref state, ref dt);
                return state;
            }, deltas);
        }

        //TVar = TFloat
        public static Provider<T> Create<T, TDerivative>(Func<T, TDerivative> curve, T initialState, Provider<TFloat> timer)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            T state = initialState;
            return Provider<T>.Create((dt) => {
                state = curve(state).MakeStep(ref state, ref dt);
                return state;
            }, timer.ToDeltaTs());
        }

        //T = Vector2
        public static Provider<Vector2> Create(Func<Vector2, Vector2> curve, Vector2 initialState, Provider<TFloat> timer) {
            Vector2 state = initialState;
            return Provider<Vector2>.Create((dt) => {
                return state += curve(state) * dt;
            }, timer.ToDeltaTs());
        }

        //T = Vector3
        public static Provider<Vector3> Create(Func<Vector3, Vector3> curve, Vector3 initialState, Provider<TFloat> timer) {
            Vector3 state = initialState;
            return Provider<Vector3>.Create((dt) => {
                return state += curve(state) * dt;
            }, timer.ToDeltaTs());
        }
    }
}