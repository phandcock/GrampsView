using GrampsView.Data.Model;
using GrampsView.Models.HLinks;

using SharedSharp.Model;

using System;

namespace GrampsView.Test.NUnit.Utility
{
    public partial class DateObjectModelAbstractTest : DateObjectModel, IDateObjectModel
    {
        public override int? GetAge => throw new NotImplementedException();

        public override string GetYear => throw new NotImplementedException();

        public override string LongDate => throw new NotImplementedException();

        public override string ShortDate => throw new NotImplementedException();

        public override DateTime SingleDate => new();

        public override DateTime SortDate => new();

        public DateObjectModelAbstractTest()
        {
            Valid = true;
        }

        public CardListLineCollection AsCardListLine(string? argTitle = null)
        {
            return new CardListLineCollection();
        }

        public override HLinkBase AsHLink(string v)
        {
            throw new NotImplementedException();
        }
    }
}