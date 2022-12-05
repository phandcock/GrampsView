namespace GrampsView.Views
{
    using GrampsView.ViewModels;

    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonNameDetailPage : ViewBasePage
    {
        private PersonNameDetailViewModel _viewModel { get; set; }

        public PersonNameDetailPage()
        {
            InitializeComponent(); BindingContext = _viewModel = Ioc.Default.GetRequiredService<PersonNameDetailViewModel>();
        }
    }
}