using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Collections.Generic;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class WeaknessTests {
        [TestMethod]
        public void TestGC() {
            var target = new object();
            var reference = new WeakReference(target);

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object must be alive.");

            GC.KeepAlive(target);
            target = null;
            GC.Collect();
            Assert.IsFalse(reference.IsAlive, "Object must be collected.");
            var Σ = 1;
        }

        [TestMethod]
        public void TestGC2() {
            Action handler1 = new NormalClass().ClassMethod;
            Action handler2 = new NormalClass().ClassMethod;

            var reference1 = new WeakReference(handler1);
            var reference2 = new WeakReference(handler2);

            Action eventHandler = null;
            eventHandler += handler2;
            eventHandler -= handler2;

            GC.Collect();
            Assert.IsTrue(reference1.IsAlive, "Object must be alive.");
            Assert.IsTrue(reference2.IsAlive, "Object must be alive.");

            GC.KeepAlive(handler1);
            GC.KeepAlive(handler2);
            handler1 = null;
            handler2 = null;

            GC.Collect();
            Assert.IsFalse(reference1.IsAlive, "Object must be collected.");
            Assert.IsFalse(reference2.IsAlive, "Object must be collected.");
        }

        //[TestMethod]
        public void TestGC3_WTF() {
            var handlers = new List<Action>();
            var weakReferences = new List<WeakReference>();

            int testValue = 0;
            //((Action)(() => {
                //This code somehow leaves a strong reference to the last element (if it's not wrapped). (in debug)
                for (int i = 0; i < 20; i++) {
                    int number = i;
                    //Action handler = () => { }; //without <testValue> the lambda is static
                    //Action handler = () => testValue++; //without <number> the handlers somehow leak to the method level.
                    Action handler = () => testValue += number;
                    handlers.Add(handler);
                    weakReferences.Add(new WeakReference(handler));
                    handler = null;
                }
            //}))();

            //((Action)(() => {
            //Action eventHandler = null;
            //eventHandler -= handlers[13]; //WTF???! This line causes handler[13] not to be collected (in debug)
                handlers[13].ToString();
            //}))();

            //GC.Collect();
            //for (int i = 0; i < weakReferences.Count; i++) {
            //    Assert.IsTrue(weakReferences[i].IsAlive, string.Format("weakReferences[{0}] must be alive.", i));
            //}

            handlers.Clear();
            //Array.Clear(handlers, 0, handlers.Count());
            //((Action)(() => {
                //for (int i = 0; i < handlers.Count; i++) {
                //    handlers[i] = null;
                //}
            //}))();

            GC.Collect();

            for (int i = 0; i < -1; ) { } //This for loop is required for the bug to occur.

            var aliveReferences = Enumerable.Range(0, weakReferences.Count).Where(i => weakReferences[i].IsAlive).ToArray();
            Assert.IsFalse(aliveReferences.Any(), string.Format("weakReferences ({0}) must be collected.", string.Join(",", aliveReferences)));
        }

        //[TestMethod]
        public void TestGC3_WTF_clean() {
            var handlers = new List<Action>();
            var weakReferences = new List<WeakReference>();

            int testValue = 0;
            //((Action)(() => {
            for (int i = 0; i < 20; i++) {
                int number = i;
                Action handler = () => testValue += number;
                handlers.Add(handler);
                weakReferences.Add(new WeakReference(handler));
                handler = null;
            }
            //}))();
            //((Action)(() => {
                handlers[13].ToString();
            //}))();

            handlers.Clear();

            GC.Collect();

            //for (int i = 0; i < -1; ) { } //bug.
            //for (testValue = 0; testValue < -1; ) { } //bug
            //if (testValue == 0) { } //bug
            //testValue = 0; //no bug
            if (false) { } //This is required for the bug to occur.

            var aliveReferences = Enumerable.Range(0, weakReferences.Count).Where(i => weakReferences[i].IsAlive).ToArray();
            //Console.WriteLine("Uncollected handlers: {0}", string.Join(",", aliveReferences));
            Assert.IsFalse(aliveReferences.Any(), string.Format("weakReferences ({0}) must be collected.", string.Join(",", aliveReferences)));
        }

        //[TestMethod]
        public void TestGC3_WTF_clean_noBug() {
            var handlers = new List<Action>();
            var weakReferences = new List<WeakReference>();

            int testValue = 0;
            //((Action)(() => {
            for (int i = 0; i < 20; i++) {
                int number = i;
                Action handler = () => testValue += number;
                handlers.Add(handler);
                weakReferences.Add(new WeakReference(handler));
                handler = null;
            }
            //}))();
            //((Action)(() => {
            handlers[13].ToString();
            //}))();

            handlers.Clear();

            GC.Collect();

            //for (testValue = 0; testValue < -1; ) { } //bug. This for loop is required for the bug to occur.
            //if (testValue == 0) { } //bug.
            //testValue = 0; //no bug.            

            var aliveReferences = Enumerable.Range(0, weakReferences.Count).Where(i => weakReferences[i].IsAlive).ToArray();
            Assert.IsFalse(aliveReferences.Any(), string.Format("weakReferences ({0}) must be collected.", string.Join(",", aliveReferences)));
        }


        [TestMethod]
        public void TestEventHandlersGC() {
            var target = new object();
            var reference = new WeakReference(target);

            Func<string> handlers = null;
            handlers += target.ToString;

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object must be alive.");

            GC.KeepAlive(target);
            target = null;

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object should still be alive because of the strong events.");
            GC.KeepAlive(handlers);
        }

        void DebugInstanceMethod() {
            System.Diagnostics.Debug.WriteLine("DebugInstanceMethod called");
        }

        [TestMethod]
        public void TestNotifierGC() {
            var target = new NormalClass();
            var reference = new WeakReference(target);

            INotifier notifier = new PrivateNotifier();
            notifier.ValueChanged += target.ClassMethod;

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object must be alive.");

            GC.KeepAlive(target);
            target = null;

            GC.Collect();
            Assert.IsFalse(reference.IsAlive, "Object must be collected.");

            GC.KeepAlive(notifier);
        }

        class TestHelper {
            bool _value = false;

            public void SetTrue() {
                _value = true;
            }

            public void SetFalse() {
                _value = true;
            }

            public bool Value { get { return _value; } }
        }


        [TestMethod]
        public void TestNotifierGC2() {
            var target = new TestHelper();
            var reference = new WeakReference(target);

            Assert.IsFalse(target.Value, "Object must be false.");

            PrivateNotifier notifier = new PrivateNotifier();
            notifier.ValueChanged += target.SetTrue;

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object must be alive 1.");

            notifier.SignalValueChanged();
            Assert.IsTrue(target.Value, "Object must be true.");
            target.SetFalse();

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object must be alive 2.");

            GC.KeepAlive(target);
            target = null;
            //xxx
            Assert.IsTrue(reference.IsAlive, "Object must be alive 3.");


            notifier.SignalValueChanged();
            Assert.IsTrue(((TestHelper)reference.Target).Value, "Object must be true.");
            ((TestHelper)reference.Target).SetFalse();

            GC.Collect();
            Assert.IsFalse(reference.IsAlive, "Object must be collected 5.");

            GC.KeepAlive(notifier);
        }

        [TestMethod]
        public void TestNotifierLambdaGC() {
            var target = new object();
            var reference = new WeakReference(target);
            Action handler = null;
                        
            ((Action)(() => {
                var targetCopy = target;
                handler = () => targetCopy.ToString();
            }))();

            INotifier notifier = new PrivateNotifier();
            notifier.ValueChanged += handler;

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Object must be alive.");

            GC.KeepAlive(target);
            target = null; //modifies the closure!

            GC.Collect();
            Assert.IsTrue(reference.IsAlive, "Lambdas should be registered strongly.");

            notifier.ValueChanged -= handler;
            handler = null;

            GC.Collect();

            Assert.IsFalse(reference.IsAlive, "Lambdas should be unsubscribed and collected.");

            GC.KeepAlive(notifier);
        }
    }
}
