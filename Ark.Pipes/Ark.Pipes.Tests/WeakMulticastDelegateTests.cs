using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Collections.Generic;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class WeakMulticastDelegateTests {
        [TestMethod]
        public void TestWeakMulticastDelegateAddAutoRemove() {
            const int count = 100;
            var handlers = new List<Action>();
            var weakReferences = new List<WeakReference>();

            int testValue = 0;
            ((Action)(() => {
                for (int i = 0; i < count; i++) {
                    int number = i;
                    Action handler = () => testValue += number;
                    handler += () => testValue += number * 2;
                    handlers.Add(handler);
                    weakReferences.Add(new WeakReference(handler));
                }
            }))();

            Action normalDelegate = null;
            WeakMulticastDelegate<Action> fastDelegate = new WeakMulticastDelegate<Action>();

            ((Action)(() => {
                foreach (var handler in handlers) {
                    normalDelegate += handler;
                    fastDelegate.AddHandler(handler);
                }
            }))();

            {
                {
                    testValue = 0;
                    normalDelegate();
                    var expectedResult = testValue;

                    testValue = 0;
                    fastDelegate.Invoker();
                    var obtainedResult = testValue;

                    Assert.AreEqual(expectedResult, obtainedResult, "Delegate sets don't match.");
                }

                var rng = new Random(666);

                for (int i = 0; i < count; i++) {
                    int idx = rng.Next(count);
                    ((Action)(() => {
                        if (handlers[idx] != null) {
                            var handler = handlers[idx];
                            handlers[idx] = null;
                            normalDelegate -= handler;
                            //fastDelegate.RemoveHandler(handler); //We want to check auto-removal of the dead handlers.
                            handler = null;
                        }
                    }))();

                    GC.Collect();
                    Assert.IsFalse(weakReferences[idx].IsAlive);

                    {
                        testValue = 0;
                        normalDelegate();
                        var expectedResult = testValue;

                        testValue = 0;
                        fastDelegate.Invoker();
                        var obtainedResult = testValue;

                        Assert.AreEqual(expectedResult, obtainedResult, "Delegate sets don't match.");

                        var expectedCount = normalDelegate.GetInvocationList().Count();
                        var obtainedCount = fastDelegate.Count();

                        Assert.AreEqual(expectedCount, obtainedCount, "Delegate sets sizes don't match.");
                    }
                }
            }
        }

        //[TestMethod]
        //public void TestWeakMulticastDelegateRemove() {
        //    const int count = 100;
        //    var targets = new List<NormalClass>();
        //    var weakReferences = new List<WeakReference>();
        //    WeakMulticastDelegate<Action> weakDelegate = new WeakMulticastDelegate<Action>();

        //    ((Action)(() => {
        //        for (int i = 0; i < count; i++) {
        //            var target = new NormalClass();
        //            targets.Add(target);
        //            Action handler = target.ClassMethod;
        //            weakReferences.Add(new WeakReference(handler));
        //            weakDelegate.AddHandler(handler);
        //        }
        //    }))();

        //    Assert.AreEqual(count, weakDelegate.Count());
            

        //    //weakDelegate.Invoke();
        //    GC.Collect();
        //    Assert.AreEqual(count, weakDelegate.Count());

        //    Action newHandler = targets[13].ClassMethod;
        //    weakDelegate.RemoveHandler(newHandler);

        //    Assert.AreEqual(weakDelegate.Count(), count - 1);
        //    weakDelegate.RemoveAll(newHandler);
        //    Assert.AreEqual(weakDelegate.Count(), count - 1);
        //}
    }
}
