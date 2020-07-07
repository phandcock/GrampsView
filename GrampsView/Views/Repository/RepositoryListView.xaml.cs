//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="RepositoryListPage.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for page.
    /// </summary>
    public sealed partial class RepositoryListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryListPage" /> class.
        /// </summary>
        public RepositoryListPage()
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
        //public RepositoryListPageModel ViewModel
        //{
        //    get
        //    {
        //        return (RepositoryListPageModel)DataContext;
        //    }
        //}
    }
}