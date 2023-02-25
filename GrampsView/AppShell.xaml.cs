// Copyright (c) phandcock.  All rights reserved.

using GrampsView.ViewModels;

namespace GrampsView
{
    public partial class AppShell : Shell
    {
        private ShellNavigationState temp;

        public AppShell()
        {
            InitializeComponent();

            BindingContext = Ioc.Default.GetRequiredService<AppShellViewModel>();

            Uri = new Stack<ShellNavigationState>();
        }

        private Stack<ShellNavigationState> Uri { get; set; } // Navigation stack.

        // Prevents applications from adding redundant data to the stack when the back button is clicked.

        //protected override bool OnBackButtonPressed()
        //{
        //    if (Uri.Count > 0)
        //    {
        //        Shell.Current.GoToAsync(Uri.Pop());
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //protected override void OnNavigated(ShellNavigatedEventArgs args)
        ////{
        //    base.OnNavigated(args);
        //    if (Uri != null && args.Previous != null)
        //    {
        //        if (temp == null || temp != args.Previous)
        //        {
        //            Uri.Push(args.Previous);
        //            temp = args.Current;
        //        }
        //    }
        //}

        //protected override bool OnBackButtonPressed()
        //{
        //    IReadOnlyList<Page> t = Shell.Current.Navigation.NavigationStack;


        //    base.OnBackButtonPressed();

        //    //     Shell.Current.Navigation.RemovePage()
        //    return true;
        //}

        //protected override void OnNavigating(ShellNavigatingEventArgs args)
        //{

        //    //  string t = args.Current.Location.
        //    //if (Shell.Current != null)
        //    //{
        //    //    IReadOnlyList<Page> t = Shell.Current.Navigation.NavigationStack;
        //    //}

        //    base.OnNavigating(args);
        //}
    }
}