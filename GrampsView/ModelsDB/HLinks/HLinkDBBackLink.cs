// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Models.HLinks;
using GrampsView.Models.HLinks.Models;
using GrampsView.ModelsDB.HLinks.Models;

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema

    public class HLinkDBBackLink : HLinkDBBase
    {
        public HLinkDBBackLink()
        {
            HLinkKey = HLinkKey.NewAsGUID();
        }

        public HLinkDBBackLink(HLinkAddressDBModel ArgHLinkLink)
        {
            _HLinkAddressModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkAddressModel;
        }

        public HLinkDBBackLink(HLinkCitationDBModel ArgHLinkLink)
        {
            _HLinkCitationModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkCitationModel;
        }

        public HLinkDBBackLink(HLinkEventDBModel ArgHLinkLink)
        {
            _HLinkEventModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkEventModel;
        }

        public HLinkDBBackLink(HLinkFamilyDBModel ArgHLinkLink)
        {
            _HLinkFamilyModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkFamilyModel;
        }

        public HLinkDBBackLink(HLinkMediaModel ArgHLinkLink)
        {
            _HLinkMediaModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkMediaModel;
        }

        public HLinkDBBackLink(HLinkNameMapModel ArgHLinkLink)
        {
            _HLinkNameMapModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkNameMapModel;
        }

        public HLinkDBBackLink(HLinkNoteDBModel ArgHLinkLink)
        {
            _HLinkNoteModel = ArgHLinkLink;
            _HLinkNoteModel.DisplayAs = CommonEnums.DisplayFormat.SmallCard;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkNoteModel;
        }

        public HLinkDBBackLink(HLinkPersonModel ArgHLinkLink)
        {
            _HLinkPersonModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkPersonModel;
        }

        public HLinkDBBackLink(HLinkPersonNameModel ArgHLinkLink)
        {
            _HLinkPersonNameModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkPersonNameModel;
        }

        public HLinkDBBackLink(HLinkPlaceModel ArgHLinkLink)
        {
            _HLinkPlaceModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkPlaceModel;
        }

        public HLinkDBBackLink(HLinkRepositoryModel ArgHLinkLink)
        {
            _HLinkRepositoryModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkRepositoryModel;
        }

        public HLinkDBBackLink(HLinkSourceModel ArgHLinkLink)
        {
            _HLinkSourceModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkSourceModel;
        }

        public HLinkDBBackLink(HLinkTagModel ArgHLinkLink)
        {
            _HLinkTagModel = ArgHLinkLink;

            HLinkKey = ArgHLinkLink.HLinkKey;

            HLinkType = HLinkDBBackLinkEnum.HLinkTagModel;
        }

        public enum HLinkDBBackLinkEnum : int
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

        public HLinkAddressDBModel _HLinkAddressModel { get; set; } = new HLinkAddressDBModel();

        public HLinkCitationDBModel _HLinkCitationModel { get; set; } = new HLinkCitationDBModel();

        public HLinkEventDBModel _HLinkEventModel { get; set; } = new HLinkEventDBModel();

        public HLinkFamilyDBModel _HLinkFamilyModel { get; set; } = new HLinkFamilyDBModel();

        public HLinkMediaModel _HLinkMediaModel { get; set; } = new HLinkMediaModel();

        public HLinkNameMapModel _HLinkNameMapModel { get; set; } = new HLinkNameMapModel();

        public HLinkNoteDBModel _HLinkNoteModel { get; set; } = new HLinkNoteDBModel();

        public HLinkPersonModel _HLinkPersonModel { get; set; } = new HLinkPersonModel();

        public HLinkPersonNameModel _HLinkPersonNameModel { get; set; } = new HLinkPersonNameModel();

        public HLinkPlaceModel _HLinkPlaceModel { get; set; } = new HLinkPlaceModel();

        public HLinkRepositoryModel _HLinkRepositoryModel { get; set; } = new HLinkRepositoryModel();

        public HLinkSourceModel _HLinkSourceModel { get; set; } = new HLinkSourceModel();

        public HLinkTagModel _HLinkTagModel { get; set; } = new HLinkTagModel();

        public HLinkDBBase HLink
        {
            get
            {
                switch (HLinkType)
                {
                    //case HLinkDBBackLinkEnum.HLinkAddressModel:
                    //    //_HLinkAddressModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkAddressModel;

                    case HLinkDBBackLinkEnum.HLinkCitationModel:
                        //_HLinkCitationModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkCitationModel;

                    case HLinkDBBackLinkEnum.HLinkEventModel:
                        //_HLinkEventModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkEventModel;

                    case HLinkDBBackLinkEnum.HLinkFamilyModel:
                        //_HLinkFamilyModel.HLinkGlyphItem = this.HLinkGlyphItem;
                        return _HLinkFamilyModel;

                    //case HLinkDBBackLinkEnum.HLinkMediaModel:
                    //    //_HLinkMediaModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkMediaModel;

                    //case HLinkDBBackLinkEnum.HLinkNameMapModel:
                    //    //_HLinkNameMapModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkNameMapModel;

                    //case HLinkDBBackLinkEnum.HLinkNoteModel:
                    //    //_HLinkNoteModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkNoteModel;

                    //case HLinkDBBackLinkEnum.HLinkPersonModel:
                    //    //_HLinkPersonModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkPersonModel;

                    //case HLinkDBBackLinkEnum.HLinkPersonNameModel:
                    //    //_HLinkPersonNameModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkPersonNameModel;

                    //case HLinkDBBackLinkEnum.HLinkPlaceModel:
                    //    //_HLinkPlaceModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkPlaceModel;

                    //case HLinkDBBackLinkEnum.HLinkRepositoryModel:
                    //    //_HLinkRepositoryModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkRepositoryModel;

                    //case HLinkDBBackLinkEnum.HLinkSourceModel:
                    //    //_HLinkSourceModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkSourceModel;

                    //case HLinkDBBackLinkEnum.HLinkTagModel:
                    //    //_HLinkTagModel.HLinkGlyphItem = this.HLinkGlyphItem;
                    //    return _HLinkTagModel;

                    case HLinkDBBackLinkEnum.Unknown:
                        break;

                    default:
                        return default(HLinkDBBase);
                }

                return default(HLinkDBBase);
            }
        }

        public HLinkDBBackLinkEnum HLinkType { get; set; } = HLinkDBBackLinkEnum.Unknown;

        public override bool Valid
        {
            get
            {
                return !((HLinkType == HLinkDBBackLinkEnum.HLinkBookMarkModel) && (HLinkType == HLinkDBBackLinkEnum.Unknown));
            }
        }

        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType())
            {
                return false;
            }

            return (HLinkType == (obj as HLinkDBBackLink).HLinkType) && (HLink.HLinkKey == (obj as HLinkDBBackLink).HLink.HLinkKey);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }
    }
}