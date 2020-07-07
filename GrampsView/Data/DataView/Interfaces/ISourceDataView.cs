//-----------------------------------------------------------------------
//
// Interface for the Note Repository
//
// <copyright file="ISourceDataView.cs" company="PlaceholderCompany">
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
    public interface ISourceDataView : IDataViewBase<SourceModel, HLinkSourceModel, HLinkSourceModelCollection>
    {
        /// <summary>
        /// Gets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        RepositoryModelDictionary<SourceModel, HLinkSourceModel> SourceData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkSourceModelCollection GetAllAsHLink();
    }
}