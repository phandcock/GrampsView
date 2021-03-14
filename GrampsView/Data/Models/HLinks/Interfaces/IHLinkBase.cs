namespace GrampsView.Data.Model
{
    using GrampsView.Common.CustomClasses;

    using System;
    using System.Collections;
    using System.Threading.Tasks;

    /// <summary>
    /// Public interfaces for the Event elements.
    /// </summary>
    public interface IHLinkBase : IComparer, IComparable
    {
        bool GPriv
        {
            get; set;
        }

        ItemGlyph HLinkGlyphItem
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        string HLinkKey
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the priv as string.
        /// </summary>
        /// <value>
        /// The priv as string.
        /// </value>
        string PrivAsString
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid.
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        bool Valid
        {
            get;
        }

        int GetHashCode();

        /// <summary>
        /// Sets the base fields.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        void SetBase(HLinkBase arg);

        Task UCNavigate();
    }
}