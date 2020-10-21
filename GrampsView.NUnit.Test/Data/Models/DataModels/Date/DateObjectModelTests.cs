namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using GrampsView.Data.Model;
    using GrampsView.NUnit.Test.Utility;

    [TestFixture()]
    public class DateObjectModelTests
    {
        private DateObjectModelAbstractTest testVal;

        [Test()]
        public void AsCardListLineTest()
        {
            CardListLineCollection AsCardListLineTest_Basic = testVal.AsCardListLine("Test Title");

            if (!string.IsNullOrEmpty(AsCardListLineTest_Basic.Title)) { Assert.Fail(); return; }

            Assert.True(AsCardListLineTest_Basic.Count == 0);

            Assert.Pass();
        }

        [TearDown]
        public void Cleanup()
        {
        }

        [Test()]
        public void CompareTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CompareToTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void CompareToTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void DateDifferenceDecodedTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DateDifferenceTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void DateObjectModelTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void EqualsTest()
        {
            Assert.Fail();
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
        }
    }
}