// <copyright file="NoteCardSmall.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// The Code-Behind for the Note Card.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl"/>
    /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector"/>
    /// /// /// ///
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2"/>
    public partial class NoteCardSmall : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCardSmall"/> class.
        /// </summary>
        public NoteCardSmall()
        {
            InitializeComponent();

            // DataContextChanged += (s, e) => Bindings.Update();
        }

        ///// <summary>
        ///// Gets the Note ViewModel.
        ///// </summary>
        ///// <value>
        ///// The ViewModel.
        ///// </value>
        //public NoteModel ViewModel
        //{
        //    get
        //    {
        //        if ((DataContext != null) && (DataContext.GetType() == typeof(NoteModel)))
        //        {
        //            return (NoteModel)DataContext;
        //        }
        //        else
        //        {
        //            return new NoteModel();
        //        }
        //    }
        //}
    }
}