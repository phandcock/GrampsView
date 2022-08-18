﻿namespace GrampsView.Data.Model
{
    using SharedSharp.Model;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModel : IModelBase, IComparable<DateObjectModel>, IComparer<DateObjectModel>
    {
        Nullable<int> GetAge
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

        CardListLineCollection AsCardListLine(string argTitle = null);

        TimeSpan DateDifference(IDateObjectModel otherDate);

        string DateDifferenceDecoded(IDateObjectModel otherDate);
    }
}