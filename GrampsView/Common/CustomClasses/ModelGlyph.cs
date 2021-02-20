namespace GrampsView.Data.Model
{
    using GrampsView.Common;

    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// Cut down model that only has image code without the dependencies that muck things up by recursion.
    /// </summary>
    [DataContract]
    public class ModelGlyph : CommonBindableBase
    {
        private string _HLinkMedia = new HLinkMediaModel();
        private CommonEnums.ModelDisplayType _ImageType = CommonEnums.ModelDisplayType.Symbol;
        private string _Symbol = CommonConstants.IconDDefault;
        private Color _SymbolColour = Color.White;

        public ModelGlyph()
        {
        }

        [DataMember]
        public string HLinkMedia
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

        [DataMember]
        public CommonEnums.ModelDisplayType ImageType
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
                if (value != Color.Default)
                {
                }

                SetProperty(ref _SymbolColour, value);
            }
        }

        public bool Valid
        {
            get
            {
                switch (ImageType)
                {
                    case CommonEnums.ModelDisplayType.Symbol:
                        {
                            return true;
                        }
                    case CommonEnums.ModelDisplayType.Image:
                    case CommonEnums.ModelDisplayType.Media:
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
    }
}