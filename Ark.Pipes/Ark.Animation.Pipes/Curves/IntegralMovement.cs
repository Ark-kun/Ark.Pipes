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
        public static Func<T, TDerivative> AsTimeCurve<T, TDerivative>(this Func<T, TDerivative> curve, T initialState) {
            return (state) => curve(state);
        }

        public static Func<Provider<TFloat>, Provider<T>> AsTimerCurve<T, TDerivative>(this Func<T, TDerivative> curve, T initialState)
            where TDerivative : IIsDerivativeOf<T, TFloat> {
            return (timer) => {
                T state = initialState;
                TFloat varValue = timer.Value;
                return Provider.Create((t) => {
                    T newState;
                    curve(state).MakeStep(ref state, ref varValue, ref t, out newState); //State is cleaned when passed as out. TODO: investigate
                    state = newState;
                    varValue = t;
                    return state;
                }, timer);
            };
        }

        public static Func<Provider<DeltaT>, Provider<T>> AsDeltaTimerCurve<T, TDerivative>(this Func<T, TDerivative> curve, T initialState)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            return (deltas) => {
                T state = initialState;
                return Provider.Create((dt) => curve(state).MakeStep(state, dt), deltas);
            };
        }

    }

    public static class IntegralMovement {
        public static Provider<T> Create<T, TDerivative>(Func<T, TFloat, TDerivative> curve, T initialState, Provider<TFloat> timer)
            where TDerivative : IIsDerivativeOf<T, TFloat> {
            T state = initialState;
            TFloat time = timer.Value;
            return Provider.Create((newTime) => {
                T newState;
                curve(state, time).MakeStep(ref state, ref time, ref newTime, out newState);
                state = newState;
                return state;
            }, timer);
        }

        public static Provider<T> Create<T, TDerivative>(Func<T, TDerivative> curve, T initialState, Provider<TFloat> timer)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            return Create(curve, initialState, timer.ToDeltaTs());
        }

        public static Provider<T> Create<T, TDerivative>(Func<T, TDerivative> curve, T initialState, Provider<DeltaT> deltas)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            T state = initialState;
            return Provider.Create((dt) => {
                T newState;
                curve(state).MakeStep(ref state, ref dt, out newState);
                state = newState;
                return state;
            }, deltas);
        }

        public static Provider<Vector2> Create(Func<Vector2, Vector2> curve, Vector2 initialState, Provider<TFloat> timer) {
            return Create(curve, initialState, timer.ToDeltaTs());
        }

        public static Provider<Vector2> Create(Func<Vector2, Vector2> curve, Vector2 initialState, Provider<DeltaT> deltas) {
            Vector2 state = initialState;
            return Provider.Create((dt) => {
                return state += curve(state) * dt;
            }, deltas);
        }

        public static Provider<Vector3> Create(Func<Vector3, Vector3> curve, Vector3 initialState, Provider<TFloat> timer) {
            return Create(curve, initialState, timer.ToDeltaTs());
        }

        public static Provider<Vector3> Create(Func<Vector3, Vector3> curve, Vector3 initialState, Provider<DeltaT> deltas) {
            Vector3 state = initialState;
            return Provider.Create((dt) => {
                return state += curve(state) * dt;
            }, deltas);
        }
    }
}