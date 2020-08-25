//-----------------------------------------------------------------------
//
//
// <copyright file="IDateObjectModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModel : IComparable<DateObjectModel>, IComparer<DateObjectModel>
    {
        string GCformat { get; }

        bool GDualdated { get; }

        int GetAge { get; }

        int GetDecade { get; }

        DateTime GetMonthDay { get; }

        string GetYear { get; }

        string GVal { get; }

        string LongDate { get; }
        string ShortDate { get; }

        string ShortDateOrEmpty { get; }

        DateTime SingleDate { get; }

        DateTime SortDate { get; }

        bool Valid { get; set; }

        TimeSpan DateDifference(IDateObjectModel otherDate);

        string DateDifferenceDecoded(IDateObjectModel otherDate);
    }
}