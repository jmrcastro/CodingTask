using CodingTaskWindowsForms;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class BusinessLogicTest
    {
        BusinessLogic BL = new BusinessLogic();

        [TestMethod]
        public void TestGetResultSqrt()
        {
            Assert.AreEqual("8", BL.GetResult("sqrt(64)"));
        }

        [TestMethod]
        public void TestGetResultNegativeSqrt()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("sqrt(-1)"));
        }

        [TestMethod]
        public void TestGetResultEmptySqrt()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("sqrt()"));
        }

        [TestMethod]
        public void TestGetResultPow()
        {
            Assert.AreEqual("16", BL.GetResult("2^4"));
        }

        [TestMethod]
        public void TestGetResultEmptyPow()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("^4"));
        }

        [TestMethod]
        public void TestGetResultDiv()
        {
            Assert.AreEqual("3", BL.GetResult("99/33"));
        }

        [TestMethod]
        public void TestGetResultEmptyDiv()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("/33"));
        }

        [TestMethod]
        public void TestGetResultMul()
        {
            Assert.AreEqual("275", BL.GetResult("5*55"));
        }

        [TestMethod]
        public void TestGetResultEmptyMul()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("*55"));
        }

        [TestMethod]
        public void TestGetResultMinus()
        {
            Assert.AreEqual("11", BL.GetResult("33-22"));
        }

        [TestMethod]
        public void TestGetResultMinusMinus()
        {
            Assert.AreEqual("-55", BL.GetResult("-33-22"));
        }

        [TestMethod]
        public void TestGetResultPlus()
        {
            Assert.AreEqual("12", BL.GetResult("1+11"));
        }

        [TestMethod]
        public void TestGetResultPlusPlus()
        {
            Assert.AreEqual("12", BL.GetResult("+1+11"));
        }

        [TestMethod]
        public void TestGetResultComplex1()
        {
            Assert.AreEqual("22", BL.GetResult("2^2+3*6"));
        }

        [TestMethod]
        public void TestGetResultComplex2()
        {
            Assert.AreEqual("0", BL.GetResult("2+4/sqrt(4)-4"));
        }

        [TestMethod]
        public void TestGetResultComplex3()
        {
            Assert.AreEqual("-27", BL.GetResult("1+2-3*2*5"));
        }

        [TestMethod]
        public void TestGetResultComplex4()
        {
            Assert.AreEqual("-208", BL.GetResult("-5*-6*-7+2"));
        }

        [TestMethod]
        public void TestGetResultString()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("Test"));
        }

        [TestMethod]
        public void TestGetResultOneNumber()
        {
            Assert.AreEqual("5", BL.GetResult("5"));
        }

        [TestMethod]
        public void TestGetResultDivByZero()
        {
            Assert.AreEqual(BL.errorString, BL.GetResult("7/0"));
        }
    }
}
