// <copyright file="AttributeCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector"/>
    /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2"/>
    /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl"/>
    public partial class AttributeCardSmall : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeCardSmall"/> class.
        /// </summary>
        public AttributeCardSmall()
        {
            InitializeComponent();

            //DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets. </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public AttributeModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(AttributeModel)))
        //        {
        //            return (AttributeModel)DataContext;
        //        }
        //        else
        //        {
        //            return new AttributeModel();
        //        }
        //    }
        //}
    }
}