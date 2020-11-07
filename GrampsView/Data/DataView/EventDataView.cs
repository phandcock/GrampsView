namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Event data store handy routines.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.EventModel, GrampsView.Data.ViewModel.HLinkEventModel, GrampsView.Data.Collections.HLinkEventModelCollection}"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IEventDataView"/>
    public class EventDataView : DataViewBase<EventModel, HLinkEventModel, HLinkEventModelCollection>, IEventDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDataView"/> class.
        /// </summary>
        public EventDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<EventModel> DataDefaultSort
        {
            get
            {
                return CollectionSortEventDateAsc(null).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<EventModel> DataViewData
        {
            get
            {
                return EventData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the event data.
        /// </summary>
        /// <value>
        /// The event data.
        /// </value>
        [DataMember]
        public RepositoryModelDictionary<EventModel, HLinkEventModel> EventData
        {
            get
            {
                return DataStore.Instance.DS.EventData;
            }
        }

        /// <summary>
        /// Gets the get event groups by decade.
        /// </summary>
        /// <value>
        /// The get groups by letter.
        /// </value>
        public override List<CommonGroupInfoCollection<EventModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<EventModel>> groups = new List<CommonGroupInfoCollection<EventModel>>();

                var query = from item in DataViewData
                            orderby item.GDate
                            group item by item.GDate.GetDecade into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<EventModel> info = new CommonGroupInfoCollection<EventModel>();

                    // Handle 0's
                    if (g.GroupName == 0)
                    {
                        info.Key = "Unknown Date";
                    }
                    else
                    {
                        info.Key = g.GroupName + "'s";
                    }

                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }

                    groups.Add(info);
                }

                return groups;
            }
        }

        /// <summary>
        /// Collections the sort event date asc.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Collection of Events sorted by Date.
        /// </returns>
        public IOrderedEnumerable<EventModel> CollectionSortEventDateAsc(ObservableCollection<EventModel> collectionArg)
        {
            if (collectionArg == null)
            {
                collectionArg = new ObservableCollection<EventModel>(DataViewData);
            }

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
            EventModel t = EventData.GetModelFromHLink(hlinkEM);

            return t;
        }

        public override CardGroupBase<HLinkEventModel> GetAllAsCardGroup()
        {
            CardGroupBase<HLinkEventModel> t = new CardGroupBase<HLinkEventModel>();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkEventModelCollection GetAllAsHLink()
        {
            HLinkEventModelCollection t = new HLinkEventModelCollection();

            foreach (var item in DataDefaultSort)
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

            if (t.Any())
            {
                return t.FirstOrDefault().DeRef;
            }

            return new EventModel();
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
        public EventModel GetEventType(string eventType)
        {
            var selectedEvents = from aEvent in DataViewData
                                 where aEvent.GType == eventType
                                 select aEvent;

            EventModel t = selectedEvents.FirstOrDefault();

            if (t != null)
            {
                return t;
            }
            else
            {
                return null;
            }
        }

        public override CardGroupBase<HLinkEventModel> GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroupBase<HLinkEventModel> returnCardGroup = new CardGroupBase<HLinkEventModel>();

            foreach (EventModel item in tt)
            {
                returnCardGroup.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Event Changes";

            return returnCardGroup;
        }

        public override EventModel GetModelFromHLinkString(string HLinkString)
        {
            return EventData[HLinkString];
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
        public override HLinkEventModelCollection HLinkCollectionSort(HLinkEventModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkEventModel> t = collectionArg.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate).ThenBy(HLinkEventModel => HLinkEventModel.DeRef.GDescription);

            HLinkEventModelCollection tt = new HLinkEventModelCollection();

            foreach (HLinkEventModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkEventModel> Search(string queryString)
        {
            CardGroupBase<HLinkEventModel> itemsFound = new CardGroupBase<HLinkEventModel>();

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            IEnumerable<EventModel> temp = DataViewData.Where(x => x.GDescription.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).OrderBy(y => y.GetDefaultText);

            foreach (EventModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}