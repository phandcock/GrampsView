// TODO Needs XML 1.71 check

//// gramps XML 1.71
////
////    primary-object
////    format
////    type
////    styledtext
///     tagref
///TODO Update fields as per Schema

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// data model for an event.
    /// </summary>
    [DataContract]
    public sealed class NoteModel : ModelBase, INoteModel, IComparable, IComparer
    {
        /// <summary>
        /// The g type to do.
        /// </summary>
        public const string GTypeToDo = "To Do";

        private FormattedString _FormattedText = new FormattedString();

        private string _GText = string.Empty;

        /// <summary>
        /// The local IsFormated.
        /// </summary>
        private bool _IsFormated;

        /// <summary>
        /// The local type.
        /// </summary>
        private string _Type = string.Empty;

        public NoteModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconNotes;
            HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        /// <summary>
        /// Gets the default text for notes which is the first twenty characters.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public new string GetDefaultText
        {
            get
            {
                return TextShort;
            }
        }

        /// <summary>
        /// Gets the formatted text in a medium size.
        /// </summary>
        /// <value>
        /// The formatted text in a medium sizemedium.
        /// </value>
        /// TODO Add styled text
        public FormattedString GFormattedTextMedium
        {
            get
            {
                return GetFormatted(CommonFontSize.FontMedium);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        /// TODO Add styled text
        public FormattedString GFormattedTextSmall
        {
            get
            {
                return GetFormatted(CommonFontSize.FontSmall);
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
            get
            {
                return _IsFormated;
            }

            set
            {
                SetProperty(ref _IsFormated, value);
            }
        }

        [DataMember]
        public StyledTextModelCollection GStyledTextCollection { get; set; } = new StyledTextModelCollection();

        /// <summary>
        /// Gets or sets the g tag reference collection.
        /// </summary>
        /// <value>
        /// The g tag reference collection.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection
        {
            get; set;
        }

        = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [DataMember]
        public string GText
        {
            get
            {
                return _GText;
            }

            set
            {
                SetProperty(ref _GText, value);
            }
        }

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
            get
            {
                return _Type;
            }

            set
            {
                SetProperty(ref _Type, value);
            }
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
                };
                return t;
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
                return GText.Substring(0, Math.Min(GText.Length, 100));
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
            int testFlag = string.Compare(firstEvent.GText, secondEvent.GText, StringComparison.CurrentCulture);

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
            int testFlag = string.Compare(GText, secondEvent.GText, StringComparison.CurrentCulture);

            return testFlag;
        }

        // TODO Handle Styled Text
        private FormattedString GetFormatted(double argFontSize)
        {
            // Cache the formatted string. Xamarin/NewtonSoft has problems serialising the string on
            // the UI thread
            if (_FormattedText.Spans.Count == 0)
            {
                FormattedString loadString = new FormattedString();

                loadString.Spans.Add(new Span { Text = GText, FontSize = argFontSize });

                return loadString;
            }

            return _FormattedText;
        }
    }
}