namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections.Generic;

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
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// <seealso cref="GrampsView.Data.ViewModel.ISourceModel"/>
    /// <seealso cref="System.IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>

    public sealed class SourceModel : ModelBase, ISourceModel, IComparable, IComparer<SourceModel>
    {
        private HLinkMediaModelCollection _MediaCollection = new HLinkMediaModelCollection();

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceModel"/> class.
        /// </summary>
        public SourceModel()
        {
            ModelItemGlyph.Symbol = Constants.IconSource;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundSource");
        }

        //public HLinkRepositoryRefModel GetDefaultRepository
        //{
        //    get
        //    {
        //        if (GRepositoryRefCollection.Count > 0)
        //        {
        //            HLinkRepositoryRefModel t = GRepositoryRefCollection[0];

        //            if (t.Valid)
        //            {
        //                return t;
        //            }
        //        }

        //        return new HLinkRepositoryRefModel();
        //    }
        //}

        /// <summary>
        /// Gets or sets the media reference collection.
        /// </summary>
        /// <value>
        /// The media reference collection.
        /// </value>

        public HLinkMediaModelCollection GMediaRefCollection
        {
            get => _MediaCollection;

            set => SetProperty(ref _MediaCollection, value);
        }

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>

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
        }

        /// <summary>
        /// Gets or sets the sauthor.
        /// </summary>
        /// <value>
        /// The sauthor.
        /// </value>

        public string GSAuthor
        {
            get; set;
        }

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

        /// <summary>
        /// Gets or sets the spub information.
        /// </summary>
        /// <value>
        /// The spub information.
        /// </value>

        public string GSPubInfo
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the stitle.
        /// </summary>
        /// <value>
        /// The stitle.
        /// </value>

        public string GSTitle
        {
            get; set;
        }

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
                HLinkSourceModel t = new HLinkSourceModel
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
            return GSTitle.Substring(0, Math.Min(40, GSTitle.Length)); ;
        }
    }
}