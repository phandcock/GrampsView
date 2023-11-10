// Copyright (c) phandcock.  All rights reserved.

using GrampsView.DBModels;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;

namespace GrampsView.Data.DataLayer.Interfaces
{
    /// <summary>
    /// Interface for the Event Repository
    /// </summary>
    public interface IEventDataLayer : IDataLayerBase<EventDBModel, HLinkEventDBModel, HLinkEventDBModelCollection>
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<EventDBModel> DataAsDefaultSort { get; }

        // TODO add this to the general Interface
        DbSet<EventDBModel> EventAccess { get; }

        /// <summary>
        /// Collections the sort event date asc.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        IOrderedEnumerable<EventDBModel> CollectionSortEventDateAsc(ObservableCollection<EventDBModel> collectionArg);

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkEventDBModelCollection GetAllAsHLink();

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <param name="eventCollection">
        /// The event collection.
        /// </param>
        /// <param name="eventType">
        /// Type of the event.
        /// </param>
        /// <returns>
        /// </returns>
        EventDBModel GetEventType(HLinkEventDBModelCollection eventCollection, string eventType);

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <param name="eventType">
        /// Type of the event.
        /// </param>
        /// <returns>
        /// </returns>
        EventDBModel GetEventType(string eventType);

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkEventDBModelCollection HLinkCollectionSort(HLinkEventDBModelCollection collectionArg);
    }
}