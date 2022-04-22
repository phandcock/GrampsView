namespace GrampsView.Data.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class DOMStrTests
    {
        private DateObjectModelStr testVal;

        [TestCleanup]
        public void Cleanup()
        {
        }

        public void InitYearMonth()
        {
            string aVal = "1939-01";

            testVal = new DateObjectModelStr(aVal);
        }

        public void InitYearMonthDay()
        {
            string aVal = "1939-01-01";

            testVal = new DateObjectModelStr(aVal);
        }

        public void InitYearOnly()
        {
            string aVal = "1939";

            testVal = new DateObjectModelStr(aVal);
        }
    }
}