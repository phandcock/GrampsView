// Copyright (c) phandcock. All rights reserved.

using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks.Models;

using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GrampsView.Data.Repository
{
    /// <summary>
    /// Static Data Store.
    /// </summary>

    [KnownType(typeof(ObservableCollection<object>))]
    public class DataInstance : ObservableObject
    {
        /// <summary>
        /// The address data
        /// </summary>
        private RepositoryModelDictionary<AddressModel, HLinkAdressModel> _AddressData = new RepositoryModelDictionary<AddressModel, HLinkAdressModel>("AddressData");

        /// <summary>
        /// The book mark collection
        /// </summary>
        private HLinkBackLinkModelCollection _BookMarkCollection = new HLinkBackLinkModelCollection("BookMarkData");

        /// <summary>
        /// The local header data.
        /// </summary>
        private RepositoryModelDictionary<HeaderModel, HLinkHeaderModel> _HeaderData = new RepositoryModelDictionary<HeaderModel, HLinkHeaderModel>("HeaderData");

        /// <summary>
        /// The local media data.
        /// </summary>
        private RepositoryModelDictionary<MediaModel, HLinkMediaModel> _MediaData = new RepositoryModelDictionary<MediaModel, HLinkMediaModel>("MediaData");

        /// <summary>
        /// The local name map data.
        /// </summary>
        private RepositoryModelDictionary<NameMapModel, HLinkNameMapModel> _NameMapData = new RepositoryModelDictionary<NameMapModel, HLinkNameMapModel>("NameMapData");

        /// <summary>
        /// The local person data.
        /// </summary>
        private RepositoryModelDictionary<PersonModel, HLinkPersonModel> _PersonData = new RepositoryModelDictionary<PersonModel, HLinkPersonModel>("PersonData");

        /// <summary>
        /// The local person name data.
        /// </summary>
        private RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel> _PersonNameData = new RepositoryModelDictionary<PersonNameModel, HLinkPersonNameModel>("PersonNameData");

        /// <summary>
        /// The local place data.
        /// </summary>
        private RepositoryModelDictionary<PlaceModel, HLinkPlaceModel> _PlaceData = new RepositoryModelDictionary<PlaceModel, HLinkPlaceModel>("PlaceData");

        /// <summary>
        /// The local repository data.
        /// </summary>
        private RepositoryModelDictionary<RepositoryModel, HLinkRepositoryModel> _RepositoryData = new RepositoryModelDictionary<RepositoryModel, HLinkRepositoryModel>("RepositoryData");

        /// <summary>
        /// The local source data.
        /// </summary>
        private RepositoryModelDictionary<SourceModel, HLinkSourceModel> _SourceData = new RepositoryModelDictionary<SourceModel, HLinkSourceModel>("SourceData");

        /// <summary>
        /// The local tag data.
        /// </summary>
        private RepositoryModelDictionary<TagModel, HLinkTagModel> _TagData = new RepositoryModelDictionary<TagModel, HLinkTagModel>("TagData");

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
                if (value != null)
                {
                    SetProperty(ref _BookMarkCollection, value);
                }
            }
        }

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

        public bool IsDataLoaded
        {
            get;
            set;
        }

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

        /// <summary>
        /// The local Header data.
        /// </summary>
        /// <summary>
        /// Gets or sets a value indicating whether this instance is data loaded.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is data loaded; otherwise, <c> false </c>.
        /// </value>
        /// <summary>
        /// The local Media data.
        /// </summary>
        /// <summary>
        /// The local NameMap data.
        /// </summary>
        /// <summary>
        /// The local Person data.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="DataInstance"/> class.
        /// </summary>
        public DataInstance()
        {
        }

        /// <summary>
        /// The local Place data.
        /// </summary>
        /// <summary>
        /// The local Place data.
        /// </summary>
        /// <summary>
        /// Gets or sets source Data repository.
        /// </summary>
        /// <summary>
        /// The local tag data.
        /// </summary>
    }
}