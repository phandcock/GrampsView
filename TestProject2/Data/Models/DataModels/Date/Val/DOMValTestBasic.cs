namespace GrampsView.Data.Model.Tests
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

   [TestClass]
    public partial class DOMValTests
    {
      [TestMethod]
        public void DOMVal_Age()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.GetAge == (DateTime.Now.AddYears(-1939).Year));
        }

      [TestMethod]
        public void DOMVal_Basic()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMVal_Init_Year()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMVal_Init_YearMonth()
        {
            InitYearMonth();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMVal_Init_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMVal_LongDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.LongDate == "1 Oct 1939");
        }

      [TestMethod]
        public void DOMVal_ShortDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.ShortDate == "1Oct1939");
        }
    }
}