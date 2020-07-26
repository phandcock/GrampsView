// <copyright file="InstructionCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

/// <summary>
/// Code behind for the InstructionCardLarge UserControl.
/// </summary>
namespace GrampsView.UserControls
{
    using Xamarin.Forms;

    /// <summary>
    /// Code behind for the InstructionCardLarge UserControl.
    /// </summary>
    public partial class InstructionCardLarge : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionCardLarge"/> class.
        /// </summary>
        public InstructionCardLarge()
        {
            InitializeComponent();
        }

        private void InstructionCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            InstructionCardLarge thisObject = sender as InstructionCardLarge;

            if (thisObject != null)
            {
                if (this.BindingContext is string)
                {
                    thisObject.Instructions.Text = thisObject.BindingContext as string;
                }

                if (this.BindingContext is InstructionCardLarge)
                {
                    thisObject.Instructions.Text = (thisObject.BindingContext as InstructionCardLarge).BindingContext as string;
                }
            }
        }
    }
}