namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;

    using Newtonsoft.Json;

    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// Base for Models.
    /// </summary>
    /// <seealso cref="GrampsView.Common.ObservableObject"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// ///
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    [DataContract]
    [KnownType(typeof(ObservableCollection<object>))]

    //[KnownType(typeof(HLinkBookMarkModel))]
    [KnownType(typeof(HLinkCitationModel))]
    [KnownType(typeof(HLinkEventModel))]
    [KnownType(typeof(HLinkFamilyModel))]
    [KnownType(typeof(HLinkHeaderModel))]
    [KnownType(typeof(HLinkMediaModel))]
    [KnownType(typeof(HLinkNameMapModel))]
    [KnownType(typeof(HLinkPersonModel))]
    [KnownType(typeof(HLinkNoteModel))]
    [KnownType(typeof(HLinkPlaceModel))]
    [KnownType(typeof(HLinkRepositoryModel))]
    [KnownType(typeof(HLinkSourceModel))]
    [KnownType(typeof(HLinkTagModel))]
    [KnownType(typeof(ItemGlyph))]
    public class ModelBase : ObservableObject, IModelBase
    {
        /// <summary>
        /// The local h link reference collection.
        /// </summary>
        private HLinkBackLinkModelCollection _BackHLinkReferenceCollection = new HLinkBackLinkModelCollection();

        /// <summary>
        /// The local change.
        /// </summary>
        private DateTime _Change = DateTime.MinValue;

        /// <summary>
        /// The local handle.
        /// </summary>
        private string _Handle = string.Empty;

        private string _Id = string.Empty;

        private ItemGlyph _ModelItemGlyph = new ItemGlyph();

        public ModelBase()
        {
            ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
            ModelItemGlyph.Symbol = CommonConstants.IconDDefault;
            ModelItemGlyph.SymbolColour = Xamarin.Forms.Color.FromHex("#A9A9A9"); //  CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            UCNavigateCommand = new AsyncCommand(() => UCNavigate());
        }

        /// <summary>
        /// Gets or sets the h link reference collection.
        /// </summary>
        /// <value>
        /// The h link reference collection.
        /// </value>
        [DataMember]
        public HLinkBackLinkModelCollection BackHLinkReferenceCollection
        {
            get => _BackHLinkReferenceCollection;

            set => SetProperty(ref _BackHLinkReferenceCollection, value);
        }

        /// <summary>
        /// Gets or sets the change CDATA #REQUIRED.
        /// </summary>
        /// <value>
        /// The change.
        /// </value>
        [DataMember]
        public DateTime Change
        {
            get => _Change;

            set => SetProperty(ref _Change, value);
        }

        ///// <summary>
        ///// Gets the de reference.
        ///// </summary>
        ///// <value>
        ///// The de reference.
        ///// </value>
        //public virtual ModelBase DeRef
        //{
        //    get
        //    {
        //        return new ModelBase();
        //    }
        //}

        /// <summary>
        /// Gets the get default text for this Model.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public virtual string GetDefaultText
        {
            get
            {
                return HLinkKey.Value;
            }
        }

        public virtual string GetDefaultTextShort
        {
            get
            {
                return GetDefaultText.Substring(0, Math.Min(GetDefaultText.Length, 40));
            }
        }

        /// <summary>
        /// Gets or sets the handle ID #REQUIRED.
        /// </summary>
        /// <value>
        /// The handle.
        /// </value>
        [DataMember]
        public string Handle
        {
            get => _Handle;

            set
            {
                SetProperty(ref _Handle, value);

                HLinkKey.Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        [DataMember]
        public HLinkKey HLinkKey
        {
            get;

            set;
        } = new HLinkKey();

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DataMember]
        public string Id
        {
            get => _Id;

            set => SetProperty(ref _Id, value);
        }

        [DataMember]
        public ItemGlyph ModelItemGlyph
        {
            get => _ModelItemGlyph;

            set => SetProperty(ref _ModelItemGlyph, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ModelBase"/> is priv.
        /// </summary>
        /// <value>
        /// <c>true</c> if priv; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public bool Priv
        {
            get;

            set;
        } = false;

        public IAsyncCommand UCNavigateCommand
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether returns true if the modelbase is valid.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public virtual bool Valid
        {
            get
            {
                return HLinkKey.Valid && ModelItemGlyph.Valid;
            }
        }

        public static bool operator !=(ModelBase left, ModelBase right)
        {
            return !(left == right);
        }

        public static bool operator <(ModelBase left, ModelBase right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(ModelBase left, ModelBase right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator ==(ModelBase left, ModelBase right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator >(ModelBase left, ModelBase right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(ModelBase left, ModelBase right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the specified a.
        /// </summary>
        /// <param name="argFirstModelBase">
        /// a.
        /// </param>
        /// <param name="argSecondModelBase">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        public int Compare(object argFirstModelBase, object argSecondModelBase)
        {
            if (argFirstModelBase is null)
            {
                throw new ArgumentNullException(nameof(argFirstModelBase));
            }

            if (argSecondModelBase is null)
            {
                throw new ArgumentNullException(nameof(argSecondModelBase));
            }

            if (!(argFirstModelBase is ModelBase))
            {
                return 0;
            }

            if (!(argSecondModelBase is ModelBase))
            {
                return 0;
            }

            ModelBase firstSource = (ModelBase)argFirstModelBase;
            ModelBase secondSource = (ModelBase)argSecondModelBase;

            // compare on Page first TODO compare on Page?
            int testFlag = Compare(firstSource.HLinkKey, secondSource.HLinkKey);

            return testFlag;
        }

        public int CompareTo(ModelBase other)
        {
            if (other is null)
            {
                return 1; // this is bigger
            }

            // compare on Page first TODO compare on Page?
            int testFlag = Compare(HLinkKey, other.HLinkKey);

            return testFlag;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Id))
            {
                return false;
            }

            if (string.IsNullOrEmpty((obj as ModelBase).Id))
            {
                return false;
            }

            if (this.Id == (obj as ModelBase).Id)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        public void LoadBasics(ModelBase argBasics)
        {
            Contract.Requires(!(argBasics is null));

            if (!(String.IsNullOrEmpty(argBasics.Id)))
            {
                Id = argBasics.Id;
            }

            if (argBasics.Change != DateTime.MinValue)
            {
                Change = argBasics.Change;
            }

            Priv = argBasics.Priv;

            if (!(String.IsNullOrEmpty(argBasics.Handle)))
            {
                Handle = argBasics.Handle;
            }
        }

        public virtual Task UCNavigate() => throw new NotImplementedException();

        public async Task UCNavigateBase<T>(T dataIn, string argPage) where T : new()
        {
            string ser = JsonConvert.SerializeObject(dataIn);

            await CommonRoutines.NavigateAsync($"{argPage}?BaseParamsModel={ser}");
        }
    }
}