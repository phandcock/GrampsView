﻿namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Datamodel for media object files.
    /// </summary>
    public class MediaDataView : DataViewBase<MediaModel, HLinkMediaModel, HLinkMediaModelCollection>, IMediaDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaDataView"/> class.
        /// </summary>
        public MediaDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<MediaModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(MediaModel => MediaModel.GDescription).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<MediaModel> DataViewData
        {
            get
            {
                return DataStore.Instance.DS.MediaData.Values.ToList();
            }
        }

        public IReadOnlyList<MediaModel> DataViewDataPublic
        {
            get
            {
                return DataStore.Instance.DS.MediaData.Values.SkipWhile(MediaModel => MediaModel.IsInternalMediaFile).ToList();
            }
        }

        /// <summary>
        /// Gets the get groups by letter.
        /// </summary>
        /// <value>
        /// The get groups by letter.
        /// </value>
        public override List<CommonGroupInfoCollection<MediaModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<MediaModel>> groups = new List<CommonGroupInfoCollection<MediaModel>>();

                var query = from item in DataViewData
                            orderby item.GDescription
                            group item by (item.GDescription + " ").ToUpper(CultureInfo.CurrentCulture).Substring(0, 1) into g
                            select new
                            {
                                GroupName = g.Key,
                                Items = g
                            };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<MediaModel> info = new CommonGroupInfoCollection<MediaModel>
                    {
                        Key = g.GroupName,
                    };

                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }

                    groups.Add(info);
                }

                return groups;
            }
        }

        public override CardGroupBase<HLinkMediaModel> GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(NotClipped => NotClipped.IsInternalMediaFile == false).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                CardGroupBase<HLinkMediaModel> returnCardGroup = new CardGroupBase<HLinkMediaModel>();

                foreach (IMediaModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Media Changes";

                return returnCardGroup;
            }
        }

        /// <summary>
        /// Gets all as card group.
        /// </summary>
        /// <returns>
        /// CardGroup of all non-clipped media items
        /// </returns>
        /// <remarks>
        /// Only returns the original mediaitems and not the clipped ones added to speed things up.
        /// </remarks>
        public override CardGroupBase<HLinkMediaModel> GetAllAsCardGroupBase()
        {
            CardGroupBase<HLinkMediaModel> t = new CardGroupBase<HLinkMediaModel>();

            foreach (IMediaModel item in DataDefaultSort.SkipWhile(x => x.IsInternalMediaFile))
            {
                t.Add(item.HLink);
            }

            return t;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkMediaModelCollection GetAllAsHLink()
        {
            HLinkMediaModelCollection t = new HLinkMediaModelCollection();

            foreach (IMediaModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        public HLinkMediaModelCollection GetAllNotClippedAsHLink()
        {
            HLinkMediaModelCollection t = new HLinkMediaModelCollection();

            foreach (IMediaModel item in DataDefaultSort.Where(x => x.IsInternalMediaFile == false))
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        public override MediaModel GetModelFromHLinkString(string HLinkString)
        {
            return DataStore.Instance.DS.MediaData[HLinkString];
        }

        /// <summary>
        /// Gets the random from collection.
        /// </summary>
        /// <param name="argCollection">
        /// The collection.
        /// </param>
        /// <param name="DefaultHLink">
        /// The default h link.
        /// </param>
        /// <returns>
        /// </returns>
        public IHLinkMediaModel GetRandomFromCollection(HLinkMediaModelCollection argCollection)
        {
            // handle null argument
            if (argCollection == null)
            {
                argCollection = GetAllAsHLink();
            }

            IHLinkMediaModel tt = new HLinkMediaModel
            {
                // HLinkKey = DefaultHLink
            };

            Random randomValue = new Random();

            if (argCollection.Count > 0)
            {
                // get a random value
                int q = randomValue.Next(0, argCollection.Count);

                tt = argCollection[q];
            }

            // return the image hlink
            return tt;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="argCollection">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// </returns>
        public override HLinkMediaModelCollection HLinkCollectionSort(HLinkMediaModelCollection argCollection)
        {
            if (argCollection == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkMediaModel> t = argCollection.OrderBy(hLinkMediaModel => hLinkMediaModel.DeRef.GDescription);

            HLinkMediaModelCollection tt = new HLinkMediaModelCollection();

            foreach (HLinkMediaModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        /// <summary>
        /// Searches the media items.
        /// </summary>
        /// <param name="argQueryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// </returns>
        public override CardGroupBase<HLinkMediaModel> Search(string argQuery)
        {
            CardGroupBase<HLinkMediaModel> itemsFound = new CardGroupBase<HLinkMediaModel>
            {
                Title = "Media"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.GDescription.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (IMediaModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}