//-----------------------------------------------------------------------
// <summary>
// Various data modesl to small to be worth putting in their own file
// is first launched.
// </summary>
//
// <copyright file="TagListPage.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for page.
    /// </summary>
    public sealed partial class TagListPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagListPage" /> class.
        /// </summary>
        public TagListPage()
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
        //public TagListPageModel ViewModel
        //{
        //    get
        //    {
        //        return (TagListPageModel)DataContext;
        //    }
        //}
    }
}