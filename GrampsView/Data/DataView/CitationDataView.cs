namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CitationDataView : DataViewBase<CitationModel, HLinkCitationModel, HLinkCitationModelCollection>, ICitationDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView"/> class.
        /// </summary>
        public CitationDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<CitationModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(citationModel => citationModel.GSourceRef.DeRef.GSTitle).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<CitationModel> DataViewData
        {
            get
            {
                return DataStore.Instance.DS.CitationData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets the latest changes.
        /// </summary>
        /// <returns>
        /// </returns>
        public override CardGroupBase<HLinkCitationModel> GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                CardGroupBase<HLinkCitationModel> returnCardGroup = new CardGroupBase<HLinkCitationModel>();

                foreach (ICitationModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Citation Changes";

                return returnCardGroup;
            }
        }

        public override CardGroup GetAllAsGroupedCardGroup()
        {
            CardGroup t = new CardGroup();

            var query = from item in DataViewData
                        orderby item.ToString(), item.GDateContent, item.GPage
                        group item by (item.ToString()) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                CardGroupBase<HLinkCitationModel> info = new CardGroupBase<HLinkCitationModel>
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
        public HLinkCitationModelCollection GetAllAsHLink()
        {
            HLinkCitationModelCollection t = new HLinkCitationModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CitationModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return DataStore.Instance.DS.CitationData[argHLinkKey.Value];
        }

        public override CitationModel GetModelFromId(string argId)
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
        public override HLinkCitationModelCollection HLinkCollectionSort(HLinkCitationModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkCitationModel> t = collectionArg.OrderBy(HLinkCitationModel => HLinkCitationModel.DeRef.GDateContent);

            HLinkCitationModelCollection tt = new HLinkCitationModelCollection();

            foreach (HLinkCitationModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkCitationModel> Search(string argQuery)
        {
            CardGroupBase<HLinkCitationModel> itemsFound = new CardGroupBase<HLinkCitationModel>
            {
                Title = "Citations"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.GDateContent.ShortDate.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (ICitationModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}