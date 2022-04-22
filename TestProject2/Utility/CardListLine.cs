namespace GrampsView.e2e.Test.Utility
{

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SharedSharp.Model;

    public static class CardListLineUtils
    {
        public static void CheckCardListLine(CardListLine argCardListLine, string argLabel, string argValue)
        {
            if (argCardListLine.Label != argLabel)
            {
                Assert.Fail($"The CardListLine string was '{argCardListLine.Label}' when it should have been '{ argLabel}'");
            }

            if (argCardListLine.Value != argValue)
            {
                Assert.Fail($"The CardListLine string was '{argCardListLine.Value}' when it should have been '{argValue}'");
            }
        }
    }
}