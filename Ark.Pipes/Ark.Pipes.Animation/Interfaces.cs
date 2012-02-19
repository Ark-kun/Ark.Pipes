namespace Ark.Animation {
    public interface IHasPosition<T> {
        T Position { get; set; }
    }

    public interface IHasOrientation<T> {
        T Orientation { get; set; }
    }
}
