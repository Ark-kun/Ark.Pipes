namespace Ark.Abstract {
    public interface IAdditive<TAddition, TResult> {
        TResult Plus(ref TAddition value);
    }

    public interface IAdditive<TOther>
        : IAdditive<TOther, TOther> {
    }

    public interface IMultiplicative<TMultiplier, TResult> {
        TResult MultipliedBy(ref TMultiplier multiplier);
    }

    public interface IMultiplicative<TOther>
        : IMultiplicative<TOther, TOther> {
    }

    public interface IDelta<T>
        : IAdditive<T> {
        void ConstructFromDifference(ref T value1, ref T value2);
    }

    public interface IIsDerivativeOfEx<TResult, TDeltaVar> {
        TResult MakeStep(ref TResult state, ref TDeltaVar deltaArg);
    }

    public interface IIsDerivativeOf<TResult, TVar> {
        TResult MakeStep(ref TResult state, ref TVar arg, ref TVar newArg);
    }
}