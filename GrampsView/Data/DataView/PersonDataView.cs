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
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    public class PersonDataView : DataViewBase<PersonModel, HLinkPersonModel, HLinkPersonModelCollection>, IPersonDataView
    {
        /// <summary>
        /// The local current person h link key.
        /// </summary>
        private string localCurrentPersonHLinkKey = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDataView"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        public PersonDataView()
        {
            // TODO fix this force to gilbert handcock for now
            localCurrentPersonHLinkKey = "_c47a1a91aec0a6220a5";
        }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public HLinkPersonModel Current
        {
            get
            {
                HLinkPersonModel t = new HLinkPersonModel
                {
                    HLinkKey = localCurrentPersonHLinkKey,
                };

                return t;
            }

            set
            {
                Contract.Assert(value != null);

                localCurrentPersonHLinkKey = value.HLinkKey;

                // TODO set property
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<PersonModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(PersonModel => PersonModel.GPersonNamesCollection.GetPrimaryName.DeRef.SortName).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<PersonModel> DataViewData
        {
            get
            {
                return PersonData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelDictionary<PersonModel, HLinkPersonModel> PersonData
        {
            get
            {
                return DataStore.Instance.DS.PersonData;
            }
        }

        //        return groups;
        //    }
        //}
        /// <summary>
        /// Collections the sort birth date asc.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        public static ObservableCollection<PersonModel> CollectionSortBirthDateAsc(ObservableCollection<PersonModel> collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            // sort the list
            IEnumerable<PersonModel> sortedList = collectionArg.OrderBy(PersonModel => PersonModel.BirthDate);

            return new ObservableCollection<PersonModel>(sortedList);
        }

        public override CardGroupBase<HLinkPersonModel> GetAllAsCardGroup()
        {
            CardGroupBase<HLinkPersonModel> t = new CardGroupBase<HLinkPersonModel>();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public CardGroup GetAllAsGroupedBirthDayCardGroup()
        {
            CardGroup t = new CardGroup();

            var query = from item in DataViewData
                        orderby item.BirthDate.GetMonthDay, item.GPersonNamesCollection.GetPrimaryName.DeRef.SortName
                        where ((item.IsLiving == true) && (item.BirthDate.Valid) && (item.BirthDate.ValidMonth == true) && (item.BirthDate.ValidDay == true))
                        group item by (item.BirthDate.GetMonthDay) into g
                        select new { GroupName = g.Key, Items = g };

            foreach (var g in query)
            {
                CardGroup info = new CardGroup
                {
                    Title = g.GroupName.ToString("MMMM dd", CultureInfo.CurrentCulture.DateTimeFormat),
                };

                foreach (var item in g.Items)
                {
                    info.Add(item.HLink);
                }

                t.Add(info);
            }

            return t;
        }

        public CardGroup GetAllAsGroupedSurnameCardGroup()
        {
            CardGroup t = new CardGroup();

            var query = from item in DataViewData
                        orderby item.GPersonNamesCollection.GetPrimaryName.DeRef.SortName
                        group item by (item.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname) into g
                        select new { GroupName = g.Key, Items = g };

            foreach (var g in query)
            {
                CardGroup info = new CardGroup
                {
                    Title = g.GroupName,
                };

                foreach (var item in g.Items)
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
        public HLinkPersonModelCollection GetAllAsHLink()
        {
            HLinkPersonModelCollection t = new HLinkPersonModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>
        /// Gets the latest changes for the Person Data View.
        /// </summary>
        /// <returns>
        /// </returns>
        public override CardGroupBase<HLinkPersonModel> GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroupBase<HLinkPersonModel> returnCardGroup = new CardGroupBase<HLinkPersonModel>();

            foreach (PersonModel item in tt)
            {
                returnCardGroup.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Person Changes";

            return returnCardGroup;
        }

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="HLinkString">
        /// The h link string.
        /// </param>
        /// <returns>
        /// </returns>
        public override PersonModel GetModelFromHLinkString(string HLinkString)
        {
            return PersonData[HLinkString];
        }

        //    return groups;
        //}
        /// <summary>
        /// Gets the person plus family events.
        /// </summary>
        /// <param name="argPerson">
        /// The argument person.
        /// </param>
        /// <returns>
        /// Person and where parent in families events.
        /// </returns>
        public ObservableCollection<EventModel> GetPersonPlusFamilyEvents(PersonModel argPerson)
        {
            if (argPerson is null)
            {
                return new ObservableCollection<EventModel>();
            }

            ObservableCollection<EventModel> t = argPerson.GEventRefCollection.DeRef;

            foreach (HLinkFamilyModel theFamily in argPerson.GParentInRefCollection)
            {
                foreach (EventModel theFamilyEvent in theFamily.DeRef.GEventRefCollection.DeRef)
                {
                    t.Add(theFamilyEvent);
                }
            }

            return t;
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
        public override HLinkPersonModelCollection HLinkCollectionSort(HLinkPersonModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkPersonModel> t = collectionArg.OrderBy(HLinkPersonModel => HLinkPersonModel.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName);

            HLinkPersonModelCollection tt = new HLinkPersonModelCollection();

            foreach (HLinkPersonModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        /// <summary>
        /// Searches the items.
        /// </summary>
        /// <param name="argQueryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// List of Serch HLinks.
        /// </returns>
        public override CardGroupBase<HLinkPersonModel> Search(string argQuery)
        {
            CardGroupBase<HLinkPersonModel> itemsFound = new CardGroupBase<HLinkPersonModel>
            {
                Title = "People"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            // Get list of peoples names
            CardGroupBase<HLinkPersonNameModel> tt = DV.PersonNameDV.Search(argQuery);

            foreach (HLinkPersonNameModel item in tt)
            {
                foreach (HLinkBackLink item_backlink in item.DeRef.BackHLinkReferenceCollection)
                {
                    if (item_backlink.HLinkType == HLinkBackLink.HLinkBackLinkEnum.HLinkPersonModel)
                    {
                        itemsFound.Add(item_backlink.HLink() as HLinkPersonModel);
                    }
                }
            }

            return itemsFound;
        }
    }
}