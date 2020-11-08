// TODO Needs XML 1.71 check

// TODO fix Deref caching

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System;
    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// Cut down model that only has image code without the dependencies that muck things up by recursion.
    /// </summary>
    [DataContract]
    public class HLinkHomeImageModel : HLinkBase, IHLinkHomeImageModel
    {
        private CommonEnums.HomeImageType _HomeImageType = CommonEnums.HomeImageType.Symbol;

        private Color _HomeSymbolColour = Color.White;

        private string _IDefaultSymbol = CommonConstants.IconDDefault;

        /// <summary>
        /// Initializes a new instance of the <see cref="HLinkHomeImageModel"/> class.
        /// </summary>
        public HLinkHomeImageModel()
        {
        }

        public HLinkMediaModel ConvertToHLinkMediaModel
        {
            get
            {
                HLinkMediaModel HLMediaModel = new HLinkMediaModel()
                {
                    // Copy fields
                    HLinkKey = HLinkKey,
                };

                return HLMediaModel;
            }
        }

        /// <summary>
        /// Gets the associated media model
        /// </summary>
        /// <value>
        /// The media model.
        /// </value>
        public IMediaModel DeRef
        {
            get
            {
                if ((Valid) && !string.IsNullOrEmpty(HLinkKey))
                {
                    return DV.MediaDV.GetModelFromHLinkString(HLinkKey);
                }
                else
                {
                    return new MediaModel();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [home use image].
        /// </summary>
        /// <value>
        /// <c>true</c> if [home use image]; otherwise, <c>false</c>.
        /// </value>
        [DataMember]
        public CommonEnums.HomeImageType HomeImageType
        {
            get
            {
                return _HomeImageType;
            }

            set
            {
                SetProperty(ref _HomeImageType, value);
            }
        }

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        [DataMember]
        public string HomeSymbol
        {
            get
            {
                return _IDefaultSymbol;
            }

            set
            {
                SetProperty(ref _IDefaultSymbol, value);
            }
        }

        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        [DataMember]
        public Color HomeSymbolColour
        {
            get
            {
                return _HomeSymbolColour;
            }

            set
            {
                if (value != Color.Default)
                {
                }

                SetProperty(ref _HomeSymbolColour, value);
            }
        }

        public bool IsImageType
        {
            get
            {
                return (Valid) && (_HomeImageType == CommonEnums.HomeImageType.ThumbNail);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this is for an image or a symbol.
        /// </summary>
        /// <value>
        /// <c>true</c> if image; otherwise, <c>false</c>.
        /// </value>
        public bool LinkToImage
        {
            get
            {
                if (HomeImageType == CommonEnums.HomeImageType.ThumbNail)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid. <note
        /// type="note">Can have a HLink or be a pointer to an image. <br/><br/> So, MUST be valid
        /// for both types and MUST be invalid for a default new instance. <br/></note>
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public override bool Valid
        {
            get
            {
                switch (HomeImageType)
                {
                    case CommonEnums.HomeImageType.Symbol:
                        {
                            return true;
                        }
                    case CommonEnums.HomeImageType.ThumbNail:
                        {
                            return !string.IsNullOrEmpty(HLinkKey);
                        }
                    case CommonEnums.HomeImageType.Unknown:
                        {
                            return false;
                        }

                    default:
                        {
                            // TODO Unknown type
                            return false;
                        }
                }
            }
        }

        /// <summary>
        /// Converts from media model.
        /// </summary>
        /// <param name="argMediaModel">
        /// The argument media model.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// argMediaModel
        /// </exception>
        public void ConvertFromMediaModel(IMediaModel argMediaModel)
        {
            if (argMediaModel is null)
            {
                throw new ArgumentNullException(nameof(argMediaModel));
            }

            // Copy fields
            GPriv = argMediaModel.Priv;
            HLinkKey = argMediaModel.HLinkKey;
            HomeImageType = CommonEnums.HomeImageType.ThumbNail;
        }
    }
}