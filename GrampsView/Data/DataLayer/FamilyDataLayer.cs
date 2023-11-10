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

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Collections;
using System.Globalization;

namespace GrampsView.Data.DataView
{
    // The Family Repository </summary>
    public class FamilyDataLayer : DataLayerBase<FamilyDBModel, HLinkFamilyDBModel, HLinkFamilyDBModelCollection>, IFamilyDataLayer
    {
        public override IReadOnlyList<FamilyDBModel> DataAsDefaultSort
        {
            get
            {
                // Cache it
                if (_DataAsDefaultSort.Count > 0)
                {
                    return _DataAsDefaultSort;
                }

                _DataAsDefaultSort = DataAsList.OrderBy(FamilyModel => FamilyModel.DefaultTextShort).ToList();

                return _DataAsDefaultSort;
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<FamilyDBModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<FamilyDBModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<FamilyDBModel> t = FamilyAccess.ToList().AsReadOnly();

                foreach (FamilyDBModel? item in t)
                {
                    _DataAsList.Add(item);
                }

                return _DataAsList;
            }
        }

        public DbSet<FamilyDBModel> FamilyAccess
        {
            get
            {
                try
                {
                    return Ioc.Default.GetRequiredService<IStoreDB>().FamilyAccess;
                }
                catch (Exception ex)
                {
                    ErrorInfo t = new("FamilyAccess")
                    {
                    };

                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("FamilyAccess", ex);

                    //Ioc.Default.GetRequiredService<IStoreDB>().Clear();
                    //Ioc.Default.GetRequiredService<IStoreDB>().InitialiseDB();

                    return null;
                }
            }
        }

        public override HLinkFamilyDBModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataAsDefaultSort.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkFamilyDBModelCollection returnCardGroup = new HLinkFamilyDBModelCollection();

                foreach (FamilyDBModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Family Changes";

                return returnCardGroup;
            }
        }

        private List<FamilyDBModel> _DataAsDefaultSort { get; set; } = new List<FamilyDBModel>();

        private List<FamilyDBModel> _DataAsList { get; set; } = new List<FamilyDBModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyDataView"/> class.
        /// </summary>
        public FamilyDataLayer()
        {
        }

        public override HLinkFamilyDBModelCollection GetAllAsCardGroupBase()
        {
            HLinkFamilyDBModelCollection t = new HLinkFamilyDBModelCollection();

            foreach (FamilyDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkFamilyDBModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkFamilyDBModelCollection> t = new Group<HLinkFamilyDBModelCollection>();

            // Union on the Father and Mother Surnames first
            var queryBase = (
                        from item in DataAsList
                        select new
                        {
                            key = item.GFather.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.GSurName.GetPrimarySurname,
                            item
                        }
                        )
                        .Union(
                                from item in DataAsList
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
                HLinkFamilyDBModelCollection info = new HLinkFamilyDBModelCollection
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
        public HLinkFamilyDBModelCollection GetAllAsHLink()
        {
            HLinkFamilyDBModelCollection t = new HLinkFamilyDBModelCollection();

            // Handle the case where there is no data.
            if (DataAsList.Count == 0)
            {
                return t;
            }

            foreach (FamilyDBModel item in DataAsDefaultSort)
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

        public override FamilyDBModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<FamilyDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().FamilyAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First();
            }

            return new FamilyDBModel();
        }

        public override FamilyDBModel GetModelFromId(string argId)
        {
            return DataAsList.Where(X => X.Id == argId).FirstOrDefault();
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
        public override HLinkFamilyDBModelCollection HLinkCollectionSort(HLinkFamilyDBModelCollection collectionArg)
        {
            // Handle the case where there is no data.
            if (DataAsList.Count == 0)
            {
                return null;
            }

            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkFamilyDBModel> t = collectionArg.OrderBy(HLinkFamilyModel => HLinkFamilyModel.DeRef);

            HLinkFamilyDBModelCollection tt = new HLinkFamilyDBModelCollection();

            foreach (HLinkFamilyDBModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkFamilyDBModelCollection Search(string argQuery)
        {
            HLinkFamilyDBModelCollection itemsFound = new HLinkFamilyDBModelCollection
            {
                Title = "Families"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IOrderedEnumerable<FamilyDBModel> temp = DataAsList.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (FamilyDBModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}