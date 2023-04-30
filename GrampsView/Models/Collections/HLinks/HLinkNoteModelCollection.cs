// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;


namespace GrampsView.Models.Collections.HLinks
{


    /// <summary>
    /// // TODO Needs XML 1.71 check
    /// </summary>
    /// <seealso cref="HLinkBaseCollection&lt;HLinkNoteModel&gt;" />
    [KnownType(typeof(ObservableCollection<HLinkNoteModel>))]
    public class HLinkNoteModelCollection : HLinkBaseCollection<HLinkNoteModel>
    {
        public HLinkNoteModelCollection()
        {
            Title = "Note Collection";
        }

        /// <summary>
        /// <para> Gets the get persons biography. </para>
        /// <para> Assumes that it is the first Note with a type of "Person Note" or "Biography". </para>
        /// </summary>
        /// <value>
        /// HLink to the first Type with a biography or person note. Returns an HLink with the Valid
        /// flag set to false if none found.
        /// </value>
        public HLinkNoteModel GetBio
        {
            get
            {
                HLinkNoteModel temp = this.FirstOrDefault(x => x.DeRef.GType == Constants.NoteTypeBiography || x.DeRef.GType == Constants.NoteTypePersonNote);

                return temp is null ? new HLinkNoteModel() : temp;
            }
        }

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public INoteModel GetFirstModel => Count > 0 ? this[0].DeRef : (INoteModel)new NoteModel();

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public string GetSummary => Count == 0 ? string.Empty : this[0].DeRef.GStyledText.GText;

        public HLinkNoteModelCollection GetCollectionWithoutOne(HLinkNoteModel argExcludedNoteModel)
        {
            HLinkNoteModelCollection t = new();

            foreach (HLinkNoteModel item in Items)
            {
                if (item.HLinkKey != argExcludedNoteModel.HLinkKey)
                {
                    t.Add(item);
                }
            }

            t.Title = Title;

            return t;
        }

        public HLinkNoteModel GetFirstOfType(string argType)
        {
            IEnumerable<HLinkNoteModel> q = Items.Where(HLinkNote => HLinkNote.DeRef.GType == argType);

            return q.Any() ? q.First() : new HLinkNoteModel();
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

            base.SetGlyph();
        }

        /// <summary>
        /// Helper method to sort and set the first image link.
        /// </summary>
        public override void Sort()
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