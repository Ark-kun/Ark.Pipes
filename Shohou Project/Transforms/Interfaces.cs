
namespace Ark.Geometry.Transforms {
    public interface ITransform<T> {
        T Transform(T value);
    }
    public interface IInvertibleTransform<T> : ITransform<T> {
        IInvertibleTransform<T> Inverse { get; }
    }

    public interface IHasTransform<T> {
        ITransform<T> Transform { get; }
    }
}