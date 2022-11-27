using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;

using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GrampsView.Models.DataModels
{
    /// <summary>
    /// Base for Models.
    /// </summary>
    /// <seealso cref="Common.ObservableObject"/>
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// /// ///
    /// <seealso cref="Data.ViewModel.IModelBase"/>

    public class ModelBase : ObservableObject, IModelBase
    {
        /// <summary>
        /// The local h link reference collection.
        /// </summary>
        private HLinkBackLinkModelCollection _BackHLinkReferenceCollection = new();

        /// <summary>
        /// The local change.
        /// </summary>
        private DateTime _Change = DateTime.MinValue;

        private string _Id = string.Empty;

        private ItemGlyph _ModelItemGlyph = new();


        public ModelBase()
        {
            ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
            ModelItemGlyph.Symbol = Constants.IconDDefault;
            ModelItemGlyph.SymbolColour = Color.FromArgb("#A9A9A9"); //  CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            UCNavigateCommand = new AsyncRelayCommand(UCNavigate);
        }

        /// <summary>
        /// Gets or sets the h link reference collection.
        /// </summary>
        /// <value>
        /// The h link reference collection.
        /// </value>
        [JsonInclude]
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
        [JsonInclude]
        public DateTime Change
        {
            get => _Change;

            set => SetProperty(ref _Change, value);
        }

        public virtual string DefaultTextShort => ToString()[..Math.Min(ToString().Length, 40)];

        [JsonInclude]
        public HLinkKey HLinkKey
        {
            get;

            set;
        } = new HLinkKey();

        [JsonInclude]
        public string Id
        {
            get => _Id;

            set => SetProperty(ref _Id, value);
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonInclude]
        public ItemGlyph ModelItemGlyph
        {
            get => _ModelItemGlyph;

            set => SetProperty(ref _ModelItemGlyph, value);
        }

        [JsonInclude]
        public bool Priv
        {
            get;

            set;
        } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ModelBase"/> is priv.
        /// </summary>
        /// <value>
        /// <c> true </c> if priv; otherwise, <c> false </c>.
        /// </value>
        public IAsyncRelayCommand UCNavigateCommand
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether returns true if the modelbase is valid.
        /// </summary>
        /// <value>
        /// <c> true </c> if this instance is valid; otherwise, <c> false </c>.
        /// </value>
        public virtual bool Valid => HLinkKey.Valid && ModelItemGlyph.Valid;

        public static bool operator !=(ModelBase left, ModelBase right)
        {
            return !(left == right);
        }

        public static bool operator <(ModelBase left, ModelBase right)
        {
            return left is null ? right is not null : left.CompareTo(right) < SharedSharpConstants.CompareEquals;
        }

        public static bool operator <=(ModelBase left, ModelBase right)
        {
            return left is null || left.CompareTo(right) <= SharedSharpConstants.CompareEquals;
        }

        public static bool operator ==(ModelBase left, ModelBase right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator >(ModelBase left, ModelBase right)
        {
            return left is not null && left.CompareTo(right) > SharedSharpConstants.CompareEquals;
        }

        public static bool operator >=(ModelBase left, ModelBase right)
        {
            return left is null ? right is null : left.CompareTo(right) >= SharedSharpConstants.CompareEquals;
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
        public virtual int Compare(object argFirstModelBase, object argSecondModelBase)
        {
            if (argFirstModelBase is null)
            {
                throw new ArgumentNullException(nameof(argFirstModelBase));
            }

            if (argSecondModelBase is null)
            {
                throw new ArgumentNullException(nameof(argSecondModelBase));
            }

            ModelBase firstSource = (ModelBase)argFirstModelBase;
            ModelBase secondSource = (ModelBase)argSecondModelBase;

            return firstSource is null
                ? SharedSharpConstants.CompareEquals
                : secondSource is null ? SharedSharpConstants.CompareEquals : Compare(firstSource.HLinkKey, secondSource.HLinkKey);
        }

        public int CompareTo(ModelBase other)
        {
            if (other is null)
            {
                return SharedSharpConstants.CompareGreaterThan;
            }

            // This is effectively random
            return HLinkKey.CompareTo(other.HLinkKey);
        }

        public virtual int CompareTo(object obj)
        {
            // Only comparable if ModelBase
            if ((ModelBase)obj == null)
            {
                throw new NotImplementedException();
            }

            // This is effectively random
            return Compare(HLinkKey, (obj as ModelBase).HLinkKey);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is null)
            { return false; }

            return obj.GetType() == GetType()
&& !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty((obj as ModelBase).Id) && Id == (obj as ModelBase).Id;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        public void LoadBasics(ModelBase argBasics)
        {
            Contract.Requires(argBasics is not null);

            if (!string.IsNullOrEmpty(argBasics.Id))
            {
                Id = argBasics.Id;
            }

            if (argBasics.Change != DateTime.MinValue)
            {
                Change = argBasics.Change;
            }

            Priv = argBasics.Priv;

            if (argBasics.HLinkKey.Valid)
            {
                HLinkKey = argBasics.HLinkKey;
            }
        }

        public virtual Task UCNavigate()
        {
            throw new NotImplementedException();
        }

        public async Task UCNavigateBase<T>(T dataIn, string argPage) where T : new()
        {
            string ser = JsonSerializer.Serialize(dataIn);

            await SharedSharpNavigation.NavigateAsync($"{argPage}?BaseParamsModel={ser}");
        }
    }
}