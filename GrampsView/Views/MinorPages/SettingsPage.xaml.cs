//-----------------------------------------------------------------------
//
// ViewModel for the About View
//
// <copyright file="AboutView.xaml.cs" company="MeMyselfAndI">
// Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Views
{
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// About Fly-out constructor.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public sealed partial class SettingsPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}