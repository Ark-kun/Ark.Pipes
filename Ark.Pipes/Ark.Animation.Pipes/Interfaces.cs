namespace Ark.Animation {
    public interface IHasChangeablePosition<T> {
        T Position { get; set; }
    }

    public interface IHasChangeableOrientation<T> {
        T Orientation { get; set; }
    }
}