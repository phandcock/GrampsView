// TODO Needs XML 1.71 check

/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;

    using static GrampsView.Data.Model.HLinkBackLink;

    /// <summary>
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<HLinkBackLink>))]
    public class HLinkBackLinkModelCollection : HLinkBaseCollection<HLinkBackLink>
    {
        public HLinkBackLinkModelCollection()
        {
            Title = "BackLink Collection";
        }

        public CardGroup AsGroupedCardGroup
        {
            get
            {
                CardGroup t = new CardGroup();

                var query = from item in Items
                            orderby item.HLinkType

                            group item by (item.HLinkType) into g
                            select new
                            {
                                GroupName = g.Key,
                                Items = g
                            };

                foreach (var g in query)
                {
                    CardGroup info = new CardGroup()
                    {
                        Title = g.GroupName.ToString(),
                    };

                    foreach (var item in g.Items)
                    {
                        info.Add(item.HLink());
                    }

                    t.Add(info);
                }

                return t;
            }
        }

        public override CardGroup CardGroupAsProperty
        {
            get
            {
                CardGroup t = new CardGroup();

                foreach (HLinkBackLink item in Items)
                {
                    t.Add(item.HLink());
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
                //argHLink.HLinkGlyphItem.ImageHLink = t.ImageHLink;
                //argHLink.HLinkGlyphItem.ImageSymbol = t.ImageSymbol;
                //argHLink.HLinkGlyphItem.ImageSymbolColour = t.ImageSymbolColour;
                //argHLink.HLinkGlyphItem
            }

            //// Set the first image link. Assumes main image is manually set to the first image in
            //// Gramps if we need it to be, e.g. Citations.
            SetFirstImage();
        }
    }
}