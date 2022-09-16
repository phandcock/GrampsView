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

    public class SourceDataView : DataViewBase<SourceModel, HLinkSourceModel, HLinkSourceModelCollection>, ISourceDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceDataView"/> class.
        /// </summary>
        public SourceDataView()
        {
        }

        public override IReadOnlyList<SourceModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(SourceModel => SourceModel.GSTitle).ToList();
            }
        }

        /// <summary>
        /// Gets the local source data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<SourceModel> DataViewData
        {
            get
            {
                return SourceData.Values.ToList();
            }
        }

        public override HLinkSourceModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkSourceModelCollection returnCardGroup = new HLinkSourceModelCollection();

                foreach (SourceModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Source Changes";

                return returnCardGroup;
            }
        }

        /// <summary>
        /// Gets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>

        public RepositoryModelDictionary<SourceModel, HLinkSourceModel> SourceData
        {
            get
            {
                return DataStore.Instance.DS.SourceData;
            }

            // set { this.SetProperty(ref DataStore.Instance.DS.SourceData, value); }
        }

        public override HLinkSourceModelCollection GetAllAsCardGroupBase()
        {
            HLinkSourceModelCollection t = new HLinkSourceModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkSourceModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkSourceModelCollection> t = new Group<HLinkSourceModelCollection>();

            var query = from item in DataViewData
                        orderby item.GRepositoryRefCollection.GetFirst.ToString(), item.GSTitle
                        group item by (item.GRepositoryRefCollection.GetFirst.ToString()) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkSourceModelCollection info = new HLinkSourceModelCollection { };

                if (g.GroupName != null)
                {
                    info.Title = g.GroupName;
                }
                else
                {
                    info.Title = "Unknown";
                }

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
        public HLinkSourceModelCollection GetAllAsHLink()
        {
            HLinkSourceModelCollection t = new HLinkSourceModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override SourceModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return SourceData[argHLinkKey.Value];
        }

        public override SourceModel GetModelFromId(string argId)
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
        public override HLinkSourceModelCollection HLinkCollectionSort(HLinkSourceModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkSourceModel> t = collectionArg.OrderBy(HLinkSourceModel => HLinkSourceModel.DeRef.GSTitle);

            HLinkSourceModelCollection tt = new HLinkSourceModelCollection();

            foreach (HLinkSourceModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkSourceModelCollection Search(string argQuery)
        {
            HLinkSourceModelCollection itemsFound = new HLinkSourceModelCollection
            {
                Title = "Sources"
            };
            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (SourceModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}