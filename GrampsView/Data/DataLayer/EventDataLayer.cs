// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataLayer;
using GrampsView.Data.DataLayer.Interfaces;
using GrampsView.Data.StoreDB;
using GrampsView.DBModels;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Event data store handy routines.
    /// </summary>

    public class EventDataLayer : DataLayerBase<EventDBModel, HLinkEventDBModel, HLinkEventDBModelCollection>, IEventDataLayer
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<EventDBModel> DataAsDefaultSort
        {
            get
            {
                // Cache it
                if (_DataAsDefaultSort.Count > 0)
                {
                    return _DataAsDefaultSort;
                }

                _DataAsDefaultSort = DataAsList.OrderBy(EventModel => EventModel.GDate).ToList();

                return _DataAsDefaultSort;
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<EventDBModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<EventDBModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<EventDBModel> t = EventAccess.ToList().AsReadOnly();

                foreach (EventDBModel? item in t)
                {
                    _DataAsList.Add(item);
                }

                return _DataAsList;
            }
        }

        public DbSet<EventDBModel> EventAccess
        {
            get
            {
                return localStoreDB.EventAccess;
            }
        }

        public override HLinkEventDBModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataAsDefaultSort.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkEventDBModelCollection returnCardGroup = new();

                foreach (EventDBModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Event Changes";

                return returnCardGroup;
            }
        }

        private List<EventDBModel> _DataAsDefaultSort { get; set; } = new List<EventDBModel>();

        private List<EventDBModel> _DataAsList { get; set; } = new List<EventDBModel>();



        /// <summary>
        /// Collections the sort event date asc.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Collection of Events sorted by Date.
        /// </returns>
        public IOrderedEnumerable<EventDBModel> CollectionSortEventDateAsc(ObservableCollection<EventDBModel>? collectionArg)
        {
            collectionArg ??= new ObservableCollection<EventDBModel>(DataAsList);

            // sort the list
            return collectionArg.OrderBy(EventModel => EventModel.GDate.SortDate);
        }

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="hlinkEM">
        /// The h link string.
        /// </param>
        /// <returns>
        /// Event model from hLinkString.
        /// </returns>
        public EventDBModel Get(HLinkEventDBModel hlinkEM)
        {
            EventDBModel t = hlinkEM.DeRef;

            return t;
        }

        public override Group<HLinkEventDBModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkEventDBModelCollection> t = new();

            var query = from item in DataAsList
                        orderby item.GDate
                        group item by item.GDate.GetDecade into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkEventDBModelCollection info = new()
                {
                    Title = g.GroupName.ToString(),
                };

                foreach (EventDBModel? item in g.Items)
                {
                    info.Add(item.HLink);
                }

                t.Add(info);
            }

            return t;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkEventDBModelCollection GetAllAsHLink()
        {
            HLinkEventDBModelCollection t = new();

            foreach (EventDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>
        /// Gets the first event type in the collection.
        /// </summary>
        /// <param name="eventCollection">
        /// The event collection.
        /// </param>
        /// <param name="eventType">
        /// Type of the event.
        /// </param>
        /// <returns>
        /// An Event Model from hlink collection if the EventType is found.
        /// </returns>
        public EventDBModel GetEventType(HLinkEventDBModelCollection eventCollection, string eventType)
        {
            if (eventCollection is null)
            {
                throw new ArgumentNullException(nameof(eventCollection));
            }

            IEnumerable<HLinkEventDBModel> t = eventCollection.Where(HLinkEventModel => HLinkEventModel.DeRef.GType == eventType);

            return t.Any() ? t.FirstOrDefault().DeRef : new EventDBModel();
        }

        /// <summary>
        /// Gets the first event that matches the event type.
        /// </summary>
        /// <param name="eventType">
        /// Type of the event.
        /// </param>
        /// <returns>
        /// EVentModel of the selected event or null if none found.
        /// </returns>
        public EventDBModel? GetEventType(string eventType)
        {
            IEnumerable<EventDBModel> selectedEvents = from aEvent in DataAsList
                                                       where aEvent.GType == eventType
                                                       select aEvent;

            EventDBModel t = selectedEvents.FirstOrDefault();

            return t ?? null;
        }

        public override EventDBModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<EventDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().EventAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First();
            }

            return new EventDBModel();
        }

        public override EventDBModel GetModelFromId(string argId)
        {
            return DataAsList.Where(X => X.Id == argId).FirstOrDefault();
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkEventDBModelCollection? HLinkCollectionSort(HLinkEventDBModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkEventDBModel> t = collectionArg.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate).ThenBy(HLinkEventModel => HLinkEventModel.DeRef.GDescription);

            HLinkEventDBModelCollection tt = new();

            foreach (HLinkEventDBModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkEventDBModelCollection Search(string argQuery)
        {
            HLinkEventDBModelCollection itemsFound = new()
            {
                Title = "Events"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IEnumerable<EventDBModel> temp = DataAsList.Where(x => x.GDescription.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (EventDBModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}