//// gramps XML 1.71 - Done
////
//// HLink
//// Priv
//// region
//// attribute
//// citationref
//// noteref

namespace GrampsView.Common.CustomClasses
{
    public class HLinkLoadImageModel : HLinkBase
    {
        ///// <summary>
        ///// The local internal default character icon
        ///// </summary
        private string _IDefaultSymbol = GrampsView.Common.Constants.IconDDefault;

        /// <summary>
        /// The local home use image.
        /// </summary>
        private CommonEnums.HLinkGlyphType _ImageType = CommonEnums.HLinkGlyphType.Symbol;

        private Color _SymbolColour = Microsoft.Maui.Graphics.Colors.White;

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkMediaModel"/> class.
        /// </summary>
        public HLinkLoadImageModel()
        {
        }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model.
        /// </value>
        public IMediaModel DeRef => Valid ? (IMediaModel)DV.MediaDV.GetModelFromHLinkKey(HLinkKey) : new MediaModel();

        public int GCorner1X
        {
            get; set;
        }

        public int GCorner1Y
        {
            get; set;
        }

        public int GCorner2X
        {
            get; set;
        }

        public int GCorner2Y
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the g corner1 x.
        /// </summary>
        /// <value>
        /// The g corner1 x.
        /// </value>
        /// <summary>
        /// Gets or sets the g corner1 y.
        /// </summary>
        /// <value>
        /// The g corner1 y.
        /// </value>
        /// <summary>
        /// Gets or sets the g corner2 x.
        /// </summary>
        /// <value>
        /// The g corner2 x.
        /// </value>
        /// <summary>
        /// Gets or sets the g corner2 y.
        /// </summary>
        /// <value>
        /// The g corner2 y.
        /// </value>
        /// <summary>
        /// Gets or sets a value indicating whether [home use image].
        /// </summary>
        /// <value>
        /// <c> true </c> if [home use image]; otherwise, <c> false </c>.
        /// </value>
        [DataMember]
        public CommonEnums.HLinkGlyphType ImageType
        {
            get => _ImageType;

            set => SetProperty(ref _ImageType, value);
        }

        // TODO Change to use GV static styles
        /// <summary>
        /// Gets or sets the home symbol.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        [DataMember]
        public string Symbol
        {
            get => _IDefaultSymbol;

            set => SetProperty(ref _IDefaultSymbol, value);
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        [DataMember]
        public Color SymbolColour
        {
            get => _SymbolColour;

            set
            {
                if (value != Microsoft.Maui.Graphics.Colors.DarkSlateGrey)
                {
                }

                _ = SetProperty(ref _SymbolColour, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note"> Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
        /// for both types and MUST be invalid for a default new instance. <br/></note>
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public override bool Valid
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
                            return HLinkKey.Valid;
                        }
                    default:
                        {
                            // TODO Unknown type
                            return false;
                        }
                }
            }
        }

        protected override IModelBase GetDeRef()
        {
            return DeRef;
        }
    }
}