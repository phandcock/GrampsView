namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;

    using System.Runtime.Serialization;

    using Xamarin.Forms;

    [DataContract]
    public class ItemGlyph : CommonBindableBase
    {
        private string _HLinkMedia = string.Empty;
        private string _ImageSymbol = CommonConstants.IconDDefault;
        private Color _ImageSymbolColour = Color.White;
        private CommonEnums.HLinkGlyphType _ImageType = CommonEnums.HLinkGlyphType.Symbol;
        private string _Symbol = CommonConstants.IconDDefault;
        private Color _SymbolColour = Color.White;

        public ItemGlyph()
        {
        }

        public HLinkMediaModel HLinkMedia
        {
            get
            {
                return DataStore.Instance.DS.MediaData.Find(_HLinkMedia).HLink;
            }
        }

        [DataMember]
        public string HLinkMediHLink
        {
            get
            {
                return _HLinkMedia;
            }

            set
            {
                SetProperty(ref _HLinkMedia, value);
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
                            return HLinkMedia.Valid;
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
    }
}