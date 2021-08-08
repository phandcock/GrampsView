namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// Data model for a Tag item. XML 1.71 check complete
    /// </summary>
    [DataContract]
    public sealed class TagModel : ModelBase, ITagModel, IComparable, IComparer
    {
        /// <summary>
        /// The color.
        /// </summary>
        private Color _GColor = Color.White;

        /// <summary>
        /// The name.
        /// </summary>
        private string _GName = string.Empty;

        /// <summary>
        /// The priority.
        /// </summary>
        private int _GPriority;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagModel"/> class.
        /// </summary>
        public TagModel()
        {
            ModelItemGlyph.Symbol = CommonConstants.IconTag;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        public override string DefaultText
        {
            get
            {
                return GName;
            }
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [DataMember]
        public Color GColor
        {
            get => _GColor;

            set => SetProperty(ref _GColor, value);
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [DataMember]
        public string GName
        {
            get => _GName;

            set => SetProperty(ref _GName, value);
        }

        /// <summary>
        /// Gets or sets the Priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        [DataMember]
        public int GPriority
        {
            get => _GPriority;

            set => SetProperty(ref _GPriority, value);
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkTagModel HLink
        {
            get
            {
                HLinkTagModel t = new HLinkTagModel
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
        int IComparer.Compare(object a, object b)
        {
            TagModel firstEvent = (TagModel)a;
            TagModel secondEvent = (TagModel)b;

            // compare on Priority first
            int testFlag = string.Compare(firstEvent.GName, secondEvent.GName, StringComparison.CurrentCulture);

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
        int IComparable.CompareTo(object obj)
        {
            TagModel secondEvent = (TagModel)obj;

            // compare on Name first
            int testFlag = string.Compare(GName, secondEvent.GName, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}