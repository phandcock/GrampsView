namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonNameDetailPage : ViewBase
    {
        private PersonNameDetailViewModel _viewModel { get; set; }

        public PersonNameDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = App.Current.Services.GetService<PersonNameDetailViewModel>();
        }
    }
}