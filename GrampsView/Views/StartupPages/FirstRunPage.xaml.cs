//-----------------------------------------------------------------------
//
// ViewModel for the About Flyout
//
// <copyright file="FirstRunView.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// </summary>
    public sealed partial class FirstRunPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstRunView" /> class.
        /// </summary>
        public FirstRunPage()
        {
            InitializeComponent();

            // setup for close var viewModel = this.DataContext as AboutFlyoutViewModel;
            // ViewModel.CloseFlyout = () => this.Hide();
        }
    }
}