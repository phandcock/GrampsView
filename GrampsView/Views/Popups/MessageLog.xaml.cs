namespace GrampsView.Views
{
    using Xamarin.CommunityToolkit.UI.Views;

    public sealed partial class MessageLog : Popup
    {
        public MessageLog()
        {
            InitializeComponent();

            //BindingContext = DataStore.Instance.CN.DataLog;

            //Size = Common.CardSizes.Current.ScreenSize;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            //Dismiss(null);
        }
    }
}