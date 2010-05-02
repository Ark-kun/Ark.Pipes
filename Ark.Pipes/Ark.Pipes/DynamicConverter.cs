namespace Ark.Pipes {
    public abstract class DynamicConverter<T, TResult> : Provider<TResult> {
        public DynamicConverter()
            : this(Constant<T>.Default) {
        }

        public DynamicConverter(Provider<T> input) {
            Input = input;
        }

        public Provider<T> Input { get; set; }
    }

    public abstract class DynamicConverter<T1, T2, TResult> : Provider<TResult> {
        public DynamicConverter()
            : this(Constant<T1>.Default, Constant<T2>.Default) {
        }

        public DynamicConverter(Provider<T1> input1, Provider<T2> input2) {
            Input1 = input1;
            Input2 = input2;
        }

        public Provider<T1> Input1 { get; set; }
        public Provider<T2> Input2 { get; set; }
    }
}