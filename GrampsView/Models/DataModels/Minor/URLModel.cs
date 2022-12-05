using GrampsView.Common;
using GrampsView.Data.Model;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;


namespace GrampsView.Models.DataModels.Minor
{
    /// <summary>
    /// GRAMPS URL element class. TODO Needs XML 1.71 check
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
    public class URLModel : ModelBase, IURLModel
    {
        //// "url-content"
        //// "priv"
        //// "type"
        //// "href"
        //// "description"

        /// <summary>
        /// Initializes a new instance of the <see cref="URLModel"/> class.
        /// </summary>
        public URLModel()
        {
            OpenURLCommand = new AsyncRelayCommand(OpenURL);

            ModelItemGlyph.Symbol = Constants.IconURL;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        public string GDescription
        {
            get;
            set;
        }

        public Uri GHRef
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The g description.
        /// </value>
        /// <summary>
        /// Gets or sets the hlink reference.
        /// </summary>
        /// <value>
        /// The gh reference.
        /// </value>
        public string GType
        {
            get;
            set;
        }

        public HLinkURLModel HLink
        {
            get
            {
                HLinkURLModel t = new()
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public Placemark MapLocation { get; set; } = new Placemark();

        public IAsyncRelayCommand OpenURLCommand
        {
            get; private set;
        }

        /// <summary>
        /// Opens the URL.
        /// </summary>
        public async Task OpenURL()
        {
            if (GHRef is null)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad URI for URL Model"));
                return;
            }

            if (GHRef.IsWellFormedOriginalString())
            {
                _ = await Launcher.OpenAsync(GHRef);
            }
        }

        /// <summary>
        /// Gets the default text.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string ToString()
        {
            string returnVal = string.Empty;

            if (!string.IsNullOrEmpty(GType))
            {
                returnVal = GType + ":";
            }

            return returnVal + GDescription;
        }
    }
}