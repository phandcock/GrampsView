namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using System;

    [TestFixture()]
    public partial class DateObjectModelRangeTests
    {
        [Test()]
        public void TestDateObjectModelRange_Age()
        {
            InitYearMonthDay();

            Assert.True(testVal.GetAge == (DateTime.Now.AddYears(-1939).Year));
        }

        [Test()]
        public void TestDateObjectModelRange_Init_Year()
        {
            InitYearOnly();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void TestDateObjectModelRange_Init_YearMonth()
        {
            InitYearMonth();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void TestDateObjectModelRange_Init_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void TestDateObjectModelRange_LongDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.LongDate == "Between 1 Jan 1939 and 11 Oct 1948");
        }

        [Test()]
        public void TestDateObjectModelRange_ShortDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.ShortDate == "Range 1Jan1939 -11Oct1948");
        }
    }
}