namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Forms;

    public partial class MediaImageSkia : Grid
    {
        private HLinkHomeImageModel newHLinkMedia = new HLinkHomeImageModel();

        public MediaImageSkia()
        {
            InitializeComponent();

            //// Handle IsEnabled on the control and pass to the image Tap event
            //this.daImage.IsEnabled = true;
            //if (!this.IsEnabled)
            //{
            //    this.daImage.IsEnabled = false;
            //}
        }

        private HLinkHomeImageModel WorkHLMediaModel
        {
            get; set;
        }

        private void DaImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.Instance.CN.NotifyError("Error in MediaImageSkia.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }

        private void DaImage_Finish(object sender, FFImageLoading.Forms.CachedImageEvents.FinishEventArgs e)
        {
        }

        private void MediaImageSkia_BindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                newHLinkMedia = new HLinkHomeImageModel();

                if (this.BindingContext is IHLinkMediaModel)
                {
                    newHLinkMedia = (this.BindingContext as IHLinkMediaModel).DeRef.HomeImageHLink;
                }

                if (this.BindingContext is HLinkHomeImageModel)
                {
                    newHLinkMedia = this.BindingContext as HLinkHomeImageModel;
                }

                if (!(newHLinkMedia.Valid))
                {
                    return;
                }

                this.ShowSomething(newHLinkMedia);
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("MediaImageSkia", ex);

                throw;
            }
        }

        private void ShowImage(IMediaModel argMediaModel)
        {
            try
            {
                if (string.IsNullOrEmpty(argMediaModel.MediaStorageFilePath))
                {
                    DataStore.Instance.CN.NotifyError("The media file path is null for Id:" + argMediaModel.Id);
                    return;
                }
                // Input valid so start work
                MediaImageFull newMediaControl = new MediaImageFull
                {
                    BindingContext = argMediaModel.HLink,
                    Margin = 3
                };

                this.MediaImageSkiaRoot.Children.Clear();
                this.MediaImageSkiaRoot.Children.Add(newMediaControl);
            }
            catch (Exception ex)
            {
                Common.AdditionalInfoItems argDetail = new Common.AdditionalInfoItems
                {
                    { "Type", "Image" },
                    { "Media Model Id", argMediaModel.Id },
                    { "Media Model Path", argMediaModel.MediaStorageFilePath },
                };

                DataStore.Instance.CN.NotifyException("MediaImageSkia", ex, argExtraItems: argDetail);
                throw;
            }
        }

        private void ShowSomething(HLinkHomeImageModel argHHomeMedia)
        {
            try
            {
                if (!argHHomeMedia.Valid)
                {
                    //DataStore.Instance.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
                    return;
                }

                if (argHHomeMedia == WorkHLMediaModel)
                {
                    return;
                }

                // Save the HLink so can check for duplicate changes later
                WorkHLMediaModel = argHHomeMedia;

                if (!argHHomeMedia.Valid || !argHHomeMedia.LinkToImage)
                {
                    ShowSymbol(argHHomeMedia);
                    return;
                }
                else
                {
                    ShowImage(argHHomeMedia.DeRef);
                }
            }
            catch (Exception ex)
            {
                DataStore.Instance.CN.NotifyException("MediaImageSkia", ex);

                throw;
            }
        }

        private void ShowSymbol(HLinkHomeImageModel argHLMediaModel)
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
                    Glyph = argHLMediaModel.HomeSymbol,
                    Color = argHLMediaModel.HomeSymbolColour,
                    FontFamily = "FA-Solid"
                };

                if (fontGlyph.Glyph == null)
                {
                    DataStore.Instance.CN.NotifyError("MediaImageSkia (" + argHLMediaModel.HLinkKey + ") Null Glyph");
                }

                if (fontGlyph.Color == null)
                {
                    DataStore.Instance.CN.NotifyError("MediaImageSkia (" + argHLMediaModel.HLinkKey + ") Null Colour");
                }

                newImageControl.Source = fontGlyph;

                this.MediaImageSkiaRoot.Children.Clear();
                this.MediaImageSkiaRoot.Children.Add(newImageControl);
            }
            catch (Exception ex)
            {
                Common.AdditionalInfoItems argDetail = new Common.AdditionalInfoItems
                {
                    { "Type", "Symbol" },
                    { "Media Model Hlink Key", argHLMediaModel.HLinkKey },
                    { "Media Model Symbol", argHLMediaModel.HomeSymbol },
                };

                DataStore.Instance.CN.NotifyException("MediaImageSkia", ex, argExtraItems: argDetail);
                throw;
            }
        }
    }
}