namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using SharedSharp.Interfaces;

    using System;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;

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
            OpenURLCommand = new AsyncCommand(OpenURL);

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
                HLinkURLModel t = new HLinkURLModel
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public Placemark MapLocation { get; set; } = new Placemark();

        public IAsyncCommand OpenURLCommand
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
                App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad URI for URL Model"));
                return;
            }

            if (GHRef.IsWellFormedOriginalString())
            {
                await Launcher.OpenAsync(GHRef);
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