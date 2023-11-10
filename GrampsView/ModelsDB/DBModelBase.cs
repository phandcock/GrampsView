// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Models.DBModels.Interfaces;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace GrampsView.DBModels
{
    public class DBModelBase : ObservableObject, IDBModelBase, INotifyPropertyChanged

    {
        private HLinkKey _HLinkKey = new HLinkKey();

        public DBModelBase()
        {
            ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;
            ModelItemGlyph.Symbol = Constants.IconDDefault;
            ModelItemGlyph.SymbolColour = Color.FromArgb("#A9A9A9"); //  CommonRoutines.ResourceColourGet("CardBackGroundUtility");

            UCNavigateCommand = new AsyncRelayCommand(UCNavigate);
        }

        public HLinkBackLinkDBModelCollection BackHLinkReferenceCollection { get; set; }
            = new HLinkBackLinkDBModelCollection("BookMarkData");

        public DateTime Change { get; set; }
            = DateTime.MinValue;

        /// <summary>
        /// Gets or sets the h link reference collection.
        /// </summary>
        /// <value>
        /// The hlink reference collection.
        /// </value>
        /// <summary>
        /// Gets or sets the change CDATA #REQUIRED.
        /// </summary>
        /// <value>
        /// The change.
        /// </value>
        public virtual string DefaultTextShort => ToString()[..Math.Min(ToString().Length, 40)];

        //public HLinkDBBase HLink { get; set; } = new HLinkDBBase();

        public HLinkKey HLinkKey
        {
            get
            {
                return _HLinkKey;
            }

            set
            {
                _HLinkKey = value;
                HLinkKeyValue = value.Value;
            }
        }

        [Key]
        public string HLinkKeyValue { get; set; } = Guid.NewGuid().ToString();

        public string Id { get; set; }
            = string.Empty;

        /// <summary>
        /// Gets or sets the hlink key.
        /// </summary>
        /// <value>
        /// The hlink key.
        /// </value>
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>

        public ItemGlyph ModelItemGlyph { get; set; }
            = new ItemGlyph();

        public bool Priv { get; set; }
            = false;

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

        public static bool operator !=(DBModelBase left, DBModelBase right)
        {
            return !(left == right);
        }

        public static bool operator <(DBModelBase left, DBModelBase right)
        {
            return left is null ? right is not null : left.CompareTo(right) < SharedConstants.CompareEquals;
        }

        public static bool operator <=(DBModelBase left, DBModelBase right)
        {
            return left is null || left.CompareTo(right) <= SharedConstants.CompareEquals;
        }

        public static bool operator ==(DBModelBase left, DBModelBase right)
        {
            return left is null ? right is null : left.Equals(right);
        }

        public static bool operator >(DBModelBase left, DBModelBase right)
        {
            return left is not null && left.CompareTo(right) > SharedConstants.CompareEquals;
        }

        public static bool operator >=(DBModelBase left, DBModelBase right)
        {
            return left is null ? right is null : left.CompareTo(right) >= SharedConstants.CompareEquals;
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

            DBModelBase firstSource = (DBModelBase)argFirstModelBase;
            DBModelBase secondSource = (DBModelBase)argSecondModelBase;

            return firstSource is null
                ? SharedConstants.CompareEquals
                : secondSource is null ? SharedConstants.CompareEquals : Compare(firstSource.HLinkKey, secondSource.HLinkKey);
        }

        public int CompareTo(DBModelBase other)
        {
            if (other is null)
            {
                return SharedConstants.CompareGreaterThan;
            }

            // This is effectively random
            return HLinkKey.CompareTo(other.HLinkKey);
        }

        public virtual int CompareTo(object obj)
        {
            // Only comparable if ModelBase
            if ((DBModelBase)obj == null)
            {
                throw new NotImplementedException();
            }

            // This is effectively random
            return Compare(HLinkKey, (obj as DBModelBase).HLinkKey);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is not null
                && obj.GetType() == GetType()
                && !string.IsNullOrEmpty(Id)
                && !string.IsNullOrEmpty((obj as DBModelBase).Id)
                && Id == (obj as DBModelBase).Id;
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        public void LoadBasics(DBModelBase argBasics)
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

        public virtual async Task UCNavigate()
        {
            throw new NotImplementedException();
        }
    }
}