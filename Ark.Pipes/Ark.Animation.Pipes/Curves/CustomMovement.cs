using System;
using Ark.Animation;
using Ark.Pipes;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

namespace Ark.Geometry.Curves {
    public static class CustomMovement {
        public static Provider<T> Create<T>(Func<T, T> changer, T initialState) { 
            T state = initialState;
            return Provider.Create(() => {
                state = changer(state);
                return state;
            });
        }

        public static Provider<T> Create<T>(Func<T, DeltaT, T> changer, T initialState, Provider<TFloat> timer) {
            return Create(changer, initialState, timer.ToDeltaTs());
        }

        public static Provider<T> Create<T>(Func<T, DeltaT, T> changer, T initialState, Provider<DeltaT> deltaTs) {
            T state = initialState;
            return Provider.Create((dt) => {
                state = changer(state, dt);
                return state;
            }, deltaTs);
        }

        public static Provider<T> Create<T>(Func<T, TFloat, T> changer, T initialState, Provider<TFloat> timer) {
            T state = initialState;
            return Provider.Create((t) => {
                state = changer(state, t);
                return state;
            }, timer);
        }

        public static Provider<T> Create<T>(Func<T, TFloat, DeltaT, T> changer, T initialState, Provider<TFloat> timer) {
            T state = initialState;
            TFloat lastTime = timer.Value;
            return Provider.Create((t) => {
                state = changer(state, t, t - lastTime);
                lastTime = t;
                return state;
            }, timer);
        }
    }
}