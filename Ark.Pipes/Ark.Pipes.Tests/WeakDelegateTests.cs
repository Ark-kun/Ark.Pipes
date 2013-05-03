using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Collections.Generic;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class WeakDelegateTests {
        [TestMethod]
        public void TestWeakDelegateEquals() {
            var target1 = new NormalClass();
            var target2 = new NormalClass();
            Action handler1 = target1.ClassMethod;
            Action handler2 = target2.ClassMethod;
            var weakDelegate1 = new WeakDelegate<Action>(handler1);
            var weakDelegate1a = new WeakDelegate<Action>(handler1);
            var weakDelegate2 = new WeakDelegate<Action>(handler2);

            Assert.AreEqual(weakDelegate1.GetHashCode(), weakDelegate1a.GetHashCode());
            //The hash code is no longer  target.GetHashCode()
            //Assert.AreEqual(weakDelegate1.GetHashCode(), handler1.GetHashCode());
            Assert.AreEqual(weakDelegate1.GetHashCode(), handler1.GetGoodHashCode());

            //Equality tests
            Assert.IsTrue(weakDelegate1 == weakDelegate1a);
            Assert.IsFalse(weakDelegate1 == weakDelegate2);
            Assert.IsTrue(weakDelegate1 != weakDelegate2);
            Assert.IsTrue(weakDelegate1.Equals(handler1));
            Assert.IsTrue(weakDelegate1.Equals((object)handler1));
            Assert.IsFalse(weakDelegate1.Equals(handler2));
            Assert.IsFalse(weakDelegate1.Equals((object)handler2));
            Assert.IsTrue(weakDelegate1.Equals(weakDelegate1a));
            Assert.IsTrue(weakDelegate1.Equals((object)weakDelegate1a));
            Assert.IsFalse(weakDelegate1.Equals(weakDelegate2));
            Assert.IsFalse(weakDelegate1.Equals((object)weakDelegate2));
            Assert.IsFalse(weakDelegate1.Equals(13));
            Assert.IsFalse(weakDelegate1 == null);
            Assert.IsFalse(weakDelegate1.Equals((string)null));
            Assert.IsFalse(weakDelegate1.Equals((HashedWeakReference<string>)null));
            Assert.IsTrue((HashedWeakReference<string>)null == (HashedWeakReference<string>)null); //Is this correct?
        }

        [TestMethod]
        public void TestWeakDelegateAutoDeregistration() {
            Action eventHandlers = null;

            Action handler1 = new NormalClass().ClassMethod;
            WeakReference reference1 = new WeakReference(handler1);
            //WeakDelegate<Action> weakDelegate1 = new WeakDelegate<Action>(handler1);

            eventHandlers += handler1.Weaken(h => eventHandlers -= h);

            Assert.IsTrue(reference1.IsAlive, "The reference must be alive at this point.");

            GC.KeepAlive(handler1);
            handler1 = null;
            GC.Collect();

            Assert.IsFalse(reference1.IsAlive, "The reference must be dead now.");

            eventHandlers();
            Assert.IsNull(eventHandlers, "The WeakDelegate must unsubscribe itself when it notices that the reference is dead.");

        }
    }
}
