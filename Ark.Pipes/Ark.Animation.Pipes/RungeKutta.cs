using System;
using Ark.Abstract;

namespace Ark.Animation {
    public class RungeKutta {
        public static T Integrate<T, TDerivative, TVar, TDeltaVar>(Func<T, TDerivative> derivative, T state, TVar time, TDeltaVar deltaT)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>
            where TDeltaVar : IMultiplicative<float, TDeltaVar>, IAdditive<TVar> {
            return Integrate((s, t) => derivative(s), state, time, deltaT);
        }

        public static T Integrate<T, TDerivative, TVar, TDeltaVar>(Func<T, TVar, TDerivative> derivative, T state, TVar time, TDeltaVar deltaT)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>
            where TDeltaVar : IMultiplicative<float, TDeltaVar>, IAdditive<TVar> {
            T result;
            Integrate(derivative, ref state, time, deltaT, out result);
            return result;
        }

        public static void Integrate<T, TDerivative, TVar, TDeltaVar>(Func<T, TVar, TDerivative> derivative, ref T state, TVar time, TDeltaVar deltaT, out T result)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>
            where TDeltaVar : IMultiplicative<float, TDeltaVar>, IAdditive<TVar> {
            float ratio1_2 = 0.5f;
            float ratio1_6 = 1.0f / 6;
            float ratio2_6 = 2.0f / 6;
            TDeltaVar dt_1_2;
            TDeltaVar dt_1_6;
            TDeltaVar dt_2_6;
            deltaT.MultipliedBy(ref ratio1_2, out dt_1_2);
            deltaT.MultipliedBy(ref ratio1_6, out dt_1_6);
            deltaT.MultipliedBy(ref ratio2_6, out dt_2_6);

            T curState;
            TVar curTime = time;

            TDerivative d0 = derivative(state, curTime);

            dt_1_2.Plus(ref curTime, out curTime);
            d0.MakeStep(ref state, ref dt_1_2, out curState);
            TDerivative d1 = derivative(curState, curTime);

            d1.MakeStep(ref state, ref dt_1_2, out curState);
            TDerivative d2 = derivative(curState, curTime);

            dt_1_2.Plus(ref curTime, out curTime);
            d2.MakeStep(ref state, ref deltaT, out curState);
            TDerivative d3 = derivative(curState, curTime);

            d0.MakeStep(ref state, ref dt_1_6, out result);
            d1.MakeStep(ref result, ref dt_2_6, out result);
            d2.MakeStep(ref result, ref dt_2_6, out result);
            d3.MakeStep(ref result, ref dt_1_6, out result);
        }
    }
}
