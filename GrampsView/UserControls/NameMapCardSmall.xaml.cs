// <copyright file="NameMapCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    public partial class NameMapCardSmall : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameMapCardSmall"/> class.
        /// </summary>
        public NameMapCardSmall()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public NameMapModel ViewModel
        //{
        //    get
        //    {
        //        return (NameMapModel)DataContext;
        //    }
        //}
    }
}