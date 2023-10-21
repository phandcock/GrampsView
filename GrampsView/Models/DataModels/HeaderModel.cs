// Copyright (c) phandcock. All rights reserved.

using GrampsView.Common;
using GrampsView.Data.DataView;
using GrampsView.Data.Model;

namespace GrampsView.Models.DataModels
{
    /// <summary>
    /// Data model for a Gramps file header.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Done </description>
    /// </item>
    /// </list>
    /// </summary>

    public class HeaderModel : ModelBase, IHeaderModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderModel"/> class.
        /// </summary>

        public CardListLineCollection AsCardListLineCollection
        {
            get
            {
                CardListLineCollection HeaderCard = new()
                {
                        new CardListLine("Created using version:", GCreatedVersion),
                        new CardListLine("Created on:", GCreatedDate),

                        new CardListLine("Researcher Name:", GResearcherName),
                        new CardListLine("Researcher Address:", GResearcherAddress),
                        new CardListLine("Researcher City:", GResearcherCity),
                        new CardListLine("Researcher Locality:", GResearcherLocality),
                        new CardListLine("Researcher State:", GResearcherState),
                        new CardListLine("Researcher Country:", GResearcherCountry),
                        new CardListLine("Researcher Email:", GResearcherEmail),
                        new CardListLine("Researcher Phone:", GResearcherPhone),
                        new CardListLine("Researcher Postal:", GResearcherPostal),
                        new CardListLine("MediaPath:", GMediaPath),
                        new CardListLine("Application Version:", VersionTracking.CurrentVersion),
            };

                HeaderCard.Title = "Header Details";

                return HeaderCard;
            }
        }

        public CardListLineCollection DetailAsCardListLineCollection
        {
            get
            {
                CardListLineCollection HeaderCard = AsCardListLineCollection;

                HeaderCard.AddRange(

                    new CardListLineCollection()
                    {
                        new CardListLine("Address Items", DV.AddressDV.DataViewData.Count),
                        new CardListLine("Citation Items", DL.CitationDL.DataAsList.Count),
                        new CardListLine("Event Items", DL.EventDL.DataAsList.Count),
                        new CardListLine("Family Items", DL.FamilyDL.DataAsList.Count),
                        new CardListLine("Media Items", DV.MediaDV.DataViewData.Count),
                        new CardListLine("Note Items", DL.NoteDL.Count),
                        new CardListLine("Person Items", DV.PersonDV.DataViewData.Count),
                        new CardListLine("Person Name Items", DV.PersonNameDV.DataViewData.Count),
                        new CardListLine("Place Items", DV.PlaceDV.DataViewData.Count),
                        new CardListLine("Repository Items", DV.RepositoryDV.DataViewData.Count),
                        new CardListLine("Source Items", DV.SourceDV.DataViewData.Count),
                        new CardListLine("Tag Items", DV.TagDV.DataViewData.Count),
            });

                return HeaderCard;
            }
        }

        public string GCreatedDate
        {
            get;

            set;
        } = string.Empty;

        public string GCreatedVersion
        {
            get;

            set;
        } = string.Empty;

        public string GMediaPath
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherAddress
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherCity
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherCountry
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherEmail
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherLocality
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherName
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherPhone
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherPostal
        {
            get;

            set;
        } = string.Empty;

        public string GResearcherState
        {
            get;

            set;
        } = string.Empty;

        public HLinkHeaderModel HLink
        {
            get
            {
                HLinkHeaderModel t = new()
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public HeaderModel()
        {
            ModelItemGlyph.Symbol = Constants.IconHeader;
        }
    }
}