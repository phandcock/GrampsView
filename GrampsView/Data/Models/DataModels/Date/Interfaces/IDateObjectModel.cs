namespace GrampsView.Data.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModel : IComparable<DateObjectModel>, IComparer<DateObjectModel>
    {
        Nullable<int> GetAge
        {
            get;
        }

        Nullable<int> GetDecade
        {
            get;
        }

        DateTime GetMonthDay
        {
            get;
        }

        string GetYear
        {
            get;
        }

        string LongDate
        {
            get;
        }

        string ShortDate
        {
            get;
        }

        string ShortDateOrEmpty
        {
            get;
        }

        DateTime SingleDate
        {
            get;
        }

        DateTime SortDate
        {
            get;
        }

        bool Valid
        {
            get; set;
        }

        bool ValidDay
        {
            get; set;
        }

        bool ValidMonth
        {
            get; set;
        }

        bool ValidYear
        {
            get; set;
        }

        CardListLineCollection AsCardListLine(string argTitle = null);

        HLinkDateModel AsHLink();

        HLinkDateModel AsHLink(string argTitle);

        TimeSpan DateDifference(IDateObjectModel otherDate);

        string DateDifferenceDecoded(IDateObjectModel otherDate);
    }
}