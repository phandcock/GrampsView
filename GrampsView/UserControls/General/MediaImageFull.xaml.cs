namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Forms;

    public partial class MediaImageFull : Frame
    {
        private HLinkMediaModel CurrentHLinkMediaModel = new HLinkMediaModel();

        public MediaImageFull()
        {
            InitializeComponent();
        }

        private void DaImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.Instance.CN.NotifyError("Error in MediaImageFull.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }

        private void MediaImageFull_BindingContextChanged(object sender, EventArgs e)
        {
            MediaImageFull mifModel = (sender as MediaImageFull);

            HLinkMediaModel argHLinkMediaModel = mifModel.BindingContext as HLinkMediaModel;

            if (argHLinkMediaModel == mifModel.CurrentHLinkMediaModel)
            {
                return;
            }

            if (!(argHLinkMediaModel is null) && (argHLinkMediaModel.Valid))
            {
                IMediaModel t = argHLinkMediaModel.DeRef;

                if ((t.IsMediaStorageFileValid) && (t.IsMediaFile))
                {
                    try
                    {
                        mifModel.daImage.Source = t.MediaStorageFilePath;

                        mifModel.IsVisible = true;

                        CurrentHLinkMediaModel = argHLinkMediaModel;

                        return;
                    }
                    catch (Exception ex)
                    {
                        DataStore.Instance.CN.NotifyException("Exception in MediaImageFull control", ex);
                        throw;
                    }
                }
            }

            // Nothing to display so hide
            mifModel.IsVisible = false;
        }

        //private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        //{
        //    OpenFileRequest t = new OpenFileRequest(CurrentHLinkMediaModel.DeRef.GDescription, new ReadOnlyFile(CurrentHLinkMediaModel.DeRef.MediaStorageFilePath));
        //    Launcher.OpenAsync(t);
        //}
    }
}