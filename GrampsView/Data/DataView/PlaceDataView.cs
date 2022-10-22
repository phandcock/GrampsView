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

    /// <summary>
    /// View of Place data.
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.PlaceModel, GrampsView.Data.ViewModel.HLinkPlaceModel, GrampsView.Data.Collections.HLinkPlaceModelCollection}"/>
    /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IPlaceDataView"/>
    public class PlaceDataView : DataViewBase<PlaceModel, HLinkPlaceModel, HLinkPlaceModelCollection>, IPlaceDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceDataView"/> class.
        /// </summary>
        public PlaceDataView()
        {
        }

        public override IReadOnlyList<PlaceModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(PlaceModel => PlaceModel.ToString()).ToList();
            }
        }

        /// <summary>
        /// Gets the local place data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<PlaceModel> DataViewData
        {
            get
            {
                return PlaceData.Values.ToList();
            }
        }

        public override HLinkPlaceModelCollection GetLatestChanges
        {
            get
            {
                DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

                IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

                HLinkPlaceModelCollection returnCardGroup = new HLinkPlaceModelCollection();

                foreach (PlaceModel item in tt)
                {
                    returnCardGroup.Add(item.HLink);
                }

                returnCardGroup.Title = "Latest Place Changes";

                return returnCardGroup;
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>

        public RepositoryModelDictionary<PlaceModel, HLinkPlaceModel> PlaceData
        {
            get
            {
                return DataStore.Instance.DS.PlaceData;
            }
        }

        public override HLinkPlaceModelCollection GetAllAsCardGroupBase()
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            // Sort TODO Sort t = HLinkCollectionSort(t);

            return t;
        }

        public override Group<HLinkPlaceModelCollection> GetAllAsGroupedCardGroup()
        {
            Group<HLinkPlaceModelCollection> t = new Group<HLinkPlaceModelCollection>();

            var query = from item in DataViewData
                        orderby item.ToString()
                        group item by (item.ToString().Substring(0, 1).ToUpper()) into g
                        select new
                        {
                            GroupName = g.Key,
                            Items = g
                        };

            foreach (var g in query)
            {
                HLinkPlaceModelCollection info = new HLinkPlaceModelCollection
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
        public HLinkPlaceModelCollection GetAllAsHLink()
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override PlaceModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return PlaceData[argHLinkKey.Value];
        }

        public override PlaceModel GetModelFromId(string argId)
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
        public override HLinkPlaceModelCollection HLinkCollectionSort(HLinkPlaceModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkPlaceModel> t = collectionArg.OrderBy(HLinkPlaceModel => HLinkPlaceModel.DeRef.GPName);

            HLinkPlaceModelCollection tt = new HLinkPlaceModelCollection();

            foreach (HLinkPlaceModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkPlaceModelCollection Search(string argQuery)
        {
            HLinkPlaceModelCollection itemsFound = new HLinkPlaceModelCollection
            {
                Title = "Places"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.ToString());

            foreach (PlaceModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}