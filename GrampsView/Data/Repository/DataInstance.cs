/// <summary>
/// Data Repository singleton
/// </summary>
namespace GrampsView.Data.Repository
{
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Models.DataModels;
    using GrampsView.Models.DataModels.Minor;
    using GrampsView.Models.HLinks.Models;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Static Data Store.
    /// </summary>

    [KnownType(typeof(ObservableCollection<object>))]
    public class DataInstance : ObservableObject
    {
        /// <summary>
        /// The address data
        /// </summary>
        private RepositoryModelDictionary<AddressModel, HLinkAdressModel> _AddressData = new RepositoryModelDictionary<AddressModel, HLinkAdressModel>();

        /// <summary>
        /// The book mark collection
        /// </summary>
        private HLinkBackLinkModelCollection _BookMarkCollection = new HLinkBackLinkModelCollection();

        /// <summary>
        /// The citation data
        /// </summary>
        private RepositoryModelDictionary<CitationModel, HLinkCitationModel> _CitationData = new RepositoryModelDictionary<CitationModel, HLinkCitationModel>();

        /// <summary>
        /// The local event data.
        /// </summary>
        private RepositoryModelDictionary<EventModel, HLinkEventModel> _EventData = new RepositoryModelDictionary<EventModel, HLinkEventModel>();

        /// <summary>
        /// The local family data.
        /// </summary>
        private RepositoryModelDictionary<FamilyModel, HLinkFamilyModel> _FamilyData = new RepositoryModelDictionary<FamilyModel, HLinkFamilyModel>();

        /// <summary>
        /// The local header data.
        /// </summary>
        private RepositoryModelDictionary<HeaderModel, HLinkHeaderModel> _HeaderData = new RepositoryModelDictionary<HeaderModel, HLinkHeaderModel>();

        /// <summary>
        /// The local media data.
        /// </summary>
        private RepositoryModelDictionary<MediaModel, HLinkMediaModel> _MediaData = new RepositoryModelDictionary<MediaModel, HLinkMediaModel>();

        /// <summary>
        /// The local name map data.
        /// </summary>
        private RepositoryModelDictionary<NameMapModel, HLinkNameMapModel> _NameMapData = new RepositoryModelDictionary<NameMapModel, HLinkNameMapModel>();

        /// <summary>
        /// The local note data.
        /// </summary>
        private RepositoryModelDictionary<NoteModel, HLinkNoteModel> _NoteData = new RepositoryModelDictionary<NoteModel, HLinkNoteModel>();

        /// <summary>
        /// The local person data.
        /// </summary>
        private RepositoryModelDictionary<PersonModel, HLinkPersonModel> _PersonData = new RepositoryModelDictionary<PersonModel, HLinkPersonModel>();

        /// <summary>
        /// The local person name data.
        /// </summary>
        private RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel> _PersonNameData = new RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel>();

        /// <summary>
        /// The local place data.
        /// </summary>
        private RepositoryModelDictionary<PlaceModel, HLinkPlaceModel> _PlaceData = new RepositoryModelDictionary<PlaceModel, HLinkPlaceModel>();

        /// <summary>
        /// The local repository data.
        /// </summary>
        private RepositoryModelDictionary<RepositoryModel, HLinkRepositoryModel> _RepositoryData = new RepositoryModelDictionary<RepositoryModel, HLinkRepositoryModel>();

        /// <summary>
        /// The local source data.
        /// </summary>
        private RepositoryModelDictionary<SourceModel, HLinkSourceModel> _SourceData = new RepositoryModelDictionary<SourceModel, HLinkSourceModel>();

        /// <summary>
        /// The local tag data.
        /// </summary>
        private RepositoryModelDictionary<TagModel, HLinkTagModel> _TagData = new RepositoryModelDictionary<TagModel, HLinkTagModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataInstance"/> class.
        /// </summary>
        public DataInstance()
        {
        }

        [JsonInclude]
        public RepositoryModelDictionary<AddressModel, HLinkAdressModel> AddressData
        {
            get
            {
                return _AddressData;
            }

            set
            {
                SetProperty(ref _AddressData, value);
            }
        }

        [JsonInclude]
        public HLinkBackLinkModelCollection BookMarkCollection
        {
            get
            {
                return _BookMarkCollection;
            }

            set
            {
                SetProperty(ref _BookMarkCollection, value);
            }
        }

        /// <summary>
        /// The local citation data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<CitationModel, HLinkCitationModel> CitationData
        {
            get
            {
                return _CitationData;
            }

            set
            {
                SetProperty(ref _CitationData, value);
            }
        }

        /// <summary>
        /// The local Event data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<EventModel, HLinkEventModel> EventData
        {
            get
            {
                return _EventData;
            }

            set
            {
                SetProperty(ref _EventData, value);
            }
        }

        /// <summary>
        /// The local Family data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<FamilyModel, HLinkFamilyModel> FamilyData
        {
            get
            {
                return _FamilyData;
            }

            set
            {
                SetProperty(ref _FamilyData, value);
            }
        }

        /// <summary>
        /// The local Header data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<HeaderModel, HLinkHeaderModel> HeaderData
        {
            get
            {
                return _HeaderData;
            }

            set
            {
                SetProperty(ref _HeaderData, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is data loaded.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is data loaded; otherwise, <c> false </c>.
        /// </value>

        public bool IsDataLoaded
        {
            get;
            set;
        }

        /// <summary>
        /// The local Media data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<MediaModel, HLinkMediaModel> MediaData
        {
            get
            {
                return _MediaData;
            }

            set
            {
                SetProperty(ref _MediaData, value);
            }
        }

        /// <summary>
        /// The local NameMap data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<NameMapModel, HLinkNameMapModel> NameMapData
        {
            get
            {
                return _NameMapData;
            }

            set
            {
                SetProperty(ref _NameMapData, value);
            }
        }

        /// <summary>
        /// The local Note data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<NoteModel, HLinkNoteModel> NoteData
        {
            get
            {
                return _NoteData;
            }

            set
            {
                SetProperty(ref _NoteData, value);
            }
        }

        /// <summary>
        /// The local Person data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<PersonModel, HLinkPersonModel> PersonData
        {
            get
            {
                return _PersonData;
            }

            set
            {
                SetProperty(ref _PersonData, value);
            }
        }

        [JsonInclude]
        public RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel> PersonNameData
        {
            get
            {
                return _PersonNameData;
            }

            set
            {
                SetProperty(ref _PersonNameData, value);
            }
        }

        /// <summary>
        /// The local Place data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<PlaceModel, HLinkPlaceModel> PlaceData
        {
            get
            {
                return _PlaceData;
            }

            set
            {
                SetProperty(ref _PlaceData, value);
            }
        }

        /// <summary>
        /// The local Place data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<RepositoryModel, HLinkRepositoryModel> RepositoryData
        {
            get
            {
                return _RepositoryData;
            }

            set
            {
                SetProperty(ref _RepositoryData, value);
            }
        }

        /// <summary>
        /// Gets or sets source Data repository.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<SourceModel, HLinkSourceModel> SourceData
        {
            get
            {
                return _SourceData;
            }

            set
            {
                SetProperty(ref _SourceData, value);
            }
        }

        /// <summary>
        /// The local tag data.
        /// </summary>

        [JsonInclude]
        public RepositoryModelDictionary<TagModel, HLinkTagModel> TagData
        {
            get
            {
                return _TagData;
            }

            set
            {
                SetProperty(ref _TagData, value);
            }
        }
    }
}