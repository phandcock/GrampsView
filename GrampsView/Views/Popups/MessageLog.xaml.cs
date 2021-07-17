namespace GrampsView.Views
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using Xamarin.Forms;

    public sealed partial class MessageLog : MessageLogPopup
    {
        public MessageLog()
        {
            InitializeComponent();

            BindingContext = DataStore.Instance.CN.DataLog;

            Size = new Size(DataStore.Instance.AD.ScreenSize.Width - 100, DataStore.Instance.AD.ScreenSize.Height - 100);
        }
    }
}