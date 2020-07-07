// <copyright file="NoteCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using System.Diagnostics.Contracts;

    using Xam.Forms.Markdown;

    using Xamarin.Forms;

    /// <summary>
    /// Code behind for Note Card Large.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.UserControl"/>
    public partial class NoteCardFull : Frame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoteCardFull"/> class.
        /// </summary>
        public NoteCardFull()
        {
            InitializeComponent();
        }

        private void PersonCardSmallRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            NoteCardFull card = (sender as NoteCardFull);

            if (card is null)
            {
                this.IsVisible = false;
                return;
            }

            Contract.Requires(BindingContext is HLinkNoteModel);

            HLinkNoteModel t = this.BindingContext as HLinkNoteModel;

            if (t is null)
            {
                this.IsVisible = false;
                return;
            }

            if (t.Valid)
            {
                this.IsVisible = true;
            }
            else
            {
                this.IsVisible = false;
            }
        }
    }
}