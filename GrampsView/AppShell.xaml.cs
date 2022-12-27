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
            return base.OnBackButtonPressed();
        }
    }
}