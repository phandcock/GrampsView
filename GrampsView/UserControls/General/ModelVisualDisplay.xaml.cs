namespace GrampsView.UserControls
{
    using FFImageLoading.Forms;

    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.CommunityToolkit.UI.Views;
    using Xamarin.Forms;

    public partial class ModelVisualDisplay : Grid
    {
        private MediaModel newMedia = new MediaModel();

        public ModelVisualDisplay()
        {
            InitializeComponent();
        }

        private MediaModel WorkMediaModel
        {
            get; set;
        }

        private void DaImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            ErrorInfo t = new ErrorInfo("Error in ModelVisualDisplay.")
            {
                { "Error is ", e.Exception.Message }
            };

            DataStore.Instance.CN.NotifyError(t);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }

        private void DaImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
        }

        private void ModelVisualDisplay_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                newMedia = new MediaModel();

                if (this.BindingContext is MediaModel)
                {
                    newMedia = (this.BindingContext as MediaModel);
                }

                if (!(newMedia.Valid))
                {
                    return;
                }

                this.ShowSomething(newMedia);
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("ModelVisualDisplay", ex);

                throw;
            }
        }

        private void ShowImage(IMediaModel argMediaModel)
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
                CachedImage newMediaControl = new CachedImage
                {
                    Source = argMediaModel.HLink.DeRef.MediaStorageFilePath,
                    Margin = 3,
                    Aspect = Aspect.AspectFit,
                    BackgroundColor = Color.Transparent,
                    CacheType = FFImageLoading.Cache.CacheType.All,
                    DownsampleToViewSize = true,
                    //Error = "DaImage_Error",
                    // HeightRequest = 100, // "{Binding MediaDetailImageHeight, Source={x:Static common:CardSizes.Current}, Mode=OneWay}"
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = true,
                    LoadingPlaceholder = "ic_launcher.png",
                    RetryCount = 3,
                    RetryDelay = 1000,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                this.ModelVisualDisplayRoot.Children.Clear();
                this.ModelVisualDisplayRoot.Children.Add(newMediaControl);
            }
            catch (Exception ex)
            {
                ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.MediaStorageFilePath },
                };

                DataStore.Instance.CN.NotifyException("ModelVisualDisplay", ex, argExtraItems: argDetail);
                throw;
            }
        }

        private void ShowMedia(IMediaModel argMediaModel)
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
                    BackgroundColor = Color.AliceBlue,
                    HeightRequest = 300,

                    // MediaFailed = "daMediaElement_MediaFailed"
                    ShowsPlaybackControls = true,
                    Source = argMediaModel.HLink.DeRef.MediaStorageFilePath,

                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                this.ModelVisualDisplayRoot.Children.Clear();
                this.ModelVisualDisplayRoot.Children.Add(newMediaControl);
            }
            catch (Exception ex)
            {
                ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.MediaStorageFilePath },
                };

                DataStore.Instance.CN.NotifyException("ModelVisualDisplay", ex, argExtraItems: argDetail);
                throw;
            }
        }

        private void ShowSomething(MediaModel argMediaModel)
        {
            try
            {
                if (!argMediaModel.Valid)
                {
                    //DataStore.Instance.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                    return;
                }

                if (argMediaModel == WorkMediaModel)
                {
                    return;
                }

                // Save the HLink so can check for duplicate changes later
                WorkMediaModel = argMediaModel;

                if (argMediaModel.Valid)
                {
                    switch (argMediaModel.MediaDisplayType)
                    {
                        case CommonEnums.HLinkGlyphType.Image:
                            {
                                ShowImage(argMediaModel);
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Media:
                            {
                                ShowMedia(argMediaModel);
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Symbol:
                            {
                                ShowSymbol(argMediaModel);
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
                DataStore.Instance.CN.NotifyException("ModelVisualDisplay", ex);

                throw;
            }
        }

        private void ShowSymbol(MediaModel argMediaModel)
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
                    Glyph = argMediaModel.ModelItemGlyph.Symbol,
                    Color = argMediaModel.ModelItemGlyph.SymbolColour,
                    FontFamily = "FA-Solid"
                };

                if (fontGlyph.Glyph == null)
                {
                    ErrorInfo t = new ErrorInfo("ModelVisualDisplay", "Null Glyph")
                        {
                            { "HLinkKey", argMediaModel.HLinkKey }
                        };

                    DataStore.Instance.CN.NotifyError(t);
                }

                if (fontGlyph.Color == null)
                {
                    ErrorInfo t = new ErrorInfo("ModelVisualDisplay", "Null Glyph Colour")
                        {
                            { "HLinkKey", argMediaModel.HLinkKey }
                        };

                    DataStore.Instance.CN.NotifyError(t);
                }

                newImageControl.Source = fontGlyph;

                this.ModelVisualDisplayRoot.Children.Clear();
                this.ModelVisualDisplayRoot.Children.Add(newImageControl);
            }
            catch (Exception ex)
            {
                ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Symbol" },
                    { "Media Model Hlink Key", argMediaModel.HLinkKey },
                    { "Media Model Symbol", argMediaModel.ModelItemGlyph.Symbol },
                };

                DataStore.Instance.CN.NotifyException("ModelVisualDisplay", ex, argExtraItems: argDetail);
                throw;
            }
        }
    }
}