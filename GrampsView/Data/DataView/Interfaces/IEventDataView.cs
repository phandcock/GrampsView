//-----------------------------------------------------------------------
//
// Interface for the Event Repository
//
// <copyright file="IEventDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using GrampsView.Data.Collections;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// Interface for the Event Repository.
    /// </summary>
    public interface IEventDataView : IDataViewBase<EventModel, HLinkEventModel, HLinkEventModelCollection>
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        new IReadOnlyList<EventModel> DataDefaultSort { get; }

        /// <summary>
        /// Gets or sets the event data.
        /// </summary>
        /// <value>
        /// The event data.
        /// </value>
        RepositoryModelDictionary<EventModel, HLinkEventModel> EventData
        {
            get;
        }

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