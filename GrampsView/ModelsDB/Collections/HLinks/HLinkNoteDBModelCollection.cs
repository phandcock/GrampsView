// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.ModelsDB.HLinks.Models;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;


namespace GrampsView.Models.Collections.HLinks
{


    /// <summary>
    /// // TODO Needs XML 1.71 check
    /// </summary>
    /// <seealso cref="HLinkBaseCollection&lt;HLinkNoteModel&gt;" />
    [KnownType(typeof(ObservableCollection<HLinkNoteDBModel>))]
    public class HLinkNoteDBModelCollection : HLinkDBBaseCollection<HLinkNoteDBModel>
    {
        public HLinkNoteDBModelCollection()
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
        public HLinkNoteDBModel GetBio
        {
            get
            {
                HLinkNoteDBModel temp = this.FirstOrDefault(x => x.DeRef.GType == Constants.NoteTypeBiography || x.DeRef.GType == Constants.NoteTypePersonNote);

                return temp is null ? new HLinkNoteDBModel() : temp;
            }
        }

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public INoteDBModel GetFirstModel => Count > 0 ? this[0].DeRef : (INoteDBModel)new NoteDBModel();

        /// <summary>
        /// Gets the get summary.
        /// </summary>
        /// <value>
        /// The get summary.
        /// </value>
        public string GetSummary => Count == 0 ? string.Empty : this[0].DeRef.GStyledText.GText;

        public HLinkNoteDBModelCollection GetCollectionWithoutOne(HLinkNoteDBModel argExcludedNoteModel)
        {
            HLinkNoteDBModelCollection t = new();

            foreach (HLinkNoteDBModel item in Items)
            {
                if (item.HLinkKey != argExcludedNoteModel.HLinkKey)
                {
                    t.Add(item);
                }
            }

            t.Title = Title;

            return t;
        }

        public HLinkNoteDBModel GetFirstOfType(string argType)
        {
            IEnumerable<HLinkNoteDBModel> q = Items.Where(HLinkNote => HLinkNote.DeRef.GType == argType);

            return q.Any() ? q.First() : new HLinkNoteDBModel();
        }

        public override void SetGlyph()
        {
            foreach (HLinkNoteDBModel argHLink in this)
            {
                ItemGlyph t = DL.NoteDL.GetGlyph(argHLink.HLinkKey);

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
            List<HLinkNoteDBModel> t = this.OrderBy(hlinkNoteModel => hlinkNoteModel.DeRef.GStyledText.GText).ToList();

            Items.Clear();

            foreach (HLinkNoteDBModel item in t)
            {
                Items.Add(item);
            }
        }
    }
}