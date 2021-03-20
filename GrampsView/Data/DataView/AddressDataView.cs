namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    // Event repository </summary>
    public class AddressDataView : DataViewBase<AddressModel, HLinkAdressModel, HLinkOCAddressModelCollection>, IAddressDataView
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
                return DataViewData.OrderBy(addressModel => addressModel.GetDefaultText).ToList();
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

        public override List<CommonGroupInfoCollection<AddressModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<AddressModel>> groups = new List<CommonGroupInfoCollection<AddressModel>>();

                var query = from item in DataViewData
                            orderby item.GetDefaultText
                            group item by item.GetDefaultText into g
                            select new
                            {
                                GroupName = g.Key,
                                Items = g
                            };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<AddressModel> info = new CommonGroupInfoCollection<AddressModel>();

                    // Handle 0's
                    if (string.IsNullOrEmpty(g.GroupName))
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

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkOCAddressModelCollection GetAllAsHLink()
        {
            HLinkOCAddressModelCollection t = new HLinkOCAddressModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        ///// <summary>
        ///// Gets the first image from collection.
        ///// </summary>
        ///// <param name="theCollection">
        ///// The collection.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public new HLinkMediaModel GetFirstImageFromCollection(HLinkOCAddressModelCollection theCollection)
        //{
        //    if (theCollection == null)
        //    {
        //        return null;
        //    }

        // HLinkMediaModel returnMediaModel = new HLinkMediaModel();

        // if (theCollection.Count > 0) { // step through each mediamodel hlink in the collection
        // Accept either a direct // mediamodel reference or a hlink to a Source media reference.

        // for (int i = 0; i < theCollection.Count; i++) { HLinkAdressModel currentHLink = theCollection[i];

        // returnMediaModel.HLinkGlyphItem = currentHLink.DeRef.GCitationRefCollection.FirstHLinkHomeImage;

        // //// Still needed Handle Source Links //if (currentHLink.DeRef.so.LinkToImage) //{ //
        // returnMediaModel = currentHLink.DeRef.HLinkGlyphItem; //}

        // if (returnMediaModel.Valid) { break; } } }

        //    // return the image
        //    return returnMediaModel;
        //}

        public override AddressModel GetModelFromHLinkString(string HLinkString)
        {
            return AddressData[HLinkString];
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
        public override HLinkOCAddressModelCollection HLinkCollectionSort(HLinkOCAddressModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkAdressModel> t = collectionArg.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.GetDefaultText);

            HLinkOCAddressModelCollection tt = new HLinkOCAddressModelCollection();

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

            var temp = DataViewData.Where(x => x.GetDefaultText.Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (IAddressModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}