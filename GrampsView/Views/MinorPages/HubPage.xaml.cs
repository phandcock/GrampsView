//-----------------------------------------------------------------------
//
//
//
// <copyright file="HubView.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=321224
namespace GrampsView.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class HubPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HubView" /> class.
        /// </summary>
        public HubPage()
        {
            InitializeComponent();
        }
    }
}