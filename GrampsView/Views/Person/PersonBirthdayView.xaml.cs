namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PersonBirthdayPage : ViewBasePage
    {
        private PersonBirthdayViewModel _viewModel { get; set; }

        public PersonBirthdayPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<PersonBirthdayViewModel>();
        }
    }
}