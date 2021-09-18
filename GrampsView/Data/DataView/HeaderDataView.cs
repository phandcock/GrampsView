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
    /// repository for the XML file header.
    /// </summary>
    public class HeaderDataView : DataViewBase<HeaderModel, HLinkHeaderModel, HLinkHeaderModelCollection>, IHeaderDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderDataView"/> class.
        /// </summary>
        public HeaderDataView()
        {
        }

        public override IReadOnlyList<HeaderModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(HeaderModel => HeaderModel.GCreatedDate).ToList();
            }
        }

        public override IReadOnlyList<HeaderModel> DataViewData
        {
            get
            {
                return HeaderData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        public RepositoryModelDictionary<HeaderModel, HLinkHeaderModel> HeaderData
        {
            get
            {
                return DataStore.Instance.DS.HeaderData;
            }
        }

        /// <summary>
        /// Gets the header data.
        /// </summary>
        /// <value>
        /// The header data.
        /// </value>
        public HeaderModel HeaderDataModel
        {
            get
            {
                if (HeaderData.Count > 0)
                {
                    return DataViewData[0];
                }
                else
                {
                    return new HeaderModel();
                }
            }
        }

        /// <summary>
        /// Gets header as card group.
        /// </summary>
        /// <returns>
        /// CardGroup
        /// </returns>
        /// <remarks>
        /// Assume sonly one header as per the spec.
        /// </remarks>
        public override HLinkHeaderModelCollection GetAllAsCardGroupBase()
        {
            HLinkHeaderModelCollection t = new HLinkHeaderModelCollection();

            t.Add(HeaderDataModel.HLink);

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkHeaderModelCollection> GetAllAsGroupedCardGroup() => throw new System.NotImplementedException();

        public override HeaderModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return HeaderData.Values.First();
        }

        public override HeaderModel GetModelFromId(string argId)
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
        public override HLinkHeaderModelCollection HLinkCollectionSort(HLinkHeaderModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkHeaderModel> t = collectionArg.OrderBy(HLinkHeaderModel => HLinkHeaderModel.DeRef.GMediaPath);

            HLinkHeaderModelCollection tt = new HLinkHeaderModelCollection();

            foreach (HLinkHeaderModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkHeaderModelCollection Search(string argQuery)
        {
            HLinkHeaderModelCollection itemsFound = new HLinkHeaderModelCollection();

            return itemsFound;
        }
    }
}