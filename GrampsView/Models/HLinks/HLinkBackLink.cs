// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Models.HLinks;
using GrampsView.Models.HLinks.Models;

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema

    public class HLinkBackLink : HLinkBase
    {
        public HLinkBackLink()
        {
            HLinkKey = HLinkKey.NewAsGUID();
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

        public HLinkBackLink(HLinkMediaModel ArgHLinkLink)
        {
            _HLinkMediaModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkBackLinkEnum.HLinkMediaModel;
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

        public HLinkAdressModel _HLinkAddressModel { get; set; } = new HLinkAdressModel();

        public HLinkCitationModel _HLinkCitationModel { get; set; } = new HLinkCitationModel();

        public HLinkEventModel _HLinkEventModel { get; set; } = new HLinkEventModel();

        public HLinkFamilyModel _HLinkFamilyModel { get; set; } = new HLinkFamilyModel();

        public HLinkMediaModel _HLinkMediaModel { get; set; } = new HLinkMediaModel();

        public HLinkNameMapModel _HLinkNameMapModel { get; set; } = new HLinkNameMapModel();

        public HLinkNoteModel _HLinkNoteModel { get; set; } = new HLinkNoteModel();

        public HLinkPersonModel _HLinkPersonModel { get; set; } = new HLinkPersonModel();

        public HLinkPersonNameModel _HLinkPersonNameModel { get; set; } = new HLinkPersonNameModel();

        public HLinkPlaceModel _HLinkPlaceModel { get; set; } = new HLinkPlaceModel();

        public HLinkRepositoryModel _HLinkRepositoryModel { get; set; } = new HLinkRepositoryModel();

        public HLinkSourceModel _HLinkSourceModel { get; set; } = new HLinkSourceModel();

        public HLinkTagModel _HLinkTagModel { get; set; } = new HLinkTagModel();

        public HLinkBase HLink
        {
            get
            {
                switch (HLinkType)
                {
                    case HLinkBackLinkEnum.HLinkAddressModel:
                        //_HLinkAddressModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkAddressModel;

                    case HLinkBackLinkEnum.HLinkCitationModel:
                        //_HLinkCitationModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkCitationModel;

                    case HLinkBackLinkEnum.HLinkEventModel:
                        //_HLinkEventModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkEventModel;

                    case HLinkBackLinkEnum.HLinkFamilyModel:
                        //_HLinkFamilyModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkFamilyModel;

                    case HLinkBackLinkEnum.HLinkMediaModel:
                        //_HLinkMediaModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkMediaModel;

                    case HLinkBackLinkEnum.HLinkNameMapModel:
                        //_HLinkNameMapModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkNameMapModel;

                    case HLinkBackLinkEnum.HLinkNoteModel:
                        //_HLinkNoteModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkNoteModel;

                    case HLinkBackLinkEnum.HLinkPersonModel:
                        //_HLinkPersonModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkPersonModel;

                    case HLinkBackLinkEnum.HLinkPersonNameModel:
                        //_HLinkPersonNameModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkPersonNameModel;

                    case HLinkBackLinkEnum.HLinkPlaceModel:
                        //_HLinkPlaceModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkPlaceModel;

                    case HLinkBackLinkEnum.HLinkRepositoryModel:
                        //_HLinkRepositoryModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkRepositoryModel;

                    case HLinkBackLinkEnum.HLinkSourceModel:
                        //_HLinkSourceModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkSourceModel;

                    case HLinkBackLinkEnum.HLinkTagModel:
                        //_HLinkTagModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkTagModel;

                    case HLinkBackLinkEnum.Unknown:
                        break;

                    default:
                        return default(HLinkBase);
                }

                return default(HLinkBase);
            }
        }

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

            return (HLinkType == (obj as HLinkBackLink).HLinkType) && (HLink.HLinkKey == (obj as HLinkBackLink).HLink.HLinkKey);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}