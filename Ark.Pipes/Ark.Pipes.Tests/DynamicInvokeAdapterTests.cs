using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Collections.Generic;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class DynamicInvokeAdapterTests {
        [TestMethod]
        public void TestInvocationArgumentsPacker() {
            bool flag2 = false;
            Func<object[], object> dynamicHandler = (args) => (int)args[0] * 2;
            Func<object[], object> dynamicHandler2 = (args) => flag2 = true; ;
            Func<object[], object> dynamicHandler3 = (args) => 42;
            Func<object[], object> dynamicHandler4 = (args) => 42;

            //var adapter = new DynamicInvokeAdapter<Func<int, int>>(dynamicHandler);
            //var res = adapter.Invoke(10);
            //Assert.AreEqual(20, res);

            //var adapter2 = new DynamicInvokeAdapter<Action>(dynamicHandler2);
            //adapter2.Invoke();
            //Assert.IsTrue(flag2);

            var adapter3 = new DynamicInvokeAdapter<Func<int>>(dynamicHandler3);
            var res3 = adapter3.Invoke();
            Assert.AreEqual(42, res3);

            bool flag4 = false;
            var adapter4 = new DynamicInvokeAdapter<Action<int>>((args) => { flag4 = true; return 0; });
            adapter4.Invoke(42);
            Assert.IsTrue(flag4);

            bool flag5 = false;
            var adapter5 = new DynamicInvokeAdapter<Action>((args) => { flag5 = true; return null; });
            adapter5.Invoke();
            Assert.IsTrue(flag5);

            bool flag6 = false;
            var adapter6 = new DynamicInvokeAdapter<Action<bool>>((args) => { flag6 = (bool)args[0]; return null; });
            adapter6.Invoke(true);
            Assert.IsTrue(flag6);
        }

        //[TestMethod]
        public void TestInvocationArgumentsPacker2() {
            CreateDelegateTest.Main();
            //Action<int> actionMethod = CreateDelegateTest.GetActionDelegate<int>();
            //Func<int> functionMethod = CreateDelegateTest.GetFunctionDelegate<int>();

            //Action<object> dynamicAction = (o) => { };
            //Action<ValueType> dynamicAction2 = (o) => { };
            //Action<string> intAction = dynamicAction;
            //Action<int> intAction2 = dynamicAction2;


        }
    }

    //public class CreateDelegateTest {
    //    //public static void Main() {
    //    //    Action<int> actionMethod = CreateDelegateTest.GetActionDelegate<int>();
    //    //    Func<int> functionMethod = CreateDelegateTest.GetFunctionDelegate<int>();
    //    //}

    //    public static Func<TResult> GetFunctionDelegate<TResult>() {
    //        return (Func<TResult>)Delegate.CreateDelegate(typeof(Func<TResult>), typeof(CreateDelegateTest), "FunctionMethod");
    //    }

    //    public static Action<T> GetActionDelegate<T>() {
    //        return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), typeof(CreateDelegateTest), "ActionMethod");
    //    }

    //    public static TResult FunctionMethod<TResult>() {
    //        return default(TResult);
    //    }

    //    public static void ActionMethod<T>(T arg) {
    //    }
    //}

    public static class CreateDelegateTest {
        public static void Main() {
            Action<int> actionMethod = CreateDelegateTest.GetActionDelegate<int>();
            Func<int> functionMethod = CreateDelegateTest.GetFunctionDelegate<int>();
        }

        public static Func<TResult> GetFunctionDelegate<TResult>() {
            return (Func<TResult>)Delegate.CreateDelegate(typeof(Func<TResult>), typeof(CreateDelegateTest), "FunctionMethod");
        }

        public static Action<T> GetActionDelegate<T>() {
            return (Action<T>)Delegate.CreateDelegate(typeof(Action<T>), typeof(CreateDelegateTest), "ActionMethod");
        }

        public static TResult FunctionMethod<TResult>() {
            return default(TResult);
        }

        public static void ActionMethod<T>(T arg) {
        }
    }
}
