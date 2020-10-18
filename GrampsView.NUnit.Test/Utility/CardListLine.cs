

namespace GrampsView.NUnit.Test.Utility
{
    using GrampsView.Data.Model;

    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CardListLineUtils
    {
        public static bool CheckCardListLine(CardListLine argCardListLine, string argLabel, string argValue)
        {
            if ((argCardListLine.Label == argLabel) && (argCardListLine.Value == argValue) ) { return true; }

            return false;
        }


    }
}
