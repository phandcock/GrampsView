namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PersonBirthdayPage : ViewBase
    {
        private PersonBirthdayViewModel _viewModel { get; set; }

        public PersonBirthdayPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<PersonBirthdayViewModel>();
        }
    }
}