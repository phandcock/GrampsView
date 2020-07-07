//-----------------------------------------------------------------------
//
// Interface for the Note Repository
//
// <copyright file="INameMapDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using GrampsView.Data.Collections;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interface for the Note Repository.
    /// </summary>
    public interface INameMapDataView : IDataViewBase<NameMapModel, HLinkNameMapModel, HLinkNameMapModelCollection>
    {
        /// <summary>
        /// Gets or sets the media data.
        /// </summary>
        /// <value>
        /// The media data.
        /// </value>
        RepositoryModelDictionary<NameMapModel, HLinkNameMapModel> NameMapData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkNameMapModelCollection GetAllAsHLink();
    }
}