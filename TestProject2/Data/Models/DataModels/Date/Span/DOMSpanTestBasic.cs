namespace GrampsView.Data.Model.Tests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

   [TestClass]
    public partial class DateObjectModelSpanTests
    {
      [TestMethod]
        public void TestDateObjectModelSpan_Age()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.GetAge == (DateTime.Now.AddYears(-1939).Year));
        }

      [TestMethod]
        public void TestDateObjectModelSpan_Init_Year()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void TestDateObjectModelSpan_Init_YearMonth()
        {
            InitYearMonth();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void TestDateObjectModelSpan_Init_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void TestDateObjectModelSpan_LongDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.LongDate == "From 1 Jan 1939 to 11 Oct 1948");
        }

      [TestMethod]
        public void TestDateObjectModelSpan_ShortDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.ShortDate == "1Jan1939-11Oct1948");
        }
    }
}