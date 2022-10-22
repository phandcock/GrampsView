namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    // The Family Repository </summary>
    public class FamilyDataView : DataViewBase<FamilyModel, HLinkFamilyModel, HLinkFamilyModelCollection>, IFamilyDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyDataView"/> class.
        /// </summary>
        public FamilyDataView()
        {
        }

        public override IReadOnlyList<FamilyModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(FamilyModel => FamilyModel).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<FamilyModel> DataViewData
        {
            get
            {
                return FamilyData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the family data.
        /// </summary>
        /// <value>
        /// The family data.
        /// </value>

        public RepositoryModelDictionary<FamilyModel, HLinkFamilyModel> FamilyData
        {
            get
            {
                return DataStore.Instance.DS.FamilyData;
            }
        }

        public override HLinkFamilyModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkFamilyModelCollection returnCardGroup = new HLinkFamilyModelCollection();

                foreach (FamilyModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Family Changes";

                return returnCardGroup;
            }
        }

        public override HLinkFamilyModelCollection GetAllAsCardGroupBase()
        {
            HLinkFamilyModelCollection t = new HLinkFamilyModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkFamilyModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkFamilyModelCollection> t = new Group<HLinkFamilyModelCollection>();

            // Union on the Father and Mother Surnames first
            var queryBase = (
                        from item in DataViewData
                        select new
                        {
                            key = item.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname,
                            item
                        }
                        )
                        .Union(
                                from item in DataViewData
                                select new
                                {
                                    key = item.GMother.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname,
                                    item
                                }
                                );

            var query =
                    from x in queryBase
                    orderby x.key, x.item
                    group x by x.key into g
                    select new
                    {
                        GroupName = g.Key,
                        Items = g
                    };

            foreach (var g in query)
            {
                HLinkFamilyModelCollection info = new HLinkFamilyModelCollection
                {
                    Title = g.GroupName,
                };

                foreach (var item in g.Items)
                {
                    info.Add(item.item.HLink);
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
        public HLinkFamilyModelCollection GetAllAsHLink()
        {
            HLinkFamilyModelCollection t = new HLinkFamilyModelCollection();

            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return t;
            }

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        ///// <summary>
        ///// Gets any children of the family.
        ///// </summary>
        ///// <returns>
        ///// </returns>
        //public HLinkPersonModelCollection GetChildren(HLinkFamilyModel hlinkFamily)
        //{
        //    HLinkPersonModelCollection t = new HLinkPersonModelCollection();

        // // Handle the case where there is no data. if (FamilyData.Count == 0) { return new
        // HLinkPersonModelCollection(); }

        //    // TODO fix this
        //    if (FamilyData.GetModelFromHLink(hlinkFamily).GChildRefCollection.Count > 0)
        //    {
        //        t.Add(FamilyData.GetModelFromHLink(hlinkFamily).GChildRefCollection[0].DeRef.HLink);
        //        return t;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// Get the currents partner.
        ///// </summary>
        ///// <returns>
        ///// </returns>
        //public HLinkPersonModel GetCurrentPartner(HLinkFamilyModel hlinkFamily)
        //{
        //    // Handle the case where there is no data.
        //    if (FamilyData.Count == 0)
        //    {
        //        return null;
        //    }

        //    return FamilyData.GetModelFromHLink(hlinkFamily).GMother;
        //}

        ///// <summary>
        ///// Currents the spouses.
        ///// </summary>
        ///// <param name="hlinkFamily">
        ///// The hlink family.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public HLinkPersonModelCollection GetCurrentSpouses(HLinkFamilyModel hlinkFamily)
        //{
        //    HLinkPersonModelCollection t = new HLinkPersonModelCollection();

        // // Handle the case where there is no data. if (FamilyData.Count == 0) { return t; }

        //    t.Add(FamilyData.GetModelFromHLink(hlinkFamily).GMother);
        //    return t;
        //}

        ///// <summary>
        ///// Gets the father.
        ///// </summary>
        ///// <param name="arg">
        ///// The argument.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public IPersonModel GetFather(HLinkKey argHLinkKey)
        //{
        //    // Handle the case where there is no data.
        //    if (FamilyData.Count == 0)
        //    {
        //        return null;
        //    }

        //    return GetModelFromHLinkKey(argHLinkKey).GFather.DeRef;
        //}

        public override FamilyModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return FamilyData[argHLinkKey.Value];
        }

        public override FamilyModel GetModelFromId(string argId)
        {
            return DataViewData.Where(X => X.Id == argId).FirstOrDefault();
        }

        ///// <summary>
        ///// Gets the mother.
        ///// </summary>
        ///// <param name="arg">
        ///// The argument.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public IPersonModel GetMother(HLinkKey argHLinkKey)
        //{
        //    // Handle the case where there is no data.
        //    if (FamilyData.Count == 0)
        //    {
        //        return null;
        //    }

        //    return GetModelFromHLinkKey(argHLinkKey).GMother.DeRef;
        //}

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkFamilyModelCollection HLinkCollectionSort(HLinkFamilyModelCollection collectionArg)
        {
            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return null;
            }

            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkFamilyModel> t = collectionArg.OrderBy(HLinkFamilyModel => HLinkFamilyModel.DeRef);

            HLinkFamilyModelCollection tt = new HLinkFamilyModelCollection();

            foreach (HLinkFamilyModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkFamilyModelCollection Search(string argQuery)
        {
            HLinkFamilyModelCollection itemsFound = new HLinkFamilyModelCollection
            {
                Title = "Families"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (FamilyModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}