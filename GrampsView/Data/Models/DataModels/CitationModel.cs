/// XML 171 - All fields defined
///
/// SecondaryColor-object date-content page confidence noteref objref srcattribute sourceref tagref

/// <summary>
/// </summary>
namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Data model for a Citation.
    /// </summary>
    [DataContract]
    [KnownType(typeof(HLinkSourceModel))]
    public sealed class CitationModel : ModelBase, ICitationModel, IComparable, IComparer, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationModel"/> class.
        /// </summary>
        public CitationModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconCitation;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundCitation");
        }

        /// <summary>
        /// Gets or sets the Confidence level.
        /// </summary>
        /// <value>
        /// The Confidence.
        /// </value>
        [DataMember]
        public CommonEnums.DataConfidence GConfidence
        {
            get;
            set;
        }

            = CommonEnums.DataConfidence.Unknown;

        /// <summary>
        /// Gets or sets the content of the DateContent field.
        /// </summary>
        /// <value>
        /// The content of the g date.
        /// </value>
        [DataMember]
        public DateObjectModel GDateContent
        {
            get;
            set;
        }

            = new DateObjectModelVal();

        /// <summary>
        /// Gets or sets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get;
            set;
        }

            = new HLinkMediaModelCollection();

        /// <summary>
        /// Gets or sets the note reference.
        /// </summary>
        /// <value>
        /// The g note reference.
        /// </value>
        [DataMember]
        public HLinkNoteModelCollection GNoteRefCollection
        {
            get;
            set;
        }

            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The g page.
        /// </value>
        [DataMember]
        public string GPage
        {
            get;
            set;
        }

            = string.Empty;

        /// <summary>
        /// Gets or sets the source attribute.
        /// </summary>
        /// <value>
        /// The g source attribute.
        /// </value>
        [DataMember]
        public OCSrcAttributeModelCollection GSourceAttributeCollection
        {
            get;
            set;
        }

            = new OCSrcAttributeModelCollection();

        /// <summary>
        /// Gets or sets the source reference.
        /// </summary>
        /// <value>
        /// The g source reference.
        /// </value>
        [DataMember]
        public HLinkSourceModel GSourceRef
        {
            get;
            set;
        }

        = new HLinkSourceModel();

        /// <summary>
        /// Gets or sets the gramps tag reference.
        /// </summary>
        /// <value>
        /// The g tag reference.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRef
        {
            get;
            set;
        }

            = new HLinkTagModelCollection();

        /// <summary>
        /// Gets the get hlink.
        /// </summary>
        /// <value>
        /// The get hlink.
        /// </value>
        public HLinkCitationModel HLink
        {
            get
            {
                HLinkCitationModel t = new HLinkCitationModel
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
                return 0;
            }

            if (b is null)
            {
                return 0;
            }

            CitationModel firstEvent = (CitationModel)a;
            CitationModel secondEvent = (CitationModel)b;

            // compare on Date first
            int testFlag = DateTime.Compare(firstEvent.GDateContent.SortDate, secondEvent.GDateContent.SortDate);

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
        public override int CompareTo(object obj)
        {
            if (obj is null)
            {
                return 0;
            }

            CitationModel secondEvent = (CitationModel)obj;

            int testFlag = DateTime.Compare(GDateContent.SortDate, secondEvent.GDateContent.SortDate);

            return testFlag;
        }

        /// <summary>
        /// Gets the get default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string ToString()
        {
            if (GSourceRef.Valid)
            {
                return GSourceRef.DeRef.GSTitle;
            }

            return "???Source Reference not found";
        }
    }
}