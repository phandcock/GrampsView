namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;

    using System;
    using System.Diagnostics.Contracts;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    /// <summary>
    /// duh
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> Not Done </description>
    /// </item>
    /// </list>
    /// <para> <br/> </para>
    /// </summary>
    /// TODO Update fields as per Schema

    public class HLinkBase : ObservableObject, IHLinkBase
    {
        public HLinkBase()
        {
            UCNavigateCommand = new AsyncCommand(UCNavigate);
        }

        public CommonEnums.DisplayFormat DisplayAs { get; set; } = CommonEnums.DisplayFormat.Default;

        public ItemGlyph HLinkGlyphItem
        {
            get;

            set;
        } = new ItemGlyph();

        public HLinkKey HLinkKey
        {
            get;

            set;
        } = new HLinkKey();

        /// <summary>
        /// Gets or sets the hlink key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
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
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid.
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public virtual bool Valid
        {
            get
            {
                if (!(HLinkKey.Valid && HLinkGlyphItem.Valid))
                {
                }

                return HLinkKey.Valid && HLinkGlyphItem.Valid;
            }
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

        // if (this.GetType() != obj.GetType()) { return false; }
        public virtual Task UCNavigate() => throw new NotImplementedException();

        // if (obj is null) { return false; }
        public async Task UCNavigateBase<T>(T dataIn, string argPage) where T : new()
        {
            string ser = JsonSerializer.Serialize(dataIn);

            await SharedSharp.Common.SharedSharpNavigation.NavigateAsync($"{argPage}?BaseParamsHLink={ser}");
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

        // TODO fix when using c# and covariant classes
        //protected abstract IModelBase GetDeRef();

        //public static bool operator !=(HLinkBase left, HLinkBase right)
        //{
        //    return !(left == right);
        //}

        //public static bool operator <(HLinkBase left, HLinkBase right)
        //{
        //    return left is null ? right is object : left.CompareTo(right) < 0;
        //}

        //public static bool operator <=(HLinkBase left, HLinkBase right)
        //{
        //    return left is null || left.CompareTo(right) <= 0;
        //}

        //public static bool operator ==(HLinkBase left, HLinkBase right)
        //{
        //    if (left is null)
        //    {
        //        return right is null;
        //    }

        //    return left.Equals(right);
        //}

        //public static bool operator >(HLinkBase left, HLinkBase right)
        //{
        //    return left is object && left.CompareTo(right) > 0;
        //}

        //public static bool operator >=(HLinkBase left, HLinkBase right)
        //{
        //    return left is null ? right is null : left.CompareTo(right) >= 0;
        //}

        ///// <summary>
        ///// Compares the specified x.
        ///// </summary>
        ///// <param name="x">
        ///// The x.
        ///// </param>
        ///// <param name="y">
        ///// The y.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //int IComparer.Compare(object x, object y)
        //{
        //    return Compare(x, y);
        //}

        ///// <summary>
        ///// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
        ///// </summary>
        ///// <param name="obj">
        ///// The object.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //public int CompareTo(object obj)
        //{
        //    if (obj == null)
        //    {
        //        return 1;
        //    }

        //    return string.Compare(HLinkKey.Value, (obj as HLinkBase).HLinkKey.Value, true, CultureInfo.CurrentCulture);
        //}
    }
}