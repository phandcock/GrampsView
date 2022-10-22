namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Models.DataModels.Minor;

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class PersonNameDataView : DataViewBase<PersonNameModel, HLinkPersonNameModel, HLinkPersonNameModelCollection>, IPersonNameDataView
    {
        public PersonNameDataView()
        {
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public override IReadOnlyList<PersonNameModel> DataDefaultSort
        {
            get
            {
                return DataViewData.OrderBy(addressModel => addressModel.ToString()).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override IReadOnlyList<PersonNameModel> DataViewData
        {
            get
            {
                return PersonNameData.Values.ToList();
            }
        }

        /// <summary>
        /// Gets or sets the citation data.
        /// </summary>
        /// <value>
        /// The citation data.
        /// </value>
        public RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel> PersonNameData
        {
            get
            {
                return DataStore.Instance.DS.PersonNameData;
            }
        }

        public override HLinkPersonNameModelCollection GetAllAsCardGroupBase()
        {
            HLinkPersonNameModelCollection t = new HLinkPersonNameModelCollection();

            foreach (PersonNameModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override Group<HLinkPersonNameModelCollection> GetAllAsGroupedCardGroup() => throw new System.NotImplementedException();

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkPersonNameModelCollection GetAllAsHLink()
        {
            HLinkPersonNameModelCollection t = new HLinkPersonNameModelCollection();

            foreach (IPersonNameModel item in DataDefaultSort)
            {
                t.Add(item.HLink);
            }

            return t;
        }

        public override PersonNameModel GetModelFromHLinkKey(HLinkKey argHLinkKey)
        {
            return PersonNameData[argHLinkKey.Value];
        }

        public override PersonNameModel GetModelFromId(string argId)
        {
            return DataViewData.Where(X => X.Id == argId).FirstOrDefault();
        }

        /// <summary>
        /// Sorts hlink address collection.
        /// </summary>
        /// <param name="collectionArg">
        /// The address collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkPersonNameModelCollection HLinkCollectionSort(HLinkPersonNameModelCollection collectionArg)
        {
            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkPersonNameModel> t = collectionArg.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.ToString());

            HLinkPersonNameModelCollection tt = new HLinkPersonNameModelCollection();

            foreach (HLinkPersonNameModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override HLinkPersonNameModelCollection Search(string argQuery)
        {
            argQuery = argQuery.ToLower();

            HLinkPersonNameModelCollection itemsFound = new HLinkPersonNameModelCollection
            {
                Title = "Person Names"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            // Search by Full Name Search by First and Last Name Search by Called By Search by Nick Name
            IEnumerable<PersonNameModel> temp = DataViewData
                .Where(x =>
                   (x.FullName.ToLower(CultureInfo.CurrentCulture).Contains(argQuery))
                || (x.ToString().ToLower(CultureInfo.CurrentCulture).Contains(argQuery))
                || (x.GCall.ToLower(CultureInfo.CurrentCulture).Contains(argQuery))
                || (x.GNick.ToLower(CultureInfo.CurrentCulture).Contains(argQuery))
                )
                .DistinctBy(x => x.HLinkKey.Value);

            IOrderedEnumerable<PersonNameModel> orderTemp = temp.OrderBy(x => x.GSurName.GetPrimarySurname);

            foreach (PersonNameModel tempMO in orderTemp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}