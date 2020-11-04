// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of HLinks to Notes.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{GrampsView.Data.ViewModel.HLinkNoteModel}"/>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkNoteModel>))]
    public class HLinkNoteModelCollection : HLinkBaseCollection<HLinkNoteModel>
    {
        public HLinkNoteModelCollection()
        {
            Title = "Note Collection";
        }

        /// <summary>
        /// <para>Gets the get persons biography.</para>
        /// <para>Assumes that it is the first Note with a type of "Person Note".</para>
        /// </summary>
        /// <value>
        /// HLink to the first person note. Returns an HLink with the Valis flag set to false if
        /// none found.
        /// </value>
        public HLinkNoteModel GetBio
        {
            get
            {
                HLinkNoteModel temp = this.Where(x => x.DeRef.GType == "Person Note").FirstOrDefault();

                if (temp is null)
                {
                    return new HLinkNoteModel();
                }

                return temp;
            }
        }

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public INoteModel GetFirstModel
        {
            get
            {
                if (Count > 0)
                {
                    return this[0].DeRef;
                }
                else
                {
                    return new NoteModel();
                }
            }
        }

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public string GetSummary
        {
            get
            {
                if (Count == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return this[0].DeRef.GText;
                }
            }
        }

        public override CardGroup GetCardGroup()
        {
            CardGroup t = base.GetCardGroup();

            t.Title = Title;

            return t;
        }

        public CardGroup GetCardGroupWithoutBio()
        {
            CardGroup t = new CardGroup();

            HLinkNoteModel bio = GetBio;

            foreach (HLinkNoteModel item in Items)
            {
                if (item.HLinkKey != bio.HLinkKey)
                {
                    t.Add(item);
                }
            }

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

            FirstHLinkHomeImage.HomeImageType = CommonEnums.HomeImageType.Unknown;

            if (Count > 0)
            {
                // For Note collections just grab the first one
                FirstHLinkHomeImage = this[0].DeRef.HomeImageHLink;

                // Sort the collection
                List<HLinkNoteModel> t = this.OrderBy(hlinkNoteModel => hlinkNoteModel.DeRef.GText).ToList();

                Items.Clear();

                foreach (HLinkNoteModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}