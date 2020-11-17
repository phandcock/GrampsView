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

    /// <summary>
    // Event repository </summary>
    public class CitationDataView : DataViewBase<CitationModel, HLinkCitationModel, HLinkCitationModelCollection>, ICitationDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView"/> class.
        /// </summary>
        public CitationDataView()
        {
        }

        /// <summary>
        /// Gets or sets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        public RepositoryModelDictionary<CitationModel, HLinkCitationModel> CitationData
        {
            get
            {
                return DataStore.Instance.DS.CitationData;
            }
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
                return CitationData.Values.ToList();
            }
        }

        public override List<CommonGroupInfoCollection<CitationModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<CitationModel>> groups = new List<CommonGroupInfoCollection<CitationModel>>();

                var query = from item in DataViewData
                            orderby item.GDateContent.SortDate
                            group item by item.GDateContent.GetDecade into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<CitationModel> info = new CommonGroupInfoCollection<CitationModel>();

                    // Handle 0's
                    if (g.GroupName == 0)
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

        public override CardGroupBase<HLinkCitationModel> GetAllAsCardGroup()
        {
            CardGroupBase<HLinkCitationModel> t = new CardGroupBase<HLinkCitationModel>();

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
        public HLinkCitationModelCollection GetAllAsHLink()
        {
            HLinkCitationModelCollection t = new HLinkCitationModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        /// <summary>
        /// Gets the first image from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// </returns>
        public new HLinkHomeImageModel GetFirstImageFromCollection(HLinkCitationModelCollection theCollection)
        {
            if (theCollection == null)
            {
                return null;
            }

            HLinkHomeImageModel returnMediaModel = new HLinkHomeImageModel();

            if (theCollection.Count > 0)
            {
                // step through each mediamodel hlink in the collection Accept either a direct
                // mediamodel reference or a hlink to a Source media reference.

                for (int i = 0; i < theCollection.Count; i++)
                {
                    HLinkCitationModel currentHLink = theCollection[i];

                    returnMediaModel = currentHLink.DeRef.GMediaRefCollection.FirstHLinkHomeImage;

                    // Handle Source Links
                    if (currentHLink.DeRef.HomeImageHLink.LinkToImage)
                    {
                        returnMediaModel = currentHLink.DeRef.HomeImageHLink;
                    }

                    if (returnMediaModel.Valid)
                    {
                        break;
                    }
                }
            }

            // return the image
            return returnMediaModel;
        }

        /// <summary>
        /// Gets the latest changes.
        /// </summary>
        /// <returns>
        /// </returns>
        public override CardGroupBase<HLinkCitationModel> GetLatestChanges()
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

        public override CitationModel GetModelFromHLinkString(string HLinkString)
        {
            return CitationData[HLinkString];
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

            var temp = DataViewData.Where(x => x.GDateContent.ShortDate.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (ICitationModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}