//-----------------------------------------------------------------------
//
//
// <copyright file="IDateObjectModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    // using static GrampsView.Data.Model.DateObjectModel;

    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModel
    {
        string GCformat { get; }

        bool GDualdated { get; }

        int GetAge { get; }

        int GetDecade { get; }

        string GetLongDateAsString { get; }
        string GetShortDateAsString { get; }
        string GetShortDateOrEmptyAsString { get; }
        string GetYear { get; }
    }
}