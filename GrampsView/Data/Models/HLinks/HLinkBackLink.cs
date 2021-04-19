// XML 171 - Not in definition so created this for use with BackLink functionality

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkBackLink : HLinkBase
    {
        [DataMember]
        private HLinkAdressModel _HLinkAddressModel;

        [DataMember]
        private HLinkCitationModel _HLinkCitationModel;

        [DataMember]
        private HLinkEventModel _HLinkEventModel;

        [DataMember]
        private HLinkFamilyModel _HLinkFamilyModel;

        [DataMember]
        private HLinkMediaModel _HLinkMediaModel;

        [DataMember]
        private HLinkNameMapModel _HLinkNameMapModel;

        [DataMember]
        private HLinkNoteModel _HLinkNoteModel;

        [DataMember]
        private HLinkPersonModel _HLinkPersonModel;

        [DataMember]
        private HLinkPersonNameModel _HLinkPersonNameModel;

        [DataMember]
        private HLinkPlaceModel _HLinkPlaceModel;

        [DataMember]
        private HLinkRepositoryModel _HLinkRepositoryModel;

        [DataMember]
        private HLinkSourceModel _HLinkSourceModel;

        [DataMember]
        private HLinkTagModel _HLinkTagModel;

        public HLinkBackLink()
        {
        }

        public HLinkBackLink(HLinkAdressModel ArgHLinkLink)
        {
            _HLinkAddressModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkAddressModel;
        }

        public HLinkBackLink(HLinkCitationModel ArgHLinkLink)
        {
            _HLinkCitationModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkCitationModel;
        }

        public HLinkBackLink(HLinkMediaModel ArgHLinkLink)
        {
            _HLinkMediaModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkMediaModel;
        }

        public HLinkBackLink(HLinkEventModel ArgHLinkLink)
        {
            _HLinkEventModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkEventModel;
        }

        public HLinkBackLink(HLinkFamilyModel ArgHLinkLink)
        {
            _HLinkFamilyModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkFamilyModel;
        }

        public HLinkBackLink(HLinkNameMapModel ArgHLinkLink)
        {
            _HLinkNameMapModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkNameMapModel;
        }

        public HLinkBackLink(HLinkNoteModel ArgHLinkLink)
        {
            _HLinkNoteModel = ArgHLinkLink;
            _HLinkNoteModel.DisplayAs = CommonEnums.DisplayFormat.SmallCard;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkNoteModel;
        }

        public HLinkBackLink(HLinkPersonModel ArgHLinkLink)
        {
            _HLinkPersonModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkPersonModel;
        }

        public HLinkBackLink(HLinkPersonNameModel ArgHLinkLink)
        {
            _HLinkPersonNameModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkPersonNameModel;
        }

        public HLinkBackLink(HLinkPlaceModel ArgHLinkLink)
        {
            _HLinkPlaceModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkPlaceModel;
        }

        public HLinkBackLink(HLinkRepositoryModel ArgHLinkLink)
        {
            _HLinkRepositoryModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkRepositoryModel;
        }

        public HLinkBackLink(HLinkSourceModel ArgHLinkLink)
        {
            _HLinkSourceModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkSourceModel;
        }

        public HLinkBackLink(HLinkTagModel ArgHLinkLink)
        {
            _HLinkTagModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkTagModel;
        }

        public enum HLinkBackLinkEnum : int
        {
            HLinkAddressModel,
            HLinkBookMarkModel,
            HLinkCitationModel,
            HLinkEventModel,
            HLinkFamilyModel,
            HLinkMediaModel,
            HLinkNameMapModel,
            HLinkNoteModel,
            HLinkPersonModel,
            HLinkPersonNameModel,
            HLinkPlaceModel,
            HLinkRepositoryModel,
            HLinkSourceAttrModel,
            HLinkSourceModel,
            HLinkTagModel,
            Unknown
        }

        [DataMember]
        public HLinkBackLinkEnum HLinkType { get; set; } = HLinkBackLinkEnum.Unknown;

        public override bool Valid
        {
            get
            {
                return !((HLinkType == HLinkBackLinkEnum.HLinkBookMarkModel) && (HLinkType == HLinkBackLinkEnum.Unknown));
            }
        }

        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return (HLinkType == (obj as HLinkBackLink).HLinkType) && (HLink().HLinkKey == (obj as HLinkBackLink).HLink().HLinkKey);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        public HLinkBase HLink()
        {
            switch (HLinkType)
            {
                case HLinkBackLinkEnum.HLinkAddressModel:
                    return _HLinkAddressModel;

                case HLinkBackLinkEnum.HLinkCitationModel:
                    return _HLinkCitationModel;

                case HLinkBackLinkEnum.HLinkEventModel:
                    return _HLinkEventModel;

                case HLinkBackLinkEnum.HLinkFamilyModel:
                    return _HLinkFamilyModel;

                case HLinkBackLinkEnum.HLinkMediaModel:
                    return _HLinkMediaModel;

                case HLinkBackLinkEnum.HLinkNameMapModel:
                    return _HLinkNameMapModel;

                case HLinkBackLinkEnum.HLinkNoteModel:
                    return _HLinkNoteModel;

                case HLinkBackLinkEnum.HLinkPersonModel:
                    return _HLinkPersonModel;

                case HLinkBackLinkEnum.HLinkPersonNameModel:
                    return _HLinkPersonNameModel;

                case HLinkBackLinkEnum.HLinkPlaceModel:
                    return _HLinkPlaceModel;

                case HLinkBackLinkEnum.HLinkRepositoryModel:
                    return _HLinkRepositoryModel;

                case HLinkBackLinkEnum.HLinkSourceModel:
                    return _HLinkSourceModel;

                case HLinkBackLinkEnum.HLinkTagModel:
                    return _HLinkTagModel;

                case HLinkBackLinkEnum.Unknown:
                    break;

                default:
                    return default(HLinkBase);
            }

            return default(HLinkBase);
        }
    }
}