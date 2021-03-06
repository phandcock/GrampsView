﻿// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    /// <summary>
    /// Collection of HLinks to Notes.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.HLinkBaseCollection{Data.ViewModel.HLinkNoteModel}"/>
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
        /// <para>Assumes that it is the first Note with a type of "Person Note" or "Biography".</para>
        /// </summary>
        /// <value>
        /// HLink to the first Type with a biography or person note. Returns an HLink with the Valid
        /// flag set to false if none found.
        /// </value>
        public HLinkNoteModel GetBio
        {
            get
            {
                HLinkNoteModel temp = this.FirstOrDefault(x => x.DeRef.GType == CommonConstants.NoteTypeBiography || x.DeRef.GType == CommonConstants.NoteTypePersonNote);

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
                    return this[0].DeRef.GStyledText.GText;
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

        public override void SetGlyph()
        {
            foreach (HLinkNoteModel argHLink in this)
            {
                ItemGlyph t = DV.NoteDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            if (CommonLocalSettings.SortHLinkCollections)
            {
                Sort();
            }
        }

        /// <summary>
        /// Helper method to sort and set the first image link.
        /// </summary>
        public void Sort()
        {
            // Sort the collection
            List<HLinkNoteModel> t = this.OrderBy(hlinkNoteModel => hlinkNoteModel.DeRef.GStyledText.GText).ToList();

            Items.Clear();

            foreach (HLinkNoteModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}