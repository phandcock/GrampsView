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

        protected override bool OnBackButtonPressed()
        {
            var t = Shell.Current.Navigation.NavigationStack;

            return base.OnBackButtonPressed();
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);
        }
    }
}