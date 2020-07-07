//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="PlaceDetailPage.xaml.cs" company="MeMyselfAndI">
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
    public partial class PlaceDetailPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceDetailPage" /> class.
        /// </summary>
        public PlaceDetailPage()
        {
            InitializeComponent();
        }

        ///// <summary>
        ///// Gets.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public PlaceDetailPageModel ViewModel
        //{
        //    get
        //    {
        //        return (PlaceDetailPageModel)DataContext;
        //    }
        //}
    }
}