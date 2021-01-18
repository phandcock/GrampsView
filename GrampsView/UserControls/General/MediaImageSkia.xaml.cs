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

            // Handle IsEnabled on the control and pass to the image Tap event
            this.daImage.IsEnabled = true;
            if (!this.IsEnabled)
            {
                this.daImage.IsEnabled = false;
            }
        }

        private HLinkHomeImageModel WorkHLMediaModel { get; set; }

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
            if (string.IsNullOrEmpty(argMediaModel.MediaStorageFilePath))
            {
                DataStore.Instance.CN.NotifyError("The media file path is null for Id:" + argMediaModel.Id);
                return;
            }
            // Input valid so start work
            daSymbol.IsVisible = false;
            daImage.IsVisible = true;

            //this.daImage.DownsampleToViewSize = true;

            this.daImage.BindingContext = argMediaModel.HLink;
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
            // Input valid so start work
            daSymbol.IsVisible = true;
            daImage.IsVisible = false;

            // Set symbol
            FontImageSource tt = this.daSymbol.Source as FontImageSource;
            tt.Glyph = argHLMediaModel.HomeSymbol;
            tt.Color = argHLMediaModel.HomeSymbolColour;

            if (tt.Glyph == null)
            {
                DataStore.Instance.CN.NotifyError("MediaImageSkia (" + argHLMediaModel.HLinkKey + ") Null Glyph");
            }

            if (tt.Color == null)
            {
                DataStore.Instance.CN.NotifyError("MediaImageSkia (" + argHLMediaModel.HLinkKey + ") Null Colour");
            }

            this.daSymbol.Source = tt;
        }
    }
}