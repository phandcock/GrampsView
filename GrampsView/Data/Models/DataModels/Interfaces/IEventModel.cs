//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IEventModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IEventModel : IModelBase, IComparable, IComparer
    {
        /// <summary>
        /// Gets a get h link Event Model that points to the ViewModel.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        HLinkEventModel HLink { get; }
    }
}