// TODO Needs XML 1.71 check

////<code>
////  <element name = "header" >
////    < element name="created">
////      <attribute name = "date" >
////        < data type="date" />
////      </attribute>
////      <attribute name = "version" >
////        < text />
////      </ attribute >
////    </ element >
////    < element name="researcher">
////      <optional>
////        <ref name="researcher-content" />
////      </optional>
////    </element>
////    <optional>
////      <element name = "mediapath" >
////        < text />
////      </ element >
////    </ optional >
////  </ element >

////      <define name = "researcher-content" >
////  < element name="resname">
////    <text />
////  </element>
////  <optional>
////    <element name = "resaddr" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="reslocality">
////      <text />
////    </element>
////  </optional>
////  <optional>
////    <element name = "rescity" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="resstate">
////      <text />
////    </element>
////  </optional>
////  <optional>
////    <element name = "rescountry" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="respostal">
////      <text />
////    </element>
////  </optional>
////  <optional>
////    <element name = "resphone" >
////      < text />
////    </ element >
////  </ optional >
////  < optional >
////    < element name="resemail">
////      <text />
////    </element>
////  </optional>
////</define>

/// TODO Update fields as per Schema

namespace GrampsView.Data.Model
{
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;

    using Xamarin.Essentials;

    //// </code>
    [DataContract]
    public class HeaderModel : ModelBase, IHeaderModel
    {
        /// <summary>
        /// The local database version.
        /// </summary>
        private int _DatabaseVersion;

        /// <summary>
        /// created date.
        /// </summary>
        private string _GCreatedDate = string.Empty;

        /// <summary>
        /// crated version.
        /// </summary>
        private string _GCreatedVersion = string.Empty;

        /// <summary>
        /// Media Path.
        /// </summary>
        private string _GMediaPath = string.Empty;

        /// <summary>
        /// Researcher Address.
        /// </summary>
        private string _GResearcherAddress = string.Empty;

        /// <summary>
        /// Researcher City.
        /// </summary>
        private string _GResearcherCity = string.Empty;

        /// <summary>
        /// Researcher Country.
        /// </summary>
        private string _GResearcherCountry = string.Empty;

        /// <summary>
        /// Researcher Email.
        /// </summary>
        private string _GResearcherEmail = string.Empty;

        /// <summary>
        /// Researcher Locality.
        /// </summary>
        private string _GResearcherLocality = string.Empty;

        /// <summary>
        /// Researcher Name.
        /// </summary>
        private string _GResearcherName = string.Empty;

        /// <summary>
        /// Researcher Phone.
        /// </summary>
        private string _GResearcherPhone = string.Empty;

        /// <summary>
        /// Researcher Postal Address.
        /// </summary>
        private string _GResearcherPostal = string.Empty;

        /// <summary>
        /// Researcher State.
        /// </summary>
        private string _GResearcherState = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderModel"/> class.
        /// </summary>
        public HeaderModel()
        {
            HomeImageHLink.HomeSymbol = Common.CommonConstants.IconHeader;
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
                        new CardListLine("Researcher State:", GResearcherState),
                        new CardListLine("Researcher Country:", GResearcherCountry),
                        new CardListLine("Researcher Email:", GResearcherEmail),
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
                CardListLineCollection HeaderCard = new CardListLineCollection
                    {
                        new CardListLine("Created using version:", GCreatedVersion),
                        new CardListLine("Created on:", GCreatedDate),
                        new CardListLine("Researcher Name:", GResearcherName),
                        new CardListLine("Researcher State:", GResearcherState),
                        new CardListLine("Researcher Country:", GResearcherCountry),
                        new CardListLine("Researcher Email:", GResearcherEmail),
                        new CardListLine("MediaPath:", GMediaPath),
                        new CardListLine("Application Version:", VersionTracking.CurrentVersion),

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

            };

                HeaderCard.Title = "Header Details";

                return HeaderCard;
            }
        }

        /// <summary>
        /// Gets or sets the database version.
        /// </summary>
        /// <value>
        /// The database version.
        /// </value>
        [DataMember]
        public int DatabaseVersion
        {
            get
            {
                return _DatabaseVersion;
            }

            set
            {
                SetProperty(ref _DatabaseVersion, value);
            }
        }

        /// <summary>
        /// Gets or sets date the metadata was created.
        /// </summary>
        [DataMember]
        public string GCreatedDate
        {
            get
            {
                return _GCreatedDate;
            }

            set
            {
                SetProperty(ref _GCreatedDate, value);
            }
        }

        /// <summary>
        /// Gets or sets the data version.
        /// </summary>
        [DataMember]
        public string GCreatedVersion
        {
            get
            {
                return _GCreatedVersion;
            }

            set
            {
                SetProperty(ref _GCreatedVersion, value);
            }
        }

        /// <summary>
        /// Gets or sets the absolute path to the start of the media file file structure.
        /// </summary>
        [DataMember]
        public string GMediaPath
        {
            get
            {
                return _GMediaPath;
            }

            set
            {
                SetProperty(ref _GMediaPath, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers address.
        /// </summary>
        [DataMember]
        public string GResearcherAddress
        {
            get
            {
                return _GResearcherAddress;
            }

            set
            {
                SetProperty(ref _GResearcherAddress, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers city.
        /// </summary>
        [DataMember]
        public string GResearcherCity
        {
            get
            {
                return _GResearcherCity;
            }

            set
            {
                SetProperty(ref _GResearcherCity, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers country.
        /// </summary>
        [DataMember]
        public string GResearcherCountry
        {
            get
            {
                return _GResearcherCountry;
            }

            set
            {
                SetProperty(ref _GResearcherCountry, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers email address.
        /// </summary>
        [DataMember]
        public string GResearcherEmail
        {
            get
            {
                return _GResearcherEmail;
            }

            set
            {
                SetProperty(ref _GResearcherEmail, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers locality.
        /// </summary>
        [DataMember]
        public string GResearcherLocality
        {
            get
            {
                return _GResearcherLocality;
            }

            set
            {
                SetProperty(ref _GResearcherLocality, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers name.
        /// </summary>
        [DataMember]
        public string GResearcherName
        {
            get
            {
                return _GResearcherName;
            }

            set
            {
                SetProperty(ref _GResearcherName, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers phone.
        /// </summary>
        [DataMember]
        public string GResearcherPhone
        {
            get
            {
                return _GResearcherPhone;
            }

            set
            {
                SetProperty(ref _GResearcherPhone, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers postal address.
        /// </summary>
        [DataMember]
        public string GResearcherPostal
        {
            get
            {
                return _GResearcherPostal;
            }

            set
            {
                SetProperty(ref _GResearcherPostal, value);
            }
        }

        /// <summary>
        /// Gets or sets the researchers state.
        /// </summary>
        [DataMember]
        public string GResearcherState
        {
            get
            {
                return _GResearcherState;
            }

            set
            {
                SetProperty(ref _GResearcherState, value);
            }
        }

        public HLinkHeaderModel HLink
        {
            get
            {
                HLinkHeaderModel t = new HLinkHeaderModel
                {
                    HLinkKey = HLinkKey,
                };

                return t;
            }
        }
    }
}