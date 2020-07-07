// <copyright file="SourceCardSmall .xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Model;

    using System.Diagnostics.Contracts;

    using Xamarin.Forms;

    /// <summary>
    /// Code behind for Source Card.
    /// </summary>
    public partial class SourceCardSmall : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SourceCardSmall"/> class.
        /// </summary>
        public SourceCardSmall()
        {
            InitializeComponent();
        }

        private void Grid_BindingContextChanged(object sender, System.EventArgs e)
        {
            SourceCardSmall card = (sender as SourceCardSmall);

            if (card is null)
            {
                this.IsVisible = false;
                return;
            }

            Contract.Requires(BindingContext is HLinkSourceModel);
        }
    }
}