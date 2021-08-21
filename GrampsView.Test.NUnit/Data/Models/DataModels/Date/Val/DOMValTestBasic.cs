namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    using System;

    [TestFixture()]
    public partial class DOMValTests
    {
        [Test()]
        public void DOMVal_Age()
        {
            InitYearOnly();

            Assert.True(testVal.GetAge == (DateTime.Now.AddYears(-1939).Year));
        }

        [Test()]
        public void DOMVal_Basic()
        {
            InitYearOnly();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMVal_Init_Year()
        {
            InitYearOnly();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMVal_Init_YearMonth()
        {
            InitYearMonth();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMVal_Init_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMVal_LongDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.LongDate == "1 Oct 1939");
        }

        [Test()]
        public void DOMVal_ShortDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.ShortDate == "1Oct1939");
        }
    }
}