namespace GrampsView.Views
{
    using Microsoft.Extensions.DependencyInjection;

    public partial class PersonDetailPage : ViewBase
    {
        private PersonDetailPage _viewModel { get; set; }

        public PersonDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = App.Current.Services.GetService<PersonDetailPage>();
        }
    }
}