// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// <para> Source ViewModel. </para>
    /// <list type="table">
    /// <item>
    /// <description> Area </description>
    /// <description> Status </description>
    /// </item>
    /// <item>
    /// <description> XML 1.71 Checked </description>
    /// <description> Yes </description>
    /// </item>
    /// </list>
    /// </summary>


    public sealed class SourceModel : ModelBase, ISourceModel, IComparable, IComparer<SourceModel>
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="SourceModel"/> class.
        /// </summary>
        public SourceModel()
        {
            ModelItemGlyph.Symbol = Constants.IconSource;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundSource");
        }

        /// <summary>
        /// Gets or sets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>

        public HLinkMediaModelCollection GMediaRefCollection { get; set; }
                = new();

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>

        public HLinkNoteDBModelCollection GNoteRefCollection
        {
            get;
            set;
        }
            = new HLinkNoteDBModelCollection();

        /// <summary>
        /// Gets or sets the repository reference collection.
        /// </summary>
        /// <value>
        /// The repository reference collection.
        /// </value>

        public HLinkRepositoryRefCollection GRepositoryRefCollection
        {
            get; set;
        }
            = new HLinkRepositoryRefCollection();

        /// <summary>
        /// Gets or sets the sabbrev.
        /// </summary>
        /// <value>
        /// The sabbrev.
        /// </value>

        public string GSAbbrev
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the sauthor.
        /// </summary>
        /// <value>
        /// The sauthor.
        /// </value>

        public string GSAuthor
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the source attribute collection.
        /// </summary>
        /// <value>
        /// The source attribute collection.
        /// </value>

        public HLinkAttributeModelCollection GSourceAttributeCollection
        {
            get; set;
        }
            = new();

        /// <summary>
        /// Gets or sets the spub information.
        /// </summary>
        /// <value>
        /// The spub information.
        /// </value>

        public string GSPubInfo
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the stitle.
        /// </summary>
        /// <value>
        /// The stitle.
        /// </value>

        public string GSTitle
        {
            get; set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The tag reference collection.
        /// </value>

        public HLinkTagModelCollection GTagRefCollection { get; set; }
            = new();

        /// <summary>
        /// Gets the hlink.
        /// </summary>
        /// <value>
        /// The hlink.
        /// </value>
        public HLinkSourceModel HLink
        {
            get
            {
                HLinkSourceModel t = new()
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
            if ((a is null) || (b is null))
            {
                return 0;   // equal
            }

            SourceModel firstSource = (SourceModel)a;
            SourceModel secondSource = (SourceModel)b;

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
            if (obj == null)
            {
                return 1;
            }

            SourceModel secondSource = (SourceModel)obj;

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

            int testFlag = string.Compare(GSTitle, secondSource.GSTitle, StringComparison.CurrentCulture);

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
            return GSTitle[..Math.Min(40, GSTitle.Length)];
            ;
        }
    }
}