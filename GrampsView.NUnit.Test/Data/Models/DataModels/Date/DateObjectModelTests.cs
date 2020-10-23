namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Model;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    public class DateObjectModelTests
    {
        private DateObjectModelAbstractTest compareVal;
        private DateObjectModelAbstractTest testVal;

        [Test()]
        public void AsCardListLineTest()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (!string.IsNullOrEmpty(AsCardListLineTest_Basic.Title)) { Assert.Fail(); return; }

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
            Assert.True(testVal.DateDifferenceDecoded(compareVal) == "");
        }

        [Test()]
        public void DateDifferenceTest()
        {
            Assert.True(testVal.DateDifference(compareVal) == new System.TimeSpan());
        }

        [Test()]
        public void DateObjectModelTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.True(testVal == compareVal);
        }

        [Test()]
        public void GetHashCodeTest()
        {
            Assert.Fail();
        }

        [SetUp]
        public void Init()
        {
            testVal = new DateObjectModelAbstractTest();

            compareVal = new DateObjectModelAbstractTest();
        }
    }
}