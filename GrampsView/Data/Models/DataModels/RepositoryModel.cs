namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;

    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    /// <summary>
    /// <br/>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// <seealso cref="GrampsView.Data.ViewModel.IRepositoryModel"/>
    /// <seealso cref="System.IComparable"/>
    /// <seealso cref="System.Collections.IComparer"/>
    [DataContract]
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
            ModelItemGlyph.Symbol = CommonConstants.IconRepository;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundRepository");
        }

        /// <summary>
        /// Gets or sets address collection.
        /// </summary>
        [DataMember]
        public HLinkAddressModelCollection GAddress
        {
            get; set;
        }

        = new HLinkAddressModelCollection();

        /// <summary>
        /// Gets the default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string GetDefaultText
        {
            get
            {
                return GRName;
            }
        }

        /// <summary>
        /// Gets or sets the note reference collection.
        /// </summary>
        /// <value>
        /// The note reference collection.
        /// </value>
        [DataMember]
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
        [DataMember]
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
        [DataMember]
        public HLinkTagModelCollection GTagRefCollection { get; set; } = new HLinkTagModelCollection();

        /// <summary>
        /// Gets or sets the repository type.
        /// </summary>
        /// <value>
        /// The type of the repository.
        /// </value>
        [DataMember]
        public string GType
        {
            get => _Type;

            set => SetProperty(ref _Type, value);
        }

        /// <summary>
        /// Gets or sets URL collection.
        /// </summary>
        [DataMember]
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
                HLinkRepositoryModel t = new HLinkRepositoryModel
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
    }
}