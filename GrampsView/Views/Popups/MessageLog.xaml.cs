namespace GrampsView.Views
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    using Xamarin.Essentials;

    public sealed partial class MessageLog : MessageLogPopup
    {
        public MessageLog()
        {
            InitializeComponent();

            BindingContext = DataStore.Instance.CN.DataLog;

            Size = new Xamarin.Forms.Size(DeviceDisplay.MainDisplayInfo.Width, DeviceDisplay.MainDisplayInfo.Height);
        }
    }
}