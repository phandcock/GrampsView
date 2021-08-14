namespace GrampsView.Data.Model
{
    using GrampsView.Common.CustomClasses;

    using System.Threading.Tasks;

    public interface IHLinkBase
    {
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
        HLinkKey HLinkKey
        {
            get;
            set;
        }

        bool Priv
        {
            get; set;
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