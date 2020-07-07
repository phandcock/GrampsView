//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="SearchView.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// Search Results Page.
    /// </summary>
    public sealed partial class SearchPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchView" /> class.
        /// </summary>
        public SearchPage()
        {
            InitializeComponent();
        }

        private void ContentPage_Appearing(object sender, System.EventArgs e)
        {
            this.SearchBar.Focus();
        }
    }
}