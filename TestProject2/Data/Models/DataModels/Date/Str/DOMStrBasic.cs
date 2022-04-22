namespace GrampsView.Data.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class DOMStrTests
    {
      [TestMethod]
        public void DOMStrAge()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.GetAge == null);
        }

      [TestMethod]
        public void DOMStrBasic()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMStrInit_Year()
        {
            InitYearOnly();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMStrInit_YearMonth()
        {
            InitYearMonth();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMStrInit_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.Valid);
        }

      [TestMethod]
        public void DOMStrLongDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.LongDate == "1939-01-01");
        }

      [TestMethod]
        public void DOMStrShortDate()
        {
            InitYearMonthDay();

            Assert.IsTrue(testVal.ShortDate == "1939-01-01");
        }
    }
}