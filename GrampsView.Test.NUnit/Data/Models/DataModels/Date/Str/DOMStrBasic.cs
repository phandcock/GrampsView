namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    [TestFixture()]
    public partial class DOMStrTests
    {
        [Test()]
        public void DOMStrAge()
        {
            InitYearMonthDay();

            Assert.True(testVal.GetAge == null);
        }

        [Test()]
        public void DOMStrBasic()
        {
            InitYearOnly();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMStrInit_Year()
        {
            InitYearOnly();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMStrInit_YearMonth()
        {
            InitYearMonth();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMStrInit_YearMonthDay()
        {
            InitYearMonthDay();

            Assert.True(testVal.Valid);
        }

        [Test()]
        public void DOMStrLongDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.LongDate == "1939-01-01");
        }

        [Test()]
        public void DOMStrShortDate()
        {
            InitYearMonthDay();

            Assert.True(testVal.ShortDate == "1939-01-01");
        }
    }
}