namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using Microsoft.Extensions.DependencyInjection;

    using SharedSharpNu.Interfaces;

    using System;
    using System.Diagnostics.Contracts;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Essentials;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// Data model for a Map reference.
    /// <list type="table">
    /// <listheader>
    /// <term> Item </term>
    /// <term> Status </term>
    /// </listheader>
    /// <item>
    /// <description> XML 1.71 check </description>
    /// <description> NA </description>
    /// </item>
    /// </list>
    /// </summary>
    public class MapModel : ModelBase, IMapModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapModel"> MapModel </see> class.
        /// </summary>
        public MapModel()
        {
            OpenMapCommand = new AsyncCommand(OpenMap);

            ModelItemGlyph.Symbol = Constants.IconMap;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        public string Description
        {
            get;
            set;
        } = string.Empty;

        public HLinkMapModel HLink
        {
            get
            {
                HLinkMapModel t = new HLinkMapModel
                {
                    DeRef = this,
                    HLinkKey = HLinkKey,
                    HLinkGlyphItem = ModelItemGlyph,
                };

                return t;
            }
        }

        public MapType MapType
        {
            get;
            set;
        } = MapType.Unknown;

        public Location MyLocation
        {
            get;
            set;
        } = new Location();

        public Placemark MyPlaceMark
        {
            get;
            set;
        } = new Placemark();

        /// <summary>
        /// Gets or sets the Lat / Long GPS location..
        /// </summary>
        /// <value>
        /// The xamarin essentials location object.
        /// </value>
        /// <summary>
        /// Gets or sets the PlaceMark location description.
        /// </summary>
        /// <value>
        /// The xamarin essentials placemark.
        /// </value>
        public IAsyncCommand OpenMapCommand
        {
            get; private set;
        }

        /// <summary>
        /// Opens the Map.
        /// </summary>
        public async Task OpenMap()
        {
            switch (MapType)
            {
                case MapType.LatLong:
                    {
                        try
                        {
                            await Map.OpenAsync(MyLocation);
                        }
                        catch (Exception ex)
                        {
                            App.Current.Services.GetService<IErrorNotifications>().NotifyException("No map application available to open", ex);

                            throw;
                        }

                        break;
                    }

                case MapType.Place:
                    {
                        try
                        {
                            MapLaunchOptions mapOptions = new MapLaunchOptions
                            {
                                Name = ToString(),
                            };

                            await MyPlaceMark.OpenMapsAsync(mapOptions);
                        }
                        catch (Exception ex)
                        {
                            App.Current.Services.GetService<IErrorNotifications>().NotifyException("No map application available to open", ex);

                            throw;
                        }

                        break;
                    }

                default:
                    {
                        Contract.Assert(false, "Bad Map Type");
                        break;
                    }
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
            return Description;
        }
    }
}