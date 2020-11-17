namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    // Event repository </summary>
    public class PersonNameDataView : DataViewBase<PersonNameModel, HLinkPersonNameModel, HLinkPersonNameModelCollection>, IPersonNameDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationDataView"/> class.
        /// </summary>
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
                return DataViewData.OrderBy(addressModel => addressModel.GetDefaultText).ToList();
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

        public override List<CommonGroupInfoCollection<PersonNameModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<PersonNameModel>> groups = new List<CommonGroupInfoCollection<PersonNameModel>>();

                var query = from item in DataViewData
                            orderby item.GetDefaultText
                            group item by item.GetDefaultText into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<PersonNameModel> info = new CommonGroupInfoCollection<PersonNameModel>();

                    // Handle 0's
                    if (string.IsNullOrEmpty(g.GroupName))
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

        public override CardGroupBase<HLinkPersonNameModel> GetAllAsCardGroup()
        {
            CardGroupBase<HLinkPersonNameModel> t = new CardGroupBase<HLinkPersonNameModel>();

            foreach (PersonNameModel item in DataDefaultSort)
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
        public HLinkPersonNameModelCollection GetAllAsHLink()
        {
            HLinkPersonNameModelCollection t = new HLinkPersonNameModelCollection();

            foreach (IPersonNameModel item in DataDefaultSort)
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
        public new HLinkHomeImageModel GetFirstImageFromCollection(HLinkPersonNameModelCollection theCollection)
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

                // do { } while (!mediaFoundFlag);
                for (int i = 0; i < theCollection.Count; i++)
                {
                    HLinkPersonNameModel currentHLink = theCollection[i];

                    returnMediaModel = currentHLink.DeRef.GCitationRefCollection.FirstHLinkHomeImage;

                    // TODO Still needed Handle Source Links
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

        public override CardGroupBase<HLinkPersonNameModel> GetLatestChanges()
        {
            throw new NotImplementedException();
        }

        public override PersonNameModel GetModelFromHLinkString(string HLinkString)
        {
            return PersonNameData[HLinkString];
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

            IOrderedEnumerable<HLinkPersonNameModel> t = collectionArg.OrderBy(HLinkAdressModel => HLinkAdressModel.DeRef.GetDefaultText);

            HLinkPersonNameModelCollection tt = new HLinkPersonNameModelCollection();

            foreach (HLinkPersonNameModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override CardGroupBase<HLinkPersonNameModel> Search(string argQuery)
        {
            CardGroupBase<HLinkPersonNameModel> itemsFound = new CardGroupBase<HLinkPersonNameModel>
            {
                Title = "Person Names"
            };

            if (string.IsNullOrEmpty(argQuery))
            {
                return itemsFound;
            }

            // Search by Full Name
            var temp = DataViewData.Where(x => x.FullName.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (PersonNameModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            // Search by First and Last Name
            temp = DataViewData.Where(x => x.ShortName.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (PersonNameModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            // Search by Called By
            temp = DataViewData.Where(x => x.GCall.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (PersonNameModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            // Search by Nick Name
            temp = DataViewData.Where(x => x.GNick.ToLower(CultureInfo.CurrentCulture).Contains(argQuery)).OrderBy(y => y.GetDefaultText);

            foreach (PersonNameModel tempMO in temp)
            {
                itemsFound.Add(tempMO.HLink);
            }

            return itemsFound;
        }
    }
}