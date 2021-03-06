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

        public override void SetGlyph()
        {
            foreach (HLinkPersonModel argHLink in this)
            {
                ItemGlyph t = DV.PersonDV.GetGlyph(argHLink.HLinkKey);

                argHLink.HLinkGlyphItem.ImageType = t.ImageType;
                argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            if (Common.CommonLocalSettings.SortHLinkCollections)
            {
                Sort();
            }
        }

        /// <summary>
        /// Helper method to sort and set the firt image link.
        /// </summary>
        public void Sort()
        {
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