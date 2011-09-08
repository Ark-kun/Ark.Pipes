namespace Ark.Pipes {
    public interface IIn<in T> {
        void SetValue(T value);
    }

    public interface IOut<out T> {
        T GetValue();
    }
}
