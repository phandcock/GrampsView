using GrampsView.ViewModels.Person;

namespace GrampsView.Views
{
    public partial class PersonDetailPage : ViewBasePage
    {
        public PersonDetailPage()
        {
            InitializeComponent();
            BindingContext = Ioc.Default.GetRequiredService<PersonDetailViewModel>();
        }
    }
}