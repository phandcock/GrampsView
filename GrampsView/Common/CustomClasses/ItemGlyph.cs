// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Views;

using Microsoft.EntityFrameworkCore;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace GrampsView.Common.CustomClasses
{
    /// <summary>
    /// Holds details on how to display the HLink or DataModel <note type="note"> Can be one of three
    /// states: <br/><br/> a) Symbol = Dsiplay only symbol for card <br/> b) Image = Display Symbol
    /// for card and Image, e.g. photo <br/> c) Media = Display Symbol for card and image with media
    /// if selected to play or display <br/></note>
    /// </summary>
    /// 
    [PrimaryKey(nameof(Id))]
    public class ItemGlyph : ObservableObject, IItemGlyph
    {
        public ItemGlyph()
        {
            UCNavigateCommand = new AsyncRelayCommand(UCNavigate);
            Id = ImageHLink.Value;
        }

        public string Id { get; set; }

        private HLinkKey _ImageHLink = new HLinkKey();

        [JsonInclude]
        public HLinkKey ImageHLink
        {
            get
            {
                return _ImageHLink;
            }

            set
            {
                _ImageHLink = value;
                Id = _ImageHLink.Value;
            }
        }

        [JsonIgnore]
        public HLinkMediaModel ImageHLinkMediaModel
        {
            get
            {
                if (ImageHLink == null)
                {
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("ImageHLinkMediaModel is null"));
                    return new HLinkMediaModel();
                }

                return GetHLinkMediaModelKey(ImageHLink);
            }
        }

        [JsonIgnore]
        public string ImageSymbol
        {
            get; set;
        } = Constants.IconDDefault;

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        /// <remarks>
        /// This is set when the model is created.
        /// </remarks>
        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        /// <remarks>
        /// This is set when the model is created.
        /// </remarks>
        [JsonIgnore]
        public Color ImageSymbolColour
        {
            get; set;
        } = Colors.White;

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
                    Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("MediaHLinkMediaModel is null"));
                    return new HLinkMediaModel();
                }

                return GetHLinkMediaModelKey(MediaHLink);
            }
        }

        [JsonInclude]
        public string Symbol
        {
            get; set;
        } = Constants.IconDDefault;

        [JsonInclude]
        public Color SymbolColour
        {
            get; set;
        } = Colors.White;

        /// <summary>
        /// Gets or sets the home symbol font glyph.
        /// </summary>
        /// <value>
        /// The home symbol.
        /// </value>
        /// <summary>
        /// Gets or sets the Home Symbol background colour.
        /// </summary>
        /// <value>
        /// The background colour.
        /// </value>
        public IAsyncRelayCommand UCNavigateCommand
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
                        Ioc.Default.GetRequiredService<IMessenger>().Send(new MediaDetailPage(ImageHLinkMediaModel));

                        break;
                    }
                case CommonEnums.HLinkGlyphType.Media:
                    {
                        ser = JsonSerializer.Serialize(MediaHLinkMediaModel);

                        Ioc.Default.GetRequiredService<IMessenger>().Send(new MediaDetailPage(MediaHLinkMediaModel));

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
                        // TODO What to do for symbol if anything
                        break;
                    }
            }

            return;
        }

        private HLinkMediaModel GetHLinkMediaModelKey(HLinkKey argHLinkKey)
        {
            if (MediaHLink == null)
            {
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo($"{argHLinkKey} is null"));
                return new HLinkMediaModel();
            }

            return DataStore.Instance.DS.MediaData.Find(argHLinkKey.Value).HLink;
        }
    }
}