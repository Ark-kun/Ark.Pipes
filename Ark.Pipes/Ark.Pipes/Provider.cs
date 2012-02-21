using System;

namespace Ark.Pipes {
    public abstract class Provider<T> : INotifyingOut<T> {
        public abstract T GetValue();

        public T Value {
            get { return GetValue(); }
        }

        public virtual INotifier Notifier {
            get { return Ark.Pipes.Notifier.AlwaysUnreliable; }
        }

        static public implicit operator Provider<T>(T value) {
            return new Constant<T>(value);
        }

        //static public implicit operator Provider<T>(object value) {
        //    Type thisType = typeof(T);
        //    Type otherType = typeof(object);
        //    if (thisType == otherType) {
        //        throw new NotSupportedException(string.Format("Cannot convert {0} to {1}", otherType, thisType));
        //    }
        //    return new Constant<T>(value);
        //}

        //[System.Runtime.CompilerServices.SpecialName]
        //static public Provider<T> op_Implicit<T1>(Provider<T1> value) where T1 : T {
        //    Type thisType = typeof(T);
        //    Type otherType = typeof(T1);
        //    if (!thisType.IsAssignableFrom(otherType)) {
        //        throw new NotSupportedException(string.Format("Cannot convert {0} to {1}", otherType, thisType));
        //    }
        //    return new Function<T1, T>((v) => (T)Convert.ChangeType(v, thisType), value);
        //}

        static public implicit operator Provider<T>(Func<T> value) {
            return new Function<T>(value);
        }

        [System.Runtime.CompilerServices.SpecialName]
        static public Provider<T> op_Implicit(INotifyingOut<T> value) {
            return new Function<T>(value);
        }

        [System.Runtime.CompilerServices.SpecialName]
        static public Provider<T> op_Implicit<T1>(Func<T1, T> value) {
            return new Function<T1, T>(value);
        }

        static public implicit operator Provider<T>(Func<T, T> value) {
            return new Function<T, T>(value);
        }

        static public implicit operator T(Provider<T> provider) {
            return provider.Value;
        }

        public Provider<T> AddChangeTrigger(Action<Action> changedTriggerSetter) {
            return new Function<T>(GetValue, changedTriggerSetter);
        }
        public Provider<T> AddChangeTrigger(ITrigger changedTrigger) {
            return new Function<T>(GetValue, changedTrigger);
        }

        #region Syntax-sugar factories
        static public Constant<T> Create(T value) {
            return new Constant<T>(value);
        }

        public static Function<T> Create(Func<T> function) {
            return new Function<T>(function);
        }

        static public Function<T> Create(Func<T> function, Action<Action> changedTriggerSetter) {
            return new Function<T>(function, changedTriggerSetter);
        }

        static public Function<T> Create(Func<T> function, ITrigger changedTrigger) {
            return new Function<T>(function, changedTrigger);
        }

        public static Function<T1, T> Create<T1>(Func<T1, T> function) {
            return new Function<T1, T>(function);
        }

        public static Function<T1, T> Create<T1>(Func<T1, T> function, Provider<T1> arg) {
            return new Function<T1, T>(function, arg);
        }

        public static Function<T1, T2, T> Create<T1, T2>(Func<T1, T2, T> function) {
            return new Function<T1, T2, T>(function);
        }

        public static Function<T1, T2, T> Create<T1, T2>(Func<T1, T2, T> function, Provider<T1> arg1, Provider<T2> arg2) {
            return new Function<T1, T2, T>(function, arg1, arg2);
        }

        public static Function<T1, T2, T3, T> Create<T1, T2, T3>(Func<T1, T2, T3, T> function) {
            return new Function<T1, T2, T3, T>(function);
        }

        public static Function<T1, T2, T3, T> Create<T1, T2, T3>(Func<T1, T2, T3, T> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3) {
            return new Function<T1, T2, T3, T>(function, arg1, arg2, arg3);
        }

        public static Function<T1, T2, T3, T4, T> Create<T1, T2, T3, T4>(Func<T1, T2, T3, T4, T> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3, Provider<T4> arg4) {
            return new Function<T1, T2, T3, T4, T>(function, arg1, arg2, arg3, arg4);
        }
        #endregion
    }

    public abstract class ProviderWithNotifier<T> : Provider<T> {
        protected PrivateNotifier _notifier = new PrivateNotifier();

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }
}