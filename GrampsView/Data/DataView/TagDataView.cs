namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Tag Data View.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.TagModel, GrampsView.Data.ViewModel.HLinkTagModel, GrampsView.Data.Collections.HLinkTagModelCollection}"/>
    /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.ITagDataView"/>
    public class TagDataView : DataViewBase<TagModel, HLinkTagModel, HLinkTagModelCollection>, ITagDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDataView"/> class.
        /// </summary>
        public TagDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<TagModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(TagModel => TagModel.GName).ToList();
            }
        }

        /// <summary>
        /// Gets the local tag data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<TagModel> DataViewData
        {
            get
            {
                return TagData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelDictionary<TagModel, HLinkTagModel> TagData
        {
            get
            {
                return DataStore.DS.TagData;
            }
        }

        public override CardGroupBase<HLinkTagModel> GetAllAsCardGroup()
        {
            CardGroupBase<HLinkTagModel> t = new CardGroupBase<HLinkTagModel>();

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
        /// Tag HLink Collection.
        /// </returns>
        public HLinkTagModelCollection GetAllAsHLink()
        {
            HLinkTagModelCollection t = new HLinkTagModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CardGroupBase<HLinkTagModel> GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroupBase<HLinkTagModel> returnCardGroup = new CardGroupBase<HLinkTagModel>();

            foreach (TagModel item in tt)
            {
                returnCardGroup.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Tag Changes";

            return returnCardGroup;
        }

        public override TagModel GetModelFromHLinkString(string HLinkString)
        {
            return TagData[HLinkString];
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted HLinks.
        /// </returns>
        public override HLinkTagModelCollection HLinkCollectionSort(HLinkTagModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkTagModel> t = collectionArg.OrderBy(HLinkTagModel => HLinkTagModel.DeRef.Handle);

            HLinkTagModelCollection tt = new HLinkTagModelCollection();

            foreach (HLinkTagModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkTagModel> Search(string queryString)
        {
            CardGroupBase<HLinkTagModel> itemsFound = new CardGroupBase<HLinkTagModel>();

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            if (string.IsNullOrEmpty(queryString))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).OrderBy(y => y.GetDefaultText);

            foreach (TagModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}