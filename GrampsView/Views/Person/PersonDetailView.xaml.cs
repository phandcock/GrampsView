using GrampsView.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace GrampsView.Views
{
    public partial class PersonDetailPage : ViewBasePage
    {
        private PersonDetailViewModel _viewModel { get; set; }

        public PersonDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = Ioc.Default.GetService<PersonDetailViewModel>();
        }


    }
}