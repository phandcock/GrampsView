// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataLayer;
using GrampsView.Data.DataLayer.Interfaces;
using GrampsView.Data.StoreDB;
using GrampsView.DBModels;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Globalization;

namespace GrampsView.Data.DataView
{
    public class CitationDataLayer : DataLayerBase<CitationDBModel, HLinkCitationDBModel, HLinkCitationDBModelCollection>, ICitationDataLayer
    {


        public DbSet<CitationDBModel> CitationAccess
        {
            get
            {
                return Ioc.Default.GetRequiredService<IStoreDB>().CitationAccess;
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<CitationDBModel> DataAsDefaultSort
        {
            get
            {
                // Cache it
                if (_DataAsDefaultSort.Count > 0)
                {
                    return _DataAsDefaultSort;
                }

                _DataAsDefaultSort = DataAsList.OrderBy(citationModel => citationModel.GSourceRef.DeRef.GSTitle).ToList();

                return _DataAsDefaultSort;
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<CitationDBModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<CitationDBModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<CitationDBModel> t = CitationAccess.ToList().AsReadOnly();

                foreach (CitationDBModel item in t)
                {
                    _DataAsList.Add(item);
                }

                return _DataAsList;
            }
        }

        /// <summary>
        /// Gets the latest changes.
        /// </summary>
        /// <returns>
        /// </returns>
        public override HLinkCitationDBModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataAsList.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkCitationDBModelCollection returnCardGroup = new HLinkCitationDBModelCollection();

                foreach (ICitationDBModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Citation Changes";

                return returnCardGroup;
            }
        }

        private List<CitationDBModel> _DataAsDefaultSort { get; set; } = new List<CitationDBModel>();

        private List<CitationDBModel> _DataAsList { get; set; } = new List<CitationDBModel>();

        public override Group<HLinkCitationDBModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkCitationDBModelCollection> t = new Group<HLinkCitationDBModelCollection>();

            var query = from item in DataAsList
                        orderby item.ToString(), item.GDateContent, item.GPage
                        group item by (item.ToString()) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkCitationDBModelCollection info = new HLinkCitationDBModelCollection
                {
                    Title = g.GroupName,
                };

                foreach (CitationDBModel item in g.Items)
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
        public HLinkCitationDBModelCollection GetAllAsHLink()
        {
            HLinkCitationDBModelCollection t = new HLinkCitationDBModelCollection();

            foreach (CitationDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CitationDBModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<CitationDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().CitationAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First();
            }

            return new CitationDBModel();
        }

        public override CitationDBModel GetModelFromId(string argId)
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
        public override HLinkCitationDBModelCollection HLinkCollectionSort(HLinkCitationDBModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkCitationDBModel> t = collectionArg.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent);

            HLinkCitationDBModelCollection tt = new HLinkCitationDBModelCollection();

            foreach (HLinkCitationDBModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkCitationDBModelCollection Search(string argQuery)
        {
            HLinkCitationDBModelCollection itemsFound = new HLinkCitationDBModelCollection
            {
                Title = "Citations"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IOrderedEnumerable<CitationDBModel> temp = DataAsList.Where(x => x.GDateContent.ShortDate.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (ICitationDBModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}