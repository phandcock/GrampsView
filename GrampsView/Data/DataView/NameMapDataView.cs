namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    // Event repository </summary>
    public class NameMapDataView : DataViewBase<NameMapModel, HLinkNameMapModel, HLinkNameMapModelCollection>, INameMapDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapDataView"/> class.
        /// </summary>
        public NameMapDataView()
        {
        }

        public override IReadOnlyList<NameMapModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(NameMapModel => NameMapModel.Id).ToList();
            }
        }

        /// <summary>
        /// Gets the local media data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<NameMapModel> DataViewData
        {
            get
            {
                return NameMapData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        [DataMember]
        public RepositoryModelDictionary<NameMapModel, HLinkNameMapModel> NameMapData
        {
            get
            {
                return DataStore.Instance.DS.NameMapData;
            }
        }

        public override CardGroupBase<HLinkNameMapModel> GetAllAsCardGroupBase()
        {
            CardGroupBase<HLinkNameMapModel> t = new CardGroupBase<HLinkNameMapModel>();

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
        public HLinkNameMapModelCollection GetAllAsHLink()
        {
            HLinkNameMapModelCollection t = new HLinkNameMapModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override NameMapModel GetModelFromHLinkString(string HLinkString)
        {
            return NameMapData[HLinkString];
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
        public override HLinkNameMapModelCollection HLinkCollectionSort(HLinkNameMapModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkNameMapModel> t = collectionArg.OrderBy(HLinkNameMapModel => HLinkNameMapModel.DeRef.HLinkKey);

            HLinkNameMapModelCollection tt = new HLinkNameMapModelCollection();

            foreach (HLinkNameMapModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkNameMapModel> Search(string argQuery)
        {
            CardGroupBase<HLinkNameMapModel> itemsFound = new CardGroupBase<HLinkNameMapModel>
            {
                Title = "Name Maps"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (NameMapModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}