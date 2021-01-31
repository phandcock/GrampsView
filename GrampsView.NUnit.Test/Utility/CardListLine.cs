namespace GrampsView.e2e.Test.Utility
{
    using global::NUnit.Framework;

    using GrampsView.Data.Model;

    public static class CardListLineUtils
    {
        public static void CheckCardListLine(CardListLine argCardListLine, string argLabel, string argValue)
        {
            if (argCardListLine.Label != argLabel)
            {
                Assert.Fail(string.Format("The CardListLine string was '{0}' when it should have been '{1}'", argCardListLine.Label, argLabel));
            }

            if (argCardListLine.Value != argValue)
            {
                Assert.Fail(string.Format("The CardListLine string was '{0}' when it should have been '{1}'", argCardListLine.Value, argValue));
            }
        }
    }
}