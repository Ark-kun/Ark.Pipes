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
            float ratio1_2 = 0.5f;
            float ratio1_6 = 1.0f / 6;
            float ratio2_6 = 2.0f / 6;
            TDeltaVar dt_1_2 = deltaT.MultipliedBy(ref ratio1_2);
            TDeltaVar dt_1_6 = deltaT.MultipliedBy(ref ratio1_6);
            TDeltaVar dt_2_6 = deltaT.MultipliedBy(ref ratio2_6);

            TVar t0 = time;
            TDerivative d0 = derivative(state, t0);

            TVar t1 = dt_1_2.Plus(ref t0);
            T state1 = d0.MakeStep(ref state, ref dt_1_2);
            TDerivative d1 = derivative(state1, t1);

            TVar t2 = t1;
            T state2 = d1.MakeStep(ref state, ref dt_1_2);
            TDerivative d2 = derivative(state2, t2);

            TVar t3 = deltaT.Plus(ref t0);
            T state3 = d2.MakeStep(ref state, ref deltaT);
            TDerivative d3 = derivative(state3, t3);

            T result = state;
            result = d0.MakeStep(ref result, ref dt_1_6);
            result = d1.MakeStep(ref result, ref dt_2_6);
            result = d2.MakeStep(ref result, ref dt_2_6);
            result = d3.MakeStep(ref result, ref dt_1_6);

            return result;
        }
    }
}
