using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;

using System.Collections;
using System.Globalization;

namespace GrampsView.Data.DataView
{

    /// <summary>
    ///   <br />
    /// </summary>
    public class TagDataView : DataViewBase<TagModel, HLinkTagModel, HLinkTagModelCollection>, ITagDataView
    {
        /// <summary>Initializes a new instance of the <see cref="TagDataView" /> class.</summary>
        public TagDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<TagModel> DataDefaultSort => DataViewData.OrderBy(TagModel => TagModel.GName).ToList();

        /// <summary>
        /// Gets the local tag data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<TagModel> DataViewData => TagData.Values.ToList();

        public override HLinkTagModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkTagModelCollection returnCardGroup = new();

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

        public RepositoryModelDictionary<TagModel, HLinkTagModel> TagData => DataStore.Instance.DS.TagData;

        public override HLinkTagModelCollection GetAllAsCardGroupBase()
        {
            HLinkTagModelCollection t = new();

            foreach (TagModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkTagModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkTagModelCollection> t = new();

            var query = from item in DataViewData
                        orderby item.GName

                        group item by item.GName into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkTagModelCollection info = new()
                {
                    Title = g.GroupName,
                };

                foreach (TagModel? item in g.Items)
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
            HLinkTagModelCollection t = new();

            foreach (TagModel item in DataDefaultSort)
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

            HLinkTagModelCollection tt = new();

            foreach (HLinkTagModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkTagModelCollection Search(string argQuery)
        {
            HLinkTagModelCollection itemsFound = new()
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

            IOrderedEnumerable<TagModel> temp = DataViewData.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (TagModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}