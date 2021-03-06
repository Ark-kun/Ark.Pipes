﻿using System;

namespace Ark.Pipes {
    sealed class Function<T, TResult> : 
#if NOTIFICATIONS_DISABLE
        Provider<TResult> 
#else
        ProviderWithNotifier<TResult> 
#endif 
    {
        Func<T, TResult> _function;
        Provider<T> _arg;

        public Function(Func<T, TResult> function, Provider<T> arg) {
            _function = function;
            _arg = arg;
#if !NOTIFICATIONS_DISABLE
            _notifier.Source = _arg.Notifier;
#endif 
        }

        public override TResult GetValue() {
            return _function(_arg.Value);
        }
    }

    sealed class Function<T1, T2, TResult> : Provider<TResult> {
        Func<T1, T2, TResult> _function;
        Provider<T1> _arg1;
        Provider<T2> _arg2;
#if !NOTIFICATIONS_DISABLE
        ArrayNotifier _notifier = new ArrayNotifier(2);
#endif 

        public Function(Func<T1, T2, TResult> function, Provider<T1> arg1, Provider<T2> arg2) {
            _function = function;
            _arg1 = arg1;
            _arg2 = arg2;
#if !NOTIFICATIONS_DISABLE
            _notifier[0] = _arg1.Notifier;
            _notifier[1] = _arg2.Notifier;
#endif 
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value);
        }

#if !NOTIFICATIONS_DISABLE
        public override INotifier Notifier {
            get { return _notifier; }
        }
#endif 
    }

    sealed class Function<T1, T2, T3, TResult> : Provider<TResult> {
        Func<T1, T2, T3, TResult> _function;
        Provider<T1> _arg1;
        Provider<T2> _arg2;
        Provider<T3> _arg3;
#if !NOTIFICATIONS_DISABLE
        ArrayNotifier _notifier = new ArrayNotifier(3);
#endif 

        public Function(Func<T1, T2, T3, TResult> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3) {
            _function = function;
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
#if !NOTIFICATIONS_DISABLE
            _notifier[0] = _arg1.Notifier;
            _notifier[1] = _arg2.Notifier;
            _notifier[2] = _arg3.Notifier;
#endif 
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value, _arg3.Value);
        }

#if !NOTIFICATIONS_DISABLE
        public override INotifier Notifier {
            get { return _notifier; }
        }
#endif 
    }

    sealed class Function<T1, T2, T3, T4, TResult> : Provider<TResult> {
        Func<T1, T2, T3, T4, TResult> _function;
        Provider<T1> _arg1;
        Provider<T2> _arg2;
        Provider<T3> _arg3;
        Provider<T4> _arg4;
#if !NOTIFICATIONS_DISABLE
        ArrayNotifier _notifier = new ArrayNotifier(4);
#endif 

        public Function(Func<T1, T2, T3, T4, TResult> function, Provider<T1> arg1, Provider<T2> arg2, Provider<T3> arg3, Provider<T4> arg4) {
            _function = function;
            _arg1 = arg1;
            _arg2 = arg2;
            _arg3 = arg3;
            _arg4 = arg4;
#if !NOTIFICATIONS_DISABLE
            _notifier[0] = _arg1.Notifier;
            _notifier[1] = _arg2.Notifier;
            _notifier[2] = _arg3.Notifier;
            _notifier[3] = _arg4.Notifier;
#endif 
        }

        public override TResult GetValue() {
            return _function(_arg1.Value, _arg2.Value, _arg3.Value, _arg4.Value);
        }

#if !NOTIFICATIONS_DISABLE
        public override INotifier Notifier {
            get { return _notifier; }
        }
#endif 
    }
}