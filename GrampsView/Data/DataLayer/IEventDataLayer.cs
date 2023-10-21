// Copyright (c) phandcock. All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels;
using GrampsView.Models.HLinks.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Interface for the Event Repository.
    /// </summary>
    public interface IEventDataLayer : IDataLayerBase<EventModel, HLinkEventModel, HLinkEventModelCollection>
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<EventModel> DataAsDefaultSort { get; }

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
        IOrderedEnumerable<EventModel> CollectionSortEventDateAsc(ObservableCollection<EventModel> collectionArg);

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkEventModelCollection GetAllAsHLink();

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
        EventModel GetEventType(HLinkEventModelCollection eventCollection, string eventType);

        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        /// <param name="eventType">
        /// Type of the event.
        /// </param>
        /// <returns>
        /// </returns>
        EventModel GetEventType(string eventType);

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        new HLinkEventModelCollection HLinkCollectionSort(HLinkEventModelCollection collectionArg);
    }
}