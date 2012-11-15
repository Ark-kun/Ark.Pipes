using Ark.Pipes.Operators;
using System;
using System.ComponentModel;

namespace Ark.Pipes {
    public abstract partial class Provider<T> :
#if NOTIFICATIONS_DISABLE
        IOut<T>
#else
        INotifyingOut<T>, INotifyPropertyChanged
#endif
    {
        public abstract T GetValue();

        public T Value {
            get { return GetValue(); }
        }

#if !NOTIFICATIONS_DISABLE
        public virtual INotifier Notifier {
            get { return Ark.Pipes.Notifier.AlwaysUnreliable; }
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged {
            //FIX: To prevent memory leaks/premature garbage collection we need to strongly subscribe a handler with a weak reference to the value delegate target.
            add { Notifier.ValueChanged += new PropertyChangedClosure(value, this).Invoke; }
            remove { Notifier.ValueChanged -= new PropertyChangedClosure(value, this).Invoke; }
        }
#endif
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
            return Source<T>.Create(value);
        }

#if !NOTIFICATIONS_DISABLE
        [System.Runtime.CompilerServices.SpecialName]
        static public Provider<T> op_Implicit(INotifyingOut<T> value) {
            return Source<T>.Create(value);
        }
#endif

        static public implicit operator T(Provider<T> provider) {
            return provider.Value;
        }
    }

    public static class Provider {
        #region Syntax-sugar factories
        static public Provider<T> Create<T>(T value) {
            return new Constant<T>(value);
        }

        public static Provider<T> Create<T>(Func<T> function) {
            return Source<T>.Create(function);
        }

#if !NOTIFICATIONS_DISABLE
        static public Provider<T> Create<T>(Func<T> function, ITrigger changedTrigger) {
            return Source<T>.Create(function, changedTrigger);
        }
#endif

        public static Provider<T> Create<T1, T>(Func<T1, T> function, Provider<T1> arg) {
            return new Function<T1, T>(function, arg);
        }

        public static Provider<T> Create<T1, T2, T>(Func<T1, T2, T> function, Provider<T1> arg1, Provider<T2> arg2) {
            return new Function<T1, T2, T>(function, arg1, arg2);
        }

        public static Provider<T> Create<T1, T2, T3, T>(Func<T1, T2, T3, T> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3) {
            return new Function<T1, T2, T3, T>(function, arg1, arg2, arg3);
        }

        public static Provider<T> Create<T1, T2, T3, T4, T>(Func<T1, T2, T3, T4, T> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3, Provider<T4> arg4) {
            return new Function<T1, T2, T3, T4, T>(function, arg1, arg2, arg3, arg4);
        }
        #endregion

        static ProviderOperators _operators;

        static Provider() {
            typeof(Ark.Pipes.Provider<int>).ToString();
            _operators = new ProviderOperators();
            Ark.Pipes.Generated.OperatorRegistrator.RegisterHandlers(_operators);
        }

        public static ProviderOperators Operators { get { return _operators; } }
    }
#if !NOTIFICATIONS_DISABLE
    public abstract class ProviderWithNotifier<T> : Provider<T> {
        protected PrivateNotifier _notifier = new PrivateNotifier();

        public override INotifier Notifier {
            get { return _notifier; }
        }
    }
#endif
}