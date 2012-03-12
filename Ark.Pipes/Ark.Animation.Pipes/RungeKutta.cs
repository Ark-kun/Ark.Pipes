using System;
using Ark.Abstract;

#if FLOAT_TYPE_DOUBLE
using TFloat = System.Double;
#else
using TFloat = System.Single;
#endif

namespace Ark.Animation {
    public class RungeKutta {
        public static T Integrate<T, TDerivative>(Func<T, TDerivative> derivative, T state, TFloat time, DeltaT deltaT)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            return Integrate((s, t) => derivative(s), state, time, deltaT);
        }

        public static T Integrate<T, TDerivative>(Func<T, TFloat, TDerivative> derivative, T state, TFloat time, DeltaT deltaT)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            T result;
            Integrate(derivative, ref state, time, deltaT, out result);
            return result;
        }

        public static void Integrate<T, TDerivative>(Func<T, TFloat, TDerivative> derivative, ref T state, TFloat time, DeltaT deltaT, out T result)
            where TDerivative : IIsDerivativeOfEx<T, DeltaT> {
            DeltaT dt_1_2 = deltaT * 0.5f;
            DeltaT dt_1_6 = deltaT * 1.0f / 6;
            DeltaT dt_2_6 = deltaT * 2.0f / 6;

            T curState;
            TFloat curTime = time;

            TDerivative d0 = derivative(state, curTime);

            curTime +=  dt_1_2;
            d0.MakeStep(ref state, ref dt_1_2, out curState);
            TDerivative d1 = derivative(curState, curTime);

            d1.MakeStep(ref state, ref dt_1_2, out curState);
            TDerivative d2 = derivative(curState, curTime);

            curTime += dt_1_2;
            d2.MakeStep(ref state, ref deltaT, out curState);
            TDerivative d3 = derivative(curState, curTime);

            d0.MakeStep(ref state, ref dt_1_6, out result);
            d1.MakeStep(ref result, ref dt_2_6, out result);
            d2.MakeStep(ref result, ref dt_2_6, out result);
            d3.MakeStep(ref result, ref dt_1_6, out result);
        }
    }
}
