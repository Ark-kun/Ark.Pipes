namespace Ark.Pipes.Input {
    public interface IMouse<TPoint> {
        Provider<TPoint> Position { get; }
        Provider<bool> IsLeftButtonPressed { get; }
        Provider<bool> IsMiddleButtonPressed { get; }
        Provider<bool> IsRightButtonPressed { get; }
    }
}
