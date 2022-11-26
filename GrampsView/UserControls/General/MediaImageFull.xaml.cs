using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Interfaces;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;

namespace GrampsView.UserControls
{
    public partial class MediaImageFull : Border
    {
        private HLinkMediaModel CurrentHLinkMediaModel = new();

        public MediaImageFull()
        {
            InitializeComponent();
        }

        // TODO
        //private void DaImage_Error(object sender, CachedImageEvents.ErrorEventArgs e)
        //{
        //    ErrorInfo extraInfo = new();

        //    if (CurrentHLinkMediaModel.Valid)
        //    {
        //        extraInfo.Add("HLinkMediaModel HLinkKey", CurrentHLinkMediaModel.HLinkKey.Value);
        //        extraInfo.Add("MediaModel Id", CurrentHLinkMediaModel.DeRef.Id);
        //    }

        //    Ioc.Default.GetService<IErrorNotifications>().NotifyException(argMessage: "Error exception in MediaImageFull.  Error is ", argException: e.Exception, argExtraItems: extraInfo);

        //    if (sender is not null)
        //    {
        //        // TODO
        //        //(sender as FFImageLoading.Forms.CachedImage).Cancel();
        //        //(sender as FFImageLoading.Forms.CachedImage).Source = null;
        //    }
        //}

        private void MediaImageFull_BindingContextChanged(object sender, EventArgs e)
        {
            Contract.Assert(sender != null);

            MediaImageFull? mifModel = sender as MediaImageFull;

            // Hide if not valid
            if ((mifModel.BindingContext is not HLinkMediaModel argHLinkMediaModel) || (!argHLinkMediaModel.Valid))
            {
                mifModel.IsVisible = false;
                return;
            }

            if (argHLinkMediaModel == mifModel.CurrentHLinkMediaModel)
            {
                return;
            }

            IMediaModel t = argHLinkMediaModel.DeRef;

            if (t.IsMediaStorageFileValid && t.IsImage)
            {
                try
                {
                    // TODO   mifModel.daImage.Source = t.MediaStorageFilePath;

                    mifModel.IsVisible = true;

                    CurrentHLinkMediaModel = argHLinkMediaModel;

                    return;
                }
                catch (Exception ex)
                {
                    Ioc.Default.GetService<IErrorNotifications>().NotifyException("Exception in MediaImageFull control", ex, new ErrorInfo());
                    throw;
                }
            }

            // Nothing to display so hide
            mifModel.IsVisible = false;
        }
    }
}