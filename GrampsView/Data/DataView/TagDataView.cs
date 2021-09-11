namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
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

        public override CardGroupBase<HLinkTagModel> GetLatestChanges
        {
            get
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
                return DataStore.Instance.DS.TagData;
            }
        }

        public override CardGroupBase<HLinkTagModel> GetAllAsCardGroupBase()
        {
            CardGroupBase<HLinkTagModel> t = new CardGroupBase<HLinkTagModel>();

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
                        orderby item.GName

                        group item by (item.GName) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                CardGroupBase<HLinkTagModel> info = new CardGroupBase<HLinkTagModel>
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

        public override TagModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return TagData[argHLinkKey.Value];
        }

        public override TagModel GetModelFromId(string argId)
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
        /// Sorted HLinks.
        /// </returns>
        public override HLinkTagModelCollection HLinkCollectionSort(HLinkTagModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkTagModel> t = collectionArg.OrderBy(HLinkTagModel => HLinkTagModel.DeRef.HLinkKey.Value);

            HLinkTagModelCollection tt = new HLinkTagModelCollection();

            foreach (HLinkTagModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkTagModel> Search(string argQuery)
        {
            CardGroupBase<HLinkTagModel> itemsFound = new CardGroupBase<HLinkTagModel>

            {
                Title = "Tags"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (TagModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}