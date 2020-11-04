// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkMediaModel>))]
    public class HLinkMediaModelCollection : HLinkBaseCollection<HLinkMediaModel>
    {
        public HLinkMediaModelCollection()
        {
            Title = "Media Collection";
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
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        public void SortAndSetFirst()
        {
            // Set the first image link. Assumes main image is manually set to the first image in
            // Gramps if we need it to be, e.g. Citations.
            IMediaModel tempMediaModel = new MediaModel();

            FirstHLinkHomeImage.HomeImageType = CommonEnums.HomeImageType.Unknown;

            if (Count > 0)
            {
                // Step through each mediamodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempMediaModel = DataStore.DS.MediaData.GetModelFromHLink(this[i]);

                    if (tempMediaModel.IsMediaFile)
                    {
                        FirstHLinkHomeImage.ConvertFromMediaModel(this[i].DeRef);

                        break;
                    }
                }

                // Sort the collection
                List<HLinkMediaModel> t = this.OrderBy(hLinkMediaModel => hLinkMediaModel.DeRef.GDescription).ToList();

                Items.Clear();

                foreach (HLinkMediaModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}