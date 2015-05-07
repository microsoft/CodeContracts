namespace SystemRequiresVerificationTests
{
    using System;
    using System.Windows;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SystemRequiresTest
    {
        [TestMethod]
        public void Rect_ConstructorAcceptsNaN_Test()
        {
            var r1 = new Rect(0, 0, double.NaN, double.NaN);
            Assert.AreEqual(double.NaN, r1.Width);
        }

        [TestMethod]
        public void Rect_ConstructorAcceptsPositiveInfinity_Test()
        {
            var r1 = new Rect(0, 0, double.PositiveInfinity, double.PositiveInfinity);
            Assert.AreEqual(double.PositiveInfinity, r1.Width);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rect_ConstructorFailsNegativeInfinity_Test()
        {
            var r1 = new Rect(0, 0, double.NegativeInfinity, double.NegativeInfinity);
        }

        [TestMethod]
        public void Rect_AssignmentAcceptsNaN_Test()
        {
            var r1 = new Rect();

            r1.Width = double.NaN;
            Assert.AreEqual(double.NaN, r1.Width);
        }

        [TestMethod]
        public void Rect_AssignmentAcceptsPositiveInfinity_Test()
        {
            var r1 = new Rect();

            r1.Width = double.PositiveInfinity;
            Assert.AreEqual(double.PositiveInfinity, r1.Width);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rect_AssignmentFailsNegativeInfinity_Test()
        {
            var r1 = new Rect();

            r1.Width = double.NegativeInfinity;
        }

        [TestMethod]
        public void Size_ConstructorAcceptsNaN_Test()
        {
            var r1 = new Size(double.NaN, double.NaN);
            Assert.AreEqual(double.NaN, r1.Width);
        }

        [TestMethod]
        public void Size_ConstructorAcceptsPositiveInfinity_Test()
        {
            var r1 = new Size(double.PositiveInfinity, double.PositiveInfinity);
            Assert.AreEqual(double.PositiveInfinity, r1.Width);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Size_ConstructorFailsNegativeInfinity_Test()
        {
            var r1 = new Size(double.NegativeInfinity, double.NegativeInfinity);
        }

        [TestMethod]
        public void Size_AssignmentAcceptsNaN_Test()
        {
            var r1 = new Size();

            r1.Width = double.NaN;
            Assert.AreEqual(double.NaN, r1.Width);
        }

        [TestMethod]
        public void Size_AssignmentAcceptsPositiveInfinity_Test()
        {
            var r1 = new Size();

            r1.Width = double.PositiveInfinity;
            Assert.AreEqual(double.PositiveInfinity, r1.Width);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Size_AssignmentFailsNegativeInfinity_Test()
        {
            var r1 = new Size();

            r1.Width = double.NegativeInfinity;
        }
    }
}
