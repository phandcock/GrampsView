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
                return DataViewData.OrderBy(PlaceModel => PlaceModel.GetDefaultText).ToList();
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

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelDictionary<PlaceModel, HLinkPlaceModel> PlaceData
        {
            get
            {
                return DataStore.Instance.DS.PlaceData;
            }
        }

        public override CardGroupBase<HLinkPlaceModel> GetAllAsCardGroupBase()
        {
            CardGroupBase<HLinkPlaceModel> t = new CardGroupBase<HLinkPlaceModel>();

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
        public HLinkPlaceModelCollection GetAllAsHLink()
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CardGroupBase<HLinkPlaceModel> GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroupBase<HLinkPlaceModel> returnCardGroup = new CardGroupBase<HLinkPlaceModel>();

            foreach (PlaceModel item in tt)
            {
                returnCardGroup.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Place Changes";

            return returnCardGroup;
        }

        public override PlaceModel GetModelFromHLinkString(string HLinkString)
        {
            return PlaceData[HLinkString];
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

            IOrderedEnumerable<HLinkPlaceModel> t = collectionArg.OrderBy(HLinkPlaceModel => HLinkPlaceModel.DeRef.GPTitle);

            HLinkPlaceModelCollection tt = new HLinkPlaceModelCollection();

            foreach (HLinkPlaceModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkPlaceModel> Search(string argQuery)
        {
            CardGroupBase<HLinkPlaceModel> itemsFound = new CardGroupBase<HLinkPlaceModel>
            {
                Title = "Places"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            var temp = DataViewData.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (PlaceModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}