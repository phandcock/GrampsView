namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    // Event repository </summary>
    public class AddressDataView : DataViewBase<AddressModel, HLinkAdressModel, HLinkAddressModelCollection>, IAddressDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView"/> class.
        /// </summary>
        public AddressDataView()
        {
        }

        public RepositoryModelDictionary<AddressModel, HLinkAdressModel> AddressData
        {
            get
            {
                return DataStore.Instance.DS.AddressData;
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<AddressModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(addressModel => addressModel.ToString()).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<AddressModel> DataViewData
        {
            get
            {
                return AddressData.Values.ToList();
            }
        }

        public override CardGroupBase<HLinkAdressModel> GetAllAsCardGroupBase()
        {
            CardGroupBase<HLinkAdressModel> t = new CardGroupBase<HLinkAdressModel>();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override CardGroup GetAllAsGroupedCardGroup()
        {
            CardGroup t = new CardGroup();

            var query = from item in DataViewData
                        orderby item.ToString()
                        group item by (item.ToString()) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                CardGroupBase<IHLinkAddressModel> info = new CardGroupBase<IHLinkAddressModel>
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
        public HLinkAddressModelCollection GetAllAsHLink()
        {
            HLinkAddressModelCollection t = new HLinkAddressModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override AddressModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return AddressData[argHLinkKey.Value];
        }

        public override AddressModel GetModelFromId(string argId)
        {
            return DataViewData.Where(X => X.Id == argId).FirstOrDefault();
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
        public override HLinkAddressModelCollection HLinkCollectionSort(HLinkAddressModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkAdressModel> t = collectionArg.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.ToString());

            HLinkAddressModelCollection tt = new HLinkAddressModelCollection();

            foreach (HLinkAdressModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkAdressModel> Search(string argQuery)
        {
            CardGroupBase<HLinkAdressModel> itemsFound = new CardGroupBase<HLinkAdressModel>
            {
                Title = "Addresses"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.ToString().Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (IAddressModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}