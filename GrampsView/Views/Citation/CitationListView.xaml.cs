//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="CitationListPage.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for page.
    /// </summary>
    public partial class CitationListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CitationListPage" /> class.
        /// </summary>
        public CitationListPage()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public CitationListPageModel ViewModel
        //{
        //    get
        //    {
        //        return (CitationListPageModel)DataContext;
        //    }
        //}
    }
}