namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using System;

    [TestFixture()]
    public partial class DateObjectModelSpanTests
    {
        [Test()]
        public void TestDateObjectModelSpan_Age()
        {
            InitYearMonthDay();

            Assert.True(testVal.GetAge == (DateTime.Now.AddYears(-1939).Year));
        }

        [Test()]
        public void TestDateObjectModelSpan_Init_Year()
        {
            InitYearOnly();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void TestDateObjectModelSpan_Init_YearMonth()
        {
            InitYearMonth();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void TestDateObjectModelSpan_Init_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void TestDateObjectModelSpan_LongDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.LongDate == "From 1 Jan 1939 to 11 Oct 1948");
        }

        [Test()]
        public void TestDateObjectModelSpan_ShortDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.ShortDate == "1Jan1939-11Oct1948");
        }
    }
}