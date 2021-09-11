namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.Runtime.Serialization;
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

            ModelItemGlyph.Symbol = CommonConstants.IconMap;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        [DataMember]
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

        [DataMember]
        public MapType MapType
        {
            get;
            set;
        } = MapType.Unknown;

        /// <summary>
        /// Gets or sets the Lat / Long GPS location..
        /// </summary>
        /// <value>
        /// The xamarin essentials location object.
        /// </value>
        [DataMember]
        public Location MyLocation
        {
            get;
            set;
        } = new Location();

        /// <summary>
        /// Gets or sets the PlaceMark location description.
        /// </summary>
        /// <value>
        /// The xamarin essentials placemark.
        /// </value>
        [DataMember]
        public Placemark MyPlaceMark
        {
            get;
            set;
        } = new Placemark();

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
                            DataStore.Instance.CN.NotifyException("No map application available to open", ex);

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
                            DataStore.Instance.CN.NotifyException("No map application available to open", ex);

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