namespace GrampsView.UserControls
{
    using System.Windows.Input;

    using Xamarin.Forms;

    public class UserControlBase : Grid
    {
        public UserControlBase()
        {
       
        }

        public ICommand UCNavigateCommand { get; private set; }

        private async void UCNavigate()
        {
            await Shell.Current.GoToAsync("EventDetailPage");
        }
    }
}