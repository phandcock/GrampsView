// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels;

namespace GrampsView
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = Ioc.Default.GetRequiredService<AppShellViewModel>();
        }

        //protected override bool OnBackButtonPressed()
        //{
        //    IReadOnlyList<Page> t = Shell.Current.Navigation.NavigationStack;

        //    //    base.OnBackButtonPressed();

        //    Shell.Current.Navigation.RemovePage()
        //    return true;
        //}

        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{
        //    if (Shell.Current != null)
        //    {
        //        IReadOnlyList<Page> t = Shell.Current.Navigation.NavigationStack;
        //    }

        //    base.OnNavigating(args);
        //}
    }
}