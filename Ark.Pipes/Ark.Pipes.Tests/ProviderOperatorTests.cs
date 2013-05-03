using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ark.Pipes;
using System.Windows;

namespace Ark.Pipes.Tests {
    [TestClass]
    public class ProviderOperatorTests {
        [TestMethod]
        public void TestProviderOperators1() {
            Provider.Operators.Arithmetic.Addition.SetHandler((String a, String b) => a + b);
            var pa = Provider.Create("aaa");
            var pc = Provider.Create("ccc");
            var pResult = pa + "bbb" + pc;
            string actualResult = pResult;
            string expectedResult = "aaabbbccc";
            Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void TestProviderOperators2() {
            Provider.Operators.Arithmetic.Multiply.SetHandler((Vector a, double b) => a * b);
            Provider.Operators.Arithmetic.Multiply.SetHandler((double a, Vector b) => a * b);
            var vector = new Vector(1, 2);
            var vectors = Provider.Create(vector);
            var multiplier = 3.0;
            var multipliers = Provider.Create(multiplier);
            var results1a = multipliers * vectors;
            var results1b = vectors * multipliers;
            var results2a = multiplier * vectors;
            var results2b = vectors * multiplier;
            var results3a = multipliers * vector; //constant vector =(
            var results3b = vector * multipliers; //constant vector =(
            var results4a = multiplier * vector;
            var results4b = vector * multiplier;
            //string actualResult = results1;
            //string expectedResult = "aaabbbccc";
            //Assert.AreEqual(actualResult, expectedResult);
        }

        [TestMethod]
        public void TestCustomOperatorRegistration() {
            Provider.Operators.Arithmetic.Addition.SetHandler((float a, float b) => a + b);
            var constant = Provider.Create(3.0f);
            var provider = Provider.Create(() => 2.0f);

            var result0 = Provider.Operators.Arithmetic.Addition.GetProvider(provider, provider);
            var result1 = Provider.Operators.Arithmetic.Addition.GetProvider(constant, provider);
            var result2 = Provider.Operators.Arithmetic.Addition.GetProvider(provider, constant);

            Assert.AreEqual(4.0, result0.Value);
            Assert.AreEqual(5.0, result1.Value);
            Assert.AreEqual(5.0, result2.Value);
        }


        [TestMethod]
        public void TestDefaultOperatorRegistration() {
            var constant = Provider.Create(3.0);
            var provider = Provider.Create(() => 2.0);

            var result0 = Provider.Operators.Arithmetic.Addition.GetProvider(provider, provider);
            var result1 = Provider.Operators.Arithmetic.Addition.GetProvider(constant, provider);
            var result2 = Provider.Operators.Arithmetic.Addition.GetProvider(provider, constant);

            Assert.AreEqual(4.0, result0.Value);
            Assert.AreEqual(5.0, result1.Value);
            Assert.AreEqual(5.0, result2.Value);
        }
    }
}
