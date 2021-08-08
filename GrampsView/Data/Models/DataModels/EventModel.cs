namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Runtime.Serialization;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Event ViewModel.
    ///
    /// XML 171 - All fields defined
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IEventModel"/>
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.IComparable"/>
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
    public sealed class EventModel : ModelBase, IEventModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventModel"/> class.
        /// </summary>
        public EventModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconEvents;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundEvent");
        }

        public override string DefaultText
        {
            get
            {
                if (!string.IsNullOrEmpty(GDescription))
                {
                    return GDescription;
                }

                return GType;
            }
        }

        // TODO display different text according to the model types
        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get;

            set;
        } = new HLinkCitationModelCollection();

        /// <summary>
        /// Gets or sets the Event Date. [ref name="date-content"].
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        [DataMember]
        public IDateObjectModel GDate
        {
            get;

            set;
        } = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets the Event Description. [element name="description"].
        /// </summary>
        /// <value>
        /// The Event description.
        /// </value>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get;

            set;
        } = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the g place.
        /// </summary>
        /// <value>
        /// The g place.
        /// </value>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
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
        public HLinkEventModel HLink
        {
            get
            {
                HLinkEventModel t = new HLinkEventModel
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

            EventModel firstEvent = (EventModel)a;
            EventModel secondEvent = (EventModel)b;

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

            EventModel secondEvent = (EventModel)obj;

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
    }
}