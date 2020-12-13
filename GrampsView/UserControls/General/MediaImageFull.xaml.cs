namespace GrampsView.UserControls
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;

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
            AdditionalInfoItems extraInfo = new AdditionalInfoItems();

            if (CurrentHLinkMediaModel.Valid)
            {
                extraInfo.Add("HLinkMediaModel HLinkKey", CurrentHLinkMediaModel.HLinkKey);
                extraInfo.Add("MediaModel Id", CurrentHLinkMediaModel.DeRef.Id);
            }

            DataStore.Instance.CN.NotifyException(argMessage: "Error exception in MediaImageFull.  Error is ", argException: e.Exception, argExtraItems: extraInfo);

            if (!(sender is null))
            {
                (sender as FFImageLoading.Forms.CachedImage).Cancel();
                (sender as FFImageLoading.Forms.CachedImage).Source = null;
            }
        }

        private void MediaImageFull_BindingContextChanged(object sender, EventArgs e)
        {
            Contract.Assert(sender != null);

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
    }
}