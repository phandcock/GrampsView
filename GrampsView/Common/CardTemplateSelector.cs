// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.DBModels;
using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.Models.HLinks.Interfaces;
using GrampsView.Models.HLinks.Models;
using GrampsView.ModelsDB.HLinks.Models;

using System.Diagnostics.Contracts;

namespace GrampsView.Common
{
    /// <summary>
    /// Card Template Selector.
    /// </summary>
    public class CardTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the address template.
        /// </summary>
        /// <value>
        /// The attribute template.
        /// </value>
        public DataTemplate? AddressTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the attribute template.
        /// </summary>
        /// <value>
        /// The attribute template.
        /// </value>
        public DataTemplate? AttributeTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Card List Line template.
        /// </summary>
        /// <value>
        /// The List Line template.
        /// </value>
        public DataTemplate? CardListLineTemplate
        {
            get;
            set;
        }

        public DataTemplate? ChildRefSingleTemplate
        {
            get;
            set;
        }

        public DataTemplate? ChildRefSmallTemplate
        {
            get;
            set;
        }

        public DataTemplate? CitationLinkCellTemplate
        {
            get;
            set;
        }

        public DataTemplate? CitationLinkMediumTemplate
        {
            get;
            set;
        }

        public DataTemplate? CitationLinkSingleTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the citation template.
        /// </summary>
        /// <value>
        /// The citation template.
        /// </value>
        public DataTemplate? CitationSmallTemplate
        {
            get;
            set;
        }

        public DataTemplate? DateRangeObjectTemplate
        {
            get;
            set;
        }

        public DataTemplate? DateSpanObjectTemplate
        {
            get;
            set;
        }

        public DataTemplate? DateStrObjectTemplate
        {
            get;
            set;
        }

        public DataTemplate? DateValObjectTemplate
        {
            get;
            set;
        }

        public DataTemplate? EventLinkCellTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the event template.
        /// </summary>
        /// <value>
        /// The event template.
        /// </value>
        public DataTemplate? EventLinkMediumTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the event template.
        /// </summary>
        /// <value>
        /// The event template.
        /// </value>
        public DataTemplate? EventSmallTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the family template.
        /// </summary>
        /// <value>
        /// The family template.
        /// </value>
        public DataTemplate? FamilyCardMediumTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Family Graph template.
        /// </summary>
        /// <value>
        /// The Family Graph template.
        /// </value>
        public DataTemplate? FamilyGraphMediumTemplate
        {
            get;
            set;
        }

        public DataTemplate? FamilyLinkCellTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the family template.
        /// </summary>
        /// <value>
        /// The family template.
        /// </value>
        public DataTemplate? FamilySingleTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the family template.
        /// </summary>
        /// <value>
        /// The family template.
        /// </value>
        public DataTemplate? FamilySmallTemplate
        {
            get;
            set;
        }

        public DataTemplate? LDSOrdTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the map template.
        /// </summary>
        /// <value>
        /// The family template.
        /// </value>
        public DataTemplate? MapTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the media card large template.
        /// </summary>
        /// <value>
        /// The media card large template.
        /// </value>
        public DataTemplate? MediaLargeTemplate
        {
            get;
            set;
        }

        public DataTemplate? MediaLinkMediumTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the media template.
        /// </summary>
        /// <value>
        /// The media template.
        /// </value>
        public DataTemplate? MediaSmallTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name map template.
        /// </summary>
        /// <value>
        /// The name map template.
        /// </value>
        public DataTemplate? NameMapTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the note card full template.
        /// </summary>
        /// <value>
        /// The note card full template.
        /// </value>
        public DataTemplate? NoteCardFullTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the note template.
        /// </summary>
        /// <value>
        /// The note template.
        /// </value>
        public DataTemplate? NoteTemplate
        {
            get;
            set;
        }

        public DataTemplate? PersonLinkCellTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the person name template.
        /// </summary>
        /// <value>
        /// The person name template.
        /// </value>
        public DataTemplate? PersonNameSingleTemplate
        {
            get;
            set;
        }

        public DataTemplate PersonNameSmallTemplate
        {
            get;
            set;
        }

        public DataTemplate? PersonRefTemplate
        {
            get;
            set;
        }

        public DataTemplate? PersonSingleTemplate
        {
            get;
            set;
        }

        public DataTemplate? PersonSmallTemplate
        {
            get;
            set;
        }

