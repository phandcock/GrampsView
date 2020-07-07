//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="IHeaderDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;

    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// I Header Data Source.
    /// </summary>
    public interface IHeaderDataView : IDataViewBase<HeaderModel, HLinkHeaderModel, HLinkHeaderModelCollection>
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<HeaderModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets or sets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        RepositoryModelDictionary<HeaderModel, HLinkHeaderModel> HeaderData
        {
            get;
        }

        /// <summary>
        /// Gets the get header data.
        /// </summary>
        /// <value>
        /// The get header data.
        /// </value>
        HeaderModel HeaderDataModel
        {
            get;
        }
    }
}