// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Models.DataModels.Date.Interfaces;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Date Object Val type Interface
    /// </summary>
    public interface IDateDBModelVal : IDateDBModel
    {
        string GCformat
        {
            get;
        }

        bool GDualdated
        {
            get;
        }

        string GNewYear
        {
            get;
        }

        CommonEnums.DateQuality GQuality
        {
            get;
        }

        string GVal
        {
            get;
        }

        DateValType GValType
        {
            get;
        }
    }
}