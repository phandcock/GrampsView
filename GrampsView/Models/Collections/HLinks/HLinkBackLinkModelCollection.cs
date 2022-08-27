namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using static GrampsView.Data.Model.HLinkBackLink;

    /// <summary>
    /// </summary>

    public class HLinkBackLinkModelCollection : HLinkBaseCollection<HLinkBackLink>
    {
        public HLinkBackLinkModelCollection()
        {
            Title = "BackLink Collection";
        }

        /// <summary>
        /// Returns a CardGroup of HLinkBase and not HLinkBackLink.
        /// </summary>
        /// <value>
        /// The card group as property.
        /// </value>
        public CardGroupHLink<HLinkBase> AsCardGroup
        {
            get
            {
                CardGroupHLink<HLinkBase> t = new CardGroupHLink<HLinkBase>();

                foreach (HLinkBackLink item in Items)
                {
                    item.HLink.DisplayAs = CommonEnums.DisplayFormat.SmallCard;
                    t.Add(item.HLink);
                }

                t.Title = Title;

                return t;
            }
        }

        /// <summary>
        /// Returns a CardGroup of HLinkBase and not HLinkBackLink.
        /// </summary>
        /// <value>
        /// The card group as property.
        /// </value>
        public CardGroupHLink<HLinkBase> AsCardGroupLink
        {
            get
            {
                CardGroupHLink<HLinkBase> t = new CardGroupHLink<HLinkBase>();

                foreach (HLinkBackLink item in Items)
                {
                    item.HLink.DisplayAs = CommonEnums.DisplayFormat.LinkCardCell;
                    t.Add(item.HLink);
                }

                t.Title = Title;

                return t;
            }
        }

        public override void SetGlyph()
        {
            // Back Reference HLinks
            foreach (HLinkBackLink argHLink in this)
            {
                ItemGlyph t = new ItemGlyph();

                switch (argHLink.HLinkType)
                {
                    case HLinkBackLinkEnum.HLinkAddressModel:
                        {
                            t = DV.AddressDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkCitationModel:
                        {
                            t = DV.CitationDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkEventModel:
                        {
                            t = DV.EventDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkFamilyModel:
                        {
                            t = DV.FamilyDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkMediaModel:
                        {
                            t = DV.MediaDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkNameMapModel:
                        {
                            t = DV.NameMapDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkNoteModel:
                        {
                            t = DV.NoteDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkPersonModel:
                        {
                            t = DV.PersonDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkPersonNameModel:
                        {
                            t = DV.PersonNameDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkPlaceModel:
                        {
                            t = DV.PlaceDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkRepositoryModel:
                        {
                            t = DV.RepositoryDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkSourceModel:
                        {
                            t = DV.SourceDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.HLinkTagModel:
                        {
                            t = DV.TagDV.GetGlyph(argHLink.HLinkKey);
                            break;
                        }

                    case HLinkBackLinkEnum.Unknown:
                        break;

                    default:

                        break;
                }

                argHLink.HLinkGlyphItem = t;
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();

            base.Sort();
        }
    }
}