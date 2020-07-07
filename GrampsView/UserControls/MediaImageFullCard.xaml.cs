// <copyright file="MediaCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using System;
    using System.Diagnostics.Contracts;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Full size Media Control Card code behind.
    /// </summary>
    public partial class MediaImageFullCard : Grid
    {
        /// <summary>
        /// Local copy of the HLInk media model
        /// </summary>
        private HLinkHomeImageModel hLMediaModel = new HLinkHomeImageModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaImageFullCard"/> class.
        /// </summary>
        public MediaImageFullCard()
        {
            InitializeComponent();
        }

        private void MediaImageFullCardRoot_BindingContextChanged(object sender, EventArgs e)
        {
            MediaImageFullCard mifModel = (sender as MediaImageFullCard);

            // Xamarin sets to null as the parent page is destroyed
            if (this.BindingContext is null)
            {
                return;
            }

            Contract.Assert(BindingContext is HLinkHomeImageModel);

            hLMediaModel = this.BindingContext as HLinkHomeImageModel;

            Contract.Assert(hLMediaModel != null);

            if (hLMediaModel.Valid)
            {
                mifModel.image.BindingContext = hLMediaModel;
            }

            // Check if anything to display
            if (mifModel.image.IsVisible)
            {
                mifModel.IsVisible = true;
            }
            else
            {
                mifModel.IsVisible = false;
            }
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            OpenFileRequest t = new OpenFileRequest(hLMediaModel.DeRef.GDescription, new ReadOnlyFile(hLMediaModel.DeRef.MediaStorageFilePath));

            Launcher.OpenAsync(t);
        }
    }
}