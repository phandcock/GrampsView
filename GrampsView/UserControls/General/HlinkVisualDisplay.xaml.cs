namespace GrampsView.UserControls
{
    using FFImageLoading.Forms;

    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    public partial class HLinkVisualDisplay : Grid
    {
        public static readonly BindableProperty FsctShowMediaProperty
      = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(HLinkVisualDisplay), propertyName: nameof(FsctShowMedia), defaultValue: false);

        public static readonly BindableProperty FsctShowSymbolsProperty
          = BindableProperty.Create(returnType: typeof(bool), declaringType: typeof(HLinkVisualDisplay), propertyName: nameof(FsctShowSymbols), defaultValue: true);

        private ItemGlyph newItemGlyph = new ItemGlyph();

        public HLinkVisualDisplay()
        {
            InitializeComponent();

            //// Handle IsEnabled on the control and pass to the image Tap event
            //this.daImage.IsEnabled = true;
            //if (!this.IsEnabled)
            //{
            //    this.daImage.IsEnabled = false;
            //}
        }

        public bool FsctShowMedia
        {
            get
            {
                return (bool)GetValue(FsctShowMediaProperty);
            }
            set
            {
                SetValue(FsctShowMediaProperty, value);
            }
        }

        public bool FsctShowSymbols
        {
            get
            {
                return (bool)GetValue(FsctShowSymbolsProperty);
            }
            set
            {
                SetValue(FsctShowSymbolsProperty, value);
            }
        }

        private ItemGlyph WorkHLMediaModel
        {
            get; set;
        }

        private void HLinkVisualDisplay_BindingContextChanged(object sender, EventArgs e)
        {
            if (BindingContext == null)
            {
                return;
            }

            try
            {
                newItemGlyph = new ItemGlyph();

                switch (BindingContext.GetType().Name)
                {
                    case nameof(IHLinkMediaModel):
                        {
                            if ((BindingContext as IHLinkMediaModel).Valid)
                            {
                                newItemGlyph = (BindingContext as IHLinkMediaModel).DeRef.ModelItemGlyph;
                            }
                            break;
                        }

                    case nameof(HLinkMediaModel):
                        {
                            if ((BindingContext as IHLinkMediaModel).Valid)
                            {
                                newItemGlyph = (BindingContext as IHLinkMediaModel).DeRef.ModelItemGlyph;
                            }
                            break;
                        }

                    case nameof(ItemGlyph):
                        {
                            newItemGlyph = BindingContext as ItemGlyph;
                            break;
                        }

                    default:
                        {
                            DataStore.Instance.CN.NotifyError(new ErrorInfo("HLinkVisualDisplay Binding Context is not a ItemGlyph but a" + BindingContext.GetType().ToString()));
                            return;
                        }
                }

                // Don't display anything if the glyph is invalid
                if (!newItemGlyph.Valid)
                {
                    return;
                }

                if (newItemGlyph.ImageHLink.Value == "_c4c5aaa038602727de3~zipimage")
                {
                }

                ShowSomething(newItemGlyph);
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("HLinkVisualDisplay", ex);

                throw;
            }
        }

        private void NewMediaControl_Error(object sender, CachedImageEvents.ErrorEventArgs e)
        {
            ErrorInfo t = new ErrorInfo("Error in HLinkVisualDisplay.")
                    {
                        { "Error is ", e.Exception.Message },
                    };

            // Component not found exception
            if (e.Exception.HResult == -2003292336)
            {
                t.Add("Ideas", "Showing bad file, perhaps an internalmediafile or the file type can not be displayed?");
            }

            t.Add("File", (sender as CachedImage).Source.ToString());

            DataStore.Instance.CN.NotifyError(t);

            (sender as CachedImage).Cancel();
            (sender as CachedImage).Source = null;
        }

        private void ShowImage(IMediaModel argMediaModel)
        {
            if (argMediaModel.IsMediaStorageFileValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(argMediaModel.MediaStorageFilePath))
                    {
                        ErrorInfo t = new ErrorInfo("The image file path is null")
                        {
                            { "Id", argMediaModel.Id }
                        };

                        DataStore.Instance.CN.NotifyError(t);
                        return;
                    }
                    // Input valid so start work
                    CachedImage newMediaControl = new CachedImage
                    {
                        Source = argMediaModel.HLink.DeRef.MediaStorageFilePath,
                        Margin = 3,
                        Aspect = Aspect.AspectFit,
                        BackgroundColor = Color.Transparent,
                        CacheType = FFImageLoading.Cache.CacheType.All,
                        DownsampleToViewSize = true,

                        // HeightRequest = 100, // "{Binding MediaDetailImageHeight,
                        // Source={x:Static common:CardSizes.Current}, Mode=OneWay}"
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        IsVisible = true,
                        ErrorPlaceholder = "ic_launcher.png",
                        LoadingPlaceholder = "ic_launcher.png",
                        RetryCount = 3,
                        RetryDelay = 1000
                    };

                    newMediaControl.Error += NewMediaControl_Error;

                    HLinkVisualDisplayRoot.Children.Clear();
                    HLinkVisualDisplayRoot.Children.Add(newMediaControl);
                }
                catch (Exception ex)
                {
                    ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.MediaStorageFilePath },
                };

                    DataStore.Instance.CN.NotifyException("HLinkVisualDisplay", ex, argExtraItems: argDetail);
                    throw;
                }
            }
        }

        private void ShowMedia(IMediaModel argMediaModel)
        {
            if (argMediaModel.IsMediaStorageFileValid)
            {
                try
                {
                    if (string.IsNullOrEmpty(argMediaModel.MediaStorageFilePath))
                    {
                        ErrorInfo t = new ErrorInfo("The media file path is null")
                        {
                            { "Id", argMediaModel.Id }
                        };

                        DataStore.Instance.CN.NotifyError(t);
                        return;
                    }
                    // Input valid so start work
                    MediaElement newMediaControl = new MediaElement
                    {
                        Aspect = Aspect.AspectFit,
                        AutoPlay = false,
                        ShowsPlaybackControls = true,
                        BackgroundColor = Color.Transparent,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        IsVisible = true,
                        Margin = 3,
                        Source = argMediaModel.HLink.DeRef.MediaStorageFilePath,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                    };

                    HLinkVisualDisplayRoot.Children.Clear();
                    HLinkVisualDisplayRoot.Children.Add(newMediaControl);
                }
                catch (Exception ex)
                {
                    ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.MediaStorageFilePath },
                };

                    DataStore.Instance.CN.NotifyException("HLinkVisualDisplay", ex, argExtraItems: argDetail);
                    throw;
                }
            }
        }

        private void ShowSomething(ItemGlyph argItemGlyph)
        {
            try
            {
                if (!argItemGlyph.Valid)
                {
                    //DataStore.Instance.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                    return;
                }

                //if (argItemGlyph == WorkHLMediaModel)
                //{
                //    return;
                //}

                // Save the HLink so can check for duplicate changes later
                WorkHLMediaModel = argItemGlyph;

                //if (argHHomeMedia.HLinkKey == "_e1823d5e765308999f4c16addb5")
                //{
                //}

                if (argItemGlyph.Valid)
                {
                    switch (argItemGlyph.ImageType)
                    {
                        case CommonEnums.HLinkGlyphType.Image:
                            {
                                ShowImage(argItemGlyph.ImageHLinkMediaModel.DeRef);
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Media:
                            {
                                if (FsctShowMedia)
                                {
                                    ShowMedia(argItemGlyph.MediaHLinkMediaModel.DeRef);
                                }
                                else
                                {
                                    ShowImage(argItemGlyph.ImageHLinkMediaModel.DeRef);
                                }
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Symbol:
                            {
                                if (FsctShowSymbols)
                                {
                                    ShowSymbol(argItemGlyph);
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("HLinkVisualDisplay", ex);

                throw;
            }
        }

        private void ShowSymbol(ItemGlyph argItemGlyph)
        {
            try
            {
                // create symbol control

                Image newImageControl = new Image
                {
                    Aspect = Aspect.AspectFit,
                    BackgroundColor = Color.Transparent,
                    IsVisible = true,
                    Margin = 5,
                    Source = new FontImageSource
                    {
                    },
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                // Input valid so start work

                // Set symbol
                FontImageSource fontGlyph = new FontImageSource
                {
                    Glyph = argItemGlyph.Symbol,
                    Color = argItemGlyph.SymbolColour,
                    FontFamily = "FA-Solid"
                };

                if (fontGlyph.Glyph == null)
                {
                    ErrorInfo t = new ErrorInfo("HLinkVisualDisplay", "Null Glyph")
                        {
                            { "HLinkKey", argItemGlyph.ToString() }
                        };

                    DataStore.Instance.CN.NotifyError(t);
                }

                if (fontGlyph.Color == null)
                {
                    ErrorInfo t = new ErrorInfo("HLinkVisualDisplay", "Null Glyph Colour")
                        {
                            { "HLinkKey", argItemGlyph.ImageHLink.Value }
                        };

                    DataStore.Instance.CN.NotifyError(t);
                }

                newImageControl.Source = fontGlyph;

                HLinkVisualDisplayRoot.Children.Clear();
                HLinkVisualDisplayRoot.Children.Add(newImageControl);
            }
            catch (Exception ex)
            {
                ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Symbol" },
                    { "Media Model HLinkKey", argItemGlyph.ImageHLink.Value  },
                    { "Media Model Symbol", argItemGlyph.Symbol },
                };

                DataStore.Instance.CN.NotifyException("HLinkVisualDisplay", ex, argExtraItems: argDetail);
                throw;
            }
        }
    }
}