        public DataTemplate? PlaceLocationTemplate
        {
            get;
            set;
        }

        public DataTemplate? PlaceNameTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the place template.
        /// </summary>
        /// <value>
        /// The place template.
        /// </value>
        public DataTemplate? PlaceTemplate
        {
            get;
            set;
        }

        public DataTemplate? RepositoryLinkSingleTemplate
        {
            get;
            set;
        }

        public DataTemplate? RepositoryRefLinkSingleTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the repository ref template.
        /// </summary>
        /// <value>
        /// The repository template.
        /// </value>
        public DataTemplate? RepositoryRefSmallTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the repository template.
        /// </summary>
        /// <value>
        /// The repository template.
        /// </value>
        public DataTemplate? RepositorySmallTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the source template.
        /// </summary>
        /// <value>
        /// The source template.
        /// </value>
        public DataTemplate? SourceLargeTemplate
        {
            get;
            set;
        }

        public DataTemplate? SourceLinkCellTemplate
        {
            get;
            set;
        }

        public DataTemplate? SourceLinkMediumTemplate
        {
            get;
            set;
        }

        public DataTemplate? SourceLinkSingleTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the source template.
        /// </summary>
        /// <value>
        /// The source template.
        /// </value>
        public DataTemplate? SourceSmallTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tag template.
        /// </summary>
        /// <value>
        /// The tag template.
        /// </value>
        public DataTemplate? TagTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the URL template.
        /// </summary>
        /// <value>
        /// The attribute template.
        /// </value>
        public DataTemplate? URLTemplate
        {
            get;
            set;
        }

        /// <summary>
        /// Selects the template for a card.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="container">
        /// The container.
        /// </param>
        /// <returns>
        /// A data template.
        /// </returns>
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            Contract.Assert(item != null);

            // TODO finish moving to using a switch statement

