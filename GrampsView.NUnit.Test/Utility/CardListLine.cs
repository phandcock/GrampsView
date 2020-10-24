namespace GrampsView.NUnit.Test.Utility
{
    using GrampsView.Data.Model;

    public static class CardListLineUtils
    {
        public static bool CheckCardListLine(CardListLine argCardListLine, string argLabel, string argValue)
        {
            if ((argCardListLine.Label == argLabel) && (argCardListLine.Value == argValue)) { return true; }

            return false;
        }
    }
}