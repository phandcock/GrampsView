// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.DataLayer;
using GrampsView.Data.Model;
using GrampsView.Data.StoreDB;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Models.DataModels.Minor;

namespace GrampsView.Data.DataView
{
    public class AddressDataLayer : DataLayerBase<AddressDBModel, HLinkAddressDBModel, HLinkAddressDBModelCollection>, IAddressDataLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView"/> class.
        /// </summary>
        public AddressDataLayer()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<AddressDBModel> DataAsDefaultSort
        {
            get
            {
                // Cache it
                if (_DataAsDefaultSort.Count > 0)
                {
                    return _DataAsDefaultSort;
                }

                _DataAsDefaultSort = DataAsList.OrderBy(AddressModel => AddressModel.ToString()).ToList();

                return _DataAsDefaultSort;
            }
        }

        //   => DataViewData.OrderBy(addressModel => addressModel.ToString()).ToList();

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<AddressDBModel> DataAsList
        {
            get
            {
                // Cache it
                if (_DataAsList.Count > 0)
                {
                    return _DataAsList;
                }

                _DataAsList = new List<AddressDBModel>();

                System.Collections.ObjectModel.ReadOnlyCollection<AddressDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().AddressAccess.ToList().AsReadOnly();

                foreach (AddressDBModel? item in t)
                {
                    _DataAsList.Add(item);
                }

                return _DataAsList;
            }
        }

        private List<AddressDBModel> _DataAsDefaultSort { get; set; } = new List<AddressDBModel>();

        private List<AddressDBModel> _DataAsList { get; set; } = new List<AddressDBModel>();
        // => AddressData.Values.ToList();

        public override HLinkAddressDBModelCollection GetAllAsCardGroupBase()
        {
            HLinkAddressDBModelCollection t = new();

            foreach (AddressDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkAddressDBModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkAddressDBModelCollection> t = new();

            var query = from item in DataAsDefaultSort
                        orderby item.ToString()
                        group item by (item.ToString()) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkAddressDBModelCollection info = new()
                {
                    Title = g.GroupName,
                };

                foreach (AddressDBModel? item in g.Items)
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
        public HLinkAddressDBModelCollection GetAllAsHLink()
        {
            HLinkAddressDBModelCollection t = new();

            foreach (AddressDBModel item in DataAsDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override AddressDBModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            IQueryable<AddressDBModel> t = Ioc.Default.GetRequiredService<IStoreDB>().AddressAccess.Where(x => x.HLinkKeyValue == argHLinkKey.Value);

            if (t.Any())
            {
                return t.First();
            }

            return new AddressDBModel();
        }

        public override AddressDBModel GetModelFromId(string argId)
        {
            AddressDBModel t = DataAsList.FirstOrDefault(X => X.Id == argId);

            if (t is null)
            {
                return new AddressDBModel();
            }

            return t;
        }

        /// <summary>
        /// Sorts hlink address collection.
        /// </summary>
        /// <param name="collectionArg">
        /// The address collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkAddressDBModelCollection HLinkCollectionSort(HLinkAddressDBModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkAddressDBModel> t = collectionArg.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.ToString());

            HLinkAddressDBModelCollection tt = new();

            foreach (HLinkAddressDBModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkAddressDBModelCollection Search(string argQuery)
        {
            HLinkAddressDBModelCollection itemsFound = new()
            {
                Title = "Addresses"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IEnumerable<AddressDBModel> temp = from gig in DataAsList.OrderBy(x => x.GCity != "")
                                               where gig.ToString().Any(x => x.ToString() == argQuery)
                                               select gig; // TODO fix this.OrderBy(y => y.ToString();

            foreach (IAddressModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}