﻿// <copyright file="MediaImageSkia.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    using Xamarin.Forms;

    public partial class MediaImageSkia : Frame
    {
        //public static readonly BindableProperty UConHLinkHomeImageModelProperty
        //                        = BindableProperty.Create(
        //                                                    returnType: typeof(HLinkHomeImageModel),
        //                                                    declaringType: typeof(MediaImageSkia),
        //                                                    propertyName: nameof(UConHLinkHomeImageModel),
        //                                                    defaultValue: new HLinkHomeImageModel(),
        //                                                    propertyChanged: UConHLinkHomeImageModelPropertyChanged);

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

        //public HLinkHomeImageModel UConHLinkHomeImageModel
        //{
        //    get { return (HLinkHomeImageModel)GetValue(UConHLinkHomeImageModelProperty); }
        //    set { SetValue(UConHLinkHomeImageModelProperty, value); }
        //}

        private HLinkHomeImageModel WorkHLMediaModel { get; set; }

        public void ReloadImage()
        {
            this.daImage.ReloadImage();

            this.daImage.LoadingPlaceholder = null;
        }

        //private static void UConHideSymbolPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //}

        //private static void UConHLinkHomeImageModelPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    MediaImageSkia t = bindable as MediaImageSkia;

        // try { if (!(newValue is HLinkHomeImageModel newHLinkMedia)) {
        // //DataStore.CN.NotifyError("Bad HlinkMediaModel (is null) passed to MediaImage"); return; }

        // if (string.IsNullOrEmpty(newHLinkMedia.HLinkKey)) { }

        // t.ShowSomething(newHLinkMedia); } catch (Exception ex) {
        // DataStore.CN.NotifyException("MediaImageSkia", ex);

        //        throw ex;
        //    }
        //}

        private void DaImage_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
            DataStore.CN.NotifyError("Error in MediaImageSkia.  Error is " + e.Exception.Message);

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
                if (!(this.BindingContext is HLinkHomeImageModel newHLinkMedia))
                {
                    //DataStore.CN.NotifyError("Bad HlinkMediaModel (is null) passed to MediaImage");
                    return;
                }

                if (!(newHLinkMedia.Valid))
                {
                }

                this.ShowSomething(newHLinkMedia);
            }
            catch (Exception ex)
            {
                DataStore.CN.NotifyException("MediaImageSkia", ex);

                throw;
            }
        }

        private void ShowImage(IMediaModel argMediaModel)
        {
            if (string.IsNullOrEmpty(argMediaModel.MediaStorageFilePath))
            {
                DataStore.CN.NotifyError("The media file path is null for Id:" + argMediaModel.Id);
                return;
            }
            // Input valid so start work
            daSymbol.IsVisible = false;
            daImage.IsVisible = true;

            this.daImage.DownsampleToViewSize = true;

            this.daImage.Source = argMediaModel.MediaStorageFilePath;
        }

        private void ShowSomething(HLinkHomeImageModel argHHomeMedia)
        {
            try
            {
                if (!argHHomeMedia.Valid)
                {
                    //DataStore.CN.NotifyError("Invalid HlinkMediaModel (" + HLinkMedia.HLinkKey + ") passed to MediaImage");
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
                DataStore.CN.NotifyException("MediaImageSkia", ex);

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
                DataStore.CN.NotifyError("MediaImageSkia (" + argHLMediaModel.HLinkKey + ") Null Glyph");
            }

            if (tt.Color == null)
            {
                DataStore.CN.NotifyError("MediaImageSkia (" + argHLMediaModel.HLinkKey + ") Null Colour");
            }

            this.daSymbol.Source = tt;
        }
    }
}