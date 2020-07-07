//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="TestCacheView.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232
namespace GrampsView.Views
{
    using System;
    using System.Collections.Generic;
    using Xamarin.Forms;

    public partial class CropTransformationPage : ContentPage
    {
        public CropTransformationPage()
        {
            InitializeComponent();
        }

        private void image_Error(object sender, FFImageLoading.Forms.CachedImageEvents.ErrorEventArgs e)
        {
        }

        private void image_Success(object sender, FFImageLoading.Forms.CachedImageEvents.SuccessEventArgs e)
        {
        }
    }
}