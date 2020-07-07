//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IMediaDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interfaces for Media Repository.
    /// </summary>
    public interface IMediaDataView : IDataViewBase<MediaModel, HLinkMediaModel, HLinkMediaModelCollection>
    {
        /// <summary>
        /// Gets or sets the media data.
        /// </summary>
        /// <value>
        /// The media data.
        /// </value>
        RepositoryModelDictionary<MediaModel, HLinkMediaModel> MediaData
        {
            get;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkMediaModelCollection GetAllAsHLink();

        HLinkMediaModelCollection GetAllNotClippedAsHLink();

        /// <summary>
        /// Gets the first icon from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// </returns>
        IHLinkMediaModel GetFirstIconFromCollection(HLinkMediaModelCollection theCollection);

        /// <summary>
        /// Gets the random from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <param name="DefaultHLink">
        /// The default h link.
        /// </param>
        /// <returns>
        /// </returns>
        IHLinkMediaModel GetRandomFromCollection(HLinkMediaModelCollection theCollection);
    }
}