//-----------------------------------------------------------------------
//
// Storage routines for the FamilyModel
//
// <copyright file="HLinkEventModelCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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
    /// Collection of EVent $$(HLinks)$$.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkEventModel>))]
    public class HLinkEventModelCollection : HLinkBaseCollection<HLinkEventModel>
    {
        public HLinkEventModelCollection()
        {
            Title = "Event Collection";
        }

        /// <summary>
        /// Gets the de reference.
        /// </summary>
        /// <value>
        /// The de reference.
        /// </value>
        public ObservableCollection<EventModel> DeRef
        {
            get
            {
                ObservableCollection<EventModel> t = new ObservableCollection<EventModel>();

                foreach (HLinkEventModel item in Items)
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
            EventModel tempModel = new EventModel();

            FirstHLinkHomeImage.HomeImageType = CommonEnums.HomeImageType.Unknown;

            if (Count > 0)
            {
                // Step through each eventmodel hlink in the collection
                for (int i = 0; i < Count; i++)
                {
                    tempModel = DV.EventDV.EventData.GetModelFromHLink(this[i]);

                    if (tempModel.HomeImageHLink.LinkToImage)
                    {
                        FirstHLinkHomeImage = tempModel.HomeImageHLink;
                        break;
                    }
                }

                // Sort the collection
                List<HLinkEventModel> t = this.OrderBy(HLinkEventModel => HLinkEventModel.DeRef.GDate.SortDate).ToList();

                Items.Clear();

                foreach (HLinkEventModel item in t)
                {
                    Items.Add(item);
                }
            }
        }
    }
}