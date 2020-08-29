//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="DateObjectModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using static GrampsView.Common.CommonEnums;

    public partial class DateObjectModel
    {
        public CardListLineCollection AsCardListLine(string argTitle = null)
        {
           
                CardListLineCollection DateModelCard = new CardListLineCollection();

                if (this.Valid)
                {
                    switch (this.GType)
                    {
                        case DateType.Range:
                            {
                                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "Range"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Start:", this.GStart),
                                new CardListLine("Stop:", this.GStop),
                                new CardListLine("Quality:", this.GQuality),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Dual Dated:", this.GDualdated),
                                new CardListLine("New Year:", this.GNewYear),
                            };

                                break;
                            }

                        case DateType.Span:
                            {
                                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "Span"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Start:", this.GStart),
                                new CardListLine("Stop:", this.GStop),
                                new CardListLine("Quality:", this.GQuality),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Dual Dated:", this.GDualdated),
                                new CardListLine("New Year:", this.GNewYear),
                            };

                                break;
                            }

                        case DateType.Str:
                            {
                                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "String"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Val:", this.GVal),
                            };

                                break;
                            }

                        case DateType.Unknown:
                            {
                                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "Unknown"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Start:", this.GStart),
                                new CardListLine("Stop:", this.GStop),
                                new CardListLine("Quality:", this.GQuality),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Dual Dated:", this.GDualdated),
                                new CardListLine("New Year:", this.GNewYear),
                            };

                                break;
                            }

                        case DateType.Val:
                            {
                                DateModelCard = new CardListLineCollection
                            {
                                new CardListLine("Date Type:", "Val"),
                                new CardListLine("Notional Date:", this.LongDate),
                                new CardListLine("Val:", this.GVal),
                                new CardListLine("C Format:", this.GCformat),
                                new CardListLine("Type:", this.GValType),
                                new CardListLine("Quality:", this.GQuality),
                                new CardListLine("Dual Dated:", this.GDualdated),
                                new CardListLine("New Year:", this.GNewYear),
                            };

                                break;
                            }

                        default:
                            {
                                DateModelCard = new CardListLineCollection
                            {
                                 new CardListLine("Unknown Date Type:", this.ToString()),
                            };

                                break;
                            }
                    }
                }

                if ( !(string.IsNullOrEmpty(argTitle)))
                {
                DateModelCard.Title = argTitle;
            }

                return DateModelCard;
            
        }
    }
}