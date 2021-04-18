namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;

    using Newtonsoft.Json;

    using System;
    using System.Collections;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>

    /// TODO Update fields as per Schema
    [DataContract]
    public class HLinkBase : CommonBindableBase, IHLinkBase
    {
        private ItemGlyph _HLinkGlyphItem = new ItemGlyph();

        /// <summary>
        /// The local h link key.
        /// </summary>
        private string _HLinkKey = string.Empty;

        /// <summary>
        /// The local priv.
        /// </summary>
        private bool _Priv;

        public HLinkBase()
        {
            UCNavigateCommand = new AsyncCommand(() => UCNavigate());
        }

        public CommonEnums.DisplayFormat DisplayAs { get; set; } = CommonEnums.DisplayFormat.Default;

        /// <summary>
        /// Gets or sets a value indicating whether [home use image].
        /// </summary>
        /// <value>
        /// <c>true</c> if [home use image]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public ItemGlyph HLinkGlyphItem
        {
            get
            {
                return _HLinkGlyphItem;
            }

            set
            {
                SetProperty(ref _HLinkGlyphItem, value);
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

        [DataMember]
        public Priv Priv
        {
            get;

            set;
        } = new Priv(false);

        /// <summary>
        /// Gets the priv as string.
        /// </summary>
        /// <value>
        /// The priv as string.
        /// </value>
        public string PrivAsString
        {
            get
            {
                return _Priv.ToString(CultureInfo.CurrentCulture);
            }
        }

        public IAsyncCommand UCNavigateCommand
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid.
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public virtual bool Valid
        {
            get
            {
                return HLinkKey.Valid;
            }
        }

        public static bool operator !=(HLinkBase left, HLinkBase right)
        {
            return !(left == right);
        }

        public static bool operator <(HLinkBase left, HLinkBase right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(HLinkBase left, HLinkBase right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator ==(HLinkBase left, HLinkBase right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator >(HLinkBase left, HLinkBase right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(HLinkBase left, HLinkBase right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
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
        int IComparer.Compare(object x, object y)
        {
            return Compare(x, y);
        }

        /// <summary>
        /// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            return string.Compare(HLinkKey.Value, (obj as HLinkBase).HLinkKey.Value, true, CultureInfo.CurrentCulture);
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

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return (this.HLinkKey == (obj as HLinkBase).HLinkKey);
        }

        public override int GetHashCode()
        {
            return HLinkKey.GetHashCode();
        }

        /// <summary>
        /// Sets the base fields.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        public void SetBase(HLinkBase arg)
        {
            Contract.Assert(arg != null);

            HLinkKey = arg.HLinkKey;
        }

        public virtual Task UCNavigate() => throw new NotImplementedException();

        public async Task UCNavigateBase<T>(T dataIn, string argPage) where T : new()
        {
            string ser = JsonConvert.SerializeObject(dataIn);

            await CommonRoutines.NavigateAsync(string.Format("{0}?BaseParamsHLink={1}", argPage, ser));
        }

        /// <summary>
        /// Compares the specified x. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        protected static int Compare(object x, object y)
        {
            Contract.Assert(!(x is null));
            Contract.Assert(!(y is null));

            return string.Compare((x as HLinkBase).HLinkKey.Value, (y as HLinkBase).HLinkKey.Value, StringComparison.CurrentCulture);
        }
    }
}