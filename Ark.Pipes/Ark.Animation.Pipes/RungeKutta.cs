using System;
using Ark.Abstract;

namespace Ark.Animation {
    public class RungeKutta {
        public static T Integrate<T, TDerivative, TDeltaVar>(Func<T, TDerivative> derivative, T state, TDeltaVar deltaT)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>, IMultiplicative<TDeltaVar, T>
            where TDeltaVar : IMultiplicative<float, TDeltaVar>
            where T : IAdditive<T>
        {
            float ratio1_2 = 0.5f;
            float ratio1_6 = 1.0f / 6;
            float ratio2_6 = 2.0f / 6;
            TDeltaVar dt_1_2 = deltaT.MultipliedBy(ref ratio1_2);
            TDeltaVar dt_1_6 = deltaT.MultipliedBy(ref ratio1_6);
            TDeltaVar dt_2_6 = deltaT.MultipliedBy(ref ratio2_6);

            TDerivative d0 = derivative(state);
            T delta05 = d0.MultipliedBy(ref dt_1_2);

            TDerivative d1 = derivative(state.Plus(ref delta05));
            T delta15 = d1.MultipliedBy(ref dt_1_2);

            TDerivative d2 = derivative(state.Plus(ref delta15));
            T delta3 = d2.MultipliedBy(ref deltaT);

            TDerivative d3 = derivative(state.Plus(ref delta3));

            T deltaR0 = d0.MultipliedBy(ref dt_1_6);
            T deltaR1 = d1.MultipliedBy(ref dt_2_6);
            T deltaR2 = d2.MultipliedBy(ref dt_2_6);
            T deltaR3 = d3.MultipliedBy(ref dt_1_6);

            T result = state.Plus(ref deltaR0).Plus(ref deltaR1).Plus(ref deltaR2).Plus(ref deltaR3);
            return result;
        }

        public static T Integrate<T, TDerivative, TVar, TDeltaVar>(Func<T, TVar, TDerivative> derivative, T state, TVar t, TDeltaVar deltaT)
            where TDerivative : IIsDerivativeOfEx<T, TDeltaVar>, IMultiplicative<TDeltaVar, T>
            where TDeltaVar : IMultiplicative<float, TDeltaVar>, IAdditive<TVar>
            where T : IAdditive<T>
        {
            float ratio1_2 = 0.5f;
            float ratio1_6 = 1.0f / 6;
            float ratio2_6 = 2.0f / 6;
            TDeltaVar dt_1_2 = deltaT.MultipliedBy(ref ratio1_2);
            TDeltaVar dt_1_6 = deltaT.MultipliedBy(ref ratio1_6);
            TDeltaVar dt_2_6 = deltaT.MultipliedBy(ref ratio2_6);

            TVar t0 = t;
            TDerivative d0 = derivative(state, t0);
            T delta05 = d0.MultipliedBy(ref dt_1_2);

            TVar t1 = dt_1_2.Plus(ref t0);
            TDerivative d1 = derivative(state.Plus(ref delta05), t1);
            T delta15 = d1.MultipliedBy(ref dt_1_2);

            TVar t2 = t1;
            TDerivative d2 = derivative(state.Plus(ref delta15), t2);
            T delta3 = d2.MultipliedBy(ref deltaT);

            TVar t3 = deltaT.Plus(ref t0);
            TDerivative d3 = derivative(state.Plus(ref delta3), t3);

            T deltaR0 = d0.MultipliedBy(ref dt_1_6);
            T deltaR1 = d1.MultipliedBy(ref dt_2_6);
            T deltaR2 = d2.MultipliedBy(ref dt_2_6);
            T deltaR3 = d3.MultipliedBy(ref dt_1_6);

            T result = state.Plus(ref deltaR0).Plus(ref deltaR1).Plus(ref deltaR2).Plus(ref deltaR3);
            return result;
        }
    }
}
