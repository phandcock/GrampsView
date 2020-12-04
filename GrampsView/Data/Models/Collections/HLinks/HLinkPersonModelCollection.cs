// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Contains pointers to family models.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkPersonModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkPersonModel>))]
    public class HLinkPersonModelCollection : HLinkBaseCollection<HLinkPersonModel>
    {
        public HLinkPersonModelCollection()
        {
            Title = "People Collection";
        }

        public HLinkPersonModelCollection(string argTitle)
        {
            Title = argTitle;
        }

        /// <summary>
        /// Gets the dereferenced person models.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public ObservableCollection<PersonModel> DeRef
        {
            get
            {
                ObservableCollection<PersonModel> t = new ObservableCollection<PersonModel>();

                foreach (HLinkPersonModel item in Items)
                {
                    t.Add(item.DeRef);
                }

                return t;
            }
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            PersonModel tempModel = new PersonModel();

            FirstHLinkHomeImage.HomeImageType = CommonEnums.HomeImageType.Unknown;

            if (Count > 0)
            {
                // Step through each citationmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.PersonDV.PersonData.GetModelFromHLink(this[i]);

                    if (tempModel.HomeImageHLink.LinkToImage)
                    {
                        FirstHLinkHomeImage = tempModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkPersonModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GPersonNamesCollection.GetPrimaryName.DeRef.SortName).ToList();

                Items.Clear();

                foreach (HLinkPersonModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}