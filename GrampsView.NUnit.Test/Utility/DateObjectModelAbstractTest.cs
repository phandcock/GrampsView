﻿namespace GrampsView.NUnit.Test.Utility
{
    using GrampsView.Data.Model;

    using System;

    public class DateObjectModelAbstractTest : DateObjectModel, IDateObjectModel
    {
        public override int GetAge => throw new NotImplementedException();

        public override string GetYear => throw new NotImplementedException();

        public override string LongDate => throw new NotImplementedException();

        public override string ShortDate => throw new NotImplementedException();

        public override DateTime SingleDate => throw new NotImplementedException();

        public override DateTime SortDate => throw new NotImplementedException();

        public override CardListLineCollection AsCardListLine(string argTitle = null)
        {
            return new CardListLineCollection();
        }
    }
}