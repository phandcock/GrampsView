namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    /// <summary>
    /// Holds details on how to display the HLink or DataModel <note type="note"> Can be one of three
    /// states: <br/><br/> a) Symbol = Dsiplay only symbol for card <br/> b) Image = Display Symbol
    /// for card and Image, e.g. photo <br/> c) Media = Display Symbol for card and image with media
    /// if selected to play or display <br/></note>
    /// </summary>

    public class ItemGlyph : ObservableObject
    {
        public ItemGlyph()
        {
            UCNavigateCommand = new AsyncCommand(UCNavigate);
        }

        [JsonInclude]
        public HLinkKey ImageHLink
        {
            get; set;
        } = new HLinkKey();

        [JsonIgnore]
        public HLinkMediaModel ImageHLinkMediaModel
        {
            get
            {
                if (ImageHLink == null)
                {
                    DataStore.Instance.CN.NotifyError(new ErrorInfo("ImageHLinkMediaModel is null"));
                    return new HLinkMediaModel();
                }

                return GetHLinkMediaModelKey(ImageHLink);
            }
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>

        [JsonInclude]
        public string ImageSymbol
        {
            get; set;
        } = CommonConstants.IconDDefault;

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>

        [JsonInclude]
        public Color ImageSymbolColour
        {
            get; set;
        } = Color.White;

        [JsonInclude]
        public CommonEnums.HLinkGlyphType ImageType
        {
            get; set;
        } = CommonEnums.HLinkGlyphType.Symbol;

        [JsonInclude]
        public HLinkKey MediaHLink
        {
            get; set;
        } = new HLinkKey();

        [JsonIgnore]
        public HLinkMediaModel MediaHLinkMediaModel
        {
            get
            {
                if (MediaHLink == null)
                {
                    DataStore.Instance.CN.NotifyError(new ErrorInfo("MediaHLinkMediaModel is null"));
                    return new HLinkMediaModel();
                }

                return GetHLinkMediaModelKey(MediaHLink);
            }
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>

        [JsonInclude]
        public string Symbol
        {
            get; set;
        } = CommonConstants.IconDDefault;

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>

        [JsonInclude]
        public Color SymbolColour
        {
            get; set;
        } = Color.White;

        public IAsyncCommand UCNavigateCommand
        {
            get;
        }

        public bool Valid
        {
            get
            {
                switch (ImageType)
                {
                    case CommonEnums.HLinkGlyphType.Image:
                        {
                            return ImageHLinkMediaModel.Valid;
                        }
                    case CommonEnums.HLinkGlyphType.Media:
                        {
                            return ImageHLinkMediaModel.Valid && MediaHLinkMediaModel.Valid;
                        }
                    case CommonEnums.HLinkGlyphType.Symbol:
                        {
                            return true;
                        }
                    case CommonEnums.HLinkGlyphType.TempLoading:
                        {
                            // Used to force valid while data is loading. Only used for odd models
                            // and hlinks
                            return true;
                        }
                    default:
                        {
                            // TODO Unknown type
                            break;
                        }
                }

                return false;
            }
        }

        public bool ValidImage => Valid && (ImageType == CommonEnums.HLinkGlyphType.Image);

        public bool ValidMedia => Valid && (ImageType == CommonEnums.HLinkGlyphType.Media);

        public async Task UCNavigate()
        {
            string ser;

            switch (ImageType)
            {
                case CommonEnums.HLinkGlyphType.Image:
                    {
                        ser = JsonSerializer.Serialize(this.ImageHLinkMediaModel);

                        await CommonRoutines.NavigateAsync($"{"MediaDetailPage"}?BaseParamsHLink={ser}");

                        break;
                    }
                case CommonEnums.HLinkGlyphType.Media:
                    {
                        ser = JsonSerializer.Serialize(this.MediaHLinkMediaModel);

                        await CommonRoutines.NavigateAsync($"{"MediaDetailPage"}?BaseParamsHLink={ser}");

                        break;
                    }

                case CommonEnums.HLinkGlyphType.Symbol:
                    break;

                case CommonEnums.HLinkGlyphType.TempLoading:
                    break;

                case CommonEnums.HLinkGlyphType.Unknown:
                    break;

                default:
                    {
                        // TODO What to do for symbol if anything)
                        break;
                    }
            }

            return;
        }

        private HLinkMediaModel GetHLinkMediaModelKey(HLinkKey argHLinkKey)
        {
            if (MediaHLink == null)
            {
                DataStore.Instance.CN.NotifyError(new ErrorInfo($"{argHLinkKey} is null"));
                return new HLinkMediaModel();
            }

            return DataStore.Instance.DS.MediaData.Find(argHLinkKey.Value).HLink;
        }
    }
}