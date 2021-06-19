namespace GrampsView.Views
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Repository;

    public sealed partial class MessageLog : MessageLogPopup
    {
        public MessageLog()
        {
            InitializeComponent();

            BindingContext = DataStore.Instance.CN.DataLog;

            Size = Common.CardSizes.Current.ScreenSize;
        }
    }
}