namespace GrampsView.Common.CustomClasses
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using Newtonsoft.Json;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;
    using Xamarin.Forms;

    [DataContract]
    public class ItemGlyph : CommonBindableBase
    {
        public ItemGlyph()
        {
            UCNavigateCommand = new AsyncCommand(UCNavigate);
        }

        [DataMember]
        public HLinkKey ImageHLink
        {
            get; set;
        } = new HLinkKey();

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
        [DataMember]
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
        [DataMember]
        public Color ImageSymbolColour
        {
            get; set;
        } = Color.White;

        [DataMember]
        public CommonEnums.HLinkGlyphType ImageType
        {
            get; set;
        } = CommonEnums.HLinkGlyphType.Symbol;

        [DataMember]
        public HLinkKey MediaHLink
        {
            get; set;
        } = new HLinkKey();

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
        [DataMember]
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
        [DataMember]
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
                    case CommonEnums.HLinkGlyphType.Symbol:
                        {
                            return true;
                        }
                    case CommonEnums.HLinkGlyphType.Image:
                        {
                            return ImageHLinkMediaModel.Valid;
                        }
                    case CommonEnums.HLinkGlyphType.Media:
                        {
                            return MediaHLinkMediaModel.Valid;
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

        public bool ValidMedia
        {
            get
            {
                return (Valid && (ImageType == CommonEnums.HLinkGlyphType.Media));
            }
        }

        public async Task UCNavigate()
        {
            string ser;

            switch (ImageType)
            {
                case CommonEnums.HLinkGlyphType.Image:
                    {
                        ser = JsonConvert.SerializeObject(this.ImageHLinkMediaModel);

                        await CommonRoutines.NavigateAsync(string.Format("{0}?BaseParamsHLink={1}", "MediaDetailPage", ser));

                        break;
                    }
                case CommonEnums.HLinkGlyphType.Media:
                    {
                        ser = JsonConvert.SerializeObject(this.MediaHLinkMediaModel);

                        await CommonRoutines.NavigateAsync(string.Format("{0}?BaseParamsHLink={1}", "MediaDetailPage", ser));

                        break;
                    }
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
                DataStore.Instance.CN.NotifyError(new ErrorInfo(string.Format("{0} is null", argHLinkKey)));
                return new HLinkMediaModel();
            }

            return DataStore.Instance.DS.MediaData.Find(argHLinkKey.Value).HLink;
        }
    }
}