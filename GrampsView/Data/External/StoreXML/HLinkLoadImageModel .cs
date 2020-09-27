//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkMediaModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

//// gramps XML 1.71 - Done
////
//// HLink
//// Priv
//// region
//// attribute
//// citationref
//// noteref

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System.Runtime.Serialization;

    using Xamarin.Forms;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    ///
    /// Cut down model that only has image code without the dependencies that muck things up by recursion.
    /// </summary>

    public class HLinkLoadImageModel : HLinkBase
    {
        /// <summary>
        /// The local home use image.
        /// </summary>
        private CommonEnums.HomeImageType _HomeImageType = CommonEnums.HomeImageType.Unknown;

        private Color _HomeSymbolColour = Color.White;

        ///// <summary>
        ///// The local internal default character icon
        ///// </summary
        private string _IDefaultSymbol = CommonConstants.IconDDefault;

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
        public IMediaModel DeRef
        {
            get
            {
                if (Valid)
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
        /// Gets or sets the g corner1 x.
        /// </summary>
        /// <value>
        /// The g corner1 x.
        /// </value>

        public int GCorner1X { get; set; }

        /// <summary>
        /// Gets or sets the g corner1 y.
        /// </summary>
        /// <value>
        /// The g corner1 y.
        /// </value>

        public int GCorner1Y { get; set; }

        /// <summary>
        /// Gets or sets the g corner2 x.
        /// </summary>
        /// <value>
        /// The g corner2 x.
        /// </value>

        public int GCorner2X { get; set; }

        /// <summary>
        /// Gets or sets the g corner2 y.
        /// </summary>
        /// <value>
        /// The g corner2 y.
        /// </value>

        public int GCorner2Y { get; set; }

        /// <summary>
        /// Gets the home image display bit map.
        /// </summary>
        /// <value>
        /// The home image display bit map.
        /// </value>
        public Image HomeImageDisplayBitMap
        {
            get
            {
                switch (_HomeImageType)
                {
                    //case CommonConstants.HomeImageTypeClippedBitmap:
                    //    {
                    //        return HomeImageClippedBitmap;
                    //    }

                    case CommonEnums.HomeImageType.ThumbNail:
                        {
                            // TODO FIx this
                            //return DeRef.ImageThumbNail;
                            return null;
                        }

                    default:
                        {
                            return null;
                        }
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

        // TODO Change to use GV static styles
        /// <summary>
        /// Gets or sets the home symbol.
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
        /// Gets or sets the background colour.
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

        // Gramps uses (0,0,0,0) or (0,0,100,100) for the entire bitmap.
        public bool NeedsClipping
        {
            get
            {
                if ((GCorner1X == 0) && (GCorner1Y == 0) && (GCorner2X == 0) && (GCorner2Y == 0))
                {
                    return false;
                }

                if ((GCorner1X == 0) && (GCorner1Y == 0) && (GCorner2X == 100) && (GCorner2Y == 100))
                {
                    return false;
                }

                return true;
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

        public HLinkHomeImageModel GetHLinkHomeImageModel()
        {
            HLinkHomeImageModel returnHLinkHomeImageModel = new HLinkHomeImageModel
            {
                // Copy fields
                GPriv = GPriv,
                HLinkKey = HLinkKey,
                //HomeImageClippedBitmap = argHLinkMediaModel.LoadingClipInfo.HomeImageClippedBitmap;
                HomeImageType = HomeImageType,
                HomeSymbol = HomeSymbol,
                HomeSymbolColour = HomeSymbolColour
            };

            return returnHLinkHomeImageModel;
        }
    }
}