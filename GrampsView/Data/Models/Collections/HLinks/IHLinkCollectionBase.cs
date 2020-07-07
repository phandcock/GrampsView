//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IHLinkCollectionBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IHLinkCollectionBase<T>
    {
        /// <summary>
        /// Gets or sets the first image h link.
        /// </summary>
        /// <value>
        /// The first image h link.
        /// </value>
        HLinkHomeImageModel FirstHLinkHomeImage { get; set; }
    }
}