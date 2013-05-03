using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class HashedWeakReferenceTests {
        [TestMethod]
        public void TestHashedWeakReferenceComparison() {
            var target = "target";
            var reference1 = new HashedWeakReference<string>(target);
            var reference2 = new HashedWeakReference<string>(target);

            Assert.AreEqual(reference1.GetHashCode(), reference2.GetHashCode());
            //The hash code is no longer  target.GetHashCode()
            //Assert.AreEqual(reference1.GetHashCode(), target.GetHashCode());
            Assert.AreEqual(reference1.GetHashCode(), RuntimeHelpers.GetHashCode(target));

            //Equality tests
            Assert.IsTrue(reference1 == reference2);
            Assert.IsFalse(reference1 != reference2);
            Assert.IsTrue(reference1.Equals(target));
            Assert.IsTrue(reference1.Equals((object)target));
            Assert.IsTrue(reference1.Equals(reference2));
            Assert.IsTrue(reference1.Equals((object)reference2));
            Assert.IsFalse(reference1.Equals(13));
            Assert.IsFalse(reference1 == null);
            Assert.IsFalse(reference1.Equals((string)null));
            Assert.IsFalse(reference1.Equals((HashedWeakReference<string>)null));
            Assert.IsTrue((HashedWeakReference<string>)null == (HashedWeakReference<string>)null); //Is this correct?
            try {
                new HashedWeakReference<string>(null);
                Assert.Fail();
            } catch { }
        }

        [TestMethod]
        public void TestHashedWeakReferenceGC() {
            var target = new List<string>();
            var reference1 = new HashedWeakReference<List<string>>(target);
            var reference2 = new HashedWeakReference<List<string>>(target);

            Assert.IsTrue(reference1 == reference2);
            Assert.IsTrue(reference1.Equals(target));

            target = null;
            GC.Collect();

            Assert.IsFalse(reference1 == reference2);
        }


        static Action GenerateActionOfLocalStruct() {
            return (new NormalStruct()).StructMethod;
        }

        static Action ExtractReferenceAction(ref NormalStruct s) {
            return s.StructMethod;
        }

        //static Action ChangeRefClass(ref NormalClass s) {
        //    return () => { s = null; };
        //}

        //static async void ChangeRefClass(ref NormalClass s) {
        //    await new System.Threading.Tasks.Task(() => { });
        //}

        [TestMethod]
        public void TestDelegateTargets() {
            var normalClass = new NormalClass();
            var normalStruct = new NormalStruct();
            Action staticDelegate = StaticClass.StaticMethod;
            Action classDelegate = normalClass.ClassMethod;
            Action structDelegate = normalStruct.StructMethod;
            Action localStructDelegate = GenerateActionOfLocalStruct(); //((Func<Action>)(() => { return (new NormalStruct()).StructMethod; }))();
            Action structReferenceDelegate = ExtractReferenceAction(ref normalStruct);

            var staticTarget = staticDelegate.Target;
            var classTarget = classDelegate.Target;
            var structTarget = structDelegate.Target; //((NormalStruct)structReferenceTarget).StructMethod() doesn't work
            var localStructTarget = localStructDelegate.Target;
            var structReferenceTarget = structReferenceDelegate.Target; //((NormalStruct)structReferenceTarget).StructMethod() doesn't work

            normalClass.ClassMethod();
            normalStruct.StructMethod();
        }

    }
}
