//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="RepositoryDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.DataView.DataViewBase{GrampsView.Data.ViewModel.RepositoryModel, GrampsView.Data.ViewModel.HLinkRepositoryModel, GrampsView.Data.Collections.HLinkRepositoryModelCollection}"/>
    /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IRepositoryDataView"/>
    public class RepositoryDataView : DataViewBase<RepositoryModel, HLinkRepositoryModel, HLinkRepositoryModelCollection>, IRepositoryDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDataView"/> class.
        /// </summary>
        public RepositoryDataView()
        {
        }

        public override IReadOnlyList<RepositoryModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(RepositoryModel => RepositoryModel.GRName).ToList();
            }
        }

        /// <summary>
        /// Gets the local repository data.
        /// </summary>
        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<RepositoryModel> DataViewData
        {
            get
            {
                return RepositoryData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        [DataMember]
        public RepositoryModelDictionary<RepositoryModel, HLinkRepositoryModel> RepositoryData
        {
            get
            {
                return DataStore.DS.RepositoryData;
            }
        }

        public override CardGroup GetAllAsCardGroup()
        {
            CardGroup t = new CardGroup();

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
        public HLinkRepositoryModelCollection GetAllAsHLink()
        {
            HLinkRepositoryModelCollection t = new HLinkRepositoryModelCollection();

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override CardGroup GetLatestChanges()
        {
            DateTime lastSixtyDays = DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0, 0));

            IEnumerable tt = DataViewData.OrderByDescending(GetLatestChangest => GetLatestChangest.Change).Where(GetLatestChangestt => GetLatestChangestt.Change > lastSixtyDays).Take(3);

            CardGroup returnCardGroup = new CardGroup();

            foreach (RepositoryModel item in tt)
            {
                returnCardGroup.Add(item.HLink);
            }

            returnCardGroup.Title = "Latest Repository Changes";

            return returnCardGroup;
        }

        public override RepositoryModel GetModelFromHLinkString(string HLinkString)
        {
            return RepositoryData[HLinkString];
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
        public override HLinkRepositoryModelCollection HLinkCollectionSort(HLinkRepositoryModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkRepositoryModel> t = collectionArg.OrderBy(HLinkRepositoryModel => HLinkRepositoryModel.DeRef.GRName);

            HLinkRepositoryModelCollection tt = new HLinkRepositoryModelCollection();

            foreach (HLinkRepositoryModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = DataViewData.Where(x => x.GetDefaultText.ToLower(CultureInfo.CurrentCulture).Contains(queryString)).OrderBy(y => y.GetDefaultText);

            foreach (RepositoryModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.HLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            return itemsFound;
        }
    }
}