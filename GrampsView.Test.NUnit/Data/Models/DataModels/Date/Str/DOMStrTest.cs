namespace GrampsView.Data.Model.Tests
{
    using global::NUnit.Framework;

    [TestFixture()]
    public partial class DOMStrTests
    {
        // TODO Add more tests and add the same to other dateobjectmodel types

        private DateObjectModelStr testVal;

        [TearDown]
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