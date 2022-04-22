namespace GrampsView.Data.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    [TestClass]
    public partial class DateObjectModelRangeTests
    {
      [TestMethod]
        public void TestDateObjectModelRange_Age()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.GetAge == (DateTime.Now.AddYears(-1939).Year));
        }

      [TestMethod]
        public void TestDateObjectModelRange_Init_Year()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void TestDateObjectModelRange_Init_YearMonth()
        {
            InitYearMonth();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void TestDateObjectModelRange_Init_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void TestDateObjectModelRange_LongDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.LongDate == "Between 1 Jan 1939 and 11 Oct 1948");
        }

      [TestMethod]
        public void TestDateObjectModelRange_ShortDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.ShortDate == "Range 1Jan1939 -11Oct1948");
        }
    }
}