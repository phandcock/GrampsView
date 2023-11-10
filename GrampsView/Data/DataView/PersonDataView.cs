// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.DBModels;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;
using GrampsView.ModelsDB.HLinks.Models;

using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace GrampsView.Data.DataView
{
    public class PersonDataView : DataViewBase<PersonModel, HLinkPersonModel, HLinkPersonModelCollection>, IPersonDataView
    {
        /// <summary>
        /// The local current person h link key.
        /// </summary>
        private HLinkKey localCurrentPersonHLinkKey = new();

        /// <summary>Initializes a new instance of the <see cref="PersonDataView" /> class.</summary>
        public PersonDataView()
        {
            // TODO fix this force to gilbert handcock for now
            localCurrentPersonHLinkKey.Value = "_c47a1a91aec0a6220a5";
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
                HLinkPersonModel t = new()
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
        public override IReadOnlyList<PersonModel> DataDefaultSort => DataViewData.OrderBy(PersonModel => PersonModel.GPersonNamesCollection.GetPrimaryName.DeRef).ToList();

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<PersonModel> DataViewData => PersonData.Values.ToList();

        /// <summary>
        /// Gets the latest changes for the Person Data View.
        /// </summary>
        /// <returns>
        /// </returns>
        public override HLinkPersonModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkPersonModelCollection returnCardGroup = new();

                foreach (PersonModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Person Changes";

                return returnCardGroup;
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>

        public RepositoryModelDictionary<PersonModel, HLinkPersonModel> PersonData => DataStore.Instance.DS.PersonData;

        public override HLinkPersonModelCollection GetAllAsCardGroupBase()
        {
            HLinkPersonModelCollection t = new();

            foreach (PersonModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public Group<HLinkPersonModelCollection> GetAllAsGroupedBirthDayCardGroup(bool BirthdayShowOnlyLivingFlag)
        {
            Group<HLinkPersonModelCollection> t = new();

            IEnumerable<(string GroupName, IGrouping<string, PersonModel> Items)> query = from item in DataViewData
                                                                                          orderby item.BirthDate.GetMonthDay, item.GPersonNamesCollection.GetPrimaryName.DeRef
                                                                                          where (item.IsLiving || (!item.IsLiving && !BirthdayShowOnlyLivingFlag)) && item.BirthDate.Valid && item.BirthDate.ValidMonth && item.BirthDate.ValidDay
                                                                                          group item by $"{item.BirthDate.GetMonthDay}" into g
                                                                                          select (
                                                                                              GroupName: g.Key,
                                                                                              Items: g
                                                                                          );

            foreach ((string GroupName, IGrouping<string, PersonModel> Items) in query)
            {
                HLinkPersonModelCollection info = new()
                {
                    Title = $"{Items.FirstOrDefault().BirthDate.NotionalDate:MMM dd}",
                };

                foreach (PersonModel item in Items)
                {
                    info.Add(item.HLink);
                }

                t.Add(info);
            }

            return t;
        }

        public override Group<HLinkPersonModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkPersonModelCollection> t = new();

            var query = from item in DataViewData
                        orderby item.GPersonNamesCollection.GetPrimaryName.DeRef
                        group item by item.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkPersonModelCollection info = new()
                {
                    Title = g.GroupName,
                };

                foreach (PersonModel? item in g.Items)
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
            HLinkPersonModelCollection t = new();

            foreach (PersonModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>Gets the specified h link string.</summary>
        /// <param name="argHLinkKey"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        public override PersonModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return PersonData[argHLinkKey.Value];
        }

        public override PersonModel GetModelFromId(string argId)
        {
            return DataViewData.Where(X => X.Id == argId).FirstOrDefault();
        }

        /// <summary>
        /// Gets the person plus family events.
        /// </summary>
        /// <param name="argPerson">
        /// The argument person.
        /// </param>
        /// <returns>
        /// Person and where parent in families events.
        /// </returns>
        public ObservableCollection<EventDBModel> GetPersonPlusFamilyEvents(PersonModel argPerson)
        {
            if (argPerson is null)
            {
                return new ObservableCollection<EventDBModel>();
            }

            ObservableCollection<EventDBModel> t = argPerson.GEventRefCollection.DeRef;

            foreach (HLinkFamilyDBModel theFamily in argPerson.GParentInRefCollection)
            {
                foreach (EventDBModel theFamilyEvent in theFamily.DeRef.GEventRefCollection.DeRef)
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

            IOrderedEnumerable<HLinkPersonModel> t = collectionArg.OrderBy(HLinkPersonModel => HLinkPersonModel.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef);

            HLinkPersonModelCollection tt = new();

            foreach (HLinkPersonModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        /// <summary>Searches the items.</summary>
        /// <param name="argQuery"></param>
        /// <returns>List of Search HLinks.</returns>
        public override HLinkPersonModelCollection Search(string argQuery)
        {
            HLinkPersonModelCollection itemsFound = new()
            {
                Title = "People"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            // Get list of peoples names
            HLinkPersonNameModelCollection tt = DV.PersonNameDV.Search(argQuery);

            // Convert to HLinkPersonModels
            List<HLinkPersonModel> ttt = new();

            foreach (HLinkPersonNameModel item in tt)
            {
                foreach (HLinkBackLink item1 in item.DeRef.BackHLinkReferenceCollection)
                {
                    if (item1.HLinkType == HLinkBackLink.HLinkBackLinkEnum.HLinkPersonModel)
                    {
                        ttt.Add(item1.HLink as HLinkPersonModel);
                    }
                }
            }

            // Get Distinct
            foreach (HLinkPersonModel? item2 in ttt.Distinct())
            {
                itemsFound.Add(item2);
            }

            return itemsFound;
        }

        public List<SearcHandlerItem> SearchShell(string argQuery)
        {
            List<SearcHandlerItem> returnValue = new();

            foreach (HLinkPersonModel item in Search(argQuery))
            {
                returnValue.Add(new SearcHandlerItem(item));
            }

            return returnValue;
        }
    }
}