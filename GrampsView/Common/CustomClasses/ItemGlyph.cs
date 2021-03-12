namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using Newtonsoft.Json;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    [DataContract]
    public class ItemGlyph : CommonBindableBase
    {
        private string _ImageHLinkMedia = string.Empty;
        private string _ImageSymbol = CommonConstants.IconDDefault;
        private Color _ImageSymbolColour = Color.White;
        private CommonEnums.HLinkGlyphType _ImageType = CommonEnums.HLinkGlyphType.Symbol;
        private string _MediaHLinkMedia = string.Empty;
        private string _Symbol = CommonConstants.IconDDefault;
        private Color _SymbolColour = Color.White;

        public ItemGlyph()
        {
            UCNavigateCommand = new AsyncCommand(() => UCNavigate());
        }

        public HLinkMediaModel ImageHLinkMedia
        {
            get
            {
                return DataStore.Instance.DS.MediaData.Find(_ImageHLinkMedia).HLink;
            }
        }

        [DataMember]
        public string ImageHLinkMediHLink
        {
            get
            {
                return _ImageHLinkMedia;
            }

            set
            {
                SetProperty(ref _ImageHLinkMedia, value);
            }
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        [DataMember]
        public string ImageSymbol
        {
            get
            {
                return _ImageSymbol;
            }

            set
            {
                SetProperty(ref _ImageSymbol, value);
            }
        }

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        [DataMember]
        public Color ImageSymbolColour
        {
            get
            {
                return _ImageSymbolColour;
            }

            set
            {
                SetProperty(ref _ImageSymbolColour, value);
            }
        }

        [DataMember]
        public CommonEnums.HLinkGlyphType ImageType
        {
            get
            {
                return _ImageType;
            }

            set
            {
                SetProperty(ref _ImageType, value);
            }
        }

        public HLinkMediaModel MediaHLinkMedia
        {
            get
            {
                return DataStore.Instance.DS.MediaData.Find(_MediaHLinkMedia).HLink;
            }
        }

        [DataMember]
        public string MediaHLinkMediHLink
        {
            get
            {
                return _MediaHLinkMedia;
            }

            set
            {
                SetProperty(ref _MediaHLinkMedia, value);
            }
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        [DataMember]
        public string Symbol
        {
            get
            {
                return _Symbol;
            }

            set
            {
                SetProperty(ref _Symbol, value);
            }
        }

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        [DataMember]
        public Color SymbolColour
        {
            get
            {
                return _SymbolColour;
            }

            set
            {
                SetProperty(ref _SymbolColour, value);
            }
        }

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
                    case CommonEnums.HLinkGlyphType.Symbol:
                        {
                            return true;
                        }
                    case CommonEnums.HLinkGlyphType.Image:
                        {
                            return ImageHLinkMedia.Valid;
                        }
                    case CommonEnums.HLinkGlyphType.Media:
                        {
                            return ImageHLinkMedia.Valid;
                        }
                    default:
                        {
                            // TODO Unknown type
                            return false;
                        }
                }
            }
        }

        public bool ValidImage
        {
            get
            {
                return (Valid && (ImageType == CommonEnums.HLinkGlyphType.Image));
            }
        }

        public async Task UCNavigate()
        {
            if ((this.ImageType == CommonEnums.HLinkGlyphType.Image) || (this.ImageType == CommonEnums.HLinkGlyphType.Media))
            {
                string ser = JsonConvert.SerializeObject(this.ImageHLinkMedia);

                await AppShell.Current.GoToAsync(string.Format("{0}?BaseParamsHLink={1}", "MediaDetailPage", ser));
            }

            return;
        }
    }
}