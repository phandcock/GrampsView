
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GrampsView.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }

            InitializeComponent();
        }
    }
}