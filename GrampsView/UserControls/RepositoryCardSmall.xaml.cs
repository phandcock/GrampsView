// <copyright file="RepositoryCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class RepositoryCardSmall : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryCardSmall"/> class.
        /// </summary>
        public RepositoryCardSmall()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public RepositoryModel ViewModel
        //{
        //    get
        //    {
        //        return (RepositoryModel)DataContext;
        //    }
        //}
    }
}