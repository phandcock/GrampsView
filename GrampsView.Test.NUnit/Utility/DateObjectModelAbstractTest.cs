namespace GrampsView.e2e.Test.Utility
{
    using GrampsView.Data.Model;

    using System;

    public partial class DateObjectModelAbstractTest : DateObjectModel, IDateObjectModel
    {
        public DateObjectModelAbstractTest()
        {
            Valid = true;
        }

        public override Nullable<int> GetAge => throw new NotImplementedException();

        public override string GetYear => throw new NotImplementedException();

        public override string LongDate => throw new NotImplementedException();

        public override string ShortDate => throw new NotImplementedException();

        public override DateTime SingleDate
        {
            get
            {
                return new DateTime();
            }
        }

        public override DateTime SortDate
        {
            get
            {
                return new DateTime();
            }
        }

        public override CardListLineCollection AsCardListLine(string argTitle = null)
        {
            return new CardListLineCollection();
        }

        public override HLinkBase AsHLink(string v)
        {
            throw new NotImplementedException();
        }
    }
}