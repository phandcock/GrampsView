//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IHLinkBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IHLinkBase : IComparer, IComparable
    {
        DisplayFormat CardType { get; set; }

        bool GPriv { get; set; }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        string HLinkKey
        {
            get;
            set;
        }

    
      
   

        /// <summary>
        /// Gets the priv as string.
        /// </summary>
        /// <value>
        /// The priv as string.
        /// </value>
        string PrivAsString { get; }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid.
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        bool Valid
        {
            get;
        }

        int GetHashCode();

        /// <summary>
        /// Sets the base fields.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        void SetBase(HLinkBase arg);
    }
}