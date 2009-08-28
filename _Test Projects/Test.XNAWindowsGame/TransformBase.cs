using System;
public class FunctionTransform<T> : ITransform<T> {
    Func<T, T> _transform;
    public FunctionTransform(Func<T, T> transform) {
        _transform = transform;
    }

    public T Transform(T value) {
        return _transform(value);
    }
}