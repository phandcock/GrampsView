namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;


    /// <summary>
    ///   <br />
    /// </summary>
    public class NameMapDataView : DataViewBase<NameMapModel, HLinkNameMapModel, HLinkNameMapModelCollection>, INameMapDataView
    {
        /// <summary>Initializes a new instance of the <see cref="NameMapDataView" /> class.</summary>
        public NameMapDataView()
        {
        }

        public override IReadOnlyList<NameMapModel> DataDefaultSort => DataViewData.OrderBy(NameMapModel => NameMapModel.Id).ToList();

        /// <summary>
        /// Gets the local media data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<NameMapModel> DataViewData => NameMapData.Values.ToList();

        /// <summary>
        /// Gets or sets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>

        public RepositoryModelDictionary<NameMapModel, HLinkNameMapModel> NameMapData => DataStore.Instance.DS.NameMapData;

        public override HLinkNameMapModelCollection GetAllAsCardGroupBase()
        {
            HLinkNameMapModelCollection t = new();

            foreach (NameMapModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkNameMapModelCollection> GetAllAsGroupedCardGroup()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkNameMapModelCollection GetAllAsHLink()
        {
            HLinkNameMapModelCollection t = new();

            foreach (NameMapModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override NameMapModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return NameMapData[argHLinkKey.Value];
        }

        public override NameMapModel GetModelFromId(string argId)
        {
            return DataViewData.Where(X => X.Id == argId).FirstOrDefault();
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

            HLinkNameMapModelCollection tt = new();

            foreach (HLinkNameMapModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkNameMapModelCollection Search(string argQuery)
        {
            HLinkNameMapModelCollection itemsFound = new()
            {
                Title = "Name Maps"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IOrderedEnumerable<NameMapModel> temp = DataViewData.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (NameMapModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}