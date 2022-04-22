namespace GrampsView.Data.Model.Tests
{
    using GrampsView.Data.Model;
    using GrampsView.e2e.Test.Utility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SharedSharp.Model;

    [TestClass]
    public class DateObjectModelTests
    {
        private DateObjectModelAbstractTest compareVal;
        private DateObjectModelAbstractTest testVal;

        [TestMethod]
        public void AsCardListLineTest()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (!string.IsNullOrEmpty(AsCardListLineTest_Basic.Title))
            {
                Assert.Fail();
                return;
            }

            Assert.IsTrue(AsCardListLineTest_Basic.Count == 0);
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void CompareTest()
        {
            Assert.IsTrue(testVal.Compare(testVal, compareVal) == 0);
        }

        [TestMethod]
        public void CompareToTest()
        {
            Assert.IsTrue(testVal.CompareTo(compareVal) == 0);
        }

        [TestMethod]
        public void CompareToTest1()
        {
            IDateObjectModel compareVal1 = new DateObjectModelAbstractTest();

            Assert.IsTrue(testVal.CompareTo(compareVal1) == 0);
        }

        [TestMethod]
        public void DateDifferenceDecodedTest()
        {
            Assert.IsTrue(testVal.DateDifferenceDecoded(compareVal) == "0 years");
        }

        [TestMethod]
        public void DateDifferenceTest()
        {
            Assert.IsTrue(testVal.DateDifference(compareVal) == new System.TimeSpan());
        }

        [TestMethod]
        public void DateObjectModelTest()
        {
            //Assert.t();
        }

        [TestMethod]
        public void EqualsTest()
        {
            Assert.IsTrue(testVal == compareVal);
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            Assert.IsTrue(testVal.GetHashCode() == compareVal.GetHashCode());
        }

        [TestInitialize]
        public void Init()
        {
            testVal = new DateObjectModelAbstractTest();

            compareVal = new DateObjectModelAbstractTest();
        }
    }
}