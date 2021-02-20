//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IModelBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using GrampsView.Data.Collections;

    using System;
    using System.ComponentModel;

    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IModelBase : IComparable<ModelBase>, INotifyPropertyChanged
    {
        HLinkBackLinkModelCollection BackHLinkReferenceCollection
        {
            get;
        }

        DateTime Change
        {
            get; set;
        }

        ModelBase DeRef
        {
            get;
        }

        /// <summary>
        /// Gets the default text.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        string GetDefaultText
        {
            get;
        }

        /// <summary>
        /// Gets or sets the model handle.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        string Handle
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        string HLinkKey
        {
            get; set;
        }

        ///// <summary>
        ///// Gets or sets the model user activity.
        ///// </summary>
        ///// <value>
        ///// The model user activity.
        ///// </value>
        //UserActivity ModelUserActivity { get; set; }

        string Id
        {
            get; set;
        }

        ItemGlyph ModelItemGlyph
        {
            get;

            set;
        }

        bool Priv
        {
            get; set;
        }

        string PrivAsString
        {
            get;
        }

        bool Valid
        {
            get;
        }

        void LoadBasics(ModelBase argBasics);
    }
}