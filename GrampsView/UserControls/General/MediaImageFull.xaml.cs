// <copyright file="MediaImageFull.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Forms;

    public partial class MediaImageFull : Frame
    {
        public static readonly BindableProperty UCHLinkMediaModelProperty = BindableProperty.Create(
                                                        propertyName: nameof(UCHLinkMediaModel),
                                                        returnType: typeof(HLinkHomeImageModel),
                                                        declaringType: typeof(MediaImageFull),
                                                        defaultValue: new HLinkHomeImageModel(),
                                                        defaultBindingMode: BindingMode.OneWay,
                                                        propertyChanged: HandleVMPropertyChanged
                                                        );

        private HLinkHomeImageModel HLinkMediaModel = new HLinkHomeImageModel();

        public MediaImageFull()
        {
            InitializeComponent();

            //this.daImage.CacheKeyFactory = new CustomCacheKeyFactory();
        }

        public HLinkHomeImageModel UCHLinkMediaModel
        {
            get
            {
                return GetValue(UCHLinkMediaModelProperty) as HLinkHomeImageModel;
            }

            set
            {
                if (UCHLinkMediaModel != value)
                {
                    SetValue(UCHLinkMediaModelProperty, value);
                }
            }
        }

        private static void HandleVMPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            MediaImageFull mifModel = (bindable as MediaImageFull);

            HLinkHomeImageModel argHLinkMediaModel = newValue as HLinkHomeImageModel;

            if (argHLinkMediaModel == mifModel.HLinkMediaModel)
            {
                return;
            }

            mifModel.HLinkMediaModel = argHLinkMediaModel;

            if (!(argHLinkMediaModel is null) && (argHLinkMediaModel.Valid))
            {
                IMediaModel t = argHLinkMediaModel.DeRef;

                if ((t.IsMediaStorageFileValid) && (t.IsMediaFile))
                {
                    try
                    {
                        mifModel.daImage.Source = t.MediaStorageFilePath;
                        var tt = mifModel.daImage.Source;

                        mifModel.IsVisible = true;

                        // TODO cleanup code so does nto use bindignContext if possible
                        mifModel.BindingContext = argHLinkMediaModel;

                        return;
                    }
                    catch (Exception ex)
                    {
                        DataStore.CN.NotifyException("Exception in MediaImageFull control", ex);
                        throw;
                    }
                }
            }

            // Nothing to display so hide
            mifModel.IsVisible = false;
        }

        private void daImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.CN.NotifyError("Error in MediaImageFull.  Error is " + e.Exception.Message);

            (sender as FFImageLoading.Forms.CachedImage).Cancel();
            (sender as FFImageLoading.Forms.CachedImage).Source = null;
        }
    }
}