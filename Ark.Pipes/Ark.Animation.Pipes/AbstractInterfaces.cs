namespace Ark.Abstract {
    public interface IAdditive<TAddition, TResult> {
        TResult Plus(TAddition value);
        void Plus(ref TAddition value, out TResult result);        
    }

    public interface IAdditive<TOther>
        : IAdditive<TOther, TOther> {
    }

    public interface IMultiplicative<TMultiplier, TResult> {
        TResult MultipliedBy(TMultiplier multiplier);
        void MultipliedBy(ref TMultiplier multiplier, out TResult result);        
    }

    public interface IMultiplicative<TOther>
        : IMultiplicative<TOther, TOther> {
    }

    public interface IDelta<T>
        : IAdditive<T> {
        void AddDelta(T value1, T value2);
        void AddDelta(ref T value1, ref T value2);        
    }

    public interface IIsDerivativeOfEx<TResult, TDeltaVar> {
        TResult MakeStep(TResult state, TDeltaVar deltaArg);
        void MakeStep(ref TResult state, ref TDeltaVar deltaArg, out TResult result);        
    }

    public interface IIsDerivativeOf<TResult, TVar> {
        TResult MakeStep(TResult state, TVar arg, TVar newArg);
        void MakeStep(ref TResult state, ref TVar arg, ref TVar newArg, out TResult result);        
    }
}