// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.DataLayer;
using GrampsView.Data.Model;
using GrampsView.Data.StoreDB;
using GrampsView.Models.DataModels;
using GrampsView.Models.DBModels;

using Microsoft.EntityFrameworkCore;

using System.Collections;
using System.Globalization;

namespace GrampsView.Data.DataView
{
    public class CitationDataLayer : DataLayerBase<CitationModel, HLinkCitationModel, HLinkCitationModelCollection>, ICitationDataLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView"/> class.
        /// </summary>
        public CitationDataLayer()
        {
        }

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
        public override IReadOnlyList<CitationModel> DataAsDefaultSort
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
        public override IReadOnlyList<CitationModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<CitationModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<CitationDBModel> t = CitationAccess.ToList().AsReadOnly();

                foreach (CitationDBModel? item in t)
                {
                    _DataAsList.Add(item.DeSerialise());
                }

                return _DataAsList;
            }
        }

        /// <summary>
        /// Gets the latest changes.
        /// </summary>
        /// <returns>
        /// </returns>
        public override HLinkCitationModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataAsList.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkCitationModelCollection returnCardGroup = new HLinkCitationModelCollection();

                foreach (ICitationModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Citation Changes";

                return returnCardGroup;
            }
        }

        private List<CitationModel> _DataAsDefaultSort { get; set; } = new List<CitationModel>();

        private List<CitationModel> _DataAsList { get; set; } = new List<CitationModel>();

        public override Group<HLinkCitationModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkCitationModelCollection> t = new Group<HLinkCitationModelCollection>();

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
                HLinkCitationModelCollection info = new HLinkCitationModelCollection
                {
                    Title = g.GroupName,
                };

                foreach (CitationModel? item in g.Items)
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
        public HLinkCitationModelCollection GetAllAsHLink()
        {
            HLinkCitationModelCollection t = new HLinkCitationModelCollection();

            foreach (CitationModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CitationModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<CitationDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().CitationAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First().DeSerialise();
            }

            return new CitationModel();
        }

        public override CitationModel GetModelFromId(string argId)
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
        public override HLinkCitationModelCollection HLinkCollectionSort(HLinkCitationModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkCitationModel> t = collectionArg.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent);

            HLinkCitationModelCollection tt = new HLinkCitationModelCollection();

            foreach (HLinkCitationModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkCitationModelCollection Search(string argQuery)
        {
            HLinkCitationModelCollection itemsFound = new HLinkCitationModelCollection
            {
                Title = "Citations"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IOrderedEnumerable<CitationModel> temp = DataAsList.Where(x => x.GDateContent.ShortDate.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (ICitationModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}