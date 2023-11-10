// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.DataModels.Date.Interfaces;
    using GrampsView.ModelsDB.Date;

    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateDBModelRange : IDateDBModel
    {
        string GCformat { get; }

        bool GDualdated { get; }

        string GNewYear { get; }

        CommonEnums.DateQuality GQuality { get; }

        DateDBModelVal GStart { get; }

        DateDBModelVal GStop { get; }
    }
}