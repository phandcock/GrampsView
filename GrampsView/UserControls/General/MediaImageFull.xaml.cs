namespace GrampsView.UserControls
{
    using GrampsView.Common.CustomClasses;
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
            ErrorInfo extraInfo = new ErrorInfo();

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

            // Hide if not valid
            if ((!(mifModel.BindingContext is HLinkMediaModel argHLinkMediaModel)) || (!argHLinkMediaModel.Valid))
            {
                mifModel.IsVisible = false;
                return;
            }

            if (argHLinkMediaModel == mifModel.CurrentHLinkMediaModel)
            {
                return;
            }

            IMediaModel t = argHLinkMediaModel.DeRef;

            if ((t.IsMediaStorageFileValid) && (t.IsImage))
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

            // Nothing to display so hide
            mifModel.IsVisible = false;
        }
    }
}