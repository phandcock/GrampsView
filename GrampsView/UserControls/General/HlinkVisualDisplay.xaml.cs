namespace GrampsView.UserControls
{
    using FFImageLoading.Forms;

    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Forms;

    public partial class HLinkVisualDisplay : Grid
    {
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

        private ItemGlyph WorkHLMediaModel
        {
            get; set;
        }

        private void HLinkVisualDisplay_BindingContextChanged(object sender, EventArgs e)
        {
            if (this.BindingContext == null)
            {
                return;
            }

            try
            {
                newItemGlyph = new ItemGlyph();

                if (this.BindingContext is IHLinkMediaModel)
                {
                    newItemGlyph = (this.BindingContext as IHLinkMediaModel).DeRef.ModelItemGlyph;
                }

                if (this.BindingContext is ItemGlyph)
                {
                    newItemGlyph = this.BindingContext as ItemGlyph;
                }

                if (!(newItemGlyph.Valid))
                {
                    return;
                }

                this.ShowSomething(newItemGlyph);
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
                {
                    "Error is ", e.Exception.Message},
            };

            t.Add("File", (sender as CachedImage).Source.ToString());

            DataStore.Instance.CN.NotifyError(t);

            (sender as CachedImage).Cancel();
            (sender as CachedImage).Source = null;
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

                    // HeightRequest = 100, // "{Binding MediaDetailImageHeight, Source={x:Static
                    // common:CardSizes.Current}, Mode=OneWay}"
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    IsVisible = true,
                    ErrorPlaceholder = "ic_launcher.png",
                    LoadingPlaceholder = "ic_launcher.png",
                    RetryCount = 3,
                    RetryDelay = 1000,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                newMediaControl.Error += NewMediaControl_Error;

                this.HLinkVisualDisplayRoot.Children.Clear();
                this.HLinkVisualDisplayRoot.Children.Add(newMediaControl);
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

        private void ShowSomething(ItemGlyph argItemGlyph)
        {
            try
            {
                if (!argItemGlyph.Valid)
                {
                    //DataStore.Instance.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                    return;
                }

                if (argItemGlyph == WorkHLMediaModel)
                {
                    return;
                }

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
                                ShowImage(argItemGlyph.HLinkMedia.DeRef);
                                break;
                            }

                        case CommonEnums.HLinkGlyphType.Symbol:
                            {
                                ShowSymbol(argItemGlyph);
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
                    Glyph = argItemGlyph.ImageSymbol,
                    Color = argItemGlyph.ImageSymbolColour,
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
                            { "HLinkKey", argItemGlyph.HLinkMediHLink }
                        };

                    DataStore.Instance.CN.NotifyError(t);
                }

                newImageControl.Source = fontGlyph;

                this.HLinkVisualDisplayRoot.Children.Clear();
                this.HLinkVisualDisplayRoot.Children.Add(newImageControl);
            }
            catch (Exception ex)
            {
                ErrorInfo argDetail = new ErrorInfo
                {
                    { "Type", "Symbol" },
                    { "Media Model Hlink Key", argItemGlyph.HLinkMediHLink },
                    { "Media Model Symbol", argItemGlyph.Symbol },
                };

                DataStore.Instance.CN.NotifyException("HLinkVisualDisplay", ex, argExtraItems: argDetail);
                throw;
            }
        }
    }
}