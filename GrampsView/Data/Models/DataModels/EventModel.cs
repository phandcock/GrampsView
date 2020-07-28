//-----------------------------------------------------------------------
//
// The data model defined by this file serves to hold Event data from the GRAMPS data file
//
// <copyright file="EventModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// XML 171 - All fields defined
//
// primary-object date-content place description attribute noteref citationref objref tagref

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Event ViewModel.
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
        /// The local g citation reference collection.
        /// </summary>
        private HLinkCitationModelCollection _GCitationRefCollection = new HLinkCitationModelCollection();

        /// <summary>
        /// Date object for the event.
        /// </summary>
        private IDateObjectModel _GDate = new DateObjectModel();

        /// <summary>
        /// Gets or sets the Event Description.
        /// </summary>
        private string _GDescription = "No event description";

        /// <summary>
        /// Media Reference Collection.
        /// </summary>
        private HLinkMediaModelCollection _GMediaCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// The local note collection.
        /// </summary>
        private HLinkNoteModelCollection _GNoteCollection = new HLinkNoteModelCollection();

        /// <summary>
        /// The local g place.
        /// </summary>
        private HLinkPlaceModel _GPlace = new HLinkPlaceModel();

        /// <summary>
        /// The local g tag reference collection.
        /// </summary>
        private HLinkTagModelCollection _GTagReferenceCollection = new HLinkTagModelCollection();

        /// <summary>
        /// The type of the event date.
        /// </summary>
        private string _GType = "Unknown";

        /// <summary>
        /// Initializes a new instance of the <see cref="EventModel"/> class.
        /// </summary>
        public EventModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconEvents;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundEvent");
        }

        /// <summary>
        /// Gets or sets the g attribute. [ref name="attribute-content"].
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GAttribute
        {
            get;
            set;
        }

            = new OCAttributeModelCollection();

        /// <summary>
        /// Gets or sets the g citation reference collection.
        /// </summary>
        /// <value>
        /// The g citation reference collection.
        /// </value>
        [DataMember]
        public HLinkCitationModelCollection GCitationRefCollection
        {
            get
            {
                return _GCitationRefCollection;
            }

            set
            {
                SetProperty(ref _GCitationRefCollection, value);

                //if (Id == "E0059" || value is null)
                //{
                //}
            }
        }

        /// <summary>
        /// Gets or sets the Event Date. [ref name="date-content"].
        /// </summary>
        /// <value>
        /// The event date.
        /// </value>
        [DataMember]
        public IDateObjectModel GDate
        {
            get
            {
                return _GDate;
            }

            set
            {
                SetProperty(ref _GDate, value);
            }
        }

        /// <summary>
        /// Gets or sets the Event Description. [element name="description"].
        /// </summary>
        /// <value>
        /// The Event description.
        /// </value>
        [DataMember]
        public string GDescription
        {
            get
            {
                return _GDescription;
            }

            set
            {
                SetProperty(ref _GDescription, value);
            }
        }

        /// <summary>
        /// Gets or sets the g media reference collection.
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return _GMediaCollection;
            }

            set
            {
                SetProperty(ref _GMediaCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g note reference collection. [element name = "noteref"].
        /// </summary>
        /// <value>
        /// The g note reference collection.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get
            {
                return _GNoteCollection;
            }

            set
            {
                SetProperty(ref _GNoteCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the g place.
        /// </summary>
        /// <value>
        /// The g place.
        /// </value>
        [DataMember]
        public HLinkPlaceModel GPlace
        {
            get
            {
                return _GPlace;
            }

            set
            {
                SetProperty(ref _GPlace, value);
            }
        }

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get
            {
                return _GTagReferenceCollection;
            }

            set
            {
                SetProperty(ref _GTagReferenceCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the Event Type. [element name = "type"].
        /// </summary>
        /// <value>
        /// The type of the Event.
        /// </value>
        [DataMember]
        public string GType
        {
            get
            {
                return _GType;
            }

            set
            {
                SetProperty(ref _GType, value);
            }
        }

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
        public int CompareTo(object obj)
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