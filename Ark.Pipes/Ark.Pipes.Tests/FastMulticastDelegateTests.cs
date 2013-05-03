using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Collections.Generic;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class FastMulticastDelegateTests {
        [TestMethod]
        public void TestFastMulticastDelegate() {
            var handlers = new List<Action>();

            int testValue = 0;
            for (int i = 0; i < 100; i++) {
                int number = i;
                Action handler = () => testValue += number;
                handler += () => testValue += number * 2;
                handlers.Add(handler);
            }

            Action normalDelegate = null;
            FastMulticastAction fastDelegate = new FastMulticastAction();

            foreach (var handler in handlers) {
                normalDelegate += handler;
                fastDelegate.AddHandler(handler);
            }

            {
                testValue = 0;
                normalDelegate();
                var expectedResult = testValue;

                testValue = 0;
                fastDelegate.Invoke();
                var obtainedResult = testValue;

                Assert.AreEqual(expectedResult, obtainedResult);
            }

            var rng = new Random(666);

            for (int i = 0; i < 100; i++) {
                int idx = rng.Next(100);
                var handler = handlers[idx];
                normalDelegate -= handler;
                fastDelegate.RemoveHandler(handler);

                {
                    testValue = 0;
                    normalDelegate();
                    var expectedResult = testValue;

                    testValue = 0;
                    fastDelegate.Invoke();
                    var obtainedResult = testValue;

                    Assert.AreEqual(expectedResult, obtainedResult);
                }
            }
        }
    }
}
