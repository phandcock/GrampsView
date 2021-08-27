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

    /// <summary> GRAMPS Map element class.

    /// <para> <br/> </para> </summary>
    public class MapModel : ModelBase, IMapModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="URLModel"/> class.
        /// </summary>
        public MapModel()
        {
            OpenMapCommand = new AsyncCommand(OpenMap);

            ModelItemGlyph.Symbol = CommonConstants.IconMap;
            ModelItemGlyph.SymbolColour = CommonRoutines.ResourceColourGet("CardBackGroundUtility");
        }

        /// <summary>
        /// Gets the default text.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        public override string DefaultText
        {
            get
            {
                return Description;
            }
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
        public Location myLocation
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
        public Placemark myPlaceMark
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
                            await Map.OpenAsync(myLocation);
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
                                Name = DefaultText,
                            };

                            await myPlaceMark.OpenMapsAsync(mapOptions);
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
    }
}