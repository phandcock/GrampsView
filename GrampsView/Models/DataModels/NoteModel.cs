

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;

using System.Collections;
using System.Text.RegularExpressions;

namespace GrampsView.Models.DataModels
{
    /// <summary>Data model for a note.</summary>
    /// <remarks>gramps XML 1.71 - Done<br /><br />  primary-object<br />////    format<br />////    type<br />////    styledtext<br />///     tagref</remarks>

    public sealed class NoteModel : ModelBase, INoteModel, IComparable, IComparer
    {
        private readonly FormattedString _TextFormatted = new();

        public NoteModel()
        {
            ModelItemGlyph.Symbol = Constants.IconNotes;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundNote");
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NoteModel"/> is format (0|1) #IMPLIED.
        /// </summary>
        /// <value>
        /// <c> true </c> if format; otherwise, <c> false </c>.
        /// </value>

        public bool GIsFormated
        {
            get;

            set;
        }

        public StyledTextModel GStyledText { get; set; } = new StyledTextModel();

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The tag reference collection.
        /// </value>

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
                HLinkNoteModel t = new()
                {
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };
                return t;
            }
        }






        /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />:<br />   - If less than 0, <paramref name="x" /> is less than <paramref name="y" />.<br />   - If 0, <paramref name="x" /> equals <paramref name="y" />.<br />   - If greater than 0, <paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">x
        /// or
        /// y</exception>
        public new int Compare(object x, object y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x));
            }

            if (y is null)
            {
                throw new ArgumentNullException(nameof(y));
            }

            NoteModel firstEvent = (NoteModel)x;
            NoteModel secondEvent = (NoteModel)y;

            int testFlag = string.Compare(firstEvent.GStyledText.GText, secondEvent.GStyledText.GText, StringComparison.Ordinal);

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

            NoteModel secondEvent = (NoteModel)obj;

            // compare on String first
            int testFlag = string.Compare(GStyledText.GText, secondEvent.GStyledText.GText, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Gets the default text for notes which is the first fourty characters minus returns,
        /// spaces and tabs.
        /// </summary>
        /// <value>
        /// Get the default text.
        /// </value>
        [Obsolete]
        public override string ToString()
        {
            string removableChars = @"\n\r\s\t";

            string pattern = "[" + removableChars + "]";

            string cleanString = Regex.Replace(GStyledText.GText, pattern, " ");

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    break;

                case Device.Android:
                    break;

                case Device.UWP:
                    return cleanString[..Math.Min(cleanString.Length, 100)];



                default:
                    break;
            }

            return cleanString[..Math.Min(cleanString.Length, 100)];
        }
    }
}