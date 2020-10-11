/// - Completed
/// -- SecondaryColor-object
/// -- stitle
/// -- sauthor
/// -- spubinfo
/// -- sabbrev
/// -- noteref
/// -- objref
/// -- srcattribute
/// -- reporef
/// -- tagref

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Source ViewModel.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.ISourceModel"/>
    /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.IComparable"/>
    /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
    public sealed class SourceModel : ModelBase, ISourceModel, IComparable, IComparer<SourceModel>
    {
        private HLinkMediaModelCollection localMediaCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceModel"/> class.
        /// </summary>
        public SourceModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconSource;
            HomeImageHLink.HomeSymbolColour = HomeImageHLink.HomeSymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundSource");
        }

        /// <summary>
        /// Gets the get default text for this ViewModel.
        /// </summary>
        /// <value>
        /// The get default text.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                return GSTitle;
            }
        }

        /// <summary>
        /// Gets or sets the g media reference collection.
        /// <code><zeroOrMore><element name="objref"><ref name="objref-content" /></element></zeroOrMore></code>
        /// </summary>
        /// <value>
        /// The g media reference collection.
        /// </value>
        [DataMember]
        public HLinkMediaModelCollection GMediaRefCollection
        {
            get
            {
                return localMediaCollection;
            }

            set
            {
                SetProperty(ref localMediaCollection, value);
            }
        }

        /// <code><ref name="SecondaryColor-object" /></code>
        /// <summary>
        /// Gets or sets the g note reference.
        /// <code><zeroOrMore><element name="noteref"><ref name="noteref-content" /></element></zeroOrMore></code>
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
        /// Gets or sets the g repository reference collection.
        /// </summary>
        /// <value>
        /// The g repository reference collection.
        /// </value>
        [DataMember]
        public HLinkRepositoryModelCollection GRepositoryRefCollection { get; set; }

        /// <summary> Gets or sets the gs title. <code> < optional > < element name="sabbrev"> <text
        /// /> </element> </optional> </code> </summary> <value> The source author. </value>
        [DataMember]
        public string GSAbbrev { get; set; }

        /// <summary> Gets or sets the gs title. <code> < optional > < element name="sauthor"> <text
        /// /> </element> </optional> </code> </summary> <value> The source author. </value>
        [DataMember]
        public string GSAuthor { get; set; }

        /// <summary>
        /// Gets or sets the g source attribute collection.
        /// </summary>
        /// <value>
        /// The g source attribute collection.
        /// </value>
        [DataMember]
        public OCAttributeModelCollection GSourceAttributeCollection { get; set; }

        /// <summary> Gets or sets the gs title. <code> <optional> <element name = "spubinfo" > <
        /// text /> </ element > </ optional > </code> </summary> <value> The source author. </value>
        [DataMember]
        public string GSPubInfo { get; set; }


        // TODO check if all fields loaded
        /// <summary> Gets or sets the gs title. <code> <optional> <element name = "stitle" > < text
        /// /> </ element > </ optional > </code> </summary> <value> The source title. </value>
        [DataMember]
        public string GSTitle { get; set; }

        /// <summary>
        /// Gets or sets the gs title.
        /// <code><zeroOrMore><element name="tagref"><ref name="tagref-content" /></element></zeroOrMore></code>
        /// </summary>
        /// <value>
        /// The source author.
        /// </value>
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkSourceModel HLink
        {
            get
            {
                HLinkSourceModel t = new HLinkSourceModel
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
            if ((a is null) || (b is null))
            {
                return 0;   // equal
            }

            SourceModel firstSource = (SourceModel)a;
            SourceModel secondSource = (SourceModel)b;

            // compare on Page first TODO compare on Page?
            int testFlag = Compare(firstSource.GSTitle, secondSource.GSTitle);

            return testFlag;
        }

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        public int Compare(SourceModel x, SourceModel y)
        {
            return Compare(x.GSTitle, y.GSTitle);
        }

        // TODO tagref*
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
            if (obj == null)
            {
                return 1;
            }

            SourceModel secondSource = (SourceModel)obj;

            // compare on Page first TODO compare on Page?
            int testFlag = string.Compare(GSTitle, secondSource.GSTitle, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="ArgHlinkSM">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(HLinkSourceModel ArgHlinkSM)
        {
            if (ArgHlinkSM == null)
            {
                return 1;
            }

            SourceModel secondSource = ArgHlinkSM.DeRef;

            // compare on Page first TODO compare on Page?
            int testFlag = string.Compare(GSTitle, secondSource.GSTitle, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}