namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using Xamarin.Essentials;

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
            ModelItemGlyph.Symbol = Common.CommonConstants.IconHeader;
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

        /// <summary>
        /// Gets or sets date the file was created.
        /// </summary>

        public string GCreatedDate
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the data version.
        /// </summary>

        public string GCreatedVersion
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the absolute path to the start of the media file file structure.
        /// </summary>

        public string GMediaPath
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers address.
        /// </summary>

        public string GResearcherAddress
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers city.
        /// </summary>

        public string GResearcherCity
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers country.
        /// </summary>

        public string GResearcherCountry
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers email address.
        /// </summary>

        public string GResearcherEmail
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers locality.
        /// </summary>

        public string GResearcherLocality
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers name.
        /// </summary>

        public string GResearcherName
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers phone.
        /// </summary>

        public string GResearcherPhone
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers postal address.
        /// </summary>

        public string GResearcherPostal
        {
            get;

            set;
        }

        /// <summary>
        /// Gets or sets the researchers state.
        /// </summary>

        public string GResearcherState
        {
            get;

            set;
        }

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