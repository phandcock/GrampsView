namespace GrampsView.UserControls
{
    using GrampsView.Data.Repository;

    using Xamarin.Forms;

    public partial class BottomMenu : Frame
    {
        public BottomMenu()
        {
            InitializeComponent();

            // BaseEventAggregator.GetEvent<DataLoadCompleteEvent>().Subscribe(CheckHeroImageLoad, ThreadOption.BackgroundThread);
        }

        public string StatusText
        {
            get
            {
                // TODO fix this so it changes as the status message changes
                return DataStore.CN.MajorStatusMessage;
            }
        }
    }
}