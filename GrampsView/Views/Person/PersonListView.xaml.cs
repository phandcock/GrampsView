namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public sealed partial class PersonListPage : ViewBasePage
    {
        private PersonListViewModel _viewModel { get; set; }

        public PersonListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetRequiredService<PersonListViewModel>();
        }
    }
}