namespace GrampsView.Data.Model
{
    using GrampsView.Common;

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
        //public DisplayFormat CardType { get; set; } = DisplayFormat.Default;

        [DataMember]
        public bool GPriv
        {
            get
            {
                return _Priv;
            }

            set
            {
                SetProperty(ref _Priv, value);
            }
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        [DataMember]
        public string HLinkKey
        {
            get
            {
                return _HLinkKey;
            }

            set
            {
                SetProperty(ref _HLinkKey, value);
            }
        }

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
                return (!string.IsNullOrEmpty(HLinkKey));
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

            return string.Compare(HLinkKey, (obj as HLinkBase).HLinkKey, true, System.Globalization.CultureInfo.CurrentCulture);
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

            await AppShell.Current.GoToAsync(string.Format("{0}?BaseParamsHLink={1}", argPage, ser));
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

            return string.Compare((x as HLinkBase).HLinkKey, (y as HLinkBase).HLinkKey, StringComparison.CurrentCulture);
        }
    }
}