// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DBModels.Interfaces;

namespace GrampsView.Models.DataModels.Date.Interfaces
{
    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateDBModel : IDBModelBase, IComparable<DateDBModelBase>, IComparer<DateDBModelBase>
    {
        int? GetAge
        {
            get;
        }

        string GetDecade
        {
            get;
        }

        string GetMonthDay
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

        new bool Valid
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

        CardListLineCollection AsCardListLine(string? argTitle = null);

        TimeSpan DateDifference(IDateObjectModel otherDate);

        string DateDifferenceDecoded(IDateObjectModel otherDate);
    }
}