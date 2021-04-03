//-----------------------------------------------------------------------
//
// Interface for the Note Repository
//
// <copyright file="IPlaceDataView.cs" company="PlaceholderCompany">
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
    public interface IPlaceDataView : IDataViewBase<PlaceModel, HLinkPlaceModel, HLinkPlaceModelCollection>
    {
        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        RepositoryModelDictionary<PlaceModel, HLinkPlaceModel> PlaceData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkPlaceModelCollection GetAllAsHLink();
    }
}