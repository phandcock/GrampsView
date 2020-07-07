//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IPlaceModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Tag elements.
    /// </summary>
    public interface IPlaceModel : IModelBase
    {
        HLinkPlaceModel HLink
        {
            get;
        }
    }
}