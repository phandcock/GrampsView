//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="RepositoryDetailPage.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// The Item Detail Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234232
namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// A page that displays details for a single item within a group while allowing gestures to flip
    /// through other items belonging to the same group.
    /// </summary>
    public partial class RepositoryDetailPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDetailPage" /> class.
        /// </summary>
        public RepositoryDetailPage()
        {
            InitializeComponent();
        }
    }
}