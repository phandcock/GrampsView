// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels.Date.Interfaces;

namespace GrampsView.Data.Model
{
    public interface IDateDBModelStr : IDateDBModel
    {
        string GVal { get; }
    }
}