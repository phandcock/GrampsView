// <copyright file="PlaceListPage.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for page.
    /// </summary>
    public sealed partial class PlaceListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceListPage" /> class.
        /// </summary>
        public PlaceListPage()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the ViewModel.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public PlaceListPageModel ViewModel
        //{
        //    get
        //    {
        //        return (PlaceListPageModel)DataContext;
        //    }
        //}
    }
}