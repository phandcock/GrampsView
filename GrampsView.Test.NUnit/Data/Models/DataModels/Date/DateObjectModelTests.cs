using GrampsView.Data.Model;
using GrampsView.Test.NUnit.Utility;

using NUnit.Framework;

using SharedSharp.Model;

namespace GrampsView.Test.NUnit.Data.Models.DataModels.Date
{
    [TestFixture()]
    public class DateObjectModelTests
    {
        private DateObjectModelAbstractTest compareVal = new();
        private DateObjectModelAbstractTest testVal = new();

        [Test()]
        public void AsCardListLineTest()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (!string.IsNullOrEmpty(AsCardListLineTest_Basic.Title))
            {
                Assert.Fail();
                return;
            }

            Assert.True(AsCardListLineTest_Basic.Count == 0);
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test()]
        public void CompareTest()
        {
            Assert.True(testVal.Compare(testVal, compareVal) == 0);
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.True(testVal.CompareTo(compareVal) == 0);
        }

        [Test()]
        public void CompareToTest1()
        {
            IDateObjectModel compareVal1 = new DateObjectModelAbstractTest();

            Assert.True(testVal.CompareTo(compareVal1) == 0);
        }

        [Test()]
        public void DateDifferenceDecodedTest()
        {
            Assert.True(testVal.DateDifferenceDecoded(compareVal) == "0 years");
        }

        [Test()]
        public void DateDifferenceTest()
        {
            Assert.True(testVal.DateDifference(compareVal) == new System.TimeSpan());
        }

        [Test()]
        public void DateObjectModelTest()
        {
            Assert.Pass();
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.True(testVal == compareVal);
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.True(testVal.GetHashCode() == compareVal.GetHashCode());
        }

        [SetUp]
        public void Init()
        {
            testVal = new DateObjectModelAbstractTest();

            compareVal = new DateObjectModelAbstractTest();
        }
    }
}