using GrampsView.Common.CustomClasses;
using GrampsView.Data.Model;

using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;


namespace GrampsView.Common
{
    /// <summary>
    /// Interfaces for ITemGlyph
    /// </summary>
    public interface IItemGlyph
    {
        HLinkKey ImageHLink
        {
            get; set;
        }

        HLinkMediaModel ImageHLinkMediaModel
        {
            get;
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>

        string ImageSymbol
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>

        Color ImageSymbolColour
        {
            get; set;
        }

        CommonEnums.HLinkGlyphType ImageType
        {
            get; set;
        }

        HLinkKey MediaHLink
        {
            get; set;
        }

        HLinkMediaModel MediaHLinkMediaModel
        {
            get;
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        string Symbol
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>

        Color SymbolColour
        {
            get; set;
        }

        IAsyncRelayCommand UCNavigateCommand
        {
            get;
        }

        bool Valid
        {
            get;
        }

        bool ValidImage { get; }

        bool ValidMedia { get; }

        Task UCNavigate();
    }
}