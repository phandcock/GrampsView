﻿//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IStyledTextModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    public interface IPlaceNameModel : IModelBase
    {
        DateObjectModel GDate { get; set; }
        string GLang { get; set; }
        string GValue { get; set; }
    }
}