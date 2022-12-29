using GrampsView.Common;
using GrampsView.Data.Collections;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels;

using System;
using System.Collections;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// <br/>
    /// </summary>
    /// <seealso cref="System.IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>

    public sealed class RepositoryModel : ModelBase, IRepositoryModel, IComparable, IComparer
    {
        /// <summary>
        /// The local r name.
        /// </summary>
        private string _RName = string.Empty;

        /// <summary>
        /// The local type.
        /// </summary>
        private string _Type = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryModel"/> class.
        /// </summary>
        public RepositoryModel()
        {
            ModelItemGlyph.Symbol = Constants.IconRepository;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets or sets address collection.
        /// </summary>

        public HLinkAddressModelCollection GAddress
        {
            get; set;
        }

        = new HLinkAddressModelCollection();

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>

        public HLinkNoteModelCollection GNoteRefCollection
        {
            get; set;
        }
            = new HLinkNoteModelCollection();

        /// <summary>
        /// Gets or sets the Repository Name.
        /// </summary>
        /// <value>
        /// The name of the Repository.
        /// </value>

        public string GRName
        {
            get => _RName;

            set => SetProperty(ref _RName, value);
        }

        /// <summary>
        /// Gets or sets the tag reference collection.
        /// </summary>
        /// <value>
        /// The tag reference collection.
        /// </value>

        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the repository type.
        /// </summary>
        /// <value>
        /// The type of the repository.
        /// </value>

        public string GType
        {
            get => _Type;

            set => SetProperty(ref _Type, value);
        }

        /// <summary>
        /// Gets or sets URL collection.
        /// </summary>

        public HLinkURLModelCollection GURL
        {
            get; set;
        }

        = new HLinkURLModelCollection();

        /// <summary>
        /// Gets the hlink.
        /// </summary>
        /// <value>
        /// The hlink.
        /// </value>
        public HLinkRepositoryModel HLink
        {
            get
            {
                HLinkRepositoryModel t = new()
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
            if ((a is null) || (b is null))
            {
                return 0;   // equal
            }

            RepositoryModel firstPersonModel = (RepositoryModel)a;
            RepositoryModel secondPersonModel = (RepositoryModel)b;

            // compare on Name first
            int testFlag = string.Compare(firstPersonModel.GRName, secondPersonModel.GRName, StringComparison.CurrentCulture);

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
            RepositoryModel secondPersonModel = (RepositoryModel)obj;

            // compare on Name first
            int testFlag = string.Compare(GRName, secondPersonModel.GRName, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Gets the default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string ToString()
        {
            return GRName;
        }
    }
}