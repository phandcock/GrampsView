// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;

using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;

namespace GrampsView.Models.HLinks
{
    /// <summary>
    /// Base functionality for HLinks<list type="table"><listheader><term> Item </term><term> Status </term></listheader><item><description> XML 1.71 check </description><description> Not Done </description></item></list><para><br /></para></summary>
    /// <remarks>TODO Update fields as per Schema</remarks>

    public class HLinkBase : ObservableObject, IComparable, IHLinkBase
    {
        public HLinkBase()
        {
            UCNavigateCommand = new AsyncRelayCommand(UCNavigate);
        }

        public CommonEnums.DisplayFormat DisplayAs { get; set; } = CommonEnums.DisplayFormat.Default;

        public ItemGlyph HLinkGlyphItem
        {
            get;

            set;
        } = new ItemGlyph();

        /// <summary>
        /// Gets or sets the hlink key used as the major record identifier.
        /// </summary>
        /// <value>
        /// The hlink key.
        /// </value>
        public HLinkKey HLinkKey
        {
            get;

            set;
        } = new HLinkKey();

        public bool Priv
        {
            get;

            set;
        } = false;

        public IAsyncRelayCommand UCNavigateCommand
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

        public int CompareTo(object? obj)
        {
            return ((IComparable)HLinkKey).CompareTo(obj);
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

        public virtual async Task UCNavigate()
        {
            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("UCNavigate", new NotImplementedException());

            throw new NotImplementedException();
        }

        public virtual Page NavigationPage()
        {
            Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("NavigationPage", new NotImplementedException());

            throw new NotImplementedException();
        }

        /// <summary>
        /// Compares the specified x. Bases it on the HLinkKey for want of anything else that makes sense.
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
            Contract.Assert(x is not null);
            Contract.Assert(y is not null);

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
    }
}