//// gramps XML 1.71 - Done
////
////    primary-object
////    format
////    type
////    styledtext
///     tagref

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// Data model for a note.
    /// </summary>
    [DataContract]
    public sealed class NoteModel : ModelBase, INoteModel, IComparable, IComparer
    {
        public const string GTypeLink = "Link";
        public const string GTypeToDo = "To Do";

        private FormattedString _TextFormatted = new FormattedString();

        public NoteModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconNotes;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        /// <summary>
        /// Gets the default text for notes which is the first fourty characters minus returns.
        /// </summary>
        /// <value>
        /// Get the default text.
        /// </value>
        public new string GetDefaultText
        {
            get
            {
                return GStyledText.GText.Replace('\r', ',').Substring(0, Math.Min(GStyledText.GText.Length, 40));
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NoteModel"/> is format (0|1) #IMPLIED.
        /// </summary>
        /// <value>
        /// <c>true</c> if format; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool GIsFormated
        {
            get;

            set;
        }

        [DataMember]
        public StyledTextModel GStyledText { get; set; } = new StyledTextModel();

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        }

        = new HLinkTagModelCollection();

        // TODO add field style*
        /// <summary>
        /// Gets or sets the type CDATA #REQUIRED.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DataMember]
        public string GType
        {
            get;

            set;
        }

        /// <summary>
        /// Gets the get hlink.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkNoteModel HLink
        {
            get
            {
                HLinkNoteModel t = new HLinkNoteModel
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }

        public FormattedString TextFormatted
        {
            get
            {
                if (_TextFormatted.Spans.Count == 0)
                {
                    _TextFormatted = GrampsTextToXamarinText.GetFormattedString(GStyledText, CommonFontSize.FontSmall);
                }

                return _TextFormatted;
            }
        }

        /// <summary>
        /// Gets the shortened form of the text. Maximum length is 100.
        /// </summary>
        /// <value>
        /// The text short.
        /// </value>
        public string TextShort
        {
            get
            {
                return GStyledText.GText.Substring(0, Math.Min(GStyledText.GText.Length, 100));
            }
        }

        public bool TypeIsList
        {
            get
            {
                return GType == GTypeLink;
            }
        }

        public bool TypeIsToDo
        {
            get
            {
                return (GType == GTypeToDo);
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

            NoteModel firstEvent = (NoteModel)a;
            NoteModel secondEvent = (NoteModel)b;

            // compare on Date first
            int testFlag = string.Compare(firstEvent.GStyledText.GText, secondEvent.GStyledText.GText, StringComparison.CurrentCulture);

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
                return 0;
            }

            NoteModel secondEvent = (NoteModel)obj;

            // compare on String first
            int testFlag = string.Compare(GStyledText.GText, secondEvent.GStyledText.GText, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}