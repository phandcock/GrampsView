﻿namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModelRange : IDateObjectModel
    {
        string GCformat { get; }

        bool GDualdated { get; }

        string GNewYear { get; }

        CommonEnums.DateQuality GQuality { get; }

        DateObjectModelVal GStart { get; }

        DateObjectModelVal GStop { get; }
    }
}