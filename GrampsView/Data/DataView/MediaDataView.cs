using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Interfaces;
using GrampsView.Models.HLinks.Interfaces;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GrampsView.Data.DataView
{
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
        public override IReadOnlyList<MediaModel> DataDefaultSort => DataViewData.OrderBy(MediaModel => MediaModel.GDescription).ToList();

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<MediaModel> DataViewData => DataStore.Instance.DS.MediaData.Values.ToList();

        public IReadOnlyList<MediaModel> DataViewDataPublic => DataStore.Instance.DS.MediaData.Values.Where(MediaModel => MediaModel.IsInternalMediaFile == false).ToList();

        public override HLinkMediaModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(NotIntenal => NotIntenal.IsInternalMediaFile == false).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkMediaModelCollection returnCardGroup = new();

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
        public override HLinkMediaModelCollection GetAllAsCardGroupBase()
        {
            HLinkMediaModelCollection t = new();

            foreach (IMediaModel item in DataDefaultSort.Where(x => x.IsInternalMediaFile == false))
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override Group<HLinkMediaModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkMediaModelCollection> t = new();

            var query = from item in DataViewData.Where(x => x.IsInternalMediaFile == false)

                        orderby item.FileContentType, item.GDescription
                        group item by item.FileContentType into g

                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkMediaModelCollection info = new()
                {
                    Title = g.GroupName,
                };

                foreach (MediaModel? item in g.Items)
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
        public HLinkMediaModelCollection GetAllAsHLink()
        {
            HLinkMediaModelCollection t = new();

            foreach (IMediaModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        public HLinkMediaModelCollection GetAllNotClippedAsHLink()
        {
            HLinkMediaModelCollection t = new();

            foreach (IMediaModel item in DataDefaultSort.Where(x => x.IsInternalMediaFile == false))
            {
                t.Add(item.HLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        public override MediaModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return DataStore.Instance.DS.MediaData[argHLinkKey.Value];
        }

        public override MediaModel GetModelFromId(string argId)
        {
            return DataViewData.Where(X => X.Id == argId).FirstOrDefault();
        }

        /// <summary>Gets the random from collection.</summary>
        /// <param name="argCollection">The collection.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public IHLinkMediaModel GetRandomFromCollection(HLinkMediaModelCollection argCollection)
        {
            // handle null argument
            argCollection ??= GetAllAsHLink();

            IHLinkMediaModel tt = new HLinkMediaModel
            {
                // HLinkKey = DefaultHLink
            };

            Random randomValue = new();

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

            HLinkMediaModelCollection tt = new();

            foreach (HLinkMediaModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        /// <summary>Searches the media items.</summary>
        /// <param name="argQuery"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        public override HLinkMediaModelCollection Search(string argQuery)
        {
            HLinkMediaModelCollection itemsFound = new()
            {
                Title = "Media"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            IOrderedEnumerable<MediaModel> temp = DataViewData.Where(NotIntenal => NotIntenal.IsInternalMediaFile == false).Where(x => x.GDescription.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (IMediaModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}