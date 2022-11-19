namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using SharedSharp.Model;

    

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
        public HeaderModel()
        {
            ModelItemGlyph.Symbol = Constants.IconHeader;
        }

        public CardListLineCollection AsCardListLineCollection
        {
            get
            {
                CardListLineCollection HeaderCard = new CardListLineCollection
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
                CardListLineCollection HeaderCard = this.AsCardListLineCollection;

                HeaderCard.AddRange(

                    new CardListLineCollection()
                    {
                        new CardListLine("Address Items", DV.AddressDV.DataViewData.Count),
                        new CardListLine("Citation Items", DV.CitationDV.DataViewData.Count),
                        new CardListLine("Event Items", DV.EventDV.DataViewData.Count),
                        new CardListLine("Family Items", DV.FamilyDV.DataViewData.Count),
                        new CardListLine("Media Items", DV.MediaDV.DataViewData.Count),
                        new CardListLine("Note Items", DV.NoteDV.DataViewData.Count),
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
        }

        public string GCreatedVersion
        {
            get;

            set;
        }

        public string GMediaPath
        {
            get;

            set;
        }

        public string GResearcherAddress
        {
            get;

            set;
        }

        public string GResearcherCity
        {
            get;

            set;
        }

        public string GResearcherCountry
        {
            get;

            set;
        }

        public string GResearcherEmail
        {
            get;

            set;
        }

        public string GResearcherLocality
        {
            get;

            set;
        }

        public string GResearcherName
        {
            get;

            set;
        }

        public string GResearcherPhone
        {
            get;

            set;
        }

        public string GResearcherPostal
        {
            get;

            set;
        }

        public string GResearcherState
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets date the file was created.
        /// </summary>
        /// <summary>
        /// Gets or sets the data version.
        /// </summary>
        /// <summary>
        /// Gets or sets the absolute path to the start of the media file file structure.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers address.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers city.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers country.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers email address.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers locality.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers name.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers phone.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers postal address.
        /// </summary>
        /// <summary>
        /// Gets or sets the researchers state.
        /// </summary>
        public HLinkHeaderModel HLink
        {
            get
            {
                HLinkHeaderModel t = new HLinkHeaderModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }
    }
}