            switch (item)
            {
                case AddressDBModel:
                case IHLinkAddressDBModel:

                    {
                        return AddressTemplate;
                    }

                case AttributeModel:

                    {
                        return AttributeTemplate;
                    }

                case CardListLineCollection:

                    {
                        return CardListLineTemplate;
                    }

                case FamilyDBModel:

                    {
                        return FamilySmallTemplate;
                    }

                case HLinkChildRefModel:

                    {
                        switch ((item as HLinkChildRefModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.SingleCard:
                                {
                                    return ChildRefSingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return ChildRefSmallTemplate;
                                }

                            default:
                                {
                                    return ChildRefSmallTemplate;
                                }
                        }
                    }

                case HLinkCitationDBModel:
                case ICitationDBModel:
                    {
                        switch ((item as HLinkCitationDBModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardCell:
                                {
                                    return CitationLinkCellTemplate;
                                }

                            case CommonEnums.DisplayFormat.LinkCardSingle:
                                {
                                    return CitationLinkSingleTemplate;
                                }
                            case CommonEnums.DisplayFormat.LinkCardMedium:
                                {
                                    return CitationLinkMediumTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return CitationSmallTemplate;
                                }

                            default:
                                {
                                    return CitationSmallTemplate;
                                }
                        }
                    }

                case HLinkDateModelRange:
                    {
                        return DateRangeObjectTemplate;
                    }

                case HLinkDateModelSpan:
                    {
                        return DateSpanObjectTemplate;
                    }

                case HLinkDateModelStr:
                    {
                        return DateStrObjectTemplate;
                    }

                case HLinkDateModelVal:
                    {
                        return DateValObjectTemplate;
                    }

                case HLinkEventDBModel:

                    {
                        switch ((item as HLinkEventDBModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardCell:
                                {
                                    return EventLinkCellTemplate;
                                }

                            case CommonEnums.DisplayFormat.LinkCardMedium:
                                {
                                    return EventLinkMediumTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return EventSmallTemplate;
                                }

                            default:
                                {
                                    return EventSmallTemplate;
                                }
                        }
                    }

                case HLinkFamilyGraphModel:

                    {
                        return FamilyGraphMediumTemplate;
                    }

                case HLinkFamilyDBModel:

                    {
                        switch ((item as HLinkFamilyDBModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardCell:
                                {
                                    return FamilyLinkCellTemplate;
                                }

                            case CommonEnums.DisplayFormat.LinkCardMedium:
                                {
                                    return FamilyCardMediumTemplate;
                                }

                            case CommonEnums.DisplayFormat.SingleCard:
                                {
                                    return FamilySingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return FamilySmallTemplate;
                                }

                            default:
                                {
                                    return FamilySmallTemplate;
                                }
                        }
                    }

                case HLinkMapModel:

                    {
                        return MapTemplate;
                    }

                case HLinkMediaModel:

                    {
                        switch ((item as HLinkMediaModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardMedium:
                                {
                                    return MediaLinkMediumTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return MediaSmallTemplate;
                                }

                            default:
                                {
                                    return MediaSmallTemplate;
                                }
                        }
                    }

                case IHLinkMediaModel:

                    {
                        return MediaSmallTemplate;
                    }

                case HLinkNoteDBModel:

                    {
                        switch ((item as HLinkNoteDBModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return NoteTemplate;
                                }

                            case CommonEnums.DisplayFormat.FullCard:
                                {
                                    return NoteCardFullTemplate;
                                }

                            default:
                                {
                                    return NoteTemplate;
                                }
                        }
                    }

                case HLinkPersonModel:

                    {
                        switch ((item as HLinkPersonModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.SingleCard:
                                {
                                    return PersonSingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return PersonSmallTemplate;
                                }

                            case CommonEnums.DisplayFormat.LinkCardCell:
                                {
                                    return PersonLinkCellTemplate;
                                }

                            default:
                                {
                                    return PersonSmallTemplate;
                                }
                        }
                    }

                case HLinkPersonNameModel:

                    {
                        switch ((item as HLinkPersonNameModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.SingleCard:
                                {
                                    return PersonNameSingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return PersonNameSmallTemplate;
                                }

                            default:
                                {
                                    return PersonNameSingleTemplate;
                                }
                        }
                    }

                case HLinkRepositoryRefModel:

                    {
                        switch ((item as HLinkRepositoryRefModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardSingle:
                                {
                                    return RepositoryRefLinkSingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return RepositoryRefSmallTemplate;
                                }

                            default:
                                {
                                    return RepositoryRefSmallTemplate;
                                }
                        }
                    }

                case HLinkRepositoryModel:

                    {
                        switch ((item as HLinkRepositoryModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardSingle:
                                {
                                    return RepositoryLinkSingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return RepositorySmallTemplate;
                                }

                            default:
                                {
                                    return RepositorySmallTemplate;
                                }
                        }
                    }

                case HLinkSourceModel:

                    {
                        switch ((item as HLinkSourceModel).DisplayAs)
                        {
                            case CommonEnums.DisplayFormat.LinkCardCell:
                                {
                                    return SourceLinkCellTemplate;
                                }

                            case CommonEnums.DisplayFormat.LinkCardMedium:
                                {
                                    return SourceLinkMediumTemplate;
                                }

                            case CommonEnums.DisplayFormat.LinkCardSingle:
                                {
                                    return SourceLinkSingleTemplate;
                                }

                            case CommonEnums.DisplayFormat.LargeCard:
                                {
                                    return SourceLargeTemplate;
                                }

                            case CommonEnums.DisplayFormat.SmallCard:
                                {
                                    return SourceSmallTemplate;
                                }

                            default:
                                {
                                    return SourceSmallTemplate;
                                }
                        }
                    }

                case HLinkURLModel:

                    {
                        return URLTemplate;
                    }

                case LdsOrdModel:

                    {
                        return LDSOrdTemplate;
                    }

                default:
                    break;
            }

            if (item is HLinkNameMapModel)
            {
                return NameMapTemplate;
            }

            if (item is NoteDBModel)
            {
                return NoteTemplate;
            }

            if (item is PlaceLocationModel or IPlaceLocationModel)
            {
                return PlaceLocationTemplate;
            }

            if (item is HLinkPlaceModel)
            {
                return PlaceTemplate;
            }

            if (item is HLinkTagModel)
            {
                return TagTemplate;
            }

            if (item is PersonNameModel)
            {
                return PersonNameSingleTemplate;
            }

            if (item is PlaceModel)
            {
                return PlaceTemplate;
            }

            if (item is PlaceNameModel)
            {
                return PlaceNameTemplate;
            }

            if (item is SourceModel)
            {
                return SourceSmallTemplate;
            }

            if (item is TagModel)
            {
                return TagTemplate;
            }

            // Error
            Contract.Assert(false, "Bad Data Template: " + item.GetType().ToString());
            return null;
        }
    }
}