// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.Models.HLinks.Models;
using GrampsView.ModelsDB.Collections.HLinks;
using GrampsView.ModelsDB.HLinks.Models;

using static GrampsView.Common.CommonEnums;

namespace GrampsView.DBModels
{
    public class EventDBModel : DBModelBase, IEventDBModel
    {
        public EventDBModel()
        {
            ModelItemGlyph.Symbol = Constants.IconEvents;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundEvent");
        }

        public EventDBModel(EventDBModel argEventModel)
        {
            ModelItemGlyph.Symbol = Constants.IconEvents;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundEvent");

            GDate = argEventModel.GDate;

            EventDBModel t = this;
        }

        public DateDBModelBase GDate
        {
            get;

            set;
        } = new DateDBModelBase();



        // TODO display different text according to the model types

        public EventModelType EventType
        {
            get;

            set;
        } = EventModelType.CUSTOM;

        /// <summary>
        /// Gets or sets the g attribute. [ref name="attribute-content"].
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>

        public HLinkAttributeModelCollection GAttribute
        {
            get;
            set;
        }

            = new HLinkAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>

        public HLinkCitationDBModelCollection GCitationRefCollection
        {
            get;

            set;
        } = new HLinkCitationDBModelCollection();



        /// <summary>
        /// Gets or sets the Event Description. [element name="description"].
        /// </summary>
        /// <value>
        /// The Event description.
        /// </value>

        public string GDescription
        {
            get;

            set;
        } = "No event description";

        /// <summary>
        /// Gets or sets the g media reference collection.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>

        public HLinkMediaModelCollection GMediaRefCollection
        {
            get;

            set;
        } = new HLinkMediaModelCollection();

        /// <summary>
        /// Gets or sets the note reference collection. [element name = "noteref"].
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>

        public HLinkNoteDBModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteDBModelCollection();

        /// <summary>
        /// Gets or sets the g place.
        /// </summary>
        /// <value>
        /// The g place.
        /// </value>

        public HLinkPlaceModel GPlace
        {
            get;

            set;
        } = new HLinkPlaceModel();

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>

        public HLinkTagModelCollection GTagRefCollection
        {
            get;

            set;
        } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the Event Type. [element name = "type"].
        /// </summary>
        /// <value>
        /// The type of the Event.
        /// </value>

        public string GType
        {
            get;

            set;
        } = "Unknown";

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkEventDBModel HLink
        {
            get
            {
                HLinkEventDBModel t = new()
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        public new int Compare(object a, object b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            EventDBModel firstEvent = (EventDBModel)a;
            EventDBModel secondEvent = (EventDBModel)b;

            // compare on Date first
            int testFlag = DateTime.Compare(firstEvent.GDate.SortDate, secondEvent.GDate.SortDate);

            if (testFlag.Equals(0))
            {
                // equal so check Description
                testFlag = string.Compare(firstEvent.GDescription, secondEvent.GDescription, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        public new int CompareTo(object obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            EventDBModel secondEvent = (EventDBModel)obj;

            // compare on Date first
            int testFlag = DateTime.Compare(GDate.SortDate, secondEvent.GDate.SortDate);

            // The same so sort by Description
            if (testFlag.Equals(0))
            {
                // equal so check Description
                testFlag = string.Compare(secondEvent.GDescription, GDescription, StringComparison.CurrentCulture);
            }

            return testFlag;
        }

        public override string ToString()
        {
            return !string.IsNullOrEmpty(GDescription) ? GDescription : GType;
        }
    }
}