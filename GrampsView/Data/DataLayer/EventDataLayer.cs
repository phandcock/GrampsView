// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.DataLayer;
using GrampsView.Data.StoreDB;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels;
using GrampsView.Models.HLinks.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;

namespace GrampsView.Data.DataView
{
    /// <summary>
    /// Event data store handy routines.
    /// </summary>

    public class EventDataLayer : DataLayerBase<EventModel, HLinkEventModel, HLinkEventModelCollection>, IEventDataLayer
    {
        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<EventModel> DataAsDefaultSort
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
        public override IReadOnlyList<EventModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<EventModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<EventDBModel> t = EventAccess.ToList().AsReadOnly();

                foreach (EventDBModel? item in t)
                {
                    _DataAsList.Add(item.DeSerialise());
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

        public override HLinkEventModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataAsDefaultSort.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkEventModelCollection returnCardGroup = new();

                foreach (EventModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Event Changes";

                return returnCardGroup;
            }
        }

        private List<EventModel> _DataAsDefaultSort { get; set; } = new List<EventModel>();

        private List<EventModel> _DataAsList { get; set; } = new List<EventModel>();



        /// <summary>
        /// Collections the sort event date asc.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Collection of Events sorted by Date.
        /// </returns>
        public IOrderedEnumerable<EventModel> CollectionSortEventDateAsc(ObservableCollection<EventModel>? collectionArg)
        {
            collectionArg ??= new ObservableCollection<EventModel>(DataAsList);

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
        public EventModel Get(HLinkEventModel hlinkEM)
        {
            EventModel t = this.GetModelFromHLink(hlinkEM);

            return t;
        }

        public override Group<HLinkEventModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkEventModelCollection> t = new();

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
                HLinkEventModelCollection info = new()
                {
                    Title = g.GroupName.ToString(),
                };

                foreach (EventModel? item in g.Items)
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
        public HLinkEventModelCollection GetAllAsHLink()
        {
            HLinkEventModelCollection t = new();

            foreach (EventModel item in DataAsDefaultSort)
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
        public EventModel GetEventType(HLinkEventModelCollection eventCollection, string eventType)
        {
            if (eventCollection is null)
            {
                throw new ArgumentNullException(nameof(eventCollection));
            }

            IEnumerable<HLinkEventModel> t = eventCollection.Where(HLinkEventModel => HLinkEventModel.DeRef.GType == eventType);

            return t.Any() ? t.FirstOrDefault().DeRef : new EventModel();
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
        public EventModel? GetEventType(string eventType)
        {
            IEnumerable<EventModel> selectedEvents = from aEvent in DataAsList
                                                     where aEvent.GType == eventType
                                                     select aEvent;

            EventModel t = selectedEvents.FirstOrDefault();

            return t ?? null;
        }

        public override EventModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<EventDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().EventAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First().DeSerialise();
            }

            return new EventModel();
        }

        public override EventModel GetModelFromId(string argId)
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
        public override HLinkEventModelCollection? HLinkCollectionSort(HLinkEventModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkEventModel> t = collectionArg.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate).ThenBy(HLinkEventModel => HLinkEventModel.DeRef.GDescription);

            HLinkEventModelCollection tt = new();

            foreach (HLinkEventModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkEventModelCollection Search(string argQuery)
        {
            HLinkEventModelCollection itemsFound = new()
            {
                Title = "Events"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IEnumerable<EventModel> temp = DataAsList.Where(x => x.GDescription.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (EventModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}