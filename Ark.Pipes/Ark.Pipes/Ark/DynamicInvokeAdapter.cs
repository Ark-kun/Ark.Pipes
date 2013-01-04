using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ark {
    class DynamicInvokeAdapter<TDelegate> where TDelegate : class {
        static Func<Func<object[], object>, TDelegate> _factory;
        TDelegate _invokeHandler;

        static DynamicInvokeAdapter() {
            if (!typeof(TDelegate).IsSubclassOf(typeof(Delegate))) {
                throw new InvalidOperationException("The TDelegate generic parameter of must be a delegate type.");
            }

            //_factory
            var methods = typeof(DynamicInvokeAdapter<TDelegate>).GetMethod("BuildInvoker_x", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.InvokeMethod | BindingFlags.IgnoreCase);
            var invokeBuilder = Delegate.CreateDelegate(typeof(TDelegate), typeof(DynamicInvokeAdapter<TDelegate>), "BuildInvoker_x", true, false);
            if (invokeBuilder == null) {
                throw new NotImplementedException(string.Format("Delegates of type {0} are not supported. Only delegates with 0-4 [generic] by-value parameters and no return value are supported.", typeof(TDelegate)));
            }
            int parameterCount = invokeBuilder.Method.GetParameters().Length;
            invokeBuilder.DynamicInvoke(new object[parameterCount]);
        }

        public DynamicInvokeAdapter(Func<object[], object> dynamicHandler) {
            _invokeHandler = _factory(dynamicHandler);
        }

        public TDelegate Invoke {
            get { return _invokeHandler; }
        }

        public static void BuildInvoker_x() {
            _factory = (h) => (TDelegate)(object)(new Action(() => h(null)));
        }

        public static void BuildInvoker_x<T>(T a) {
            _factory = (h) => (TDelegate)(object)(new Action<T>((arg) => h(new object[] { arg })));
        }

        static void BuildInvoker_x<T1, T2>(T1 a1, T2 a2) {
            _factory = (h) => (TDelegate)(object)(new Action<T1, T2>((arg1, arg2) => h(new object[] { arg1, arg2 })));
        }

        static void BuildInvoker_x<T1, T2, T3>(T1 a1, T2 a2, T3 a3) {
            _factory = (h) => (TDelegate)(object)(new Action<T1, T2, T3>((arg1, arg2, arg3) => h(new object[] { arg1, arg2, arg3 })));
        }

        static void BuildInvoker_x<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4) {
            _factory = (h) => (TDelegate)(object)(new Action<T1, T2, T3, T4>((arg1, arg2, arg3, arg4) => h(new object[] { arg1, arg2, arg3, arg4 })));
        }

        public static TResult BuildInvoker_X<TResult>() {
            _factory = (h) => (TDelegate)(object)(new Func<TResult>(() => (TResult)h(new object[] { })));
            return default(TResult);
        }

        public static TResult BuildInvoker_X<T, TResult>(T a) {
            _factory = (h) => (TDelegate)(object)(new Func<T, TResult>((arg) => (TResult)h(new object[] { arg })));
            return default(TResult);
        }

        static TResult BuildInvoker_X<T1, T2, TResult>(T1 a1, T2 a2) {
            _factory = (h) => (TDelegate)(object)(new Func<T1, T2, TResult>((arg1, arg2) => (TResult)h(new object[] { arg1, arg2 })));
            return default(TResult);
        }

        static TResult BuildInvoker_X<T1, T2, T3, TResult>(T1 a1, T2 a2, T3 a3) {
            _factory = (h) => (TDelegate)(object)(new Func<T1, T2, T3, TResult>((arg1, arg2, arg3) => (TResult)h(new object[] { arg1, arg2, arg3 })));
            return default(TResult);
        }

        static TResult BuildInvoker_X<T1, T2, T3, T4, TResult>(T1 a1, T2 a2, T3 a3, T4 a4) {
            _factory = (h) => (TDelegate)(object)(new Func<T1, T2, T3, T4, TResult>((arg1, arg2, arg3, arg4) => (TResult)h(new object[] { arg1, arg2, arg3, arg4 })));
            return default(TResult);
        }
    }
}
