//-----------------------------------------------------------------------
//
// Interface for the Note Repository
//
// <copyright file="ITagDataView.cs" company="PlaceholderCompany">
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
    public interface ITagDataView : IDataViewBase<TagModel, HLinkTagModel, HLinkTagModelCollection>
    {
        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        RepositoryModelDictionary<TagModel, HLinkTagModel> TagData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkTagModelCollection GetAllAsHLink();
    }
}